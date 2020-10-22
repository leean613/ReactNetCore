using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Exceptions
{
    public class BusinessException : Exception
    {
        public BusinessException() : base("Business exception")
        {
        }

        public BusinessException(string errorMessage) : base(errorMessage)
        {
        }
    }
}
