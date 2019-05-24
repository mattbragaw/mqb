using Akka.Actor;
using Mqb.Akka.Config;
using Mqb.Descriptors.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Identity.Akka.Config
{
    public class AkkaIdentityBuild : AkkaDataBuild
    {
        public AkkaIdentityBuild(ActorSystem actorSystem, Type actorRefsType) : base(actorSystem, actorRefsType)
        {
            if (actorRefsType.GetInterface(typeof(IIdentityRefs).Name) == null)
                throw new ArgumentException("Type doesn't implement IIdentityRefs interface.");
            if (actorRefsType.GetInterface(typeof(IIdentityRefsMutable).Name) == null)
                throw new ArgumentException("Type doesn't implement IIdentityRefsMutable interface.");
        }
    }
    public class AkkaIdentityBuild<TInt> : AkkaIdentityBuild
            where TInt : class, IDataRefs, IDataRefsMutable, IIdentityRefs, IIdentityRefsMutable
    {
        public AkkaIdentityBuild(ActorSystem actorSystem) :
            base(actorSystem, typeof(TInt))
        {
        }
    }
}
