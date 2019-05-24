using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Descriptors
{
    public interface IHasOrgsRef_R
    {
        IActorRef Orgs { get; }
    }
    public interface IHasOrgsRef_RW : IHasOrgsRef_R
    {
        new IActorRef Orgs { get; set; }
    }
}
