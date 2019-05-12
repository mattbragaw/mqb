using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Akka.Actors
{
    public class EntitiesActor : AutoSnapshotActor<EntitiesActor.EntitiesState>
    {
        
        public class EntitiesState : PersistentState
        {
            
        }

        public EntitiesActor()
        {
            PersistenceId = ConstantsDataAkka.ENTITIES;
        }

        public override string PersistenceId { get; }

    }
}
