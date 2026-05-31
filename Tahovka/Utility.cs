using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tahovka
{
    // currently doesnt do much,,, i will put other stuff later maybe 
    // a class to hold functions that dont fit anywhere else but is used across scripts
    public static class Utility
    {

        public static string CleanseString(string input)
        {
            return input.Trim().ToLower();
        }

        public static T Clamp<T>(T value, T min, T max) where T : IComparable<T> 
        {
            if (value.CompareTo(min) < 0) return min;
            if (value.CompareTo(max) > 0) return max;
            return value;
        }

    }
}
