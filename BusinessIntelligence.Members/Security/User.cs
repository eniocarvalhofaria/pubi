using System;
using System.Collections.Generic;
using System.Linq;
//using System.Web;
using System.Security;
using BusinessIntelligence.Persistence;
using System.Text;
namespace BusinessIntelligence.Members.Security
{
    public class User : PersistentObject
    {
        public User()
            : base()
        { }

        private User _Manager;

        public virtual User Manager
        {
            get { return _Manager; }
            set { _Manager = value; }
        }

        private string _Name;
        [NotNull]
        [CharAttribute(false, 100)]
        public virtual string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        private int _LoginAtempts;

        public int LoginAtempts
        {
            get { return _LoginAtempts; }
            set
            {

                _LoginAtempts = value;
                if (value >= 3)
                {
                    _State = UserState.BlockedByFailedAttempts;
                }
            }
        }
        private UserState _State = UserState.NotActivated;

        public UserState State
        {
            get { return _State; }
            set { _State = value; }
        }


        private string _EmailAddress;
        [IsUnique]
        [NotNull]
        [Indexed]
        public string EmailAddress
        {
            get { return _EmailAddress; }
            set { _EmailAddress = value; }
        }

        private string _Password;
        [NotNullAttribute]
        public string Password
        {
            get { return _Password; }
            set
            {
                _Password = Hash256(string.Format("{0}{1}", value, _EmailAddress));
            }
        }
        public static User GetByEmailAddress(string emailAddress)
        {
            User[] u = PersistenceSettings.PersistenceEngine.GetObjects<User>(FilterExpressions.Equal("EmailAddress", emailAddress));
            if (u.Length > 0)
            {
                return u[0];
            }
            else
            { return null; }

        }
        static int _MinPasswordLength;

        public static int MinPasswordLength
        {
            get { return User._MinPasswordLength; }
            set { User._MinPasswordLength = value; }
        }

        public ChangePasswordStatus ChangePassword(string currentPassword, string newPassword)
        {
            if (newPassword.Length < MinPasswordLength)
            {
                return ChangePasswordStatus.PasswordTooShort;
            }
            if (!ValidatePasswordComplexity(newPassword))
            {
                return ChangePasswordStatus.MinimumComplexityUnreached;
            }
            if (Authenticate(currentPassword))
            {

                this.Password = newPassword;
                this.Update();
                return ChangePasswordStatus.Sucessfull;
            }
            else
            {
                return ChangePasswordStatus.OldPasswordWrong;
            }
        }
        public static bool ValidatePasswordComplexity(string passWord)
        {
            return true;
        }
        public bool Authenticate(string password)
        {
            if (Hash256(string.Format("{0}{1}", password, this.EmailAddress)).Equals(this.Password))
            {
                isAuthenticated = true;
                return true;
            }
            else
            {
                isAuthenticated = false;
                this.LoginAtempts++;
                this.Update();
                return false;
            }
        }
        bool isAuthenticated = false;
        public static CreateUserStatus Create(string emailAddress, string password, string Name, out User user)
        {
            user = GetByEmailAddress(emailAddress);
            if (user != null)
            {
                return CreateUserStatus.EmailAddressAlreadyRegistered;
            }
            else
            {
                user = new User();
                user.Name = Name;
                user.EmailAddress = emailAddress;
                user.Password = password;
                user.Create();
                return CreateUserStatus.Sucessfull;
            }
        }
        public static User Login(string emailAddress, string password)
        {
            User usr = GetByEmailAddress(emailAddress);
            if (usr != null)
            {
                if (usr.Authenticate(password))
                {
                    return usr;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }

        }
        private static string Hash256(string valor)
        {
            UnicodeEncoding UE = new UnicodeEncoding();
            byte[] HashValue = new byte[256];
            byte[] MessageBytes = UE.GetBytes(valor);
            byte[] MessageBytes256 = new byte[256];
            for (int i = 0; i <= 255; i++)
            {
                MessageBytes256[i] = MessageBytes[i % MessageBytes.Length];

            }

            for (int i = 0; i <= 255; i++)
            {
                if (i < MessageBytes.Length)
                {
                    HashValue[i] = (byte)(((int)MessageBytes256[i] + i) % 256);
                }
            }
            string strHex = null;
            foreach (byte b in HashValue)
            {
                strHex += String.Format("{0:x2}", b);
            }
            return strHex;
        }

        public override void SetValue(string fieldName, object value)
        {
            if (fieldName.Equals("State1"))
            {
                _State = (UserState)Convert.ToInt32(value);

            }
            else if (fieldName.Equals("Password"))
            {
                _Password = (string)value;
            }
            else
            {
                base.SetValue(fieldName, value);
            }
        }


    }
}