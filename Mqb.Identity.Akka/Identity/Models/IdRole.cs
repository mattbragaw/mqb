using Mqb.Descriptors.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Identity.Models
{
    public class IdRole : IIdRole
    {
        public IdRole(string id, string name, string nameNormalized, string concurrencyStamp)
        {
            Id = id;
            Name = name;
            NameNormalized = nameNormalized;
            ConcurrencyStamp = concurrencyStamp;
        }

        public string Id { get; }
        public string Name { get; }
        public string NameNormalized { get; }
        public string ConcurrencyStamp { get; }
    }
    public class IdRoleMutable : IIdRoleMutable
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string NameNormalized { get; set; }
        public string ConcurrencyStamp { get; set; }
    }
}
