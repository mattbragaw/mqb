using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Akka.Actors
{
    public class EntityActor : AutoSnapshotActor<EntityActor.EntityState>
    {
        #region Commmand Definitions

        public class Deactivate : Cmd { }
        public class DeactivateAfter<T> : Cmd
        {
            public DeactivateAfter(T cmd)
            {
                Cmd = cmd;
            }

            public T Cmd { get; }
        }

        #endregion

        #region Event Definitions

        public class Created : Evnt
        {
            public Created(object model)
            {
                Model = model;
            }

            public object Model { get; }
        }
        public class Updated :Evnt
        {
            public Updated(object model)
            {
                Model = model;
            }

            public object Model { get; }
        }
        public class Deleted : Evnt { }
        public class Undeleted : Evnt { }

        #endregion

        #region State Definition

        public class EntityState : PersistentState
        {
            public string Id { get; set; }
            public Type Type { get; set; }
            public TimeSpan Timeout { get; set; } = TimeSpan.FromMinutes(5);
            public bool Deleted { get; set; }
            public object Model { get; set; }
        }

        #endregion

        public EntityActor(string id, Type type) : this(id, type, null) { }
        public EntityActor(string id, Type type, TimeSpan? receiveTimeout)
        {
            State.Id = id;
            State.Type = type;
            if (receiveTimeout.HasValue)
                State.Timeout = receiveTimeout.Value;   // default is 5 minutes

            PersistenceId = string.Format("{0}-{1}", ConstantsDataAkka.ENTITY, id);

            // handle events
            Recover<Created>(evnt => CreatedEvnt(evnt));
            Recover<Updated>(evnt => UpdatedEvnt(evnt));
            Recover<Deleted>(evnt => DeletedEvnt(evnt));
            Recover<Undeleted>(evnt => UndeletedEvnt(evnt));

            // handle commands
            Command<EntityTypeActor.Create>(cmd => CreateCmd(cmd));
            Command<EntityTypeActor.GetById>(cmd => GetByIdCmd(cmd));
            Command<DeactivateAfter<EntityTypeActor.GetById>>(cmd => GetByIdCmd(cmd.Cmd, true));
            Command<EntityTypeActor.GetIf>(cmd => GetIfCmd(cmd));
            Command<DeactivateAfter<EntityTypeActor.GetIf>>(cmd => GetIfCmd(cmd.Cmd, true));
            Command<EntityTypeActor.Update>(cmd => UpdateCmd(cmd));
            Command<EntityTypeActor.DeleteById>(cmd => DeleteAllCmd(cmd));
            Command<EntityTypeActor.DeleteIf>(cmd => DeleteIfCmd(cmd));
            Command<DeactivateAfter<EntityTypeActor.DeleteIf>>(cmd => DeleteIfCmd(cmd.Cmd, true));
            Command<EntityTypeActor.UndeleteById>(cmd => UndeleteByIdCmd(cmd));
            Command<EntityTypeActor.UndeleteIf>(cmd => UndeleteIfCmd(cmd));
            Command<DeactivateAfter<EntityTypeActor.UndeleteIf>>(cmd => UndeleteIfCmd(cmd.Cmd, true));
            Command<Deactivate>(cmd => DeactivateCmd(cmd));

            // set and handle receive timeout
            Command<ReceiveTimeout>(cmd => ReceiveTimeoutCmd(cmd));
            SetReceiveTimeout(State.Timeout);
        }

        #region Properties

        public override string PersistenceId { get; }

        #endregion

        #region Command Handlers

        private void CreateCmd(EntityTypeActor.Create cmd)
        {
            if (cmd.Model == null || cmd.Model.GetType() != State.Type)
            {
                Sender.Tell(new EntityTypeActor.Create_InvalidType(), Context.Parent);
                return;
            }

            // record event
            var created = new Created(cmd.Model);
            PersistAndTrack(created, result =>
            {
                CreatedEvnt(result);
                Sender.Tell(new EntityTypeActor.Create_Success(State.Id), Context.Parent);
            });
        }
        private void GetByIdCmd(EntityTypeActor.GetById cmd, bool deactivateAfter = false)
        {
            Sender.Tell(State.Model, Context.Parent);

            if (deactivateAfter)
                Context.Stop(Self);
        }

        private void GetIfCmd(EntityTypeActor.GetIf cmd, bool deactivateAfter = false)
        {
            if (cmd.Type == null || cmd.Type != State.Type)
            {
                Sender.Tell(new EntityTypeActor.GetIf_InvalidType(), Context.Parent);
                return;
            }

            if (cmd.Predicate(State.Model))
                Sender.Tell(State.Model, Context.Parent);
            else
                Sender.Tell(new EntityTypeActor.GetIf_NoMatch(), Context.Parent);

            if (deactivateAfter)
                Context.Stop(Self);
        }

        private void UpdateCmd(EntityTypeActor.Update cmd)
        {
            if (cmd.Model == null || cmd.Model.GetType() != State.Type)
            {
                Sender.Tell(new EntityTypeActor.Update_InvalidType(), Context.Parent);
                return;
            }

            // record event
            var updated = new Updated(cmd.Model);
            PersistAndTrack(updated, result =>
            {
                UpdatedEvnt(result);
                Sender.Tell(new EntityTypeActor.Update_Success(State.Id), Context.Parent);
            });
        }

        private void DeleteAllCmd(EntityTypeActor.DeleteById cmd)
        {
            if (!State.Deleted)
            {
                // record event
                var deleted = new Deleted();
                PersistAndTrack(deleted, result =>
                {
                    DeletedEvnt(result);
                    Sender.Tell(new EntityTypeActor.DeleteById_Success(cmd.Id), Context.Parent);
                });
            }
            else
                Sender.Tell(new EntityTypeActor.DeleteById_IdAlreadyDeleted(), Context.Parent);
        }

        private void DeleteIfCmd(EntityTypeActor.DeleteIf cmd, bool deactivateAfter = false)
        {
            if (cmd.Type == null || cmd.Type != State.Type)
            {
                Sender.Tell(new EntityTypeActor.DeleteIf_InvalidType(), Context.Parent);
                return;
            }

            if (cmd.Predicate == null || cmd.Predicate(State.Model))
            {
                // record event
                var deleted = new Deleted();
                PersistAndTrack(deleted, result =>
                {
                    DeletedEvnt(result);
                    Sender.Tell(new EntityTypeActor.DeleteIf_ItemSuccess(State.Id), Context.Parent);
                });
            }
            else
                Sender.Tell(new EntityTypeActor.DeleteIf_ItemNoMatch(), Context.Parent);

            if (deactivateAfter)
                Context.Stop(Self);
        }

        private void UndeleteByIdCmd(EntityTypeActor.UndeleteById cmd)
        {
            if (State.Deleted)
            {
                // record event
                var undeleted = new Undeleted();
                PersistAndTrack(undeleted, result =>
                {
                    UndeletedEvnt(result);
                    Sender.Tell(new EntityTypeActor.UndeleteById_Success(State.Id), Context.Parent);
                });
            }
            else
                Sender.Tell(new EntityTypeActor.UndeleteById_IdNotDeleted(), Context.Parent);
        }

        private void UndeleteIfCmd(EntityTypeActor.UndeleteIf cmd, bool deactivateAfter = false)
        {
            if (cmd.Type == null || cmd.Type != State.Type)
            {
                Sender.Tell(new EntityTypeActor.UndeleteIf_InvalidType(), Context.Parent);
                return;
            }

            if (cmd.Predicate == null || cmd.Predicate(State.Model))
            {
                // record event
                var undeleted = new Undeleted();
                PersistAndTrack(undeleted, result =>
                {
                    UndeletedEvnt(result);
                    Sender.Tell(new EntityTypeActor.UndeleteIf_ItemSuccess(State.Id), Context.Parent);
                });
            }
            else
                Sender.Tell(new EntityTypeActor.UndeleteIf_ItemNoMatch(), Context.Parent);

            if (deactivateAfter)
                Context.Stop(Self);
        }
        private void DeactivateCmd(Deactivate cmd)
        {
            Context.Stop(Self);
        }
        private void ReceiveTimeoutCmd(ReceiveTimeout cmd)
        {
            Context.Parent.Tell(new EntityTypeActor.DeactivatedId(State.Id));
        }

        #endregion

        #region Event Handlers

        private void CreatedEvnt(Created evnt)
        {
            State.Model = evnt.Model;
        }
        private void UpdatedEvnt(Updated evnt)
        {
            State.Model = evnt.Model;   
        }
        private void DeletedEvnt(Deleted evnt)
        {
            State.Deleted = true;
        }
        private void UndeletedEvnt(Undeleted evnt)
        {
            State.Deleted = false;
        }

        #endregion
    }
}
