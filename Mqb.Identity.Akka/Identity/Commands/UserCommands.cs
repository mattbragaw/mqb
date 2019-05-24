using Mqb.Akka;
using Mqb.Descriptors.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Mqb.Identity
{
    public class UserCommands
    {
        #region Commands

        public class Create
        {
            public Create(IIdUser user)
            {
                User = user;
            }

            public IIdUser User { get; }
        }
        public class CreatedResult : Ack { }
        public class CreateIdExists : Nak { }
        public class Update
        {
            public Update(IIdUser user)
            {
                User = user;
            }

            public IIdUser User { get; }
        }
        public class UpdateResult : Ack { }
        public class UpdateNotFound : Nak { }
        public class GetAll { }
        public class GetById
        {
            public GetById(string id)
            {
                Id = id;
            }

            public string Id { get; }
        }
        public class GetByIdNotFound : Ack { }
        public class GetByNormalizedUserName
        {
            public GetByNormalizedUserName(string normalizedUserName)
            {
                NormalizedUserName = normalizedUserName;
            }

            public string NormalizedUserName { get; }
        }
        public class GetByNormalizedUserNameNotFound : Ack { }
        public class GetByClaim
        {
            public GetByClaim(Claim claim)
            {
                Claim = claim;
            }

            public Claim Claim { get; }
        }
        public class GetByClaimNotFound : Ack { }
        public class GetByUserLoginInfo
        {
            public GetByUserLoginInfo(string loginProvider, string providerKey)
            {
                LoginProvider = loginProvider;
                ProviderKey = providerKey;
            }

            public string LoginProvider { get; }
            public string ProviderKey { get; }
        }
        public class GetByUserLoginInfoNotFound : Ack { }
        public class DeleteById
        {
            public DeleteById(string id)
            {
                Id = id;
            }

            public string Id { get; }
        }
        public class DeleteByIdResult : Ack { }
        public class DeleteByIdNotFound : Nak { }

        #endregion

        #region Role Commands

        public class AddUserToRole
        {
            public AddUserToRole(string userId, string roleName)
            {
                UserId = userId;
                RoleName = roleName;
            }

            public string UserId { get; }
            public string RoleName { get; }
        }
        public class AddUserToRoleUserNotFound : Nak { }
        public class AddUserToRoleRoleNotFound : Nak { }
        public class RemoveUserFromRole
        {
            public RemoveUserFromRole(string userId, string roleName)
            {
                UserId = userId;
                RoleName = roleName;
            }

            public string UserId { get; }
            public string RoleName { get; }
        }
        public class RemoveUserFromRoleUserNotFound : Nak { }
        public class RemoveUserFromRoleRoleNotFound : Nak { }
        public class GetRolesForUser
        {
            public GetRolesForUser(string userId)
            {
                UserId = userId;
            }

            public string UserId { get; }
        }
        public class GetRolesForUserNotFound : Nak { }
        public class GetUsersInRole
        {
            public GetUsersInRole(string roleName)
            {
                RoleName = roleName;
            }

            public string RoleName { get; }
        }
        public class GetUsersInRoleNotFound : Nak { }

        #endregion

    }
}
