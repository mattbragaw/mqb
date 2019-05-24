using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Descriptors
{
    public interface IHasDataTypesRef_R
    {
        IActorRef DataTypes { get; }
    }
    public interface IHasDataTypesRef_RW : IHasDataTypesRef_R
    {
        new IActorRef DataTypes { get; set; }
    }
}
