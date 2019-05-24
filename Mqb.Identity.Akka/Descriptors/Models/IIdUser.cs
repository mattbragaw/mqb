using Microsoft.AspNet.Identity;
using Mqb.Identity.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Mqb.Descriptors.Models
{
    public interface IIdUser
    {
        string Id { get; }
        string UserName { get; }
        string UserNameNormalized { get; }
        string Email { get; }
        string EmailNormalized { get; }
        bool EmailConfirmed { get; }
        string Phone { get; }
        bool PhoneConfirmed { get; }
        DateTimeOffset? LockoutEnd { get; }
        bool TwoFactorEnabled { get; }
        bool LockoutEnabled { get; }
        int AccessFailedCount { get; }
        string ConcurrencyStamp { get; }
        string SecurityStamp { get; }
        string PasswordHash { get; }
        IEnumerable<Claim> Claims { get; }
        IEnumerable<Identity.Models.UserLoginInfo> Logins { get; }
    }
    public interface IIdUserMutable : IUser<string>
    {
        new string Id { get; set; }
        string UserNameNormalized { get; set; }
        string Email { get; set; }
        string EmailNormalized { get; set; }
        bool EmailConfirmed { get; set; }
        string Phone { get; set; }
        bool PhoneConfirmed { get; set; }
        DateTimeOffset? LockoutEnd { get; set; }
        bool TwoFactorEnabled { get; set; }
        bool LockoutEnabled { get; set; }
        int AccessFailedCount { get; set; }
        string ConcurrencyStamp { get; set; }
        string SecurityStamp { get; set; }
        string PasswordHash { get; set; }
        IList<Claim> Claims { get; set; } 
        IList<UserLoginInfoMutable> Logins { get; set; }
    }
}
