using Akka.Actor;
using Mqb.Descriptors;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Services
{
    public class DataRowServiceAkka
    {
        public DataRowServiceAkka(IHasDataRowsRef_R hasDataRowsRef) : this(hasDataRowsRef.DataRows) { }
        public DataRowServiceAkka(IActorRef dataRows)
        {
            DataRows = dataRows;
        }

        public IActorRef DataRows { get; }
    }
}
