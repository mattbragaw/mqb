using Akka.Actor;
using Microsoft.AspNet.Identity;
using Mqb.Descriptors;
using Mqb.Descriptors.Models;
using Mqb.Identity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mqb.Identity
{
    public class UserStoreAkka<TUser> :
        IUserStore<TUser>,
        IUserClaimStore<TUser>,
        IUserLoginStore<TUser>,
        IUserRoleStore<TUser>,
        IUserPasswordStore<TUser>,
        IUserSecurityStampStore<TUser>,
        IUserEmailStore<TUser>,
        IUserPhoneNumberStore<TUser>
        where TUser : class, IIdUserMutable, new()
    {
        private const int _StandardTimeoutSeconds = 3;

        public UserStoreAkka(IHasIdentityUsersRef_R hasIdentityUsersRef) : this(hasIdentityUsersRef.IdentityUsers) { }
        public UserStoreAkka(IActorRef userStore)
        {
            UserStore = userStore;
        }

        public IActorRef UserStore { get; }

        #region User Store Methods
        
        public Task CreateAsync(TUser user)
        {
            return CreateAsync(user, new CancellationTokenSource().Token);
        }
        public async Task<IdentityResult> CreateAsync(TUser user, CancellationToken cancellationToken)
        {
            IIdUser idUser = ToImmutable(user);
            await UserStore.Ask<UserCommands.CreatedResult>(new UserCommands.Create(idUser), TimeSpan.FromSeconds(_StandardTimeoutSeconds), cancellationToken);

            return new IdentityResult();
        }
        public Task DeleteAsync(TUser user)
        {
            return DeleteAsync(user, new CancellationTokenSource().Token);
        }
        public async Task<IdentityResult> DeleteAsync(TUser user, CancellationToken cancellationToken)
        {
            await UserStore.Ask<UserCommands.DeleteByIdResult>(new UserCommands.DeleteById(user.Id), TimeSpan.FromSeconds(_StandardTimeoutSeconds), cancellationToken);

            return new IdentityResult();
        }
        public Task<TUser> FindByIdAsync(string userId)
        {
            return FindByIdAsync(userId, new CancellationTokenSource().Token);
        }
        public async Task<TUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            var result = await UserStore.Ask(new UserCommands.GetById(userId), TimeSpan.FromSeconds(_StandardTimeoutSeconds), cancellationToken);

            if (result is IdUser)
                return ToMutable((IdUser)result);
            else
                return null;
        }
        
        public Task<TUser> FindByNameAsync(string userName)
        {
            return FindByNameAsync(userName, new CancellationTokenSource().Token);
        }
        public async Task<TUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            var result = await UserStore.Ask(new UserCommands.GetByNormalizedUserName(normalizedUserName), TimeSpan.FromSeconds(_StandardTimeoutSeconds), cancellationToken);

            if (result is IdUser)
                return ToMutable((IdUser)result);
            else
                return null;
        }
        public async Task<IList<Claim>> GetClaimsAsync(TUser user, CancellationToken cancellationToken)
        {
            if (user != null && user.Claims != null)
                return await Task.Run(() => user.Claims);
            else
                return new List<Claim>();
        }
        public async Task<string> GetNormalizedUserNameAsync(TUser user, CancellationToken cancellationToken)
        {
            if (user != null && !string.IsNullOrEmpty(user.UserName))
                return await Task.Run(() => user.UserName.ToUpper());
            else
                return string.Empty;
        }

        public async Task<string> GetUserIdAsync(TUser user, CancellationToken cancellationToken)
        {
            if (user != null && !string.IsNullOrEmpty(user.Id))
                return await Task.Run(() => user.Id);
            else
                return Unique.String();
        }

        public async Task<string> GetUserNameAsync(TUser user, CancellationToken cancellationToken)
        {
            if (user != null && !string.IsNullOrEmpty(user.UserName))
                return await Task.Run(() => user.UserName);
            else if (user != null && !string.IsNullOrEmpty(user.Email))
                return user.Email;
            else
                return string.Empty;
        }

        public async Task<IList<TUser>> GetUsersForClaimAsync(Claim claim, CancellationToken cancellationToken)
        {
            var result = await UserStore.Ask(new UserCommands.GetByClaim(claim), TimeSpan.FromSeconds(_StandardTimeoutSeconds), cancellationToken);

            if (result != null && result is IEnumerable<IdUser>)
            {
                return ToMutable(((IEnumerable<IdUser>)result)).ToList();
            }
            else
                return null;
        }

        public Task RemoveClaimsAsync(TUser user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
        {
            if (user != null)
                foreach (var claim in claims)
                    user.Claims.Remove(claim);

            return Task.CompletedTask;
        }

        public Task ReplaceClaimAsync(TUser user, Claim claim, Claim newClaim, CancellationToken cancellationToken)
        {
            if (user != null)
            {
                user.Claims.Remove(claim);
                user.Claims.Add(newClaim);
            }

            return Task.CompletedTask;
        }

        public Task SetNormalizedUserNameAsync(TUser user, string normalizedName, CancellationToken cancellationToken)
        {
            user.UserNameNormalized = normalizedName;

            return Task.CompletedTask;
        }

        public Task SetUserNameAsync(TUser user, string userName, CancellationToken cancellationToken)
        {
            user.UserName = userName;

            return Task.CompletedTask;
        }
        public Task UpdateAsync(TUser user)
        {
            return UpdateAsync(user, new CancellationTokenSource().Token);
        }
        public async Task<IdentityResult> UpdateAsync(TUser user, CancellationToken cancellationToken)
        {
            var result = await UserStore.Ask(new UserCommands.Update(ToImmutable(user)), TimeSpan.FromSeconds(_StandardTimeoutSeconds), cancellationToken);

            return new IdentityResult();
        }

        #endregion

        #region User Claims Store Methods

        public Task AddClaimAsync(TUser user, Claim claim)
        {
            user.Claims.Add(claim);

            return Task.CompletedTask;
        }
        public Task AddClaimsAsync(TUser user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
        {
            if (user != null)
            {
                if (user.Claims == null)
                    user.Claims = new List<Claim>();

                foreach (var claim in claims)
                    AddClaimAsync(user, claim);
            }

            return Task.CompletedTask;
        }
        public Task<IList<Claim>> GetClaimsAsync(TUser user)
        {
            return Task.FromResult(user.Claims);
        }
        public Task RemoveClaimAsync(TUser user, Claim claim)
        {
            user.Claims.Remove(claim);

            return Task.CompletedTask;
        }

        #endregion

        #region User Login Store Methods

        public Task AddLoginAsync(TUser user, Microsoft.AspNet.Identity.UserLoginInfo login)
        {
            return AddLoginAsync(user, login, new CancellationTokenSource().Token);
        }
        public Task AddLoginAsync(TUser user, Microsoft.AspNet.Identity.UserLoginInfo login, CancellationToken cancellationToken)
        {
            if (user != null)
            {
                if (user.Logins == null)
                    user.Logins = new List<UserLoginInfoMutable>();

                user.Logins.Add(new UserLoginInfoMutable(login.LoginProvider, login.ProviderKey, ""));
            }

            return Task.CompletedTask;
        }
        public Task RemoveLoginAsync(TUser user, Microsoft.AspNet.Identity.UserLoginInfo login)
        {
            return RemoveLoginAsync(user, login.LoginProvider, login.ProviderKey, new CancellationTokenSource().Token);
        }
        public Task RemoveLoginAsync(TUser user, string loginProvider, string providerKey, CancellationToken cancellationToken)
        {
            if (user != null)
            {
                if (user.Logins != null)
                {
                    var login = user.Logins.FirstOrDefault(e => e.LoginProvider == loginProvider && e.ProviderKey == providerKey);

                    if (login != null)
                        user.Logins.Remove(login);
                }
            }

            return Task.CompletedTask;
        }
        public Task<IList<Microsoft.AspNet.Identity.UserLoginInfo>> GetLoginsAsync(TUser user)
        {
            return GetLoginsAsync(user, new CancellationTokenSource().Token);
        }
        public Task<IList<Microsoft.AspNet.Identity.UserLoginInfo>> GetLoginsAsync(TUser user, CancellationToken cancellationToken)
        {
            IList<Microsoft.AspNet.Identity.UserLoginInfo> response = new List<Microsoft.AspNet.Identity.UserLoginInfo>();

            if (user != null)
            {
                if (user.Logins != null)
                    response = ToImmutableIdentity(user.Logins).ToList();

                return Task.FromResult(response);
            }
            else
                return null;
        }
        public Task<TUser> FindAsync(Microsoft.AspNet.Identity.UserLoginInfo login)
        {
            return FindByLoginAsync(login.LoginProvider, login.ProviderKey, new CancellationTokenSource().Token);
        }
        public async Task<TUser> FindByLoginAsync(string loginProvider, string providerKey, CancellationToken cancellationToken)
        {
            var result = await UserStore.Ask(new UserCommands.GetByUserLoginInfo(loginProvider, providerKey), cancellationToken);

            if (result != null && result is IdUser)
                return ToMutable((IdUser)result);
            else
                return null;
        }

        #endregion

        #region User Role Store Methods

        public Task AddToRoleAsync(TUser user, string roleName)
        {
            return AddToRoleAsync(user, roleName, new CancellationTokenSource().Token);
        }
        public async Task AddToRoleAsync(TUser user, string roleName, CancellationToken cancellationToken)
        {
            await Task.Run(() => UserStore.Tell(new UserCommands.AddUserToRole(user.Id, roleName)));
        }
        public Task RemoveFromRoleAsync(TUser user, string roleName)
        {
            return RemoveFromRoleAsync(user, roleName, new CancellationTokenSource().Token);
        }
        public async Task RemoveFromRoleAsync(TUser user, string roleName, CancellationToken cancellationToken)
        {
            await Task.Run(() => UserStore.Tell(new UserCommands.RemoveUserFromRole(user.Id, roleName)));
        }
        public Task<IList<string>> GetRolesAsync(TUser user)
        {
            return GetRolesAsync(user, new CancellationTokenSource().Token);
        }
        public async Task<IList<string>> GetRolesAsync(TUser user, CancellationToken cancellationToken)
        {
            var result = await UserStore.Ask(new UserCommands.GetRolesForUser(user.Id), cancellationToken);

            if (result != null && result is IEnumerable<IdRole>)
                return ((IEnumerable<IdRole>)result).Select(e => e.Name).ToList();
            else
                return null;
        }
        public Task<bool> IsInRoleAsync(TUser user, string roleName)
        {
            return IsInRoleAsync(user, roleName, new CancellationTokenSource().Token);
        }
        public async Task<bool> IsInRoleAsync(TUser user, string roleName, CancellationToken cancellationToken)
        {
            var result = await GetRolesAsync(user, cancellationToken);

            if (result != null)
                return result.Contains(roleName);
            else
                return false;
        }
        public async Task<IList<TUser>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            var result = await UserStore.Ask(new UserCommands.GetUsersInRole(roleName), cancellationToken);

            if (result != null && result is IEnumerable<IdUser>)
                return ToMutable(((IEnumerable<IdUser>)result)).ToList();
            else
                return null;
        }

        #endregion

        #region User Password Store Methods

        public Task SetPasswordHashAsync(TUser user, string passwordHash)
        {
            return SetPasswordHashAsync(user, passwordHash, new CancellationTokenSource().Token);
        }
        public Task SetPasswordHashAsync(TUser user, string passwordHash, CancellationToken cancellationToken)
        {
            if (user != null)
                user.PasswordHash = passwordHash;

            return Task.CompletedTask;
        }
        public Task<string> GetPasswordHashAsync(TUser user)
        {
            throw new NotImplementedException();
        }
        public Task<string> GetPasswordHashAsync(TUser user, CancellationToken cancellationToken)
        {
            if (user != null)
                return Task.FromResult(user.PasswordHash);
            else
                return Task.FromResult((string)null);
        }
        public Task<bool> HasPasswordAsync(TUser user)
        {
            return HasPasswordAsync(user, new CancellationTokenSource().Token);
        }
        public Task<bool> HasPasswordAsync(TUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user != null && !string.IsNullOrEmpty(user.PasswordHash));
        }

        #endregion

        #region Security Stamp Store Methods

        public Task SetSecurityStampAsync(TUser user, string stamp)
        {
            return SetSecurityStampAsync(user, stamp, new CancellationTokenSource().Token);
        }
        public Task SetSecurityStampAsync(TUser user, string stamp, CancellationToken cancellationToken)
        {
            user.SecurityStamp = stamp;

            return Task.CompletedTask;
        }
        public Task<string> GetSecurityStampAsync(TUser user)
        {
            return GetSecurityStampAsync(user, new CancellationTokenSource().Token);
        }
        public Task<string> GetSecurityStampAsync(TUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.SecurityStamp);
        }

        #endregion

        #region User Email Store Methods

        public Task SetEmailAsync(TUser user, string email)
        {
            return SetEmailAsync(user, email, new CancellationTokenSource().Token);
        }
        public Task SetEmailAsync(TUser user, string email, CancellationToken cancellationToken)
        {
            if (user != null)
                user.Email = email;

            return Task.CompletedTask;
        }
        public Task<string> GetEmailAsync(TUser user)
        {
            return GetEmailAsync(user, new CancellationTokenSource().Token);
        }
        public Task<string> GetEmailAsync(TUser user, CancellationToken cancellationToken)
        {
            if (user != null)
                return Task.FromResult(user.Email);
            else
                throw new ArgumentNullException(nameof(user));
        }
        public Task<bool> GetEmailConfirmedAsync(TUser user)
        {
            return GetEmailConfirmedAsync(user, new CancellationTokenSource().Token);
        }
        public Task<bool> GetEmailConfirmedAsync(TUser user, CancellationToken cancellationToken)
        {
            if (user != null)
                return Task.FromResult(user.EmailConfirmed);
            else
                throw new ArgumentNullException(nameof(user));
        }
        public Task SetEmailConfirmedAsync(TUser user, bool confirmed)
        {
            return SetEmailConfirmedAsync(user, confirmed);
        }
        public Task SetEmailConfirmedAsync(TUser user, bool confirmed, CancellationToken cancellationToken)
        {
            if (user != null)
            {
                user.EmailConfirmed = confirmed;
                return Task.CompletedTask;
            }
            else
                throw new ArgumentNullException(nameof(user));
        }
        public Task<TUser> FindByEmailAsync(string email)
        {
            return FindByEmailAsync(email, new CancellationTokenSource().Token);
        }
        public Task<TUser> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        public Task<string> GetNormalizedEmailAsync(TUser user, CancellationToken cancellationToken)
        {
            if (user != null)
            {
                return Task.FromResult(user.EmailNormalized);
            }
            else
                throw new ArgumentNullException(nameof(user));
        }
        public Task SetNormalizedEmailAsync(TUser user, string normalizedEmail, CancellationToken cancellationToken)
        {
            if (user != null)
            {
                user.EmailNormalized = normalizedEmail;
                return Task.CompletedTask;
            }
            else
                throw new ArgumentNullException(nameof(user));
        }

        #endregion

        #region User Phone Number Store Methods

        public Task SetPhoneNumberAsync(TUser user, string phone)
        {
            return SetPhoneNumberAsync(user, phone, new CancellationTokenSource().Token);
        }
        public Task SetPhoneNumberAsync(TUser user, string phone, CancellationToken cancellationToken)
        {
            if (user != null)
            {
                user.Phone = phone;
                return Task.CompletedTask;
            }
            else
                throw new ArgumentNullException(nameof(user));
        }
        public Task<string> GetPhoneNumberAsync(TUser user)
        {
            return GetPhoneNumberAsync(user, new CancellationTokenSource().Token);
        }
        public Task<string> GetPhoneNumberAsync(TUser user, CancellationToken cancellationToken)
        {
            if (user != null)
                return Task.FromResult(user.Phone);
            else
                throw new ArgumentNullException(nameof(user));
        }
        public Task<bool> GetPhoneNumberConfirmedAsync(TUser user)
        {
            return GetPhoneNumberConfirmedAsync(user, new CancellationTokenSource().Token);
        }
        public Task<bool> GetPhoneNumberConfirmedAsync(TUser user, CancellationToken cancellationToken)
        {
            if (user != null)
                return Task.FromResult(user.PhoneConfirmed);
            else
                throw new ArgumentNullException(nameof(user));
        }
        public Task SetPhoneNumberConfirmedAsync(TUser user, bool confirmed)
        {
            return SetPhoneNumberConfirmedAsync(user, confirmed, new CancellationTokenSource().Token);
        }
        public Task SetPhoneNumberConfirmedAsync(TUser user, bool confirmed, CancellationToken cancellationToken)
        {
            if (user != null)
            {
                user.PhoneConfirmed = confirmed;
                return Task.CompletedTask;
            }
            else
                throw new ArgumentNullException(nameof(user));
        }

        #endregion

        public void Dispose()
        {

        }

        #region Utility Methods

        protected IdUser ToImmutable(TUser idUser)
        {
            return new IdUser(
                idUser.Id,
                idUser.UserName,
                idUser.UserNameNormalized,
                idUser.Email,
                idUser.EmailNormalized,
                idUser.EmailConfirmed,
                idUser.Phone,
                idUser.PhoneConfirmed,
                idUser.TwoFactorEnabled,
                idUser.AccessFailedCount,
                idUser.LockoutEnabled,
                idUser.LockoutEnd,
                idUser.ConcurrencyStamp,
                idUser.SecurityStamp,
                idUser.PasswordHash,
                idUser.Claims,
                ToImmutable(idUser.Logins));
        }
        protected TUser ToMutable(IdUser idUser)
        {
            return new TUser
            {
                Id = idUser.Id,
                UserName = idUser.UserName,
                UserNameNormalized = idUser.UserNameNormalized,
                Email = idUser.Email,
                EmailNormalized = idUser.EmailNormalized,
                Phone = idUser.Phone,
                EmailConfirmed = idUser.EmailConfirmed,
                PhoneConfirmed = idUser.PhoneConfirmed,
                PasswordHash = idUser.PasswordHash,
                SecurityStamp = idUser.SecurityStamp,
                ConcurrencyStamp = idUser.ConcurrencyStamp,
                TwoFactorEnabled = idUser.TwoFactorEnabled,
                AccessFailedCount = idUser.AccessFailedCount,
                LockoutEnabled = idUser.LockoutEnabled,
                LockoutEnd = idUser.LockoutEnd,
                Claims = idUser.Claims.ToList(),
                Logins = ToMutable(idUser.Logins).ToList()
            };
        }
        protected Microsoft.AspNet.Identity.UserLoginInfo ToImmutableIdentity(UserLoginInfoMutable userLoginInfo)
        {
            return new Microsoft.AspNet.Identity.UserLoginInfo(userLoginInfo.LoginProvider, userLoginInfo.ProviderKey);
        }
        protected IEnumerable<IdUser> ToImmutable(IEnumerable<TUser> idUsers)
        {
            List<IdUser> response = new List<IdUser>();

            foreach (var idUser in idUsers)
                response.Add(ToImmutable(idUser));

            return response;
        }
        protected IEnumerable<TUser> ToMutable(IEnumerable<IdUser> idUsers)
        {
            List<TUser> response = new List<TUser>();

            foreach (var idUser in idUsers)
                response.Add(ToMutable(idUser));

            return response;
        }
        protected IEnumerable<Microsoft.AspNet.Identity.UserLoginInfo> ToImmutableIdentity(IEnumerable<UserLoginInfoMutable> userLoginInfos)
        {
            List<Microsoft.AspNet.Identity.UserLoginInfo> response = new List<Microsoft.AspNet.Identity.UserLoginInfo>();

            foreach (var idUser in userLoginInfos)
                response.Add(ToImmutableIdentity(idUser));

            return response;
        }
        protected Models.UserLoginInfo ToImmutable(UserLoginInfoMutable userLoginInfo)
        {
            return new Models.UserLoginInfo(userLoginInfo.LoginProvider, userLoginInfo.ProviderKey, userLoginInfo.ProviderDisplayName);
        }
        
        protected UserLoginInfoMutable ToMutable(Models.UserLoginInfo userLoginInfo)
        {
            return new UserLoginInfoMutable(userLoginInfo.LoginProvider, userLoginInfo.ProviderKey, userLoginInfo.ProviderDisplayName);
        }
        protected IEnumerable<Models.UserLoginInfo> ToImmutable(IEnumerable<UserLoginInfoMutable> logins)
        {
            List<Models.UserLoginInfo> response = new List<Models.UserLoginInfo>();

            foreach (var idUser in logins)
                response.Add(ToImmutable(idUser));

            return response;
        }
        protected IEnumerable<UserLoginInfoMutable> ToMutable(IEnumerable<Models.UserLoginInfo> logins)
        {
            List<UserLoginInfoMutable> response = new List<UserLoginInfoMutable>();

            foreach (var idUser in logins)
                response.Add(ToMutable(idUser));

            return response;
        }

        #endregion
    }
}
