using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;
using CommandLine.Text;
using BusinessIntelligence.Authentication;
using CredentialManagement;
namespace BusinessIntelligence.App.StoreCredentials
{
    class Program
    {
        private class Options
        {
            [Option("redshiftuserid", Required = false, HelpText = "Redshift User Id")]
            public string RedshiftUserId { get; set; }
            [Option("redshiftpwd", Required = false, HelpText = "RedshiftUser Pwd")]
            public string RedshiftPassword { get; set; }
            [Option("s3accesskey", Required = false, HelpText = "s3accesskey")]
            public string s3accesskey { get; set; }
            [Option("s3secretkey", Required = false, HelpText = "s3secretkey")]
            public string s3secretkey { get; set; }
            [HelpOption]
            public string GetUsage()
            {
                return HelpText.AutoBuild(this, (HelpText current) => HelpText.DefaultParsingErrorsHandler(this, current));
            }
        }
        private static Options options = new Options();
        static void Main(string[] args)
        {
            if (!string.IsNullOrEmpty(options.s3accesskey) || !string.IsNullOrEmpty(options.s3secretkey))
            {
                EnvironmentCredentials.createCredential("AWS", options.s3accesskey, options.s3secretkey, CredentialType.Generic);
            }
            if (!string.IsNullOrEmpty(options.RedshiftUserId) || !string.IsNullOrEmpty(options.RedshiftPassword))
            {
                EnvironmentCredentials.createCredential("redshift", options.RedshiftUserId, options.RedshiftPassword, CredentialType.Generic);
            }

            
        }
    }
}
