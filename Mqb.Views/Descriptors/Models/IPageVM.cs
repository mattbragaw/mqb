using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Descriptors.Models
{
    public interface IPageVM : IBaseVM,
        IHasTitle_R
    {
    }
    public interface IPageVMMutable : IBaseVMMutable,
        IHasTitle_RW
    {
    }
}
