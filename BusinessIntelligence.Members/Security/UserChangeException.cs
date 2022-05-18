using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessIntelligence.Members
{
    public class UserChangeException : Exception
    {
        public UserChangeException(ChangePasswordStatus userChangeStatus)
        {
            _UserChangeStatus = userChangeStatus;
        }

        ChangePasswordStatus _UserChangeStatus;

        public ChangePasswordStatus UserChangeStatus
        {
            get { return _UserChangeStatus; }

        }

    }
}
