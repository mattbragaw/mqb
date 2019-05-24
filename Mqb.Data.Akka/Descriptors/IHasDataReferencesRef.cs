using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Descriptors
{
    public interface IHasDataReferencesRef_R
    {
        IActorRef DataReferences { get; }
    }
    public interface IHasDataReferencesRef_RW : IHasDataReferencesRef_R
    {
        new IActorRef DataReferences { get; set; }
    }
}
