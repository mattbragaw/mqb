using Mqb.Descriptors.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Descriptors
{
    public interface IHasDataRow_R
    {
        IDataRow DataRow { get; }
    }
    public interface IHasDataRow_RW
    {
        IDataRowMutable DataRow { get; set; }
    }
}
