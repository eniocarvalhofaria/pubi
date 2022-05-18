using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessIntelligence.Listening
{
    public enum ExecutionStep
    {
        Listen = 1,
        ETLProgram = 2,
        Executable = 3,
        Refresh = 4,
        Publish = 5,
        End = 6

    }
}
