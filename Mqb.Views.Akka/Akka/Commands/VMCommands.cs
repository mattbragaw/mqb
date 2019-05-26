using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Akka
{
    public class VMCommands
    {
        public class SetVM
        {
            public SetVM(object vm)
            {
                Vm = vm;
            }

            public object Vm { get; }
        }
        public class ChangeProperty : Cmd
        {
            public ChangeProperty(string name, object valueOld, object valueNew)
            {
                Name = name;
                ValueOld = valueOld;
                ValueNew = valueNew;
            }

            public string Name { get; }
            public object ValueOld { get; }
            public object ValueNew { get; }
        }
        public class PropertyChanged : Evnt
        {
            public PropertyChanged(string name, object valueOld, object valueNew)
            {
                Name = name;
                ValueOld = valueOld;
                ValueNew = valueNew;
            }

            public string Name { get; }
            public object ValueOld { get; }
            public object ValueNew { get; }
        }
    }
}
