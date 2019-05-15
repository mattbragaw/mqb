using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Akka.Actors
{
    public class EntityActor : AutoSnapshotActor<EntityActor.EntityState>
    {
        #region Commmand Definitions

        public class Create : Cmd
        {
            public Create(object model)
            {
                Model = model;
            }

            public object Model { get; }
        }
        public class Create_InvalidType : Nak { }
        public class Create_Success : Ack
        {
            public Create_Success(string id)
            {
                Id = id;
            }

            public string Id { get; }
        }
        public class Get : Cmd { }
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
            public Update(object model)
            {
                Model = model;
            }

            public object Model { get; }
        }
        public class Update_InvalidType : Nak { }
        public class Update_Success : Ack
        {
            public Update_Success(string id)
            {
                Id = id;
            }

            public string Id { get; }
        }
        public class Delete : Cmd { }
        public class Delete_AlreadyDeleted : Nak { }
        public class Delete_Success : Ack
        {
            public Delete_Success(string id)
            {
                Id = id;
            }

            public string Id { get; }
        }
        public class DeleteIf : Cmd
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
        public class DeleteIf_NoMatch : Ack { }
        public class DeleteIf_Success : Ack
        {
            public DeleteIf_Success(string id)
            {
                Id = id;
            }

            public string Id { get; }
        }
        public class Undelete : Cmd { }
        public class Undelete_NotDeleted : Nak { }
        public class Undelete_Success : Ack
        {
            public Undelete_Success(string id)
            {
                Id = id;
            }

            public string Id { get; }
        }
        public class UndeleteIf : Cmd
        {
            public UndeleteIf(Type type, Predicate<object> predicate)
            {
                Type = type;
                Predicate = predicate;
            }

            public Type Type { get; }
            public Predicate<object> Predicate { get; }
        }
        public class UndeleteIf_InvalidType : Nak { }
        public class UndeleteIf_NoMatch : Ack { }
        public class UndeleteIf_Success : Ack
        {
            public UndeleteIf_Success(string id)
            {
                Id = id;
            }

            public string Id { get; }
        }
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
            Command<Create>(cmd => CreateCmd(cmd));
            Command<Get>(cmd => GetCmd(cmd));
            Command<DeactivateAfter<Get>>(cmd => GetCmd(cmd.Cmd, true));
            Command<GetIf>(cmd => GetIfCmd(cmd));
            Command<DeactivateAfter<GetIf>>(cmd => GetIfCmd(cmd.Cmd, true));
            Command<Update>(cmd => UpdateCmd(cmd));
            Command<Delete>(cmd => DeleteCmd(cmd));
            Command<DeleteIf>(cmd => DeleteIfCmd(cmd));
            Command<DeactivateAfter<DeleteIf>>(cmd => DeleteIfCmd(cmd.Cmd, true));
            Command<Undelete>(cmd => UndeleteCmd(cmd));
            Command<UndeleteIf>(cmd => UndeleteIfCmd(cmd));
            Command<DeactivateAfter<UndeleteIf>>(cmd => UndeleteIfCmd(cmd.Cmd, true));
            Command<Deactivate>(cmd => DeactivateCmd(cmd));

            // set and handle receive timeout
            Command<ReceiveTimeout>(cmd => ReceiveTimeoutCmd(cmd));
            SetReceiveTimeout(State.Timeout);
        }

        #region Properties

        public override string PersistenceId { get; }

        #endregion

        #region Command Handlers

        private void CreateCmd(Create cmd)
        {
            if (cmd.Model == null || cmd.Model.GetType() != State.Type)
            {
                Sender.Tell(new Create_InvalidType());
                return;
            }

            // record event
            var created = new Created(cmd.Model);
            PersistAndTrack(created, result =>
            {
                CreatedEvnt(result);
                Sender.Tell(new Create_Success(State.Id));
            });
        }
        private void GetCmd(Get cmd, bool deactivateAfter = false)
        {
            Sender.Tell(State.Model);

            if (deactivateAfter)
                Context.Stop(Self);
        }

        private void GetIfCmd(GetIf cmd, bool deactivateAfter = false)
        {
            if (cmd.Type == null || cmd.Type != State.Type)
            {
                Sender.Tell(new GetIf_InvalidType());
                return;
            }

            if (cmd.Predicate(State.Model))
                Sender.Tell(State.Model);
            else
                Sender.Tell(new GetIf_NoMatch());

            if (deactivateAfter)
                Context.Stop(Self);
        }

        private void UpdateCmd(Update cmd)
        {
            if (cmd.Model == null || cmd.Model.GetType() != State.Type)
            {
                Sender.Tell(new Update_InvalidType());
                return;
            }

            // record event
            var updated = new Updated(cmd.Model);
            PersistAndTrack(updated, result =>
            {
                UpdatedEvnt(result);
                Sender.Tell(new Update_Success(State.Id));
            });
        }

        private void DeleteCmd(Delete cmd)
        {
            if (!State.Deleted)
            {
                // record event
                var deleted = new Deleted();
                PersistAndTrack(deleted, result =>
                {
                    DeletedEvnt(result);
                    Sender.Tell(new Delete_Success(State.Id));
                });
            }
            else
                Sender.Tell(new Delete_AlreadyDeleted());
        }

        private void DeleteIfCmd(DeleteIf cmd, bool deactivateAfter = false)
        {
            if (cmd.Type == null || cmd.Type != State.Type)
            {
                Sender.Tell(new DeleteIf_InvalidType());
                return;
            }

            if (cmd.Predicate == null || cmd.Predicate(State.Model))
            {
                // record event
                var deleted = new Deleted();
                PersistAndTrack(deleted, result =>
                {
                    DeletedEvnt(result);
                    Sender.Tell(new DeleteIf_Success(State.Id));
                });
            }
            else
                Sender.Tell(new DeleteIf_NoMatch());

            if (deactivateAfter)
                Context.Stop(Self);
        }

        private void UndeleteCmd(Undelete cmd)
        {
            if (State.Deleted)
            {
                // record event
                var undeleted = new Undeleted();
                PersistAndTrack(undeleted, result =>
                {
                    UndeletedEvnt(result);
                    Sender.Tell(new Undelete_Success(State.Id));
                });
            }
            else
                Sender.Tell(new Undelete_NotDeleted());
        }

        private void UndeleteIfCmd(UndeleteIf cmd, bool deactivateAfter = false)
        {
            if (cmd.Type == null || cmd.Type != State.Type)
            {
                Sender.Tell(new UndeleteIf_InvalidType());
                return;
            }

            if (cmd.Predicate == null || cmd.Predicate(State.Model))
            {
                // record event
                var undeleted = new Undeleted();
                PersistAndTrack(undeleted, result =>
                {
                    UndeletedEvnt(result);
                    Sender.Tell(new UndeleteIf_Success(State.Id));
                });
            }
            else
                Sender.Tell(new UndeleteIf_NoMatch());

            if (deactivateAfter)
                Context.Stop(Self);
        }
        private void DeactivateCmd(Deactivate cmd)
        {
            if (Sender == Context.Parent)
                Context.Stop(Self);
            else
                Context.Parent.Tell(new EntitySetActor.DeactivatedId(State.Id), Sender);
        }
        private void ReceiveTimeoutCmd(ReceiveTimeout cmd)
        {
            Context.Parent.Tell(new EntitySetActor.DeactivatedId(State.Id));
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
