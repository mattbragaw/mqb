using Microsoft.AspNet.Identity;
using Mqb.Descriptors.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Mqb.Identity.Models
{
    public class IdUser : IIdUser
    {
        public IdUser(
            string id, 
            string userName, 
            string userNameNormalized, 
            string email, 
            string emailNormalized, 
            bool emailConfirmed, 
            string phone, 
            bool phoneConfirmed,
            bool twoFactorEnabled, 
            int accessFailedCount, 
            bool lockoutEnabled, 
            DateTimeOffset? lockoutEnd,
            string concurrencyStamp,
            string securityStamp,
            string passwordHash,
            IEnumerable<Claim> claims, 
            IEnumerable<UserLoginInfo> logins)
        {
            Id = id;
            UserName = userName;
            UserNameNormalized = userNameNormalized;
            Email = email;
            EmailNormalized = emailNormalized;
            EmailConfirmed = emailConfirmed;
            Phone = phone;
            PhoneConfirmed = phoneConfirmed;
            
            TwoFactorEnabled = twoFactorEnabled;
            AccessFailedCount = accessFailedCount;
            LockoutEnabled = lockoutEnabled;
            LockoutEnd = lockoutEnd;

            ConcurrencyStamp = concurrencyStamp;
            SecurityStamp = securityStamp;
            PasswordHash = passwordHash;

            Claims = claims;
            Logins = logins;
        }

        public string Id { get; }
        public string UserName { get; }
        public string UserNameNormalized { get; }
        public string Email { get; }
        public string EmailNormalized { get; }
        public bool EmailConfirmed { get; }
        public string Phone { get; }
        public bool PhoneConfirmed { get; }
        public DateTimeOffset? LockoutEnd { get; }
        public bool TwoFactorEnabled { get; }
        public bool LockoutEnabled { get; }
        public int AccessFailedCount { get; }
        public string ConcurrencyStamp { get; }
        public string SecurityStamp { get; }
        public string PasswordHash { get; }
        public IEnumerable<Claim> Claims { get; }
        public IEnumerable<Identity.Models.UserLoginInfo> Logins { get; }
    }
    public class IdUserMutable : IIdUserMutable
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string UserNameNormalized { get; set; }
        public string Email { get; set; }
        public string EmailNormalized { get; set; }
        public bool EmailConfirmed { get; set; }
        public string Phone { get; set; }
        public bool PhoneConfirmed { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public string ConcurrencyStamp { get; set; }
        public string SecurityStamp { get; set; }
        public string PasswordHash { get; set; }
        public IList<Claim> Claims { get; set; } = new List<Claim>();
        public IList<UserLoginInfoMutable> Logins { get; set; } = new List<UserLoginInfoMutable>();
    }
}
