using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using BusinessIntelligence.Data;
using BusinessIntelligence.Util;
using System.Threading;
using System.Threading.Tasks;
namespace BusinessIntelligence.Authentication
{
    public class RunAs
    {

        public static void ShowDialogTimeout(System.Windows.Forms.Form dlg, int timeout)
        {
            dlg.Shown += (sender, args) => Task.Factory.StartNew(
                () => Thread.Sleep(timeout)).ContinueWith(
                _ => dlg.Close(), TaskScheduler.FromCurrentSynchronizationContext());

            dlg.ShowDialog();
        }
        static public void AuthenticateAndRecall(ApplicationInterfaceType type, string executablePath)
        {
            string userid = null;
            string pwd = null;
            Log.GetInstance().WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " Checking connectivity...");
            if (!Connections.TestVPNConnection())
            {
                System.Windows.Forms.MessageBox.Show("Você não está conectado na VPN.\r\n1. Aperte as teclas Ctrl + Shift + Esc.\r\n2. Ache os 2 processos do OpenVPN.\r\n3. Finalize os processos.\r\n4. Inicie OpenVPN como Administrador.\r\n5. Conecte o OpenVPN.\r\n6. Abra este programa novamente.");
                Environment.Exit(0);
            }

            if (!Connections.TestConnection("APPPROD"))
            {
                Log.GetInstance().WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " Connectivity not active.");
                if (type == ApplicationInterfaceType.WindowsForm)
                {
                    var a = new FormAuthenticator();
                    ShowDialogTimeout(a, 12000);

                    if (a.Cancelled)
                    {
                        Environment.Exit(0);
                    }
                    else
                    {
                        userid = a.UserId;
                        pwd = a.Pwd;
                    }
                }
                else
                {
                    userid = EnvironmentParameters.DomainUserId;
                    pwd = EnvironmentParameters.DomainPwd;
                }


                TryCallProgram(userid, pwd, executablePath);
                int i = 0;
                while ((CountProcess(Process.GetCurrentProcess().ProcessName) == 1 && i < 10))
                {
                    i++;
                    System.Threading.Thread.Sleep(1000);
                }
                Environment.Exit(0);

            }
            else
            {
                Log.GetInstance().WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " Connectivity active.");
            }
        }

        static private void TryCallProgram(string userid, string pwd, string path)
        {
            //       string path = @"G:\Projects\VisualStudio\BusinessIntelligence\BusinessIntelligence.App.Marketing.CampaignReturn\bin\Debug\BusinessIntelligence.App.Marketing.CampaignReturn.exe";
            string domain = "PEIXEURBANO";
            ProcessStartInfo info = new ProcessStartInfo();
            info.UseShellExecute = false;
            info.FileName = "runas";
            //      info.FileName = "notepad";
            info.Arguments = @"/user:" + domain + "\\" + userid + @" /netonly " + path;
            info.CreateNoWindow = false;



            Process p = Process.Start(info);
            System.Threading.Thread.Sleep(100);
            foreach (char c in pwd)
            {
                switch ((int)c)
                {
                    case 96:
                    case 180:
                    case 168:
                        {
                            System.Windows.Forms.SendKeys.SendWait(c.ToString());
                            System.Windows.Forms.SendKeys.SendWait(" ");
                            break;
                        }
                    case 37:
                    case 43:
                        {
                            System.Windows.Forms.SendKeys.SendWait("{" + c.ToString() + "}");
                            break;
                        }
                    case 126:
                    case 94:
                        {
                            System.Windows.Forms.SendKeys.SendWait("{" + c.ToString() + "}");
                            System.Windows.Forms.SendKeys.SendWait(" ");
                            break;
                        }
                    default:
                        {
                            System.Windows.Forms.SendKeys.SendWait(c.ToString());
                            break;
                        }
                }

            }
            System.Windows.Forms.SendKeys.SendWait("\r\n");
            System.Threading.Thread.Sleep(1000);
        }


        static private int CountProcess(string processName)
        {
            int count = 0;
            foreach (var p in Process.GetProcesses())
            {
                if (p.ProcessName.Replace(".vshost", "").Equals(processName.Replace(".vshost", "")))
                {
                    count++;
                }
            }
            return count;
        }
        static void callProgram(string executablePath, string arguments)
        {
            Log.GetInstance().WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " Calling " + executablePath);
            var p = new System.Diagnostics.Process();
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.EnvironmentVariables.Add("KEY", EnvironmentParameters.Key);
            if (executablePath.EndsWith(".bat"))
            {
                p.StartInfo.FileName = "cmd";

                p.Start();
                p.StandardInput.WriteLine(executablePath + " " + arguments);
                p.StandardInput.WriteLine("exit %errorlevel%");
            }
            else
            {
                p.StartInfo.FileName = executablePath;
                p.StartInfo.Arguments = arguments;

                p.Start();
            }
            var sb = new StringBuilder();
            sb.Append(p.StandardOutput.ReadToEnd());
            while (!p.HasExited)
            {
                sb.Append(p.StandardOutput.ReadToEnd());
            }
            Log.GetInstance().WriteLine(sb.ToString());
            p.WaitForExit();
            if (p.ExitCode != 0)
            {
                throw new Exception(sb.ToString());
            }
        }

    }
}
