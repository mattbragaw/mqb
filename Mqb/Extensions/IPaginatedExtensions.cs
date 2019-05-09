using Mqb.Descriptors;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Extensions
{
    public static class IPaginatedExtensions
    {
        #region Conversion Methods

        public static IPaged<T> ToImmutable<T>(this IPagedMutable<T> input)
        {
            return new Paged<T>(input, input.TotalCount, input.PageSize, input.PageIndex, input.TotalPages);
        }
        public static IPagedMutable<T> ToMutable<T>(this IPaged<T> input)
        {
            var response = new PagedMutable<T>(input)
            {
                TotalCount = input.TotalCount,
                PageSize = input.PageSize,
                PageIndex = input.PageIndex,
                TotalPages = input.TotalPages
            };

            response.CalculatePagination();

            return response;
        }

        public static IPaged<TOut> Convert<TIn, TOut>(this IPaged<TIn> input, Func<TIn, TOut> func)
        {
            var items = new List<TOut>();

            foreach (var item in input)
                items.Add(func(item));

            return new Paged<TOut>(items, input.TotalCount, input.PageSize, input.PageIndex, input.TotalPages);
        }
        public static IPagedMutable<TOut> Convert<TIn, TOut>(this IPagedMutable<TIn> input, Func<TIn, TOut> func)
        {
            var items = new List<TOut>();

            foreach (var item in input)
                items.Add(func(item));

            var response = new PagedMutable<TOut>(items)
            {
                TotalCount = input.TotalCount,
                PageSize = input.PageSize,
                PageIndex = input.PageIndex,
                TotalPages = input.TotalPages
            };

            response.CalculatePagination();

            return response;
        }

        #endregion

    }
}
