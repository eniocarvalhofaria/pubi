using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessIntelligence.Authentication;
namespace BusinessIntelligence.Util
{
    public class EnvironmentParameters
    {
        private static string _DomainUserId;

        public static string DomainUserId
        {
            get
            {
                if (string.IsNullOrEmpty(_DomainUserId))
                {
                    _DomainUserId = Util.Parameter.GetParameter("domainuserid").Value;
                }
                return EnvironmentParameters._DomainUserId;
            }
            set { EnvironmentParameters._DomainUserId = value; }
        }

        private static string _DomainPwd;

        public static string DomainPwd
        {
            get
            {
                if (string.IsNullOrEmpty(_DomainPwd))
                {
                    _DomainPwd = Util.Parameter.GetParameter("domainpwd").Value;
                }
                return EnvironmentParameters._DomainPwd;
            }
            set { EnvironmentParameters._DomainPwd = value; }
        }
        private static string _ProcessControlUserId;
        public static string ProcessControlUserId
        {
            get
            {
                if (string.IsNullOrEmpty(_ProcessControlUserId))
                {
                    _ProcessControlUserId = Util.Parameter.GetParameter("ProcessControluserid").Value;
                }
                return EnvironmentParameters._ProcessControlUserId;
            }
            set { EnvironmentParameters._ProcessControlUserId = value; }
        }

        private static string _ProcessControlPwd;

        public static string ProcessControlPwd
        {
            get
            {
                if (string.IsNullOrEmpty(_ProcessControlPwd))
                {
                    _ProcessControlPwd = Util.Parameter.GetParameter("ProcessControlpwd").Value;
                }
                return EnvironmentParameters._ProcessControlPwd;
            }
            set { EnvironmentParameters._ProcessControlPwd = value; }
        }
        private static string _RedshiftUserId;
        public static string RedshiftUserId
        {
            get
            {
                if (string.IsNullOrEmpty(_RedshiftUserId))
                {
                    if (EnvironmentCredentials.Redshift != null)
                    {
                        _RedshiftUserId = EnvironmentCredentials.Redshift.Username;
                    }
                    else
                    {
                        _RedshiftUserId = Util.Parameter.GetParameter("redshiftuserid").Value;
                    }

                }

                return _RedshiftUserId;
            }
            set { _RedshiftUserId = value; }
        }
        private static string _RedshiftPwd;
        public static string RedshiftPwd
        {
            get
            {
                if (string.IsNullOrEmpty(_RedshiftPwd))
                {
                    if (EnvironmentCredentials.Redshift != null)
                    {
                        _RedshiftPwd = EnvironmentCredentials.Redshift.Password;
                    }
                    else
                    {
                        _RedshiftPwd = Util.Parameter.GetParameter("redshiftpwd").Value;
                    }
                }
                return _RedshiftPwd;
            }
            set { _RedshiftPwd = value; }
        }
        private static string _S3AccessKey;
        public static string S3AccessKey
        {
            get
            {
                if (string.IsNullOrEmpty(_S3AccessKey))
                {
                    if (EnvironmentCredentials.AWS != null)
                    {
                        _S3AccessKey = EnvironmentCredentials.AWS.Username;
                    }
                    else
                    {
                        _S3AccessKey = Util.Parameter.GetParameter("S3AccessKey").Value;
                    }

                }

                return _S3AccessKey;
            }
            set { _S3AccessKey = value; }
        }
        private static string _S3SecretKey;
        public static string S3SecretKey
        {
            get
            {
                if (string.IsNullOrEmpty(_S3SecretKey))
                {
                    if (EnvironmentCredentials.AWS != null)
                    {
                        _S3SecretKey = EnvironmentCredentials.AWS.Password;
                    }
                    else
                    {
                        _S3SecretKey = Util.Parameter.GetParameter("S3SecretKey").Value;
                    }

                }

                return _S3SecretKey;
            }
            set { _S3SecretKey = value; }
        }
        private static string _EmailAddress;
        public static string EmailAddress
        {
            get
            {
                if (string.IsNullOrEmpty(_EmailAddress))
                {
                    if (EnvironmentCredentials.Email != null)
                    {
                        _EmailAddress = EnvironmentCredentials.Email.Username;
                    }
                    else
                    {
                        _EmailAddress = Util.Parameter.GetParameter("emailaddress").Value;
                    }
                }
                return _EmailAddress;
            }
            set { _EmailAddress = value; }
        }
        private static string _EmailPwd;
        public static string EmailPwd
        {
            get
            {
                if (string.IsNullOrEmpty(_EmailPwd))
                {
                    if (EnvironmentCredentials.Email != null)
                    {
                        _EmailPwd = EnvironmentCredentials.Email.Password;
                    }
                    else
                    {
                        _EmailPwd = Util.Parameter.GetParameter("emailpwd").Value;
                    }
                }
                return _EmailPwd;
            }
            set { _EmailPwd = value; }
        }
        public static string ExecutablePath { get { return System.Windows.Forms.Application.ExecutablePath; } }
        public static string ExecutableDirectory { get { return new System.IO.FileInfo(System.Windows.Forms.Application.ExecutablePath).DirectoryName; } }

        private static string _Key;
        public static string Key
        {
            get
            {
                if (string.IsNullOrEmpty(_Key))
                {
                    _Key = Util.Parameter.GetParameter("key").Value;
                }
                return _Key;
            }
            set { _Key = value; }
        }
    }
}
