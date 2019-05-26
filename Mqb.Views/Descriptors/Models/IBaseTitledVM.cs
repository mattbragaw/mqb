using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Descriptors.Models
{
    public interface IBaseTitledVM : IBaseVM,
        IHasTitle_R
    {
    }
    public interface IBaseTitledVMMutable : IBaseVMMutable,
        IHasTitle_RW
    {
    }
}
