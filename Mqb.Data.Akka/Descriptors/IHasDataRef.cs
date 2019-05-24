using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Descriptors
{
    public interface IHasDataRef_R
    {
        IActorRef Data { get; }
    }
    public interface IHasDataRef_RW : IHasDataRef_R
    {
        new IActorRef Data { get; set; }
    }
}
