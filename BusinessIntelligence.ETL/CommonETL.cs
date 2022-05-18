using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommandLine;
using CommandLine.Parsing;
using CommandLine.Text;
namespace BusinessIntelligence.ETL
{

    public abstract class CommonETL
    {
        public class Options
        {

            [Option("filesdirectory", Required = true, HelpText = "Directory to load files")]
            public string FilesDirectory { get; set; }


            [HelpOption]
            public string GetUsage()
            {
                return HelpText.AutoBuild(this, (HelpText current) => HelpText.DefaultParsingErrorsHandler(this, current));
            }
        }
        public static Options options = new Options();
        public static void Initialize(string[] args)
        {
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
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Environment.Exit(-1);
            }
   //         System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        }
        public static string GetFullPath(string currentPath, string relativePathTarget)
        {
            string currentDirectory = Environment.CurrentDirectory;
            Environment.CurrentDirectory = currentPath;
            Environment.CurrentDirectory = relativePathTarget;
            string ret = Environment.CurrentDirectory;
            Environment.CurrentDirectory = currentDirectory;
            return ret;
        }
     
    }

}
