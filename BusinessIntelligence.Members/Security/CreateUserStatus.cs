using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessIntelligence.Members
{
    public enum CreateUserStatus
    {
        Sucessfull = 1,
        MinimumComplexityUnreached = 2,
        PasswordTooShort = 3,
        EmailAddressAlreadyRegistered = 4,
        UnknownError = 5,
        WaitingAdminAuthorization = 6

    }
}
