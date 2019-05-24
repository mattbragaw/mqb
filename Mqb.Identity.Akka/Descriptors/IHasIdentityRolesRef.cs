using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Descriptors
{
    public interface IHasIdentityRolesRef_R
    {
        IActorRef IdentityRoles { get; }
    }
    public interface IHasIdentityRolesRef_RW : IHasIdentityRolesRef_R
    {
        new IActorRef IdentityRoles { get; set; }
    }
}
