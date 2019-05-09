using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Extensions
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<TOut> Convert<TIn, TOut>(this IEnumerable<TIn> input, Func<TIn, TOut> convertFunc)
        {
            List<TOut> output = new List<TOut>();

            foreach (var item in input)
                output.Add(convertFunc(item));

            return output;
        }
        public static IEnumerable<TOut> Convert<TIn, P1, TOut>(this IEnumerable<TIn> input, P1 p1, Func<TIn, P1, TOut> convertFunc)
        {
            List<TOut> output = new List<TOut>();

            foreach (var item in input)
                output.Add(convertFunc(item, p1));

            return output;
        }
        public static IEnumerable<TOut> Convert<TIn, P1, P2, TOut>(this IEnumerable<TIn> input, P1 p1, P2 p2, Func<TIn, P1, P2, TOut> convertFunc)
        {
            List<TOut> output = new List<TOut>();

            foreach (var item in input)
                output.Add(convertFunc(item, p1, p2));

            return output;
        }
    }
}
