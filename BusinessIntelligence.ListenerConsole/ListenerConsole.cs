using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommandLine;
using CommandLine.Parsing;
using CommandLine.Text;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Xml;
using BusinessIntelligence.Util;
using BusinessIntelligence.Data;
namespace BusinessIntelligence.Listening
{
    class ListenerConsole
    {
        private sealed class Options
        {

            [Option("emailaddress", Required = false, HelpText = "Email address to read")]
            public string EmailAddress { get; set; }

            [Option("emailpwd", Required = false, HelpText = "Password email")]
            public string EmailPWD { get; set; }

            [HelpOption]
            public string GetUsage()
            {
                return HelpText.AutoBuild(this, (HelpText current) => HelpText.DefaultParsingErrorsHandler(this, current));
            }
        }

        static Options options = new Options();


        static void Main(string[] args)
        {
            string dir = Environment.CurrentDirectory;
            try
            {
                Parser parser = new Parser
                (
                    delegate(ParserSettings settings)
                    {
                        settings.CaseSensitive = true;
                        settings.HelpWriter = Console.Out;
                    }
                );

                if (!parser.ParseArguments(args, options))
                {
                    throw new ParserException();
                }
        //        Authentication.RunAs.AuthenticateAndRecall(ApplicationInterfaceType.Console, Environment.CommandLine);
                if (!string.IsNullOrEmpty(options.EmailAddress))
                {
                    EnvironmentParameters.EmailAddress = options.EmailAddress;
                    EnvironmentParameters.EmailPwd = options.EmailPWD;
                }
                var emailListener = new EmailListener();
                emailListener.EmailAddress = EnvironmentParameters.EmailAddress;
                emailListener.EmailPWD = EnvironmentParameters.EmailPwd;
                emailListener.ApplicationsDirectoriesFileName = "ApplicationsDirectories-EmailListener.txt";

                var processExecutionListener = new ProcessExecutionListener();
                processExecutionListener.EmailAddress = EnvironmentParameters.EmailAddress;
                processExecutionListener.EmailPWD = EnvironmentParameters.EmailPwd;
                processExecutionListener.ApplicationsDirectoriesFileName = "ApplicationsDirectories-ProcessListener.txt";


                //Uma execucao somente
                processExecutionListener.Check();
                emailListener.Check();

                /*
                while (true)
                {
                    processExecutionListener.Check();
                    emailListener.Check();
                    int secondsToWait = 60;
                    Log.GetInstance().WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " Waiting " + secondsToWait.ToString() + " seconds...");
                    System.Threading.Thread.Sleep(secondsToWait * 1000);
                }
                */
            }
            catch (Exception e)
            {
                Log.GetInstance().WriteLine(e.Message + "\r\nRastreamento:\r\n" + e.StackTrace);
                Environment.Exit(-1);
            }
          

        }
    }
}