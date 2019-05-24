using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Descriptors
{
    public interface IHasTitle_R
    {
        string Title { get; }
    }
    public interface IHasTitle_RW
    {
        string Title { get; set; }
    }
}
