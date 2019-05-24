using Akka.Actor;
using Akka.Configuration;
using System;
using System.Collections.Generic;
using System.Text;


namespace Mqb.Akka.Config
{
    public class AkkaBuild
    {
        public AkkaBuild(ActorSystem actorSystem)
        {
            ActorSystem = actorSystem;
        }

        public ActorSystem ActorSystem { get; }
    }
}
