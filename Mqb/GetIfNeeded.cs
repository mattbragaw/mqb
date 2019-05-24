using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb
{
    public class GetIfNeeded<T> where T : class
    {
        public GetIfNeeded(Func<T> getValueFunc)
        {
            _GetValueFunc = getValueFunc;
        }

        Func<T> _GetValueFunc;
        T _Value;

        public T Value
        {
            get
            {
                if (_Value == null)
                    _Value = _GetValueFunc();

                return _Value;
            }
        }
    }
}
