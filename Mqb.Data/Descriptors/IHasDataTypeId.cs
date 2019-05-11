using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Descriptors
{
    public interface IHasDataTypeId_R
    {
        string DataTypeId { get; }
    }
    public interface IHasDataTypeId_RW
    {
        string DataTypeId { get; set; }
    }
}
