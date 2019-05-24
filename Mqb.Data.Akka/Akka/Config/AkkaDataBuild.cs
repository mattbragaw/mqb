using System;
using System.Collections.Generic;
using System.Text;
using Akka.Actor;
using Mqb.Descriptors.Models;

namespace Mqb.Akka.Config
{
    public class AkkaDataBuild : AkkaPersistenceBuild
    {
        public AkkaDataBuild(ActorSystem actorSystem, Type actorRefsType) : base(actorSystem)
        {
            if (actorRefsType.GetInterface(typeof(IDataRefs).Name) == null)
                throw new ArgumentException("Type doesn't implement IDataRefs interface.");
            if (actorRefsType.GetInterface(typeof(IDataRefsMutable).Name) == null)
                throw new ArgumentException("Type doesn't implement IDataRefsMutable interface.");

            ActorRefsType = actorRefsType;
        }

        public Type ActorRefsType { get; }
    }
    public class AkkaDataBuild<TInt> : AkkaDataBuild
        where TInt : class, IDataRefs, IDataRefsMutable
    {
        public AkkaDataBuild(ActorSystem actorSystem) : 
            base(actorSystem, typeof(TInt))
        {
        }
    }
}
