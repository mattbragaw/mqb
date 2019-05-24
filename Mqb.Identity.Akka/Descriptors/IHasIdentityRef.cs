using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Descriptors
{
    public interface IHasIdentityRef_R
    {
        IActorRef Identity { get; }
    }
    public interface IHasIdentityRef_RW : IHasIdentityRef_R
    {
        new IActorRef Identity { get; set; }
    }
}
