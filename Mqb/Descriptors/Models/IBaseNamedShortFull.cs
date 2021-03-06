﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Descriptors.Models
{
    public interface IBaseNamedShortFull : IBaseNamedShort, IHasNameShortFull_R
    {
    }
    public interface IBaseNamedShortFullMutable : IBaseNamedShortMutable, IHasNameShortFull_RW
    {
    }
}
