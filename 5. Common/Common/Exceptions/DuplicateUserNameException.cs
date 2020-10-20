using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Exceptions
{
    public class DuplicateUserNameException : Exception
    {
        public DuplicateUserNameException() : base("Duplicate user name.")
        {
        }

        public DuplicateUserNameException(string errorMessage) : base(errorMessage)
        {
        }
    }
}
