using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Akka.Actors
{
    public class SpacesActor : ReceiveActor
    {
        public static string GetName() => ConstantsDataAkka.DATA_SPACES;

        public SpacesActor(IActorRef entities)
        {
            Entities = entities;
        }

        public IActorRef Entities { get; }
    }
}
