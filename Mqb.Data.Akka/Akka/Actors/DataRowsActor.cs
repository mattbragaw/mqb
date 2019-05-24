using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Akka.Actors
{
    public class DataRowsActor : ReceiveActor
    {
        public static string GetName() => ConstantsDataAkka.DATA_ROWS;

        public DataRowsActor(IActorRef entitiesRef)
        {
            EntitiesRef = entitiesRef;
        }

        public IActorRef EntitiesRef { get; }
    }
}
