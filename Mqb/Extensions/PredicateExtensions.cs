using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb
{
    public static class PredicateExtensions
    {
        public static Predicate<object> Generalize<T>(this Predicate<T> predicate)
        {
            return obj =>
            {
                if (obj != null && obj.GetType() == typeof(T))
                {
                    T typedObj = (T)obj;
                    return predicate(typedObj);
                }

                return false;
            };
        }
    }
}
