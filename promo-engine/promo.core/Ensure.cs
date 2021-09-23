using System;
using System.Collections.Generic;
using System.Text;

namespace promo.core
{
    public static class Ensure
    {
        public static void EnsureNotNull<T>(this T target, string name)
        {
            if (target == null)
                throw new ArgumentNullException(name);

        }

        public static void EnsureNotNullOrWhiteSpace(this string target, string name)
        {
            if (string.IsNullOrWhiteSpace(target) == true)
                throw new ArgumentNullException(name);
        }
    }

}