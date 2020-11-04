using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Exceptions
{
    public class SendEmailException : Exception
    {
        public SendEmailException() : base("Cannot send email to user !")
        {
        }

        public SendEmailException(string errorMessage) : base(errorMessage)
        {
        }
    }
}
