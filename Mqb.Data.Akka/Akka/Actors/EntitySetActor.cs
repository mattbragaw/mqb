using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Akka.Actors
{
    public class EntitySetActor : AutoSnapshotActor<EntitySetActor.EntitySetState>
    {
        #region CRUD Command Definitions

        public class Create : Cmd
        {
            public Create(string id, object model)
            {
                Id = id;
                Model = model;
            }

            public string Id { get; }
            public object Model { get; }
        }
        public class Create_IdExists : Nak { }
        public class Create_Success : Ack
        {
            public Create_Success(string id)
            {
                Id = id;
            }

            public string Id { get; }
        }
        public class GetAll : Cmd { }
        public class GetById : Cmd
        {
            public GetById(string id)
            {
                Id = id;
            }

            public string Id { get; }
        }
        public class GetById_IdNotFound : Nak { }
        public class GetById_IdDeleted : Nak { }
        public class GetIf : Cmd
        {
            public GetIf(Type type, Predicate<object> predicate)
            {
                Type = type;
                Predicate = predicate;
            }

            public Type Type { get; }
            public Predicate<object> Predicate { get; }
        }
        public class Update : Cmd
        {
            public Update(string id, object model)
            {
                Id = id;
                Model = model;
            }

            public string Id { get; }
            public object Model { get; }
        }
        public class DeleteAll : Cmd { }
        public class DeleteById : Cmd
        {
            public DeleteById(string id)
            {
                Id = id;
            }

            public string Id { get; }
        }
        public class DeleteIf
        {
            public DeleteIf(Type type, Predicate<object> predicate)
            {
                Type = type;
                Predicate = predicate;
            }

            public Type Type { get; }
            public Predicate<object> Predicate { get; }
        }

        #endregion

        #region Activation Command Definitions

        public class ActivateAll : Cmd { }
        public class ActivateId : Cmd
        {
            public ActivateId(string id)
            {
                Id = id;
            }

            public string Id { get; }
        }
        public class ActivateIf : Cmd
        {
            public ActivateIf(Type type, Predicate<object> predicate)
            {
                Type = type;
                Predicate = predicate;
            }

            public Type Type { get; }
            public Predicate<object> Predicate { get; }
        }
        public class DeactivateAll : Cmd { }
        public class DeactivateId : Cmd
        {
            public DeactivateId(string id)
            {
                Id = id;
            }

            public string Id { get; }
        }
        public class DeactivateIf : Cmd
        {
            public DeactivateIf(Type type, Predicate<object> predicate)
            {
                Type = type;
                Predicate = predicate;
            }

            public Type Type { get; }
            public Predicate<object> Predicate { get; }
        }

        #endregion

        #region CRUD Event Definitions

        public class CreatedId : Evnt
        {
            public CreatedId(string id)
            {
                Id = id;
            }

            public string Id { get; }
        }
        public class CreatedIds : Evnt
        {
            public CreatedIds(IEnumerable<string> ids)
            {
                Ids = ids;
            }

            public IEnumerable<string> Ids { get; }
        }
        public class DeletedId : Evnt
        {
            public DeletedId(string id)
            {
                Id = id;
            }

            public string Id { get; }
        }
        public class DeletedIds : Evnt
        {
            public DeletedIds(IEnumerable<string> ids)
            {
                Ids = ids;
            }

            public IEnumerable<string> Ids { get; }
        }

        #endregion

        #region Activation Event Definitions

        public class ActivatedId : Evnt
        {
            public ActivatedId(string id)
            {
                Id = id;
            }

            public string Id { get; }
        }
        public class ActivatedIds : Evnt
        {
            public ActivatedIds(IEnumerable<string> ids)
            {
                Ids = ids;
            }

            public IEnumerable<string> Ids { get; }
        }
        public class DeactivatedId : Evnt
        {
            public DeactivatedId(string id)
            {
                Id = id;
            }

            public string Id { get; }
        }
        public class DeactivatedIds : Evnt
        {
            public DeactivatedIds(IEnumerable<string> ids)
            {
                Ids = ids;
            }

            public IEnumerable<string> Ids { get; }
        }

        #endregion

        #region State Definition

        public class EntitySetState : PersistentState
        {
            public string Id { get; set; }
            public Type Type { get; set; }
            public HashSet<string> IdsActive { get; set; } = new HashSet<string>();
            public HashSet<string> IdsInactive { get; set; } = new HashSet<string>();
            public HashSet<string> IdsDeletedActive { get; set; } = new HashSet<string>();
            public HashSet<string> IdsDeletedInactive { get; set; } = new HashSet<string>();

            // methods
            public bool IdExists(string id) => !IdUnique(id);
            public bool IdUnique(string id)
            {
                return 
                    !IdsActive.Contains(id) &&
                    !IdsInactive.Contains(id) &&
                    !IdsDeletedActive.Contains(id) &&
                    !IdsDeletedInactive.Contains(id);
            }
            public bool IdDeleted(string id)
            {
                return
                    IdsDeletedActive.Contains(id) ||
                    IdsDeletedInactive.Contains(id);
            }
            public bool IdNotDeleted(string id)
            {
                return
                    IdsActive.Contains(id) ||
                    IdsInactive.Contains(id);
            }
            public void DeleteId(string id)
            {
                IdsActive.Remove(id);
                IdsInactive.Remove(id);
                IdsDeletedInactive.Add(id);
            }

            public void ActivateId(string id)
            {
                if (IdsInactive.Contains(id))
                {
                    IdsInactive.Remove(id);
                    IdsActive.Add(id);
                }
                else if (IdsDeletedInactive.Contains(id))
                {
                    IdsDeletedInactive.Remove(id);
                    IdsDeletedActive.Add(id);
                }
            }
            public void DeactivateId(string id)
            {
                if (IdsActive.Contains(id))
                {
                    IdsActive.Remove(id);
                    IdsInactive.Add(id);
                }
                else if (IdsDeletedActive.Contains(id))
                {
                    IdsDeletedActive.Remove(id);
                    IdsDeletedInactive.Add(id);
                }
            }
        }

        #endregion
        
        public EntitySetActor(string id, Type type)
        {
            State.Id = id;
            State.Type = type;
            PersistenceId = string.Format("{0}-{1}", ConstantsDataAkka.ENTITY_SET, id);

            // handle events
            Recover<CreatedId>(evnt => CreatedIdEvnt(evnt));
            Recover<CreatedIds>(evnt => CreatedIdsEvnt(evnt));
            Recover<DeletedId>(evnt => DeletedIdEvnt(evnt));
            Recover<DeletedIds>(evnt => DeletedIdsEvnt(evnt));
            Recover<ActivatedId>(evnt => ActivatedIdEvnt(evnt));
            Recover<ActivatedIds>(evnt => ActivatedIdsEvnt(evnt));
            Recover<DeactivatedId>(evnt => DeactivateIdEvnt(evnt));
            Recover<DeactivatedIds>(evnt => DeactivateIdsEvnt(evnt));

            // handle CRUD commands
            Command<Create>(cmd => CreateCmd(cmd));
            Command<RequestActor.RequestCompleted>(cmd => CreateCompletedCmd(cmd), cmd => cmd.Request.RequestorMessage.GetType() == typeof(Create));
            Command<GetAll>(cmd => GetAllCmd(cmd));
            Command<GetById>(cmd => GetByIdCmd(cmd));
            Command<GetIf>(cmd => GetIfCmd(cmd));
            Command<Update>(cmd => UpdateCmd(cmd));
            Command<DeleteAll>(cmd => DeleteAllCmd(cmd));
            Command<DeleteById>(cmd => DeleteByIdCmd(cmd));
            Command<DeleteIf>(cmd => DeleteIfCmd(cmd));

            // handle activation commands
            Command<ActivateAll>(cmd => ActivateAllCmd(cmd));
            Command<ActivatedId>(cmd => ActivateIdCmd(cmd));
            Command<ActivateIf>(cmd => ActivateIfCmd(cmd));
            Command<DeactivateAll>(cmd => DeactivateAllCmd(cmd));
            Command<DeactivatedId>(cmd => DeactivatedIdCmd(cmd));
            Command<DeactivateIf>(cmd => DeactivateIfCmd(cmd));
        }

        private void CreateCompletedCmd(RequestActor.RequestCompleted cmd)
        {
            Create originalCmd = (Create)cmd.Request.RequestorMessage;
            if (cmd.Result is Create_Success)
                cmd.Request.Requestor.Tell(new Create_Success(originalCmd.Id));
        }

        #region Properties

        public override string PersistenceId { get; }

        #endregion

        #region CRUD Command Handlers

        private void CreateCmd(Create cmd)
        {
            // ensure id doesn't exist
            if (State.IdExists(cmd.Id))
            {
                Sender.Tell(new Create_IdExists());
                return;
            }

            // create record
            var createdId = new CreatedId(cmd.Id);
            PersistAndTrack(createdId, result =>
            {
                CreatedIdEvnt(result);
                IActorRef entityActor = Context.ActorOf(Props.Create(() => new EntityActor(cmd.Id, State.Type)), cmd.Id);
                IActorRef requestActor = Context.ActorOf(Props.Create(() => new RequestActor()), RequestActor.GetUniqueName());
                requestActor.Tell(new RequestActor.Request(Sender, cmd, entityActor, new EntityActor.Create(cmd.Model)));
            });
        }
        private void GetAllCmd(GetAll cmd)
        {
            // todo: master aggregator
            var masterAgg = Context.ActorOf(Props.Create(() => new AggregateActor()), AggregateActor.GetUniqueName());

            // todo: get all active records
            var activeAgg = Context.ActorOf(Props.Create(() => new AggregateActor()), AggregateActor.GetUniqueName());

            // todo: get all inactive records
            var inactiveAgg = Context.ActorOf(Props.Create(() => new AggregateActor()), AggregateActor.GetUniqueName());
            
            // todo: deactivate activated inactive records - ideally as requested
        }
        private void GetByIdCmd(GetById cmd)
        {
            // verify id valid
            if (!State.IdNotDeleted(cmd.Id))
            {
                if (State.IdDeleted(cmd.Id))
                {
                    Sender.Tell(new GetById_IdDeleted());
                }
                else
                {
                    Sender.Tell(new GetById_IdNotFound());
                }
            }

            // if inactive, activate
            if (State.IdsInactive.Contains(cmd.Id))
            {
                var childActor = Context.ActorOf(Props.Create(() => new EntityActor(cmd.Id, State.Type)), cmd.Id);
                State.ActivateId(cmd.Id);
            }

            // return model for id
            Context.Child(cmd.Id).Tell(new EntityActor.Get(), Sender);

            // leave child active
        }
        private void GetIfCmd(GetIf cmd)
        {
            // todo: search active records
            // todo: search inactive records
            // todo: deactivate activated inactive records
        }
        private void UpdateCmd(Update cmd)
        {
            // todo: ensure id exists
            // todo: activate if needed
            // todo: update data
            // todo: leave active
        }

        private void DeleteAllCmd(DeleteAll cmd)
        {
            // todo: delete all active records
            // todo: delete all inactive records
        }

        private void DeleteByIdCmd(DeleteById cmd)
        {
            // todo: ensure id exists
            // todo: activate if needed
            // todo: delete id
        }

        private void DeleteIfCmd(DeleteIf cmd)
        {
            // todo: search all active records deleting if found
            // todo: search all inactive records deleting if found
            // todo: deactive inactive records that were activated, but not deleted
        }

        #endregion

        #region Activation Command Handlers

        private void ActivateAllCmd(ActivateAll cmd)
        {
            // todo: activate all ids that aren't active
        }

        private void ActivateIdCmd(ActivatedId cmd)
        {
            // todo: ensure id is valid
            // todo: if id isn't active, activate it
        }

        private void ActivateIfCmd(ActivateIf cmd)
        {
            // todo: search inactive ids and activate if the match
            // todo: deactivate any ids that didn't match the search
        }

        private void DeactivateAllCmd(DeactivateAll cmd)
        {
            // todo: deactivate any ids that are active
        }

        private void DeactivatedIdCmd(DeactivatedId cmd)
        {
            // todo: ensure id exists
            // todo: deactivate id
        }

        private void DeactivateIfCmd(DeactivateIf cmd)
        {
            // todo: search active ids and deactivate if they match the search
        }

        #endregion

        #region Event Handlers

        private void CreatedIdEvnt(CreatedId evnt)
        {
            State.IdsActive.Add(evnt.Id);
        }
        private void CreatedIdsEvnt(CreatedIds evnt)
        {
            foreach (var id in evnt.Ids)
                State.IdsActive.Add(id);
        }
        private void DeletedIdEvnt(DeletedId evnt)
        {
            State.DeleteId(evnt.Id);
        }
        private void DeletedIdsEvnt(DeletedIds evnt)
        {
            foreach (var id in evnt.Ids)
                State.DeleteId(id);
        }
        private void ActivatedIdEvnt(ActivatedId evnt)
        {
            State.ActivateId(evnt.Id);
        }
        private void ActivatedIdsEvnt(ActivatedIds evnt)
        {
            foreach (var id in evnt.Ids)
                State.ActivateId(id);
        }
        private void DeactivateIdEvnt(DeactivatedId evnt)
        {
            State.DeactivateId(evnt.Id);
        }
        private void DeactivateIdsEvnt(DeactivatedIds evnt)
        {
            foreach (var id in evnt.Ids)
                State.DeactivateId(id);
        }

        #endregion

        #region Utility Methods



        #endregion
    }
}
