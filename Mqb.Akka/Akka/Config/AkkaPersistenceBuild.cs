using System;
using System.Collections.Generic;
using System.Text;
using Akka.Actor;

namespace Mqb.Akka.Config
{
    public class AkkaPersistenceBuild : AkkaBuild
    {
        public AkkaPersistenceBuild(ActorSystem actorSystem) : base(actorSystem)
        {
        }
    }
}
