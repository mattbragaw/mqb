using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Akka.Actors
{
    public class EntityActor : AutoSnapshotActor<EntityActor.EntityState>
    {
        public class EntityState : PersistentState
        {
            public string Id { get; set; }
            public object Data { get; set; }
        }

        public EntityActor(string id)
        {
            State.Id = id;
            PersistenceId = string.Format("{0}-{1}", ConstantsDataAkka.ENTITY, id);
        }

        public override string PersistenceId { get; }

    }
}
