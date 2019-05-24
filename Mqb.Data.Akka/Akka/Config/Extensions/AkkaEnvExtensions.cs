using Akka.Actor;
using Microsoft.Extensions.DependencyInjection;
using Mqb.Akka.Actors;
using Mqb.Descriptors.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Akka.Config
{
    public static class AkkaEnvExtensions
    {
        public static AkkaDataEnv WithDataActors(this AkkaEnv akkaEnv, IServiceProvider serviceProvider, Type actorRefsType)
        {
            if (actorRefsType.GetInterface(typeof(IDataRefs).Name) == null)
                throw new ArgumentException("Type doesn't implement IDataRefs interface.");
            if (actorRefsType.GetInterface(typeof(IDataRefsMutable).Name) == null)
                throw new ArgumentException("Type doesn't implement IDataRefs interface.");

            // get reference to dataRefs
            IDataRefsMutable dataRefs = (IDataRefsMutable)serviceProvider.GetRequiredService(actorRefsType);

            // data actor populates refs
            akkaEnv.ActorSystem.ActorOf(Props.Create(() => new DataActor(dataRefs)), EntitiesActor.GetName());

            // return actor system reference
            return new AkkaDataEnv(akkaEnv.ActorSystem, dataRefs);
        }
        public static AkkaDataEnv WithDataActors<TRefs>(this AkkaEnv akkaEnv, IServiceProvider serviceProvider)
            where TRefs : class, IDataRefs, IDataRefsMutable
        {
            return WithDataActors(akkaEnv, serviceProvider, typeof(TRefs));
        }
    }
}
