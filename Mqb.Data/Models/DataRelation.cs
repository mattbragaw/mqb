using Mqb.Descriptors;
using Mqb.Descriptors.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Models
{
    public class DataRelation : DataTypeChild, IDataRelation
    {
        public DataRelation(
            string id,
            string dataTypeId,
            string dataTypeIdForeign,
            string name,
            string nameSystem,
            string nameForeign,
            string nameSystemForeign,
            DataRelationType dataRelationType,
            long min,
            long? max,
            long minForeign,
            long? maxForeign) : 
            this(
                id,
                dataTypeId,
                dataTypeIdForeign,
                name,
                nameSystem,
                nameForeign,
                nameSystemForeign,
                dataRelationType,
                min,
                max,
                minForeign,
                maxForeign,
                null,
                null)
        {
        }
        public DataRelation(
            string id, 
            string dataTypeId, 
            string dataTypeIdForeign, 
            string name,
            string nameSystem,
            string nameForeign,
            string nameSystemForeign,
            DataRelationType dataRelationType,
            long min,
            long? max,
            long minForeign,
            long? maxForeign,
            IDataType dataType, 
            IDataType dataTypeForeign) : 
            base(id, dataTypeId, dataType)
        {
            DataTypeIdForeign = dataTypeIdForeign;
            Name = name;
            NameSystem = nameSystem;
            NameForeign = nameForeign;
            NameSystemForeign = nameSystemForeign;
            DataRelationType = dataRelationType;
            Min = min;
            Max = max;
            MinForeign = minForeign;
            MaxForeign = maxForeign;
            DataTypeForeign = dataTypeForeign;
        }

        public string DataTypeIdForeign { get; }
        public string Name { get; }
        public string NameSystem { get; }
        public string NameForeign { get; }
        public string NameSystemForeign { get; }
        public DataRelationType DataRelationType { get; }
        public long Min { get; }
        public long? Max { get; }
        public long MinForeign { get; }
        public long? MaxForeign { get; }
        public IDataType DataTypeForeign { get; }
    }
    public class DataRelationMutable : DataTypeChildMutable, IDataRelationMutable
    {
        public virtual string DataTypeIdForeign { get; set; }
        public virtual string Name { get; set; }
        public virtual string NameSystem { get; set; }
        public virtual string NameForeign { get; set; }
        public virtual string NameSystemForeign { get; set; }
        public virtual DataRelationType DataRelationType { get; set; }
        public virtual long Min { get; set; }
        public virtual long? Max { get; set; }
        public virtual long MinForeign { get; set; }
        public virtual long? MaxForeign { get; set; }
        public virtual IDataTypeMutable DataTypeForeign { get; set; }
    }
}
