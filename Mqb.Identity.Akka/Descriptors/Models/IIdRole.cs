using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Descriptors.Models
{
    public interface IIdRole
    {
        string Id { get; }
        string Name { get; }
        string NameNormalized { get; }
        string ConcurrencyStamp { get; }
    }
    public interface IIdRoleMutable : IRole<string>
    {
        new string Id { get; set; }
        string NameNormalized { get; set; }
        string ConcurrencyStamp { get; set; }
    }
}
