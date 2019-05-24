using Akka.Actor;
using Mqb.Akka.Config;
using Mqb.Descriptors.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Identity.Akka.Config
{
    public class AkkaIdentityEnv : AkkaDataEnv
    {
        public AkkaIdentityEnv(ActorSystem actorSystem, IIdentityRefs identityRefs) : base(actorSystem, identityRefs)
        {
            IdentityRefs = identityRefs;
        }

        public IIdentityRefs IdentityRefs { get; }
    }
}
