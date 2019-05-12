using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Akka.Actors
{
    public class EntitySetActor : AutoSnapshotActor<EntitySetActor.EntitySetState>
    {
        public class EntitySetState : PersistentState
        {
            public string Id { get; set; }
            public HashSet<string> IdsActive { get; set; } = new HashSet<string>();
            public HashSet<string> IdsInactive { get; set; } = new HashSet<string>();
            public HashSet<string> IdsDeletedActive { get; set; } = new HashSet<string>();
            public HashSet<string> IdsDeletedInactive { get; set; } = new HashSet<string>();
        }

        public EntitySetActor(string id)
        {
            State.Id = id;
            PersistenceId = string.Format("{0}-{1}", ConstantsDataAkka.ENTITY_SET, id);
        }

        public override string PersistenceId { get; }
    }
}
