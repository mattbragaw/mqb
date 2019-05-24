using Mqb.Descriptors.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Models
{
    public class PageVM : BaseVM, IPageVM
    {
        public PageVM(string title)
        {
            Title = title;
        }

        public string Title { get; }
    }
    public class PageVMMutable : BaseVMMutable, IPageVMMutable
    {
        private string _title;
        public virtual string Title
        {
            get { return _title; }
            set
            {
                var oldValue = _title;
                _title = value;
                
                OnPropertyChanged(nameof(Title));
            }
        }
    }
}
