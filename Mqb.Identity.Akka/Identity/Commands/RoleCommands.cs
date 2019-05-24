using Mqb.Akka;
using Mqb.Identity.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Identity
{
    public class RoleCommands
    {
        public class Create
        {
            public Create(IdRole role)
            {
                Role = role;
            }

            public IdRole Role { get; }
        }
        public class CreateResult : Ack { }
        public class CreateIdExists : Nak { }
        public class Update
        {
            public Update(IdRole role)
            {
                Role = role;
            }

            public IdRole Role { get; }
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
        public class GetByIdNoMatch : Ack { }
        public class GetByNormalizedName
        {
            public GetByNormalizedName(string normalizedName)
            {
                NormalizedName = normalizedName;
            }

            public string NormalizedName { get; }
        }
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
    }
}
