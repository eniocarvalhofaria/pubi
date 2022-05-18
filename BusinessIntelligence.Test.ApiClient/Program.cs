using BusinessIntelligence.Members.Marketing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessIntelligence.Test.ApiClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var pe = new Persistence.ApiPersistenceEngine("http://localhost:63203");
            /*
            pe.GetObjects<CategoryType>();
            pe.GetObjects<CategoryGroup>();
            pe.GetObjects<Category>();
            pe.GetObjects<SubCategory>();
       */
      
            var o = pe.GetObjects<CategoryType>(Persistence.FilterExpressions.Equal("Id","1"));
          //  var o = pe.GetObjects<CategoryType>();
            foreach (var item in o)
            {
                Console.WriteLine(item.ObjectTypeName + " " + item.Id.ToString() + " " + item.Name );
                foreach (var item2 in item.Children)
                {
                    Console.WriteLine("     " + item2.ObjectTypeName + " " + item2.Id.ToString() + " " + item2.Name );

                }

            }
        }
    }
}
