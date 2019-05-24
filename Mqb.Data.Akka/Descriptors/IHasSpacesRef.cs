using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Descriptors
{
    public interface IHasSpacesRef_R
    {
        IActorRef Spaces { get; }
    }
    public interface IHasSpacesRef_RW : IHasSpacesRef_R
    {
        new IActorRef Spaces { get; set; }
    }
}
