using Akka.Actor;
using Mqb.Akka.Actors;
using Mqb.Identity.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Identity.Akka.Actors
{
    public class UserStoreActor : ReceiveActor
    {
        public UserStoreActor(IActorRef entitiesActor)
        {
            Become(Starting);

            entitiesActor.Tell(new EntitiesActor.GetEntityType(typeof(IdUser)));
        }
        
        public IActorRef EntityTypeActor { get; set; } = ActorRefs.Nobody;
        public IActorRef RoleStoreActor { get; set; } = ActorRefs.Nobody;

        #region State Methods

        private void Starting()
        {
            Receive<IdentityActor.RolesRef>(cmd => {
                RoleStoreActor = cmd.Roles;
                CheckIfReady();
            });
            Receive<EntitiesActor.EntityTypeResult>(cmd =>
            {
                EntityTypeActor = cmd.ActorRef;
                CheckIfReady();
            },
            cmd => cmd.EntityType.Type == typeof(IdUser));
        }
        private void Ready()
        {

        }

        #endregion

        #region Utility Methods

        private void CheckIfReady()
        {
            if (EntityTypeActor != ActorRefs.Nobody &&
                RoleStoreActor != ActorRefs.Nobody)
                Become(Ready);
        }
        private void CreateRequest(object request, object targetRequest)
        {
            var requestActor = Context.ActorOf(Props.Create(() => new RequestActor()), RequestActor.GetUniqueName());

            var instructions = new RequestActor.Request(Sender, request, EntityTypeActor, targetRequest);

            requestActor.Tell(instructions);
        }
        private Predicate<object> GetGeneralPredicate(Predicate<IdUser> predicate) => predicate.Generalize();

        #endregion
    }
}
