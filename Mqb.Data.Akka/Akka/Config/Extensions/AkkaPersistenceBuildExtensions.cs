using Microsoft.Extensions.DependencyInjection;
using Mqb.Descriptors.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Akka.Config.Extensions
{
    public static class AkkaPersistenceBuildExtensions
    {
        public static AkkaDataBuild WithData(this AkkaPersistenceBuild akkaPersistenceBuild, IServiceCollection services, Type actorRefsType)
        {
            if (actorRefsType.GetInterface(typeof(IDataRefs).Name) == null)
                throw new ArgumentException("Type doesn't implement IDataRefs interface.");
            if (actorRefsType.GetInterface(typeof(IDataRefsMutable).Name) == null)
                throw new ArgumentException("Type doesn't implement IDataRefsMutable interface.");

            // inject actor system references
            services.AddSingleton(actorRefsType);
            services.AddSingleton(x => (IDataRefs)x.GetRequiredService(actorRefsType));
            services.AddSingleton(x => (IDataRefsMutable)x.GetRequiredService(actorRefsType));
            
            return new AkkaDataBuild(akkaPersistenceBuild.ActorSystem, actorRefsType);
        }
        public static AkkaDataBuild WithData<TRefs>(
            this AkkaPersistenceBuild akkaPersistenceBuild, 
            IServiceCollection services)
            where TRefs : class, IDataRefs, IDataRefsMutable
        {
            return WithData(akkaPersistenceBuild, services, typeof(TRefs));
        }
    }
}
