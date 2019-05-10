using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Akka
{
    public static class Path
    {
        public static string GetUniqueName(string baseType)
        {
            return string.Format("{0}-{1}", baseType, Unique.String());
        }
    }
}
