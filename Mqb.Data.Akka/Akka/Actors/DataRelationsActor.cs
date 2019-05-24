using System;
using System.Collections.Generic;
using System.Text;
using Akka.Actor;

namespace Mqb.Akka.Actors
{
    public class DataRelationsActor : ReceiveActor
    {
        public static string GetName() => ConstantsDataAkka.DATA_RELATIONS;

        public DataRelationsActor(IActorRef entities)
        {
            Entities = entities;
        }

        public IActorRef Entities { get; }

    }
}
