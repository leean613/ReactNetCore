using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Extentions
{
    public static class CommonExtensions
    {
        public static TOut IfNotNull<TIn, TOut>(this TIn value, Func<TIn, TOut> innerProperty, TOut otherwise)
            where TIn : class
        {
            return value == null ? otherwise : innerProperty(value);
        }
    }
}
