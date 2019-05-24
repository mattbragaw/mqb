using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Akka.Config
{
    public class AkkaEnv
    {
        public AkkaEnv(ActorSystem actorSystem)
        {
            ActorSystem = actorSystem;
        }

        public ActorSystem ActorSystem { get; }
    }
}
