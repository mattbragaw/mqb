using Akka.Actor;
using Mqb.Descriptors.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Akka.Actors
{
    public class DataTypesActor : ReceiveActor
    {
        public static string GetName()
        {
            return ConstantsDataAkka.DATA_TYPES;
        }

        #region Commands

        public class Create : Cmd
        {
            public Create(IDataType model)
            {
                Model = model;
            }

            public IDataType Model { get; }
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
        public class GetByParentOrgId : Cmd
        {
            public GetByParentOrgId(string parentOrgId)
            {
                ParentOrgId = parentOrgId;
            }

            public string ParentOrgId { get; }
        }
        public class GetByParentSpaceId : Cmd
        {
            public GetByParentSpaceId(string parentSpaceId)
            {
                ParentSpaceId = parentSpaceId;
            }

            public string ParentSpaceId { get; }
        }
        public class GetIf : Cmd
        {
            public GetIf(Predicate<IDataType> predicate)
            {
                Predicate = predicate;
            }

            public Predicate<IDataType> Predicate { get; }
        }
        public class Update : Cmd
        {
            public Update(IDataType model)
            {
                Model = model;
            }

            public IDataType Model { get; }
        }
        public class Update_IdNotFound : Nak { }
        public class Update_IdDeleted : Nak { }
        public class Update_Success : Ack
        {
            public Update_Success(string id)
            {
                Id = id;
            }

            public string Id { get; }
        }
        public class DeleteAll : Cmd { }
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
        public class DeleteByParentOrgId : Cmd
        {
            public DeleteByParentOrgId(string parentOrgId)
            {
                ParentOrgId = parentOrgId;
            }

            public string ParentOrgId { get; }
        }
        public class DeleteByParentOrgId_Success : Ack { }
        public class DeleteByParentSpaceId : Cmd
        {
            public DeleteByParentSpaceId(string parentSpaceId)
            {
                ParentSpaceId = parentSpaceId;
            }

            public string ParentSpaceId { get; }
        }
        public class DeleteByParentSpaceId_Success : Ack { }
        public class DeleteIf : Cmd
        {
            public DeleteIf(Predicate<IDataType> predicate)
            {
                Predicate = predicate;
            }

            public Predicate<IDataType> Predicate { get; }
        }
        public class DeleteIf_Success : Ack { }

        #endregion

        public DataTypesActor(IActorRef entitiesRef)
        {
            EntitiesRef = entitiesRef;

            Become(Starting);
        }

        #region Properties

        public IActorRef EntitiesRef { get; }
        public IActorRef EntityTypeRef { get; set; }

        #endregion

        #region State Methods

        private void Starting()
        {
            Receive<EntitiesActor.EntityTypeResult>(cmd => EntityTypeResultCmd(cmd), cmd => cmd.EntityType.Type == typeof(IDataType));

            // get entity type reference
            EntitiesRef.Tell(new EntitiesActor.GetEntityType(typeof(IDataType)));
        }
        private void Started()
        {
            Receive<Create>(cmd => CreateCmd(cmd));
            Receive<RequestActor.RequestCompleted>(cmd => CreateCompletedCmd(cmd), cmd => cmd.Request.RequestorMessage.GetType() == typeof(Create));
            Receive<GetAll>(cmd => GetAllCmd(cmd));
            Receive<RequestActor.RequestCompleted>(cmd => GetAllCompletedCmd(cmd), cmd => cmd.Request.RequestorMessage.GetType() == typeof(GetAll));
            Receive<GetById>(cmd => GetByIdCmd(cmd));
            Receive<RequestActor.RequestCompleted>(cmd => GetByIdCompletedCmd(cmd), cmd => cmd.Request.RequestorMessage.GetType() == typeof(GetById));
            Receive<GetIf>(cmd => GetIfCmd(cmd));
            Receive<RequestActor.RequestCompleted>(cmd => GetIfCompletedCmd(cmd), cmd => cmd.Request.RequestorMessage.GetType() == typeof(GetIf));
            Receive<GetByParentOrgId>(cmd => GetByParentOrgIdCmd(cmd));
            Receive<RequestActor.RequestCompleted>(cmd => GetByParentOrgIdCompletedCmd(cmd), cmd => cmd.Request.RequestorMessage.GetType() == typeof(GetByParentOrgId));
            Receive<GetByParentSpaceId>(cmd => GetByParentSpaceIdCmd(cmd));
            Receive<RequestActor.RequestCompleted>(cmd => GetByParentSpaceIdCompletedCmd(cmd), cmd => cmd.Request.RequestorMessage.GetType() == typeof(GetByParentSpaceId));
            Receive<Update>(cmd => UpdateCmd(cmd));
            Receive<RequestActor.RequestCompleted>(cmd => UpdateCompletedCmd(cmd), cmd => cmd.Request.RequestorMessage.GetType() == typeof(Update));
            Receive<DeleteAll>(cmd => DeleteAllCmd(cmd));
            Receive<RequestActor.RequestCompleted>(cmd => DeleteAllCompletedCmd(cmd), cmd => cmd.Request.RequestorMessage.GetType() == typeof(DeleteAll));
            Receive<DeleteById>(cmd => DeleteByIdCmd(cmd));
            Receive<RequestActor.RequestCompleted>(cmd => DeleteByIdCompletedCmd(cmd), cmd => cmd.Request.RequestorMessage.GetType() == typeof(DeleteById));
            Receive<DeleteIf>(cmd => DeleteIfCmd(cmd));
            Receive<RequestActor.RequestCompleted>(cmd => DeleteIfCompletedCmd(cmd), cmd => cmd.Request.RequestorMessage.GetType() == typeof(DeleteIf));
            Receive<DeleteByParentOrgId>(cmd => DeleteByParentOrgIdCmd(cmd));
            Receive<RequestActor.RequestCompleted>(cmd => DeleteByParentOrgIdCompletedCmd(cmd), cmd => cmd.Request.RequestorMessage.GetType() == typeof(DeleteByParentOrgId));
            Receive<DeleteByParentSpaceId>(cmd => DeleteByParentSpaceIdCmd(cmd));
            Receive<RequestActor.RequestCompleted>(cmd => DeleteByParentSpaceIdCompletedCmd(cmd), cmd => cmd.Request.RequestorMessage.GetType() == typeof(DeleteByParentSpaceId));
        }

        #endregion

        #region Command Handlers

        private void EntityTypeResultCmd(EntitiesActor.EntityTypeResult cmd)
        {
            EntityTypeRef = cmd.ActorRef;

            Become(Started);
        }
        private void CreateCmd(Create cmd)
        {
            CreateRequest(cmd, new EntityTypeActor.Create(cmd.Model.Id, cmd.Model));
        }
        private void CreateCompletedCmd(RequestActor.RequestCompleted cmd)
        {
            if (cmd.Result.GetType() == typeof(EntityTypeActor.Create_IdExists))
                cmd.Request.Requestor.Tell(new Create_IdExists());
            else if (cmd.Result.GetType() == typeof(EntityTypeActor.Create_Success))
            {
                EntityTypeActor.Create_Success successResult = (EntityTypeActor.Create_Success)cmd.Result;
                cmd.Request.Requestor.Tell(new Create_Success(successResult.Id));
            }
            else
                Unhandled(cmd);
        }
        private void GetAllCmd(GetAll cmd)
        {
            CreateRequest(cmd, new EntityTypeActor.GetAll());
        }
        private void GetAllCompletedCmd(RequestActor.RequestCompleted cmd)
        {
            if (cmd.Result is IEnumerable<IDataType>)
                cmd.Request.Requestor.Tell(cmd.Result);
            else
                Unhandled(cmd);
        }
        private void GetByIdCmd(GetById cmd)
        {
            CreateRequest(cmd, new EntityTypeActor.GetById(cmd.Id));
        }
        private void GetByIdCompletedCmd(RequestActor.RequestCompleted cmd)
        {
            if (cmd.Result.GetType() == typeof(EntityTypeActor.GetById_IdDeleted))
                cmd.Request.Requestor.Tell(new GetById_IdDeleted());
            else if (cmd.Result.GetType() == typeof(EntityTypeActor.GetById_IdNotFound))
                cmd.Request.Requestor.Tell(new GetById_IdNotFound());
            else if (cmd.Result.GetType() == typeof(IDataType))
            {
                cmd.Request.Requestor.Tell(cmd.Result);
            }
            else
                Unhandled(cmd);
        }
        private void GetIfCmd(GetIf cmd)
        {
            CreateRequest(cmd, new EntityTypeActor.GetIf(typeof(IDataType), GetGeneralPredicate(cmd.Predicate)));
        }
        private void GetIfCompletedCmd(RequestActor.RequestCompleted cmd)
        {
            cmd.Request.Requestor.Tell(cmd.Result);
        }
        private void GetByParentOrgIdCmd(GetByParentOrgId cmd)
        {
            CreateRequest(cmd, new EntityTypeActor.GetIf(typeof(IDataType), GetGeneralPredicate(dt => {
                return dt.ParentOrgId == cmd.ParentOrgId;
            })));
        }
        private void GetByParentOrgIdCompletedCmd(RequestActor.RequestCompleted cmd)
        {
            cmd.Request.Requestor.Tell(cmd.Result);
        }
        private void GetByParentSpaceIdCmd(GetByParentSpaceId cmd)
        {
            CreateRequest(cmd, new EntityTypeActor.GetIf(typeof(IDataType), GetGeneralPredicate(dt => {
                return dt.ParentSpaceId == cmd.ParentSpaceId;
            })));
        }
        private void GetByParentSpaceIdCompletedCmd(RequestActor.RequestCompleted cmd)
        {
            cmd.Request.Requestor.Tell(cmd.Result);
        }
        private void UpdateCmd(Update cmd)
        {
            CreateRequest(cmd, new EntityTypeActor.Update(cmd.Model.Id, cmd.Model));
        }
        private void UpdateCompletedCmd(RequestActor.RequestCompleted cmd)
        {
            if (cmd.Result.GetType() == typeof(EntityTypeActor.Update_IdDeleted))
                cmd.Request.Requestor.Tell(new Update_IdDeleted());
            else if (cmd.Result.GetType() == typeof(EntityTypeActor.Update_IdNotFound))
                cmd.Request.Requestor.Tell(new Update_IdNotFound());
            else if (cmd.Result.GetType() == typeof(EntityTypeActor.Update_Success))
            {
                EntityTypeActor.Update_Success successResult = (EntityTypeActor.Update_Success)cmd.Result;
                cmd.Request.Requestor.Tell(new Update_Success(successResult.Id));
            }
            else
                Unhandled(cmd);
        }
        private void DeleteAllCmd(DeleteAll cmd)
        {
            CreateRequest(cmd, new EntityTypeActor.DeleteAll());
        }
        private void DeleteAllCompletedCmd(RequestActor.RequestCompleted cmd)
        {
            if (cmd.Result.GetType() == typeof(EntityTypeActor.DeleteAll_Success))
                cmd.Request.Requestor.Tell(new DeleteAll_Success());
            else
                Unhandled(cmd);
        }
        private void DeleteByIdCmd(DeleteById cmd)
        {
            CreateRequest(cmd, new EntityTypeActor.DeleteById(cmd.Id));
        }

        private void DeleteByIdCompletedCmd(RequestActor.RequestCompleted cmd)
        {
            if (cmd.Result.GetType() == typeof(EntityTypeActor.DeleteById_IdAlreadyDeleted))
                cmd.Request.Requestor.Tell(new DeleteById_IdAlreadyDeleted());
            else if (cmd.Result.GetType() == typeof(EntityTypeActor.DeleteById_IdNotFound))
                cmd.Request.Requestor.Tell(new DeleteById_IdNotFound());
            else if (cmd.Result.GetType() == typeof(EntityTypeActor.DeleteById_Success))
            {
                EntityTypeActor.DeleteById_Success successResult = (EntityTypeActor.DeleteById_Success)cmd.Result;
                cmd.Request.Requestor.Tell(new DeleteById_Success(successResult.Id));
            }
            else
                Unhandled(cmd);
        }
        private void DeleteIfCmd(DeleteIf cmd)
        {
            CreateRequest(cmd, new EntityTypeActor.DeleteIf(typeof(IDataType), GetGeneralPredicate(cmd.Predicate)));
        }
        private void DeleteIfCompletedCmd(RequestActor.RequestCompleted cmd)
        {
            if (cmd.Result.GetType() == typeof(EntityTypeActor.DeleteIf_Success))
                cmd.Request.Requestor.Tell(new DeleteIf_Success());
            else
                Unhandled(cmd);
        }
        private void DeleteByParentOrgIdCmd(DeleteByParentOrgId cmd)
        {
            CreateRequest(cmd, new EntityTypeActor.DeleteIf(typeof(IDataType), GetGeneralPredicate(dt => {
                return dt.ParentOrgId == cmd.ParentOrgId;
            })));
        }
        private void DeleteByParentOrgIdCompletedCmd(RequestActor.RequestCompleted cmd)
        {
            if (cmd.Result.GetType() == typeof(EntityTypeActor.DeleteIf_Success))
                cmd.Request.Requestor.Tell(new DeleteByParentOrgId_Success());
            else
                Unhandled(cmd);
        }
        private void DeleteByParentSpaceIdCmd(DeleteByParentSpaceId cmd)
        {
            CreateRequest(cmd, new EntityTypeActor.DeleteIf(typeof(IDataType), GetGeneralPredicate(dt => {
                return dt.ParentSpaceId == cmd.ParentSpaceId;
            })));
        }
        private void DeleteByParentSpaceIdCompletedCmd(RequestActor.RequestCompleted cmd)
        {
            if (cmd.Result.GetType() == typeof(EntityTypeActor.DeleteIf_Success))
                cmd.Request.Requestor.Tell(new DeleteByParentOrgId_Success());
            else
                Unhandled(cmd);
        }

        #endregion

        #region Utility Methods

        private void CreateRequest(object request, object targetRequest)
        {
            var requestActor = Context.ActorOf(Props.Create(() => new RequestActor()), RequestActor.GetUniqueName());

            var instructions = new RequestActor.Request(Sender, request, EntityTypeRef, targetRequest);

            requestActor.Tell(instructions);
        }
        private Predicate<object> GetGeneralPredicate(Predicate<IDataType> predicate) => predicate.Generalize();

        #endregion
    }
}
