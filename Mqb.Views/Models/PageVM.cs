using Mqb.Descriptors.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Models
{
    public class PageVM : BaseTitledVM, IPageVM
    {
        public PageVM(string title) : base(title)
        {
        }
    }
    public class PageVMMutable : BaseTitledVMMutable, IPageVMMutable
    {
    }
}
