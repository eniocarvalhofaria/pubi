using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessIntelligence.Util;
using BusinessIntelligence.Authentication;
using System.Net;
using System.Configuration;
using System.Xml;
namespace BusinessIntelligence.Data
{
    public class Connections
    {
        static public bool TestConnection(string connectionName)
        {
            System.Data.Common.DbConnection connection;
            bool ret = TestConnection(connectionName, out connection);
            if (connection != null)
            {
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            return ret;
        }
        static public bool TestVPNConnection()
        {
            var x = new System.Net.NetworkInformation.Ping();
            var reply = x.Send(IPAddress.Parse("172.26.1.143"));

            if (reply.Status == System.Net.NetworkInformation.IPStatus.Success)
            {

                return true;
            }
            else
            {
                return false;
            }

        }
        static public bool TestConnection(string connectionName, out System.Data.Common.DbConnection connection)
        {
            connection = GetNewConnection(connectionName);
            if (connection == null)
            {
                return false;
            }
            else { return true; }
        }
        static public bool TestConnection(System.Data.Common.DbConnection connection)
        {
            var q = new QueryExecutor(connection);
            try
            {
                q.Execute("select 1", false);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private static string[] _allConnectionsName = null;
        public static string[] GetAllConnectionsName()
        {
            if (_allConnectionsName == null)
            {
                var ret = new List<string>();
                var conns = BusinessIntelligence.Configurations.ApplicationConfigurationInfo.TryGetNodes(Parameter.GetMainFullFileName("connections.config"), "connectionStrings");

                if (conns != null && conns.Count > 0)
                {
                    foreach (XmlNode a in conns[0].ChildNodes)
                    {
                        var name = a.Attributes["name"].Value;
                        if (!name.Contains("Local"))
                        {
                            ret.Add(name);
                        }
                    }

                }
                _allConnectionsName = ret.ToArray();
            }
            return _allConnectionsName;
        }
        public static System.Data.Common.DbConnection GetNewConnection(string connectionName)
        {
        
            var all = BusinessIntelligence.Configurations.ApplicationConfigurationInfo.TryGetObjects< ConnectionSettings>(Parameter.GetMainFullFileName("connections.config"), "connectionStrings");
            
            ConnectionSettings cd;
          var co =  all.Where<ConnectionSettings>(c => c.Name == connectionName);
        

            if (co == null || co.Count() == 0)
            {
                return null;
            }
            cd = co.ToList<ConnectionSettings>()[0];
            cd.ConnectionString = Parameter.ReplaceParameters(cd.ConnectionString);
            var connectionStringParameters = new Dictionary<string, string>();
            string serverName = null;
            foreach (var item in cd.ConnectionString.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (item.Contains("="))
                {
                    connectionStringParameters.Add(item.Split('=')[0].ToLower().Trim(), item.Split('=')[1].Trim());
                }
            }
            if (connectionStringParameters.ContainsKey("server"))
            {
                serverName = connectionStringParameters["server"].Replace(",", ":");
            }
            System.Data.Common.DbConnection dbConn = null;
            switch (cd.ProviderName)
            {
                case "System.Data.Odbc.OdbcConnection":
                    {
                        dbConn = new System.Data.Odbc.OdbcConnection(cd.ConnectionString);
                        break;
                    }
                default:
                    {
                        dbConn = System.Data.Common.DbProviderFactories.GetFactory(cd.ProviderName).CreateConnection();
                        break;
                    }

            }
            return AuthenticateConnection(serverName, dbConn, cd.ConnectionString, true);
        }
        private static System.Data.Common.DbConnection AuthenticateConnection(string credentialName, System.Data.Common.DbConnection connection, string cs, bool trustedConnection)
        {
            try
            {
                connection.ConnectionString = cs;
                connection.Open();
                return connection;
            }
            catch (System.Data.Common.DbException dbex1)
            {

            }
            CredentialManagement.Credential credential = null;
            CredentialManagement.CredentialType type;
            if (trustedConnection)
            {
                type = CredentialManagement.CredentialType.DomainPassword;
                connection.ConnectionString = cs;
                try
                {
                    connection.Open();
                    return connection;
                }
                catch (System.Data.Common.DbException dbex1)
                {

                }
            }
            else
            {
                type = CredentialManagement.CredentialType.Generic;
                credential = EnvironmentCredentials.loadCredential(credentialName, type);
            }
            int i = 0;
            int errorLimit = 3;
            while (i < errorLimit)
            {
                i++;
                if (credential == null)
                {
                    credential = EnvironmentCredentials.GetNewCredential(credentialName, type);
                }
                try
                {
                    if (credential != null)
                    {
                        if (trustedConnection)
                        {
                            if (!credential.Username.ToUpper().Contains("PEIXEURBANO\\"))
                            {
                                credential.Username = "PEIXEURBANO\\" + credential.Username;
                            }
                            credential.Save();
                            connection.Open();
                            return connection;
                        }
                        else
                        {
                            connection.ConnectionString = cs.Replace("{0}", credential.Username).Replace("{1}", credential.Password);
                            connection.Open();
                            credential.Save();
                            return connection;
                        }
                    }
                }
                catch (System.Data.Common.DbException dbex)
                {
                    credential = null;
                    System.Windows.Forms.MessageBox.Show("As credenciais fornecidas não obtiveram sucesso na conexão.");
                }
            }
            return null;
        }

        /*
        static public System.Data.Common.DbConnection GetNewConnection(string connectionName, string UserId, string Password)
        {
            switch (database)
            {

                case Database.REDSHIFT:
                    return new System.Data.Odbc.OdbcConnection(@"Driver={PostgreSQL Unicode(x64)}; Server=pu-dw-1.cphgrk2t7oss.us-east-1.redshift.amazonaws.com; Database=dw; DS=salesforce; UID=" + UserId + "; PWD=" + Password + "; Port=5439");
                default:
                    return null;

            }
        }*/

    }
}
