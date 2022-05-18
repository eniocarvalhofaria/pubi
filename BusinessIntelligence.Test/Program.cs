using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using  BusinessIntelligence.Util;
namespace BusinessIntelligence.Test
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Teste");
            Console.WriteLine("Aguardando 20 segundos");
            System.Threading.Thread.Sleep(10000);
            Console.WriteLine("20 segundos passados");
            int i = 0;
            Console.WriteLine((25 / i).ToString());
            try
            {
       //         Console.WriteLine(EnvironmentCredentials.Email.Password);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }
    }
}
