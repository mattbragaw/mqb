using System;
using System.Collections.Generic;
using System.Text;
using Akka.Actor;

namespace Mqb.Akka.Actors
{
    public class DataReferencesActor : ReceiveActor
    {
        public static string GetName() => ConstantsDataAkka.DATA_REFERENCES;

        public DataReferencesActor(IActorRef entities)
        {
            Entities = entities;
        }

        public IActorRef Entities { get; }
    }
}
