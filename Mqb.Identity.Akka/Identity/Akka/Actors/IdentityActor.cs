using Akka.Actor;
using Mqb.Descriptors.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Identity.Akka.Actors
{
    public class IdentityActor : ReceiveActor
    {
        public static string GetName() => ConstantsIdentity.IDENTITY;

        #region Commands

        public class GetUsersRef { }
        public class UsersRef
        {
            public UsersRef(IActorRef users)
            {
                Users = users;
            }

            public IActorRef Users { get; }
        }
        public class GetRolesRef { }
        public class RolesRef
        {
            public RolesRef(IActorRef roles)
            {
                Roles = roles;
            }

            public IActorRef Roles { get; }
        }

        #endregion

        public IdentityActor(IIdentityRefsMutable identityRefsMutable)
        {
            EntitiesActor = identityRefsMutable.Entities;

            Receive<GetUsersRef>(cmd => Sender.Tell(new UsersRef(Users)));
            Receive<GetRolesRef>(cmd => Sender.Tell(new RolesRef(Roles)));

            Users = Context.ActorOf(Props.Create(() => new UserStoreActor(EntitiesActor)), ConstantsIdentity.USER_STORE);
            Roles = Context.ActorOf(Props.Create(() => new RoleStoreActor(EntitiesActor)), ConstantsIdentity.ROLE_STORE);

            Users.Tell(new RolesRef(Roles));
            Roles.Tell(new UsersRef(Users));

            identityRefsMutable.Identity = Context.Self;
            identityRefsMutable.IdentityUsers = Users;
            identityRefsMutable.IdentityRoles = Roles;
        }

        public IActorRef EntitiesActor { get; }
        public IActorRef Users { get; set; }
        public IActorRef Roles { get; set; }
    }
}
