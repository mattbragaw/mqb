using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Descriptors
{
    public interface IHasIdentityUsersRef_R
    {
        IActorRef IdentityUsers { get; }
    }
    public interface IHasIdentityUsersRef_RW : IHasIdentityUsersRef_R
    {
        new IActorRef IdentityUsers { get; set; }
    }
}
