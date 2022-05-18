using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessIntelligence.Data;
using System.Data;
using System.Data.SqlClient;
namespace BusinessIntelligence.ETL.UserAccountsUnification
{
    class Program
    {
        static Dictionary<string, string> unification = new Dictionary<string, string>();
        static Dictionary<string, List<string>> keys = new Dictionary<string, List<string>>();
        static void Main(string[] args)
        {
           
            var cnRedshift = Connections.GetNewConnection(Database.REDSHIFT);
            var cn = Connections.GetNewConnection(Database.REPORTS);
            
             var queryExecutor = new QueryExecutor(cnRedshift);
            
            var loaderCPF = new BusinessIntelligence.Data.Redshift.RedshiftLoader(cnRedshift, "reports", "UserAccountCPF_stage");
            loaderCPF.ClearDestinationTable();
            var transfer = new DataTransfer(cn, ".GetUserAccountCPF.txt", loaderCPF);
            transfer.Execute();
       
           
            queryExecutor.Execute(".DeviceInfoSelection.txt");

                var extraInfo = queryExecutor.ReturnData("select  emails,EmailAddress from  reports.UserAccountActionLogEmails");
          
            foreach (DataRow row in extraInfo.Rows)
            {
                List<string> enderecos = new List<string>();
                if (row[0].ToString().StartsWith("{"))
                {
                    enderecos.AddRange( GetEmailsFromJson(row[0].ToString().ToLower()));
                }
                else
                {
                      enderecos.AddRange( GetEmailsFromSimpleText(row[0].ToString().ToLower()));
                }
                if (enderecos.Count > 0)
                {
                    var emailUserAccount = row[1].ToString().ToLower();
                   
                    if(emailUserAccount.IndexOf("@") > 1 && !enderecos.Contains(emailUserAccount))
                    {
                        enderecos.Add(emailUserAccount);
                    }
                    Unificate(enderecos.ToArray());
                }
            }
            DataTable dt = new DataTable();
            dt.Columns.Add("EmailAddress", typeof(string));
            dt.Columns.Add("UnifiedEmailAddress", typeof(string));

            foreach (var item in unification)
            {
                if (item.Key != item.Value)
                {
                    var row = dt.NewRow();
                    row[0] = item.Key;
                    row[1] = item.Value;
                    dt.Rows.Add(row);
                }
            }

       

          
            var loader = new BusinessIntelligence.Data.Redshift.RedshiftLoader(cnRedshift, "reports", "EmailUnification_stage");
            loader.ClearDestinationTable();
            if (!loader.Load(dt))
            {
                Environment.Exit(3);
            }
            
            queryExecutor.Execute(".RefreshUserAccountUnification.txt");
        }
        static string[] GetEmailsFromJson(string text)
        {
            int iEmail = text.IndexOf("emails");
            if (iEmail > -1)
            {
                int iOpenBracket = text.IndexOf("[", iEmail);
                int iCloseBracket = text.IndexOf("]", iEmail);
                if (iOpenBracket > -1 && iCloseBracket > -1)
                {
                    string enderecos = text.Substring(iOpenBracket + 1, iCloseBracket - iOpenBracket - 1).Replace("\"", "").Replace("\\", "");

                    return enderecos.Split(',');
                }

            }
            return null;
        }
        static string[] GetEmailsFromSimpleText(string text)
        {
         
            return text.Split(',');
        }
        static void Unificate(string[] emailAddress)
        {
            if (emailAddress == null)
            {
                return;
            }
            string unifiedAddress = null;
            string unifiedGetted;
            List<string> adicionar = new List<string>();
            foreach (string email in emailAddress)
            {
                if (unification.TryGetValue(email, out unifiedGetted))
                {
                    if (unifiedAddress == null)
                    {
                        unifiedAddress = unifiedGetted;
                    }
                    else if (unifiedAddress != unifiedGetted)
                    {
                        var k = keys[unifiedGetted];
                        foreach (string address in k)
                        {
                            unification[address] = unifiedAddress;
                        }
                        keys.Remove(unifiedGetted);
                        if (keys.ContainsKey(unifiedAddress))
                        {

                            keys[unifiedAddress].AddRange(k);
                        }
                        else
                        {
                            keys.Add(unifiedAddress, k);
                        }

                    }
                }
                else
                {
                    adicionar.Add(email);
                }
            }
            foreach (string email in adicionar)
            {
                if (unifiedAddress == null)
                {
                    unifiedAddress = email;
                }
                try
                {
                    unification.Add(email, unifiedAddress);
                    if (keys.ContainsKey(unifiedAddress))
                    {

                        keys[unifiedAddress].Add(email);
                    }
                    else
                    {
                        keys.Add(unifiedAddress, new List<string>(){email});
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(email);
                }
            }
        }
    }
}
