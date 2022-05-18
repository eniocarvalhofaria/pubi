using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessIntelligence.Members
{
    public enum UserState
    {
        NotActivated = 1,
        Active  = 2,
        BlockedByFailedAttempts = 3,
        BlockedByAdmin = 4,
        Authorized
    }
}
