using Akka.Actor;
using Microsoft.AspNet.Identity;
using Mqb.Descriptors;
using Mqb.Descriptors.Models;
using Mqb.Identity.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mqb.Identity
{
    public class RoleStoreAkka<TRole> :
        IRoleStore<TRole>
        where TRole: class, IIdRoleMutable, new()
    {
        private const int _StandardTimeoutSeconds = 3;

        public RoleStoreAkka(IHasIdentityRolesRef_R hasIdentityRolesRef) : this(hasIdentityRolesRef.IdentityRoles) { }
        public RoleStoreAkka(IActorRef roleStore)
        {
            RoleStore = roleStore;
        }

        public IActorRef RoleStore { get; }

        #region Role Methods

        public Task CreateAsync(TRole role)
        {
            return CreateAsync(role, new CancellationTokenSource().Token);
        }
        public async Task<IdentityResult> CreateAsync(TRole role, CancellationToken cancellationToken)
        {
            IdRole idRole = ToImmutable(role);
            var result = await RoleStore.Ask(new RoleCommands.Create(idRole), TimeSpan.FromSeconds(_StandardTimeoutSeconds), cancellationToken);

            return new IdentityResult();
        }
        public Task DeleteAsync(TRole role)
        {
            return DeleteAsync(role, new CancellationTokenSource().Token);
        }
        public async Task<IdentityResult> DeleteAsync(TRole role, CancellationToken cancellationToken)
        {
            var result = await RoleStore.Ask(new RoleCommands.DeleteById(role.Id), TimeSpan.FromSeconds(_StandardTimeoutSeconds), cancellationToken);

            return new IdentityResult();
        }
        public Task<TRole> FindByIdAsync(string roleId)
        {
            return FindByIdAsync(roleId, new CancellationTokenSource().Token);
        }
        public async Task<TRole> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            var result = await RoleStore.Ask(new RoleCommands.GetById(roleId), TimeSpan.FromSeconds(_StandardTimeoutSeconds), cancellationToken);

            if (result is IdRole)
                return ToMutable((IdRole)result);
            else
                return null;
        }
        public Task<TRole> FindByNameAsync(string roleName)
        {
            return FindByNameAsync(roleName, new CancellationTokenSource().Token);
        }
        public async Task<TRole> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            var result = await RoleStore.Ask(new RoleCommands.GetByNormalizedName(normalizedRoleName), TimeSpan.FromSeconds(_StandardTimeoutSeconds), cancellationToken);

            if (result is IdRole)
                return ToMutable((IdRole)result);
            else
                return null;
        }
        public Task<string> GetNormalizedRoleNameAsync(TRole role, CancellationToken cancellationToken)
        {
            if (role != null)
            {
                return Task.FromResult(role.NameNormalized);
            }
            else
                throw new ArgumentNullException(nameof(role));
        }
        public Task<string> GetRoleIdAsync(TRole role, CancellationToken cancellationToken)
        {
            if (role != null)
            {
                return Task.FromResult(role.Id);
            }
            else
                throw new ArgumentNullException(nameof(role));
        }
        public Task<string> GetRoleNameAsync(TRole role, CancellationToken cancellationToken)
        {
            if (role != null)
            {
                return Task.FromResult(role.Name);
            }
            else
                return Task.FromResult(string.Empty);
        }
        public Task SetNormalizedRoleNameAsync(TRole role, string nameNormalized, CancellationToken cancellationToken)
        {
            if (role != null)
            {
                role.NameNormalized = nameNormalized;
                return Task.CompletedTask;
            }
            else
                throw new ArgumentNullException(nameof(role));
        }
        public Task SetRoleNameAsync(TRole role, string roleName, CancellationToken cancellationToken)
        {
            if (role != null)
            {
                role.Name = roleName;
                return Task.CompletedTask;
            }
            else
                throw new ArgumentNullException(nameof(role));
        }
        public Task UpdateAsync(TRole role)
        {
            return UpdateAsync(role, new CancellationTokenSource().Token);
        }
        public async Task<IdentityResult> UpdateAsync(TRole role, CancellationToken cancellationToken)
        {
            var update = new IdRole(role.Id, role.Name, role.NameNormalized, Unique.String());
            var result = await RoleStore.Ask(new RoleCommands.Update(update), TimeSpan.FromSeconds(_StandardTimeoutSeconds), cancellationToken);

            return new IdentityResult();
        }

        #endregion

        public void Dispose()
        {

        }

        #region Utility Methods

        private IdRole ToImmutable(TRole role)
        {
            return new IdRole(role.Id, role.Name, role.NameNormalized, role.ConcurrencyStamp);
        }
        private TRole ToMutable(IdRole role)
        {
            return new TRole
            {
                Id = role.Id,
                Name = role.Name,
                NameNormalized = role.NameNormalized,
                ConcurrencyStamp = role.ConcurrencyStamp
            };
        }
        private IEnumerable<IdRole> ToImmutable(IEnumerable<TRole> roles)
        {
            List<IdRole> response = new List<IdRole>();

            foreach (var role in roles)
                response.Add(ToImmutable(role));

            return response;
        }
        private IEnumerable<TRole> ToImmutable(IEnumerable<IdRole> roles)
        {
            List<TRole> response = new List<TRole>();

            foreach (var role in roles)
                response.Add(ToMutable(role));

            return response;
        }

        #endregion
    }
}
