using Akka.Actor;
using Akka.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Akka.Config
{
    public class AkkaConfig
    {
        public AkkaConfig(string actorSystemName, Type actorRefsType, params string[] hoconSections) : 
            this(actorSystemName, actorRefsType, (IEnumerable<string>)hoconSections) { }
        public AkkaConfig(string actorSystemName, Type actorRefsType, IEnumerable<string> hoconSections)
        {
            ActorSystemName = actorSystemName;
            ActorRefsType = actorRefsType;
            HoconSections = hoconSections;
        }

        public string ActorSystemName { get; }
        public Type ActorRefsType { get; }
        public IEnumerable<string> HoconSections { get; }
    }
}
