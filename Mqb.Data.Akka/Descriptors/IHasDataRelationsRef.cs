using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Descriptors
{
    public interface IHasDataRelationsRef_R
    {
        IActorRef DataRelations { get; }
    }
    public interface IHasDataRelationsRef_RW : IHasDataRelationsRef_R
    {
        new IActorRef DataRelations { get; set; }
    }
}
