using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.ApiResults
{
    public enum ApiErrorCodes
    {
        Failed,
        ObjectNotFound,
        InvalidParamters,
        UserLoginInvalidUserNameOrPassword,
        UserLoginIsNotActive,
        ResetPasswordActiveCodeIsIncorrect,
        InvalidSystem
    }
}
