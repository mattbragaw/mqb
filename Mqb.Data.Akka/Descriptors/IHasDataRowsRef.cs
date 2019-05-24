using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Descriptors
{
    public interface IHasDataRowsRef_R
    {
        IActorRef DataRows { get; }
    }
    public interface IHasDataRowsRef_RW : IHasDataRowsRef_R
    {
        new IActorRef DataRows { get; set; }
    }
}
