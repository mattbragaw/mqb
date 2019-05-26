using Mqb.Descriptors.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Models
{
    public abstract class BaseTitledVM : BaseVM, IBaseTitledVM
    {
        public BaseTitledVM(string title)
        {
            Title = title;
        }

        public string Title { get; }
    }
    public abstract class BaseTitledVMMutable : BaseVMMutable, IBaseTitledVMMutable
    {
        private string _title;
        public virtual string Title
        {
            get { return _title; }
            set
            {
                SetAndNotifyIfChanged(nameof(Title), ref _title, value);
            }
        }
    }
}
