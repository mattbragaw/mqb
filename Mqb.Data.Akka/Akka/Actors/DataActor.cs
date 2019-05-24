using Akka.Actor;
using Mqb.Descriptors.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Akka.Actors
{
    public class DataActor : ReceiveActor
    {
        public DataActor(IDataRefsMutable dataRefsMutable)
        {
            DataRefsMutable = dataRefsMutable;

            // set data actor references
            dataRefsMutable.Data = Context.Self;
            dataRefsMutable.Entities = Context.ActorOf(Props.Create(() => new EntitiesActor()), EntitiesActor.GetName());
            dataRefsMutable.DataTypes = Context.ActorOf(Props.Create(() => new DataTypesActor(dataRefsMutable.Entities)), DataTypesActor.GetName());
            dataRefsMutable.DataRows = Context.ActorOf(Props.Create(() => new DataRowsActor(dataRefsMutable.Entities)), DataRowsActor.GetName());
            dataRefsMutable.DataRelations = Context.ActorOf(Props.Create(() => new DataRelationsActor(dataRefsMutable.Entities)), DataRelationsActor.GetName());
            dataRefsMutable.DataReferences = Context.ActorOf(Props.Create(() => new DataReferencesActor(dataRefsMutable.Entities)), DataReferencesActor.GetName());
            dataRefsMutable.Orgs = Context.ActorOf(Props.Create(() => new OrgsActor(dataRefsMutable.Entities)), OrgsActor.GetName());
            dataRefsMutable.Spaces = Context.ActorOf(Props.Create(() => new SpacesActor(dataRefsMutable.Entities)), SpacesActor.GetName());
        }

        public IDataRefsMutable DataRefsMutable { get; }
    }
}
