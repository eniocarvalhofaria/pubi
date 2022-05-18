using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessIntelligence.Util;
namespace BusinessIntelligence.App.Security.CreateKey
{
    class Program
    {
        static void Main(string[] args)
        {
            shuffle(Environment.MachineName, Environment.UserDomainName, Environment.UserName);
            foreach (var item in Environment.GetEnvironmentVariables())
            {

            }

        }

        static string shuffle(string value1, string value2, string value3)
        {
            string v1 = "kfM4hcidLcoklkd49fjnt*#rf%5kD9erhjnbr48k6";
            string v2 = "F9jgl46@njkdeFbsSDpmckFrisnkg@kfdinFlSFRI";
            string v3 = "JGms89fjdn%pnf8325g4jDsoRmnV8$cjknmkp0Aqj";

            string all = value1 + value2 + value3;

            for (int i = 0; i < value1.Length; i++)
            {


            }

            value1 = (value1 + "kfM4hcidLcoklkd49fjnt*#rf%5kD9erhjnbr48k6").Substring(0, 32);
            value2 = (value2 + "F9jgl46@njkdeFbsSDpmckFrisnkg@kfdinFlSFRI").Substring(0, 32);
            value3 = (value2 + "JGms89fjdn%pnf8325g4jDsoRmnV8$cjknmkp0Aqj").Substring(0, 32);
            return null;

        }
    }
}