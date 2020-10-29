using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Unknown
{
    public static class EmptyArray<T>
    {
        public static T[] Instance { get; } = new T[0];
    }
}
