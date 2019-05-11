using Mqb.Descriptors.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Models
{
    public class DataProperty : DataTypeChild, IDataProperty
    {
        public DataProperty(string id, bool system, string dataTypeId, int index, string name, string nameSystem, Type valueType, bool required) : 
            this(id, system, dataTypeId, index, name, nameSystem, valueType, required, null)
        {
        }

        public DataProperty(string id, bool system, string dataTypeId, int index, string name, string nameSystem, Type valueType, bool required, IDataType dataType) : 
            base(id, dataTypeId, dataType)
        {
            System = system;
            Index = index;
            Name = name;
            NameSystem = nameSystem;
            ValueType = valueType;
            Required = required;
        }

        public bool System { get; }
        public int Index { get; }
        public string Name { get; }
        public string NameSystem { get; }
        public Type ValueType { get; }
        public bool Required { get; }
    }
    public class DataPropertyMutable : DataTypeChildMutable, IDataPropertyMutable
    {
        public virtual bool System { get; set; }
        public virtual int Index { get; set; }
        public virtual string Name { get; set; }
        public virtual string NameSystem { get; set; }
        public virtual Type ValueType { get; set; }
        public virtual bool Required { get; set; }
    }
}
