using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Akka.Actors
{
    public class EntitiesActor : AutoSnapshotActor<EntitiesActor.EntitiesState>
    {
        #region Command Definitions

        public class GetEntityType : Cmd
        {
            public GetEntityType(Type type)
            {
                Type = type;
            }

            public Type Type { get; }
        }
        public class EntityType : Doc
        {
            public EntityType(string id, Type type)
            {
                Id = id;
                Type = type;
            }

            public string Id { get; }
            public Type Type { get; }
        }
        public class EntityTypeResult : Evnt
        {
            public EntityTypeResult(EntityType entityType, IActorRef actorRef)
            {
                EntityType = entityType;
                ActorRef = actorRef;
            }

            public EntityType EntityType { get; }
            public IActorRef ActorRef { get; }
        }

        #endregion

        #region Events Definitions

        public class EntityTypeCreated : Evnt
        {
            public EntityTypeCreated(EntityType entityType)
            {
                EntityType = entityType;
            }

            public EntityType EntityType { get; }
        }

        #endregion

        #region State Definition

        public class EntitiesState : PersistentState
        {
            public Dictionary<Type, string> EntityTypes { get; set; } = new Dictionary<Type, string>();
            public Dictionary<string, IActorRef> EntityTypeRefs { get; set; } = new Dictionary<string, IActorRef>();
        }

        #endregion
        
        public EntitiesActor()
        {
            PersistenceId = ConstantsDataAkka.ENTITIES;

            Recover<EntityTypeCreated>(evnt => EntityTypeCreatedEvnt(evnt));

            Command<GetEntityType>(cmd => GetEntityTypeCmd(cmd));
        }
        
        #region Properties

        public override string PersistenceId { get; }

        #endregion

        #region Command Handlers

        private void GetEntityTypeCmd(GetEntityType cmd)
        {
            EntityType entityType;

            if (State.EntityTypes.ContainsKey(cmd.Type))
            {
                entityType = new EntityType(State.EntityTypes[cmd.Type], cmd.Type);
                SendEntityTypeResult(entityType);
            }   
            else
            {
                entityType = new EntityType(Unique.String(), cmd.Type);
                EntityTypeCreated evnt = new EntityTypeCreated(entityType);
                PersistAndTrack(evnt, result =>
                {
                    EntityTypeCreatedEvnt(evnt);
                    SendEntityTypeResult(entityType);
                });
            }
        }

        #endregion

        #region Event Handlers

        private void EntityTypeCreatedEvnt(EntityTypeCreated evnt)
        {
            State.EntityTypes.Add(evnt.EntityType.Type, evnt.EntityType.Id);
        }

        #endregion

        #region Utility Methods

        private void SendEntityTypeResult(EntityType entityType)
        {
            EntityTypeResult result = null;

            if (State.EntityTypeRefs.ContainsKey(entityType.Id))
                result = new EntityTypeResult(entityType, State.EntityTypeRefs[entityType.Id]);
            else
            {
                IActorRef newChild = Context.ActorOf(Props.Create(() => new EntityTypeActor(entityType.Id, entityType.Type)), entityType.Id);
                State.EntityTypeRefs.Add(entityType.Id, newChild);
                result = new EntityTypeResult(entityType, newChild);
            }

            Sender.Tell(result);
        }

        #endregion
    }
}
