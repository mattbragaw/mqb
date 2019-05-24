using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Descriptors
{
    public interface IHasEntitiesRef_R
    {
        IActorRef Entities { get; }
    }
    public interface IHasEntitiesRef_RW : IHasEntitiesRef_R
    {
        new IActorRef Entities { get; set; }
    }
}
