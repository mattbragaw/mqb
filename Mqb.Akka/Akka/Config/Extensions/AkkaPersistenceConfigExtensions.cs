﻿using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Akka.Config
{
    public static class AkkaPersistenceConfigExtensions
    {
        public static AkkaPersistenceBuild Build(this AkkaPersistenceConfig config, IServiceCollection services)
        {
            var build = AkkaConfigExtensions.Build(config, services);

            return new AkkaPersistenceBuild(build.ActorSystem);
        }
    }
}
