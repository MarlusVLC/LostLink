using System;
using System.Collections.Generic;
using System.Linq;

namespace Aux_Classes
{
    public static class Extensions
    {
        public static int CountSetBits(this int n)
        {
            if (n == 0)
                return 0;

            return (n & 1) + CountSetBits(n >> 1);
        }

        public static void ForEach<T>(this IEnumerable<T> enumeration, Action<T> action)
        {
            foreach (var item in enumeration)
            {
                action(item);
            }
        }
    }
}