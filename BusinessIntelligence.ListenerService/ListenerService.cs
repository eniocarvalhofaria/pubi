using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using BusinessIntelligence.Util;


namespace BusinessIntelligence.Listening
{
    public partial class ListenerService : ServiceBase
    {
        public ListenerService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            StartService();
        }
        EmailListener emailListener = new EmailListener();
        System.Timers.Timer timer = null;
        ProcessExecutionListener processExecutionListener = new ProcessExecutionListener();
        string MASTER_LOG_NAME = "D:\\logListenerService.txt";
        public void StartService()
        {
        
            try
            {
                Log.GetInstance().FileName = MASTER_LOG_NAME;
                Log.GetInstance().WriteLine("The current working directory is: " + Environment.CurrentDirectory);
                var exDir = EnvironmentParameters.ExecutableDirectory;
                Log.GetInstance().WriteLine("Setting current directory to: " + exDir);
                Environment.CurrentDirectory = exDir;

                emailListener.EmailAddress = EnvironmentParameters.EmailAddress;
                emailListener.EmailPWD = EnvironmentParameters.EmailPwd;
                emailListener.ApplicationsDirectoriesFileName = "ApplicationsDirectories-EmailListener.txt";
                processExecutionListener.EmailAddress = EnvironmentParameters.EmailAddress;
                processExecutionListener.EmailPWD = EnvironmentParameters.EmailPwd;
                processExecutionListener.ApplicationsDirectoriesFileName = "ApplicationsDirectories-ProcessListener.txt";
                int secondsToWait = 60;
                timer = new System.Timers.Timer();
                timer.Interval = secondsToWait * 1000;
                timer.Elapsed += new System.Timers.ElapsedEventHandler(this.OnTimer);
                Log.GetInstance().WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " " + " Starting timer...");
                timer.Start();
            }
            catch (Exception ex) {

                Log.GetInstance().WriteLine(ex.Message);
                Log.GetInstance().WriteLine(ex.StackTrace);
                this.Stop();
            }
        }
        public void OnTimer(object sender, System.Timers.ElapsedEventArgs args)
        {
            Log.GetInstance().FileName = MASTER_LOG_NAME;
            try
            {
                timer.Stop();
                Log.GetInstance().WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " " + "Checking Process Execution Listener");
                processExecutionListener.Check();
                Log.GetInstance().WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " " + "Checking Email Listener");
                emailListener.Check();
                Log.GetInstance().WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " " + " Waiting 60 seconds...");
                timer.Start();
            }
            catch (Exception ex)
            { }
        }
        private bool stopping = false;
        private bool stopped = false;
        protected override void OnStop()
        {
            timer.Stop();
        }
        protected override void OnPause()
        {
            timer.Stop();
        }
        protected override void OnContinue()
        {
            timer.Start();
        }
    }
}
