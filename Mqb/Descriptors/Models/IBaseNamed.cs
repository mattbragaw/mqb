﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Descriptors.Models
{
    public interface IBaseNamed : IBase, IHasName_R
    {
    }
    public interface IBaseNamedMutable : IBaseMutable, IHasName_RW
    {

    }
}
