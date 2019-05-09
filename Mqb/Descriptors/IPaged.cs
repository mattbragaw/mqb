using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Descriptors
{
    public interface IPaged : IEnumerable,
        IHasPageIndex_R,
        IHasTotalPages_R,
        IHasNextPage_R,
        IHasPreviousPage_R,
        IHasPageSize_R,
        IHasTotalCount_R
    {
    }
    public interface IPaged<T> : IPaged, IEnumerable<T> { }
    public interface IPagedMutable : IList,
        IHasPageIndex_RW,
        IHasTotalPages_RW,
        IHasNextPage_RW,
        IHasPreviousPage_RW,
        IHasPageSize_RW,
        IHasTotalCount_RW,
        ICanCalculatePagination
    {
    }
    public interface IPagedMutable<T> : IPagedMutable, IList<T> { }
}
