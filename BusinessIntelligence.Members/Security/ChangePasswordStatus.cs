using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessIntelligence.Members
{
    public enum ChangePasswordStatus
    {
        Sucessfull = 1,
        MinimumComplexityUnreached = 2,
        PasswordTooShort = 3,
        OldPasswordWrong = 4
    }
}
