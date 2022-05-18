using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CredentialManagement;
using System.Threading;
using System.Threading.Tasks;
namespace BusinessIntelligence.Authentication
{
    public class EnvironmentCredentials
    {
        private static Credential _Redshift = null;

        public static Credential Redshift
        {
            get
            {
                if (_Redshift == null)
                {
                    _Redshift = loadCredential("redshift", CredentialType.Generic);
                }
                return _Redshift;
            }

        }
        private static Credential _AWS;
        public static Credential AWS
        {
            get
            {
                if (_AWS == null)
                {
                    _AWS = loadCredential("AWS", CredentialType.Generic);
                }
                return _AWS;
            }
        }

        private static Credential _Email = null;
        public static Credential Email
        {
            get
            {
                if (_Email == null)
                {
                    _Email = loadCredential("smtp.gmail.com", CredentialType.Generic);
                }
                return _Email;
            }
        }
        public static Credential GetNewCredential(string credentialName)
        {
            return GetNewCredential(credentialName, CredentialType.Generic);
        }


        static Task Delay(int milliseconds)
        {
            var tcs = new TaskCompletionSource<object>();
            new Timer(_ => tcs.SetResult(null)).Change(milliseconds, -1);
            return tcs.Task;
        }
        public static void ShowDialogTimeout(System.Windows.Forms.Form dlg, int timeout)
        {
            dlg.Shown += (sender, args) => Task.Factory.StartNew(
                () => Thread.Sleep(timeout)).ContinueWith(
                _ => dlg.Close(), TaskScheduler.FromCurrentSynchronizationContext());

            dlg.ShowDialog();
        }
        public static Credential GetNewCredential(string credentialName, CredentialType type)
        {
            var f = new FormAuthenticator();
            f.Text = "Preencha com suas credenciais " + credentialName;

            //   Timeout(f, 12000);
            //    f.ShowDialog();
            ShowDialogTimeout(f, 5000);
            int i = 0;
            /*
            while (f.Visible && i < 30)
            {
                i++;
                System.Threading.Thread.Sleep(1000);
            }
        */
            if (f.Cancelled || string.IsNullOrEmpty(f.UserId))
            {
                return null;
            }
            else
            {
                var cm = new Credential { Target = credentialName, Type = type };
                cm.Username = f.UserId;
                cm.Password = f.Pwd;
                cm.PersistanceType = PersistanceType.Enterprise;
                return cm;
            }
        }

        public static Credential loadCredential(string target, CredentialType type)
        {
            var cm = new Credential { Target = target, Type = type };

            if (!cm.Exists())
            {
                return null;
            }
            cm.Load();
            return cm;
        }
        public static bool createCredential(string name, string user, string pwd, CredentialType credentialType)
        {
            var cm = new Credential { Target = name, Type = credentialType };

            cm.Username = user;
            cm.Password = pwd;
            cm.PersistanceType = PersistanceType.Enterprise;
            cm.Save();
            return true;
        }
    }
}
