using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Google.Apis.Analytics.v3;
using Google.Apis.Analytics.v3.Data;
using Google.Apis.Auth.OAuth2;
using System.Threading;
using Google.Apis.Util.Store;
using Google.Apis.Services;
using System.Security.Cryptography.X509Certificates;
using System.IO;
using System.Data.SqlClient;
using BusinessIntelligence.Data;
using CommandLine;
using CommandLine.Parsing;
using CommandLine.Text;
using BusinessIntelligence.Util;
namespace BusinessIntelligence.Integration.GoogleAnalytics
{
    class Program
    {
        private static ServiceAccountCredential Authenticate()
        {
            string[] scopes =
              new string[] {
             AnalyticsService.Scope.Analytics,                 // view and manage your Google Analytics data
             AnalyticsService.Scope.AnalyticsManageUsers};     // View Google Analytics data      
            //   https://console.developers.google.com/project/374517093301/apiui/credential
            string keyFilePath = @"BusinessInteligenceIntegration-86cff78394d1.p12";    // found in developer console
            string serviceAccountEmail = "374517093301@developer.gserviceaccount.com";  // found in developer console
            //loading the Key file
            var certificate = new X509Certificate2(keyFilePath, "notasecret", X509KeyStorageFlags.Exportable);
            ServiceAccountCredential credential = new ServiceAccountCredential(new ServiceAccountCredential.Initializer(serviceAccountEmail)
            {
                Scopes = scopes
            }.FromCertificate(certificate));
            return credential;

        }
        static void Main(string[] args)
        {
            try
            {

                AnalyticsService service = new AnalyticsService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = Authenticate(),
                    ApplicationName = "BusinessIntelligenceIntegration",
                });


                var dt = new System.Data.DataTable();
                dt.Columns.Add("EventDate", typeof(string));

                dt.Columns.Add("Source", typeof(string));
                dt.Columns.Add("Medium", typeof(string));
                dt.Columns.Add("DeviceCategory", typeof(string));
                dt.Columns.Add("Visits", typeof(int));
                dt.Columns.Add("Account", typeof(string));
                dt.Columns.Add("Users", typeof(int));


                Dictionary<string, string> accounts = new Dictionary<string, string>();

                List<string> removable = new List<string>();

                //      accounts.Add("universal", "ga:78808024");
                accounts.Add("marketing", "ga:94512091");
                accounts.Add("ios app", "ga:100948726");
                accounts.Add("android app", "ga:96147214");

                for (int i = -1; i >= -10; i--)
                //       for (int i = -820; i > -1185; i--)
                {
                    string queryDate = DateTime.Now.AddDays(i).ToString("yyyy-MM-dd");
                    if (removable.Count > 0)
                    {
                        accounts.Remove(removable[0]);
                        removable.Remove(removable[0]);
                    }
                    foreach (var item in accounts)
                    {
                        //DataResource.GaResource.GetRequest request = service.Data.Ga.Get(item.Value, queryDate, queryDate, "ga:visits,ga:users");
                        DataResource.GaResource.GetRequest request = service.Data.Ga.Get(item.Value, queryDate, queryDate, "ga:sessions,ga:users");
                        //     request.Dimensions = "ga:date";
                        request.Dimensions = "ga:date,ga:source,ga:medium,ga:deviceCategory";
                        request.MaxResults = 10000;
                        request.Sort = "ga:date";
                        request.StartIndex = 1;
                        Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + string.Format(" Extracting from GA ({0}). Date: {1}.", item.Key, DateTime.Now.AddDays(i).ToString("yyyy-MM-dd")));
                        GaData result = request.Execute();

                        int l = 0;
                        if (result.Rows == null)
                        {
                            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + string.Format(" Fim dos dados ({0}).", item.Key));

                            removable.Add(item.Key);
                        }
                        else
                        {
                            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " Data extracted.");
                            foreach (var dado in result.Rows)
                            {

                                l++;
                                if (l == 9999)
                                {
                                    Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " The data has exceeded the limit.");
                                }
                                var r = dt.NewRow();

                                var eventDate = dado[0];
                                var source = dado[1];
                                if (source.Length > 15)
                                {
                                    source = source.Substring(0, 15);
                                }
                                var medium = dado[2];
                                if (medium.Length > 15)
                                {
                                    medium = medium.Substring(0, 15);
                                }
                                var deviceCategory = dado[3];
                                if (deviceCategory.Length > 15)
                                {
                                    deviceCategory = deviceCategory.Substring(0, 15);
                                }
                                var visits = Convert.ToInt32(dado[4]);
                                var users = Convert.ToInt32(dado[5]);
                                r["EventDate"] = eventDate.Substring(0, 4) + "-" + eventDate.Substring(4, 2) + "-" + eventDate.Substring(6, 2);
                                if (source.Length > 100)
                                {
                                    r["Source"] = source.Substring(0, 100);
                                }
                                else
                                {
                                    r["Source"] = source;
                                }
                                if (medium.Length > 100)
                                {
                                    r["Medium"] = medium.Substring(0, 100);
                                }
                                else
                                {
                                    r["DeviceCategory"] = deviceCategory;
                                }
                                r["Medium"] = medium;
                                r["Visits"] = visits;
                                r["Users"] = users;
                                r["Account"] = item.Key;
                                dt.Rows.Add(r);

                            }
                        }
                    }
                }
                var loader = new Data.Redshift.RedshiftLoader(Connections.GetNewConnection("REDSHIFT"), "reports", "visitsanalytics_stage");
                loader.ClearDestinationTable();
                var qex = new QueryExecutor(loader.Connection);
                loader.Load(dt);
                Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + string.Format(" Loading {0} rows in {1}.", dt.Rows.Count.ToString(), loader.TableName));
                qex.Execute(".RefreshVisits.txt");
            }
            catch (Exception ex)
            {
                Log.GetInstance().WriteLine(ex.Message);
                Log.GetInstance().WriteLine(ex.StackTrace);
                Environment.Exit(5);
            }
        }

    }
}

