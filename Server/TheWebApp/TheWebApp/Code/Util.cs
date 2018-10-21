using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheWebApp.Code
{
    public static class Util
    {
        public static IEnumerable<T> GetAllItems<T>() where T : struct
        {
            foreach (object item in Enum.GetValues(typeof(T)))
            {
                yield return (T)item;
            }
        }
    }
}