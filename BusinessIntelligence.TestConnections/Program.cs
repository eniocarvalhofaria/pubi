using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessIntelligence.Data;
using BusinessIntelligence.Util;
namespace BusinessIntelligence.TestConnections
{
    class Program
    {

        static void callProgram(string executableDirectory, string executableFile, string arguments)
        {
            Log.GetInstance().WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " Calling " + executableFile);
            string appDir = Environment.CurrentDirectory;
            if (!string.IsNullOrEmpty(executableDirectory))
            {
                Environment.CurrentDirectory = executableDirectory;
            }
            var a = new ProcessExecutor(executableFile, arguments);

            try
            {
                a.Process.StartInfo.EnvironmentVariables.Add("KEY", EnvironmentParameters.Key);
            }
            catch (Exception ex)
            { 
            
            }
            a.Execute();
            Environment.CurrentDirectory = appDir;
           
           
            if (a.ReturnCode !=0)
            {
                 throw new Exception(a.GetOutputFromApplication());
            }
        }
        static void Main(string[] args)
        {
            /*
            callProgram(@"G:\Projects\Repositorio\BusinessIntelligence\BusinessIntelligence.Test\bin\Debug",
                        "BusinessIntelligence.Test.exe", null);

            Console.WriteLine("Continuei!");
            Console.ReadKey();

            */
            /*
            while (true)
            {
                Console.WriteLine("Digite a string de conexão");
                string strConexao = Console.ReadLine();
                var conn = new System.Data.SqlClient.SqlConnection(strConexao);
                try
                {
                    conn.Open();
                    Console.WriteLine("Funcionando!");
                    conn.Close();
                    conn.Dispose();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro!");
                    Console.WriteLine(ex.Message);
                }
            }
             */

            foreach (var item in Connections.GetAllConnectionsName())
            {
                Test(item);
            }
            Console.WriteLine("Aperte Enter para sair");
            Console.ReadLine();

        }
        private static void Test(string connectionName)
        {
            Console.WriteLine("Testando " + connectionName + "...");
            if (Connections.TestConnection(connectionName))
            {
                Console.WriteLine("Conexao com " + connectionName + " funcionando.");
            }

        }

    }
}
