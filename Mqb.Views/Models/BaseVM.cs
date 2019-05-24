using Mqb.Descriptors.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Mqb.Models
{
    public abstract class BaseVM : IBaseVM
    {
    }
    public abstract class BaseVMMutable : IBaseVMMutable
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(name));
        }
        public void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            OnPropertyChanged(this, args);
        }
        public void OnPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            PropertyChanged?.Invoke(sender, args);
        }
    }
}
