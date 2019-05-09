using Mqb.Descriptors;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Mqb
{
    public class Paged : IPaged
    {
        public Paged(IEnumerable items, int totalCount, int pageSize, int pageIndex)
        {
            Items = items;
            TotalCount = totalCount;
            PageSize = pageSize;
            PageIndex = pageIndex;

            if (PageSize < 1)
                PageSize = 1;
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            HasPreviousPage = (PageIndex > 1);
            HasNextPage = (PageIndex < TotalPages);
        }
        public Paged(IEnumerable items, int totalCount, int pageSize, int pageIndex, int totalPages)
        {
            Items = items;
            TotalCount = totalCount;
            PageSize = pageSize;
            PageIndex = pageIndex;
            TotalPages = totalPages;

            HasPreviousPage = (PageIndex > 1);
            HasNextPage = (PageIndex < TotalPages);
        }

        public IEnumerable Items { get; }
        public int PageIndex { get; }
        public int TotalPages { get; }
        public bool HasNextPage { get; }
        public bool HasPreviousPage { get; }
        public int PageSize { get; }
        public int TotalCount { get; }

        public IEnumerator GetEnumerator()
        {
            return Items.GetEnumerator();
        }
    }
    public class Paged<T> : Paged, IPaged<T>
    {
        public Paged(IEnumerable<T> items, int totalCount, int pageSize, int pageIndex) :
            base(items, totalCount, pageSize, pageIndex)
        {
        }
        public Paged(IEnumerable items, int totalCount, int pageSize, int pageIndex, int totalPages) :
            base(items, totalCount, pageSize, pageIndex, totalPages)
        {
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return (IEnumerator<T>)Items.GetEnumerator();
        }
    }
    public class PagedMutable : List<object>, IPagedMutable
    {
        public PagedMutable(IEnumerable<object> collection, int totalCount, int pageSize, int pageIndex) : this(collection)
        {
            PageIndex = pageIndex;
            TotalCount = totalCount;
            PageSize = pageSize;

            CalculatePagination();
        }
        public PagedMutable()
        {
        }

        public PagedMutable(IEnumerable<object> collection) : base(collection)
        {
        }

        public PagedMutable(int capacity) : base(capacity)
        {
        }

        public int PageIndex { get; set; }
        public int TotalPages { get; set; }
        public bool HasNextPage { get; set; }
        public bool HasPreviousPage { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }

        public void CalculatePagination()
        {
            if (PageSize < 1)
                PageSize = 1;
            TotalPages = (int)Math.Ceiling(TotalCount / (double)PageSize);
            HasPreviousPage = (PageIndex > 1);
            HasNextPage = (PageIndex < TotalPages);
        }
    }
    public class PagedMutable<T> : List<T>, IPagedMutable<T>
    {
        public PagedMutable(IEnumerable<T> collection, int totalCount, int pageSize, int pageIndex) : this(collection)
        {
            PageIndex = pageIndex;
            TotalCount = totalCount;
            PageSize = pageSize;

            CalculatePagination();
        }
        public PagedMutable()
        {
        }

        public PagedMutable(IEnumerable<T> collection) : base(collection)
        {
        }

        public PagedMutable(int capacity) : base(capacity)
        {
        }

        public int PageIndex { get; set; }
        public int TotalPages { get; set; }
        public bool HasNextPage { get; set; }
        public bool HasPreviousPage { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }

        public void CalculatePagination()
        {
            if (PageSize < 1)
                PageSize = 1;

            TotalPages = (int)Math.Ceiling(TotalCount / (double)PageSize);
            HasPreviousPage = (PageIndex > 1);
            HasNextPage = (PageIndex < TotalPages);
        }
    }
}
