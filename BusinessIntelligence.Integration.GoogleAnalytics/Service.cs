using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace BusinessIntelligence.Integration.GoogleAnalytics
{
    public class Service1
    {
        string ApiKey = "AIzaSyCmD1hwAVjM8SompMtRghjM2rE3OEyrU_g";

        string userID = "enio.faria@peixeurbano.com";

        string password = "Sissa198501";

        string url = "https://www.googleapis.com/analytics/v2.4/data?key=";
      

        public Service1()
        {/*
     //      var service = new Google.GData.Analytics.AnalyticsService("BusinessIntelligenceIntegration");
            service.setUserCredentials(userID, password);

            string feedURL = url + ApiKey;
    
     //       var query = new DataQuery(feedURL);

            query.Ids = "";
            query.Dimensions = "ga:date";
            query.Metrics = "ga:visits";

            query.GAStartDate = DateTime.Now.AddDays(-16).ToString("yyyy-MM-dd");
            query.GAEndDate = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");

            query.Sort = "ga:date";
            query.StartIndex = 1;

            var dataFeedVisitas = service.Query(query);*/
        }
        

    }
}
