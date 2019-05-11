using Mqb.Descriptors.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Models
{
    public abstract class Base : IBase
    {
        public Base(string id)
        {
            Id = id;
        }

        public string Id { get; }
    }
    public abstract class BaseMutable : IBaseMutable
    {
        public virtual string Id { get; set; }
    }
}
