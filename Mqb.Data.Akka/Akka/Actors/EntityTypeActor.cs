using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mqb.Akka.Actors
{
    public class EntityTypeActor : AutoSnapshotActor<EntityTypeActor.EntitySetState>
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
        public class Create_InvalidType : Nak { }
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
        public class GetIf_InvalidType : Nak { }
        public class GetIf_NoMatch : Ack { }
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
        public class Update_IdNotFound : Nak { }
        public class Update_IdDeleted : Nak { }
        public class Update_InvalidType : Nak { }
        public class Update_Success : Ack
        {
            public Update_Success(string id)
            {
                Id = id;
            }

            public string Id { get; }
        }
        public class DeleteAll : Cmd { }
        public class DeleteAll_ItemAlreadyDeleted : Nak { }
        public class DeleteAll_ItemSuccess : Ack { }
        public class DeleteAll_Success : Ack { }
        public class DeleteById : Cmd
        {
            public DeleteById(string id)
            {
                Id = id;
            }

            public string Id { get; }
        }
        public class DeleteById_IdNotFound : Nak { }
        public class DeleteById_IdAlreadyDeleted : Nak { }
        public class DeleteById_Success : Ack
        {
            public DeleteById_Success(string id)
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
        public class DeleteIf_InvalidType : Nak { }
        public class DeleteIf_ItemSuccess : Ack
        {
            public DeleteIf_ItemSuccess(string id)
            {
                Id = id;
            }

            public string Id { get; }
        }
        public class DeleteIf_ItemNoMatch : Ack { }
        public class DeleteIf_Success : Ack { }
        public class UndeleteAll : Cmd { }
        public class UndeleteAll_Success : Ack { }
        public class UndeleteById : Cmd
        {
            public UndeleteById(string id)
            {
                Id = id;
            }

            public string Id { get; }
        }
        public class UndeleteById_IdNotFound : Nak { }
        public class UndeleteById_IdNotDeleted : Nak { }
        public class UndeleteById_Success : Ack
        {
            public UndeleteById_Success(string id)
            {
                Id = id;
            }

            public string Id { get; }
        }
        public class UndeleteIf
        {
            public UndeleteIf(Type type, Predicate<object> predicate)
            {
                Type = type;
                Predicate = predicate;
            }

            public Type Type { get; }
            public Predicate<object> Predicate { get; }
        }
        public class UndeleteIf_Success : Ack { }
        public class UndeleteIf_InvalidType : Nak { }
        public class UndeleteIf_ItemSuccess : Ack

        {
            public UndeleteIf_ItemSuccess(string id)
            {
                Id = id;
            }

            public string Id { get; }
        }
        public class UndeleteIf_ItemNoMatch : Ack { }
        
        #endregion

        #region Activation Command Definitions

        public class ActivateAll : Cmd { }
        public class ActivateAll_Success : Ack { }
        public class ActivateId : Cmd
        {
            public ActivateId(string id)
            {
                Id = id;
            }

            public string Id { get; }
        }
        public class ActivateId_IdNotFound : Nak { }
        public class ActivateId_IdAlreadyActive : Nak { }
        public class ActivateId_Success : Ack { }
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
        public class ActivateIf_Success : Ack { }
        public class DeactivateAll : Cmd { }
        public class DeactivateAll_Success : Ack { }
        public class DeactivateId : Cmd
        {
            public DeactivateId(string id)
            {
                Id = id;
            }

            public string Id { get; }
        }
        public class DeactivateId_IdNotFound : Nak { }
        public class DeactivateId_IdAlreadyInactive : Nak { }
        public class DeactivateId_Success : Ack { }
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
        public class DeactivateIf_Success : Ack { }

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
        public class UndeletedId : Evnt
        {
            public UndeletedId(string id)
            {
                Id = id;
            }

            public string Id { get; }
        }
        public class UndeletedIds : Evnt
        {
            public UndeletedIds(IEnumerable<string> ids)
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
            public void UndeleteId(string id)
            {
                IdsDeletedActive.Remove(id);
                IdsDeletedInactive.Remove(id);
                IdsActive.Add(id);
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
        
        public EntityTypeActor(string id, Type type)
        {
            State.Id = id;
            State.Type = type;
            PersistenceId = string.Format("{0}-{1}", ConstantsDataAkka.ENTITY_SET, id);

            // handle events
            Recover<CreatedId>(evnt => CreatedIdEvnt(evnt));
            Recover<CreatedIds>(evnt => CreatedIdsEvnt(evnt));
            Recover<DeletedId>(evnt => DeletedIdEvnt(evnt));
            Recover<DeletedIds>(evnt => DeletedIdsEvnt(evnt));
            Recover<UndeletedId>(evnt => UndeletedIdEvnt(evnt));
            Recover<UndeletedIds>(evnt => UndeletedIdsEvnt(evnt));
            Recover<ActivatedId>(evnt => ActivatedIdEvnt(evnt));
            Recover<ActivatedIds>(evnt => ActivatedIdsEvnt(evnt));
            Recover<DeactivatedId>(evnt => DeactivatedIdEvnt(evnt));
            Recover<DeactivatedIds>(evnt => DeactivatedIdsEvnt(evnt));

            // handle CRUD commands
            Command<Create>(cmd => CreateCmd(cmd));
            //Command<RequestActor.RequestCompleted>(cmd => CreateCompletedCmd(cmd), cmd => cmd.Request.RequestorMessage.GetType() == typeof(Create));
            Command<GetAll>(cmd => GetAllCmd(cmd));
            Command<GetById>(cmd => GetByIdCmd(cmd));
            Command<GetIf>(cmd => GetIfCmd(cmd));
            Command<Update>(cmd => UpdateCmd(cmd));
            Command<DeleteAll>(cmd => DeleteAllCmd(cmd));
            Command<AggregateActor.GetAllCompletedEvnt>(cmd => DeleteAllCompletedCmd(cmd), cmd => cmd.Cmd.OriginalCommand.GetType() == typeof(DeleteAll));
            Command<DeleteById>(cmd => DeleteByIdCmd(cmd));
            Command<DeleteIf>(cmd => DeleteIfCmd(cmd));
            Command<AggregateActor.GetIfCompletedEvnt>(cmd => DeleteIfCompletedCmd(cmd), cmd => cmd.Cmd.OriginalCommand.GetType() == typeof(DeleteIf));
            Command<UndeleteAll>(cmd => UndeleteAllCmd(cmd));
            Command<AggregateActor.GetAllCompletedEvnt>(cmd => UndeleteAllCompletedCmd(cmd), cmd => cmd.Cmd.OriginalCommand.GetType() == typeof(UndeleteAll));
            Command<UndeleteById>(cmd => UndeleteByIdCmd(cmd));
            Command<UndeleteIf>(cmd => UndeleteIfCmd(cmd));
            Command<AggregateActor.GetIfCompletedEvnt>(cmd => UndeleteIfCompletedCmd(cmd), cmd => cmd.Cmd.OriginalCommand.GetType() == typeof(UndeleteIf));

            // handle activation commands
            Command<ActivateAll>(cmd => ActivateAllCmd(cmd));
            Command<ActivateId>(cmd => ActivateIdCmd(cmd));
            Command<ActivateIf>(cmd => ActivateIfCmd(cmd));
            Command<DeactivateAll>(cmd => DeactivateAllCmd(cmd));
            Command<DeactivatedId>(cmd => DeactivatedIdCmd(cmd));
            Command<DeactivateIf>(cmd => DeactivateIfCmd(cmd));
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
                entityActor.Forward(cmd);
            });
        }
        private void GetAllCmd(GetAll cmd)
        {
            // aggregator
            var agg = Context.ActorOf(Props.Create(() => new AggregateActor()), AggregateActor.GetUniqueName());
            // active ids
            agg.Tell(new AggregateActor.GetAll(Sender, cmd, State.IdsActive.Count, State.Type));
            foreach (var activeId in State.IdsActive)
            {
                var child = Context.Child(activeId);
                if (child != ActorRefs.Nobody)
                    agg.Tell(new AggregateActor.GetAllAdd(child, new GetById(activeId)));
                else
                    agg.Tell(new AggregateActor.ReduceTargetCount());
            }
            // inactive ids
            agg.Tell(new AggregateActor.GetAll(Sender, cmd, State.IdsInactive.Count, State.Type));
            foreach (var inactiveId in State.IdsInactive)
            {
                var child = Context.Child(inactiveId);
                if (child == ActorRefs.Nobody)
                    child = CreateEntity(inactiveId);

                // get inactive data and deactivate child once it replies
                agg.Tell(new AggregateActor.GetAllAdd(child, new EntityActor.DeactivateAfter<GetById>(new GetById(inactiveId))));
            }
        }
        private void GetByIdCmd(GetById cmd)
        {
            // verify id valid
            if (!State.IdNotDeleted(cmd.Id))
            {
                if (State.IdDeleted(cmd.Id))
                    Sender.Tell(new GetById_IdDeleted());
                else
                    Sender.Tell(new GetById_IdNotFound());

                return;
            }


            IActorRef childActor = Context.Child(cmd.Id);

            // if inactive, activate
            if (State.IdsInactive.Contains(cmd.Id))
            {
                var activatedId = new ActivatedId(cmd.Id);
                PersistAndTrack(activatedId, result =>
                {
                    ActivatedIdEvnt(result);
                    if (childActor == ActorRefs.Nobody)
                        childActor = CreateEntity(cmd.Id);
                });
            }

            // return model for id
            childActor.Forward(cmd);

            // leave child active
        }
        private void GetIfCmd(GetIf cmd)
        {
            // aggregator
            var agg = Context.ActorOf(Props.Create(() => new AggregateActor()), AggregateActor.GetUniqueName());
            // active ids
            agg.Tell(new AggregateActor.GetIf(Sender, cmd, State.IdsActive.Count, State.Type, typeof(GetIf_NoMatch)));
            foreach (var activeId in State.IdsActive)
            {
                var child = Context.Child(activeId);
                if (child != ActorRefs.Nobody)
                    agg.Tell(new AggregateActor.GetIfAdd(child, cmd));
                else
                    agg.Tell(new AggregateActor.ReduceTargetCount());
            }
            // inactive ids
            agg.Tell(new AggregateActor.GetIf(Sender, cmd, State.IdsInactive.Count, State.Type, typeof(GetIf_NoMatch)));
            foreach (var inactiveId in State.IdsInactive)
            {
                var child = Context.Child(inactiveId);
                if (child == ActorRefs.Nobody)
                    child = CreateEntity(inactiveId);

                // get inactive data and deactivate child once it replies
                agg.Tell(new AggregateActor.GetIfAdd(child, new EntityActor.DeactivateAfter<GetIf>(cmd)));
            }
        }
        private void UpdateCmd(Update cmd)
        {
            // verify id valid
            if (!State.IdNotDeleted(cmd.Id))
            {
                if (State.IdDeleted(cmd.Id))
                    Sender.Tell(new Update_IdDeleted());
                else
                    Sender.Tell(new Update_IdNotFound());

                return;
            }

            IActorRef entityActor = ActorRefs.Nobody;

            if (State.IdsActive.Contains(cmd.Id))
            {
                entityActor = Context.Child(cmd.Id);
                if (entityActor == ActorRefs.Nobody)
                    entityActor = CreateEntity(cmd.Id);
            }

            // if inactive, activate
            if (entityActor == ActorRefs.Nobody && State.IdsInactive.Contains(cmd.Id))
            {
                entityActor = CreateEntity(cmd.Id);
                State.ActivateId(cmd.Id);
            }

            // update data
            entityActor.Forward(cmd);

            // leave active
        }
        private void DeleteAllCmd(DeleteAll cmd)
        {
            // aggregator
            var agg = Context.ActorOf(Props.Create(() => new AggregateActor()), AggregateActor.GetUniqueName());
            // active ids
            agg.Tell(new AggregateActor.GetAll(Sender, cmd, State.IdsActive.Count, typeof(DeleteById_Success), 10, false));
            foreach (var activeId in State.IdsActive)
            {
                var child = Context.Child(activeId);
                if (child != ActorRefs.Nobody)
                    agg.Tell(new AggregateActor.GetAllAdd(child, new DeleteById(activeId)));
                else
                    agg.Tell(new AggregateActor.ReduceTargetCount());
            }
            // inactive ids
            agg.Tell(new AggregateActor.GetAll(Sender, cmd, State.IdsInactive.Count, typeof(DeleteById_Success), 10, false));
            foreach (var inactiveId in State.IdsInactive)
            {
                var child = Context.Child(inactiveId);
                if (child == ActorRefs.Nobody)
                    child = CreateEntity(inactiveId);

                // get inactive data and deactivate child once it replies
                agg.Tell(new AggregateActor.GetAllAdd(child, new EntityActor.DeactivateAfter<DeleteById>(new DeleteById(inactiveId))));
            }
        }
        private void DeleteAllCompletedCmd(AggregateActor.GetAllCompletedEvnt cmd)
        {
            IEnumerable<DeleteById_Success> results = (IEnumerable<DeleteById_Success>)cmd.Results;
            
            var deletedIds = new DeletedIds(results.Select(e => e.Id));
            PersistAndTrack(deletedIds, result =>
            {
                DeletedIdsEvnt(result);
                cmd.Cmd.Requestor.Tell(new DeleteAll_Success());
            });
        }
        private void DeleteByIdCmd(DeleteById cmd)
        {
            // verify id valid
            if (!State.IdNotDeleted(cmd.Id))
            {
                if (State.IdDeleted(cmd.Id))
                    Sender.Tell(new DeleteById_IdAlreadyDeleted());
                else
                    Sender.Tell(new DeleteById_IdNotFound());

                return;
            }

            IActorRef entityActor = ActorRefs.Nobody;

            if (State.IdsActive.Contains(cmd.Id))
            {
                entityActor = Context.Child(cmd.Id);
                if (entityActor == ActorRefs.Nobody)
                    entityActor = CreateEntity(cmd.Id);
            }

            // if inactive, activate
            if (entityActor == ActorRefs.Nobody && State.IdsInactive.Contains(cmd.Id))
            {
                entityActor = CreateEntity(cmd.Id);
                State.ActivateId(cmd.Id);
            }

            // delete record
            var deletedId = new DeletedId(cmd.Id);
            PersistAndTrack(deletedId, result =>
            {
                DeletedIdEvnt(result);
                entityActor.Forward(cmd);
            });
        }
        private void DeleteIfCmd(DeleteIf cmd)
        {
            // aggregator
            var agg = Context.ActorOf(Props.Create(() => new AggregateActor()), AggregateActor.GetUniqueName());
            // active ids
            agg.Tell(new AggregateActor.GetIf(Sender, cmd, State.IdsActive.Count, typeof(DeleteIf_ItemSuccess), typeof(DeleteIf_ItemNoMatch), 10, false));
            foreach (var activeId in State.IdsActive)
            {
                var child = Context.Child(activeId);
                if (child != ActorRefs.Nobody)
                    agg.Tell(new AggregateActor.GetIfAdd(child, cmd));
                else
                    agg.Tell(new AggregateActor.ReduceTargetCount());
            }
            // inactive ids
            agg.Tell(new AggregateActor.GetIf(Sender, cmd, State.IdsInactive.Count, typeof(DeleteIf_ItemSuccess), typeof(DeleteIf_ItemNoMatch), 10, false));
            foreach (var inactiveId in State.IdsInactive)
            {
                var child = Context.Child(inactiveId);
                if (child == ActorRefs.Nobody)
                    child = CreateEntity(inactiveId);

                // get inactive data and deactivate child once it replies
                agg.Tell(new AggregateActor.GetIfAdd(child, new EntityActor.DeactivateAfter<DeleteIf>(cmd)));
            }
        }
        private void DeleteIfCompletedCmd(AggregateActor.GetIfCompletedEvnt cmd)
        {
            IEnumerable<DeleteIf_ItemSuccess> results = (IEnumerable<DeleteIf_ItemSuccess>)cmd.Results;

            var deletedIds = new DeletedIds(results.Select(e => e.Id));
            PersistAndTrack(deletedIds, result =>
            {
                DeletedIdsEvnt(result);
                cmd.Cmd.Requestor.Tell(new DeleteIf_Success());
            });
        }
        private void UndeleteAllCmd(UndeleteAll cmd)
        {
            // aggregator
            var agg = Context.ActorOf(Props.Create(() => new AggregateActor()), AggregateActor.GetUniqueName());
            // active ids
            agg.Tell(new AggregateActor.GetAll(Sender, cmd, State.IdsDeletedActive.Count, typeof(UndeleteById_Success), 10, false));
            foreach (var activeId in State.IdsDeletedActive)
            {
                var child = Context.Child(activeId);
                if (child != ActorRefs.Nobody)
                    agg.Tell(new AggregateActor.GetAllAdd(child, new UndeleteById(activeId)));
                else
                    agg.Tell(new AggregateActor.ReduceTargetCount());
            }
            // inactive ids
            agg.Tell(new AggregateActor.GetAll(Sender, cmd, State.IdsDeletedInactive.Count, typeof(UndeleteById_Success), 10, false));
            foreach (var inactiveId in State.IdsDeletedInactive)
            {
                var child = Context.Child(inactiveId);
                if (child == ActorRefs.Nobody)
                    child = CreateEntity(inactiveId);

                // get inactive data and deactivate child once it replies
                agg.Tell(new AggregateActor.GetAllAdd(child, new EntityActor.DeactivateAfter<UndeleteById>(new UndeleteById(inactiveId))));
            }
        }
        private void UndeleteAllCompletedCmd(AggregateActor.GetAllCompletedEvnt cmd)
        {
            IEnumerable<UndeleteById_Success> results = (IEnumerable<UndeleteById_Success>)cmd.Results;

            var deletedIds = new DeletedIds(results.Select(e => e.Id));
            PersistAndTrack(deletedIds, result =>
            {
                DeletedIdsEvnt(result);
                cmd.Cmd.Requestor.Tell(new UndeleteAll_Success());
            });
        }
        private void UndeleteByIdCmd(UndeleteById cmd)
        {
            // verify id valid
            if (!State.IdDeleted(cmd.Id))
            {
                if (State.IdNotDeleted(cmd.Id))
                    Sender.Tell(new UndeleteById_IdNotDeleted());
                else
                    Sender.Tell(new UndeleteById_IdNotFound());

                return;
            }

            IActorRef entityActor = ActorRefs.Nobody;

            if (State.IdsDeletedActive.Contains(cmd.Id))
            {
                entityActor = Context.Child(cmd.Id);
                if (entityActor == ActorRefs.Nobody)
                    entityActor = CreateEntity(cmd.Id);
            }

            // if inactive, activate
            if (entityActor == ActorRefs.Nobody && State.IdsDeletedInactive.Contains(cmd.Id))
            {
                entityActor = CreateEntity(cmd.Id);
                var activatedId = new ActivatedId(cmd.Id);
                PersistAndTrack(activatedId, result =>
                {
                    ActivatedIdEvnt(result);
                });
            }

            // undelete record
            var undeletedId = new UndeletedId(cmd.Id);
            PersistAndTrack(undeletedId, result =>
            {
                UndeletedIdEvnt(result);
                entityActor.Forward(cmd);
            });
        }
        private void UndeleteIfCmd(UndeleteIf cmd)
        {
            // aggregator
            var agg = Context.ActorOf(Props.Create(() => new AggregateActor()), AggregateActor.GetUniqueName());
            // active ids
            agg.Tell(new AggregateActor.GetIf(Sender, cmd, State.IdsDeletedActive.Count, typeof(UndeleteIf_ItemSuccess), typeof(UndeleteIf_ItemNoMatch), 10, false));
            foreach (var activeId in State.IdsDeletedActive)
            {
                var child = Context.Child(activeId);
                if (child != ActorRefs.Nobody)
                    agg.Tell(new AggregateActor.GetIfAdd(child, cmd));
                else
                    agg.Tell(new AggregateActor.ReduceTargetCount());
            }
            // inactive ids
            agg.Tell(new AggregateActor.GetIf(Sender, cmd, State.IdsDeletedInactive.Count, typeof(UndeleteIf_ItemSuccess), typeof(UndeleteIf_ItemNoMatch), 10, false));
            foreach (var inactiveId in State.IdsDeletedInactive)
            {
                var child = Context.Child(inactiveId);
                if (child == ActorRefs.Nobody)
                    child = CreateEntity(inactiveId);

                // get inactive data and deactivate child once it replies
                agg.Tell(new AggregateActor.GetIfAdd(child, new EntityActor.DeactivateAfter<UndeleteIf>(cmd)));
            }
        }
        private void UndeleteIfCompletedCmd(AggregateActor.GetIfCompletedEvnt cmd)
        {
            IEnumerable<UndeleteIf_ItemSuccess> results = (IEnumerable<UndeleteIf_ItemSuccess>)cmd.Results;

            var deletedIds = new DeletedIds(results.Select(e => e.Id));
            PersistAndTrack(deletedIds, result =>
            {
                DeletedIdsEvnt(result);
                cmd.Cmd.Requestor.Tell(new UndeleteIf_Success());
            });
        }

        #endregion

        #region Activation Command Handlers

        private void ActivateAllCmd(ActivateAll cmd)
        {
            // activate all ids that aren't active
            var activatedIds = new ActivatedIds(State.IdsInactive);
            PersistAndTrack(activatedIds, result =>
            {
                ActivatedIdsEvnt(result);
                foreach (var id in result.Ids)
                    CreateEntity(id);
                Sender.Tell(new ActivateAll_Success());
            });
        }
        private void ActivateIdCmd(ActivateId cmd)
        {
            // ensure id exists
            if (!State.IdExists(cmd.Id))
            {
                Sender.Tell(new ActivateId_IdNotFound());
                return;
            }

            // ensure id inactive
            if (State.IdsActive.Contains(cmd.Id) ||
                State.IdsDeletedActive.Contains(cmd.Id))
            {
                Sender.Tell(new ActivateId_IdAlreadyActive());
                return;
            }

            // activate id
            var activatedId = new ActivatedId(cmd.Id);
            PersistAndTrack(activatedId, result =>
            {
                ActivatedIdEvnt(result);
                Sender.Tell(new ActivateId_Success());
            });
        }
        private void ActivateIfCmd(ActivateIf cmd)
        {
            // todo: search inactive ids and activate if the match
            // todo: deactivate any ids that didn't match the search
        }
        private void DeactivateAllCmd(DeactivateAll cmd)
        {
            // deactivate any ids that are active
            var deactivatedIds = new DeactivatedIds(State.IdsActive);
            PersistAndTrack(deactivatedIds, result =>
            {
                DeactivatedIdsEvnt(result);
                foreach (var id in result.Ids)
                {
                    var child = Context.Child(id);
                    if (child != ActorRefs.Nobody)
                        child.Tell(new EntityActor.Deactivate());
                }
                Sender.Tell(new DeactivateAll_Success());
            });
        }
        private void DeactivatedIdCmd(DeactivatedId cmd)
        {
            // ensure id exists
            if (!State.IdExists(cmd.Id))
            {
                Sender.Tell(new DeactivateId_IdNotFound());
                return;
            }

            // ensure id active
            if (State.IdsInactive.Contains(cmd.Id) ||
                State.IdsDeletedInactive.Contains(cmd.Id))
            {
                Sender.Tell(new DeactivateId_IdAlreadyInactive());
                return;
            }

            // deactivate id
            var deactivatedId = new DeactivatedId(cmd.Id);
            PersistAndTrack(deactivatedId, result =>
            {
                DeactivatedIdEvnt(result);
                Sender.Tell(new DeactivateId_Success());
            });
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
        private void UndeletedIdsEvnt(UndeletedIds evnt)
        {
            foreach (var id in evnt.Ids)
                State.UndeleteId(id);
        }
        private void UndeletedIdEvnt(UndeletedId evnt)
        {
            State.UndeleteId(evnt.Id);
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
        private void DeactivatedIdEvnt(DeactivatedId evnt)
        {
            State.DeactivateId(evnt.Id);
        }
        private void DeactivatedIdsEvnt(DeactivatedIds evnt)
        {
            foreach (var id in evnt.Ids)
                State.DeactivateId(id);
        }

        #endregion

        #region Utility Methods

        private IActorRef CreateEntity(string id)
        {
            return Context.ActorOf(Props.Create(() => new EntityActor(id, State.Type)), id);
        }

        #endregion
    }
}
