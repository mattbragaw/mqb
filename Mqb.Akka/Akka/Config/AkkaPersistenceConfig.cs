using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Akka.Config
{
    public class AkkaPersistenceConfig : AkkaConfig
    {
        public AkkaPersistenceConfig(string actorSystemName, Type actorRefsType, params string[] hoconSections) : 
            base(actorSystemName, actorRefsType, hoconSections)
        {
        }
        public AkkaPersistenceConfig(string actorSystemName, Type actorRefsType, IEnumerable<string> hoconSections) : 
            base(actorSystemName, actorRefsType, hoconSections)
        {
        }
    }
}
