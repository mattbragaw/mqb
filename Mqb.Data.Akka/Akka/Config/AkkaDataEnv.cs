using System;
using System.Collections.Generic;
using System.Text;
using Akka.Actor;
using Mqb.Descriptors.Models;

namespace Mqb.Akka.Config
{
    public class AkkaDataEnv : AkkaEnv
    {
        public AkkaDataEnv(ActorSystem actorSystem, IDataRefs dataRefs) : base(actorSystem)
        {
            DataRefs = dataRefs;
        }

        public IDataRefs DataRefs { get; }
    }
}
