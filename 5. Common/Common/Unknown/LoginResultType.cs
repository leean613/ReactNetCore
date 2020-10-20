using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Unknown
{
    public enum LoginResultType : byte
    {
        Success = 1,

        InvalidUserNameOrPassword,

        UserIsNotActive,

        UserLockout,

        InvalidSystem
    }
}
