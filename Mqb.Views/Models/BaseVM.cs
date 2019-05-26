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

        protected virtual void SetAndNotifyIfChanged<TValue>(string name, ref TValue propValue, TValue newValue)
            where TValue : IEquatable<TValue>
        {
            if (EqualityComparer<TValue>.Default.Equals(propValue, newValue))
                return;

            var oldValue = propValue;
            propValue = newValue;

            OnPropertyChanged(name, oldValue, newValue);
        }
        public virtual void OnPropertyChanged(string name, object oldValue, object newValue)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(name), oldValue, newValue);
        }
        public virtual void OnPropertyChanged(PropertyChangedEventArgs args, object oldValue, object newValue)
        {
            OnPropertyChanged(this, args, oldValue, newValue);
        }
        public virtual void OnPropertyChanged(object sender, PropertyChangedEventArgs args, object oldValue, object newValue)
        {
            PropertyChanged?.Invoke(sender, args);
        }
    }
}
