using Akka.Actor;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Akka.Config
{
    public static class IServiceProviderExtensions
    {
        public static AkkaEnv GetAkkaEnv(this IServiceProvider serviceProvider)
        {
            return new AkkaEnv(serviceProvider.GetRequiredService<ActorSystem>());
        }
    }
}
