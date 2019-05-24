using Akka.Actor;
using Mqb.Akka.Actors;
using Mqb.Identity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mqb.Identity.Akka.Actors
{
    public class RoleStoreActor : ReceiveActor
    {
        public RoleStoreActor(IActorRef entitiesActor)
        {
            Become(Starting);

            entitiesActor.Tell(new EntitiesActor.GetEntityType(typeof(IdRole)));
        }

        public IActorRef EntityTypeActor { get; set; }
        public IActorRef UserStoreActor { get; set; }

        #region State Methods

        private void Starting()
        {
            Receive<IdentityActor.UsersRef>(cmd => {
                UserStoreActor = cmd.Users;
                CheckIfReady();
            });
            Receive<EntitiesActor.EntityTypeResult>(cmd =>
            {
                EntityTypeActor = cmd.ActorRef;
                CheckIfReady();
            },
            cmd => cmd.EntityType.Type == typeof(IdRole));
        }
        private void Ready()
        {
            Receive<RoleCommands.GetAll>(cmd => GetAllCmd(cmd));
            Receive<RequestActor.RequestCompleted>(cmd => GetAllCompletedCmd(cmd), cmd => cmd.Request.RequestorMessage.GetType() == typeof(RoleCommands.GetAll));
            Receive<RoleCommands.GetByNormalizedName>(cmd => GetByNormalizedNameCmd(cmd));
            Receive<RequestActor.RequestCompleted>(cmd => GetByNormalizedNameCompletedCmd(cmd), cmd => cmd.Request.RequestorMessage.GetType() == typeof(RoleCommands.GetByNormalizedName));
            Receive<RoleCommands.GetById>(cmd => GetByIdCmd(cmd));
            Receive<RequestActor.RequestCompleted>(cmd => GetByIdCompletedCmd(cmd), cmd => cmd.Request.RequestorMessage.GetType() == typeof(RoleCommands.GetById));
            Receive<RoleCommands.Create>(cmd => CreateCmd(cmd));
            Receive<RequestActor.RequestCompleted>(cmd => CreateCompletedCmd(cmd), cmd => cmd.Request.RequestorMessage.GetType() == typeof(RoleCommands.Create));
            Receive<RoleCommands.Update>(cmd => UpdateCmd(cmd));
            Receive<RequestActor.RequestCompleted>(cmd => UpdateCompletedCmd(cmd), cmd => cmd.Request.RequestorMessage.GetType() == typeof(RoleCommands.Update));
            Receive<RoleCommands.DeleteById>(cmd => DeleteByIdCmd(cmd));
            Receive<RequestActor.RequestCompleted>(cmd => DeleteByIdCompletedCmd(cmd), cmd => cmd.Request.RequestorMessage.GetType() == typeof(RoleCommands.DeleteById));
        }

        #endregion

        #region Command Handlers

        private void GetAllCmd(RoleCommands.GetAll cmd)
        {
            CreateRequest(cmd, new EntityTypeActor.GetAll());
        }
        private void GetAllCompletedCmd(RequestActor.RequestCompleted cmd)
        {
            if (cmd.Result is IEnumerable<IdRole>)
                cmd.Request.Requestor.Tell(cmd.Result);
            else
                Unhandled(cmd);
        }
        private void GetByNormalizedNameCmd(RoleCommands.GetByNormalizedName cmd)
        {
            CreateRequest(cmd, new EntityTypeActor.GetIf(typeof(IdRole), GetGeneralPredicate(obj => obj.NameNormalized == cmd.NormalizedName)));
        }
        private void GetByNormalizedNameCompletedCmd(RequestActor.RequestCompleted cmd)
        {
            if (cmd.Result is IEnumerable<IdRole>)
            {
                var results = (IEnumerable<IdRole>)cmd.Result;
                if (results != null)
                    cmd.Request.Requestor.Tell(results.FirstOrDefault());
            }
            else
                Unhandled(cmd);
        }
        private void GetByIdCmd(RoleCommands.GetById cmd)
        {
            CreateRequest(cmd, new EntityTypeActor.GetById(cmd.Id));
        }
        private void GetByIdCompletedCmd(RequestActor.RequestCompleted cmd)
        {
            if (cmd.Result is IdRole)
                cmd.Request.Requestor.Tell(cmd.Result);
            else if (cmd.Result is EntityTypeActor.GetById_IdDeleted)
                cmd.Request.Requestor.Tell(new RoleCommands.GetByIdNoMatch());
            else if (cmd.Result is EntityTypeActor.GetById_IdNotFound)
                cmd.Request.Requestor.Tell(new RoleCommands.GetByIdNoMatch());
            else
                Unhandled(cmd);
        }
        private void CreateCmd(RoleCommands.Create cmd)
        {
            CreateRequest(cmd, new EntityTypeActor.Create(cmd.Role.Id, cmd.Role));
        }
        private void CreateCompletedCmd(RequestActor.RequestCompleted cmd)
        {
            if (cmd.Result is EntityTypeActor.Create_Success)
                cmd.Request.Requestor.Tell(new RoleCommands.CreateResult());
            else if (cmd.Result is EntityTypeActor.Create_IdExists)
                cmd.Request.Requestor.Tell(new RoleCommands.CreateIdExists());
            else
                Unhandled(cmd);
        }
        private void UpdateCmd(RoleCommands.Update cmd)
        {
            CreateRequest(cmd, new EntityTypeActor.Update(cmd.Role.Id, cmd.Role));
        }
        private void UpdateCompletedCmd(RequestActor.RequestCompleted cmd)
        {
            if (cmd.Result is EntityTypeActor.Update_Success)
                cmd.Request.Requestor.Tell(new RoleCommands.UpdateResult());
            else if (cmd.Result is EntityTypeActor.Update_IdDeleted)
                cmd.Request.Requestor.Tell(new RoleCommands.UpdateNotFound());
            else if (cmd.Result is EntityTypeActor.Update_IdNotFound)
                cmd.Request.Requestor.Tell(new RoleCommands.UpdateNotFound());
            else if (cmd.Result is EntityTypeActor.Update_InvalidType)
                cmd.Request.Requestor.Tell(new RoleCommands.UpdateNotFound());
            else
                Unhandled(cmd);
        }
        private void DeleteByIdCmd(RoleCommands.DeleteById cmd)
        {
            CreateRequest(cmd, new EntityTypeActor.DeleteById(cmd.Id));
        }
        private void DeleteByIdCompletedCmd(RequestActor.RequestCompleted cmd)
        {
            if (cmd.Result is EntityTypeActor.DeleteById_Success)
                cmd.Request.Requestor.Tell(new RoleCommands.DeleteByIdResult());
            else if (cmd.Result is EntityTypeActor.DeleteById_IdAlreadyDeleted)
                cmd.Request.Requestor.Tell(new RoleCommands.DeleteByIdNotFound());
            else if (cmd.Result is EntityTypeActor.DeleteById_IdNotFound)
                cmd.Request.Requestor.Tell(new RoleCommands.DeleteByIdNotFound());
            else
                Unhandled(cmd);
        }

        #endregion

        #region Utility Methods

        private void CheckIfReady()
        {
            if (EntityTypeActor != ActorRefs.Nobody &&
                UserStoreActor != ActorRefs.Nobody)
                Become(Ready);
        }
        private void CreateRequest(object request, object targetRequest)
        {
            var requestActor = Context.ActorOf(Props.Create(() => new RequestActor()), RequestActor.GetUniqueName());

            var instructions = new RequestActor.Request(Sender, request, EntityTypeActor, targetRequest);

            requestActor.Tell(instructions);
        }
        private Predicate<object> GetGeneralPredicate(Predicate<IdRole> predicate) => predicate.Generalize();

        #endregion
    }
}
