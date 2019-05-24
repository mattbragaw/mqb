using Microsoft.Extensions.DependencyInjection;
using Mqb.Akka.Config;
using Mqb.Descriptors.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Identity.Akka.Config
{
    public static class AkkaDataBuildExtensions
    {
        public static AkkaIdentityBuild WithIdentity(this AkkaDataBuild akkaDataBuild, IServiceCollection services)
        {
            if (akkaDataBuild.ActorRefsType.GetInterface(typeof(IIdentityRefs).Name) == null)
                throw new ArgumentException("Type doesn't implement IIdentityRefs interface.");
            if (akkaDataBuild.ActorRefsType.GetInterface(typeof(IIdentityRefsMutable).Name) == null)
                throw new ArgumentException("Type doesn't implement IIdentityRefsMutable interface.");

            // inject actor system references
            services.AddSingleton(x => (IIdentityRefs)x.GetRequiredService(akkaDataBuild.ActorRefsType));
            services.AddSingleton(x => (IIdentityRefsMutable)x.GetRequiredService(akkaDataBuild.ActorRefsType));

            return new AkkaIdentityBuild(akkaDataBuild.ActorSystem, akkaDataBuild.ActorRefsType);
        }
        public static AkkaIdentityBuild WithIdentity<TRefs>(
            this AkkaDataBuild akkaDataBuild,
            IServiceCollection services)
            where TRefs : class, IDataRefs, IDataRefsMutable, IIdentityRefs, IIdentityRefsMutable
        {
            return WithIdentity(akkaDataBuild, services);
        }
    }
}
