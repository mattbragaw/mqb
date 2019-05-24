using Akka.Actor;
using Mqb.Akka.Config;
using Mqb.Descriptors.Models;
using Mqb.Identity.Akka.Actors;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Identity.Akka.Config
{
    public static class AkkaDataEnvExtensions
    {
        public static AkkaIdentityEnv WithIdentity(this AkkaDataEnv akkaDataEnv)
        {
            if (akkaDataEnv.DataRefs is IIdentityRefs == false)
                throw new ArgumentException("Type doesn't implement IIdentityRefs interface.");
            if (akkaDataEnv.DataRefs is IIdentityRefsMutable == false)
                throw new ArgumentException("Type doesn't implement IIdentityRefsMutable interface.");

            // get reference to identityRefs
            IIdentityRefsMutable identityRefs = (IIdentityRefsMutable)akkaDataEnv.DataRefs;

            // identity actor populates refs
            akkaDataEnv.ActorSystem.ActorOf(Props.Create(() => new IdentityActor(identityRefs)), IdentityActor.GetName());

            // return actor system reference
            return new AkkaIdentityEnv(akkaDataEnv.ActorSystem, identityRefs);
        }
        public static AkkaIdentityEnv WithIdentity<TRefs>(this AkkaDataEnv akkaDataEnv)
            where TRefs : class, IDataRefs, IDataRefsMutable, IIdentityRefs, IIdentityRefsMutable
        {
            return WithIdentity(akkaDataEnv);
        }
    }
}
