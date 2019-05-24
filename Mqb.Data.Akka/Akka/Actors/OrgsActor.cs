using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Akka.Actors
{
    public class OrgsActor : ReceiveActor
    {
        public static string GetName() => ConstantsDataAkka.DATA_ORGS;

        public OrgsActor(IActorRef entities)
        {
            Entities = entities;
        }

        public IActorRef Entities { get; }
    }
}
