using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Akka.Actors
{
    public class EntitiesActor : AutoSnapshotActor<EntitiesActor.EntitiesState>
    {
        #region Command Definitions

        public class GetEntitySet : Cmd
        {
            public GetEntitySet(Type type)
            {
                Type = type;
            }

            public Type Type { get; }
        }
        public class EntitySet : Doc
        {
            public EntitySet(string id, Type type)
            {
                Id = id;
                Type = type;
            }

            public string Id { get; }
            public Type Type { get; }
        }
        public class EntitySetResult : Evnt
        {
            public EntitySetResult(EntitySet entitySet, IActorRef actorRef)
            {
                EntitySet = entitySet;
                ActorRef = actorRef;
            }

            public EntitySet EntitySet { get; }
            public IActorRef ActorRef { get; }
        }

        #endregion

        #region Events Definitions

        public class EntitySetCreated : Evnt
        {
            public EntitySetCreated(EntitySet entitySet)
            {
                EntitySet = entitySet;
            }

            public EntitySet EntitySet { get; }
        }

        #endregion

        public class EntitiesState : PersistentState
        {
            public Dictionary<Type, string> EntitySets { get; set; } = new Dictionary<Type, string>();
            public Dictionary<string, IActorRef> EntitySetRefs { get; set; } = new Dictionary<string, IActorRef>();
        }

        public EntitiesActor()
        {
            PersistenceId = ConstantsDataAkka.ENTITIES;

            Recover<EntitySetCreated>(evnt => EntitySetCreatedEvnt(evnt));

            Command<GetEntitySet>(cmd => GetEntitySetCmd(cmd));
        }
        
        #region Properties

        public override string PersistenceId { get; }

        #endregion

        #region Command Handlers

        private void GetEntitySetCmd(GetEntitySet cmd)
        {
            EntitySet entitySet;

            if (State.EntitySets.ContainsKey(cmd.Type))
            {
                entitySet = new EntitySet(State.EntitySets[cmd.Type], cmd.Type);
                SendEntitySetResult(entitySet);
            }   
            else
            {
                entitySet = new EntitySet(Unique.String(), cmd.Type);
                EntitySetCreated evnt = new EntitySetCreated(entitySet);
                PersistAndTrack(evnt, result =>
                {
                    EntitySetCreatedEvnt(evnt);
                    SendEntitySetResult(entitySet);
                });
            }
        }

        #endregion

        #region Event Handlers

        private void EntitySetCreatedEvnt(EntitySetCreated evnt)
        {
            State.EntitySets.Add(evnt.EntitySet.Type, evnt.EntitySet.Id);
        }

        #endregion

        #region Utility Methods

        private void SendEntitySetResult(EntitySet entitySet)
        {
            EntitySetResult result = null;

            if (State.EntitySetRefs.ContainsKey(entitySet.Id))
                result = new EntitySetResult(entitySet, State.EntitySetRefs[entitySet.Id]);
            else
            {
                IActorRef newChild = Context.ActorOf(Props.Create(() => new EntitySetActor(entitySet.Id, entitySet.Type)), entitySet.Id);
                State.EntitySetRefs.Add(entitySet.Id, newChild);
                result = new EntitySetResult(entitySet, newChild);
            }

            Sender.Tell(result);
        }

        #endregion
    }
}
