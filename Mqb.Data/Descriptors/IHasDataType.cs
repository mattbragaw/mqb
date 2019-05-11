using Mqb.Descriptors.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Descriptors
{
    public interface IHasDataType_R
    {
        IDataType DataType { get; }
    }
    public interface IHasDataType_RW
    {
        IDataTypeMutable DataType { get; set; }
    }
}
