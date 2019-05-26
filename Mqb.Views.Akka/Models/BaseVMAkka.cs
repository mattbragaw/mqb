using Akka.Actor;
using Mqb.Akka;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Mqb.Models
{
    public abstract class BaseVMAkka : BaseVMMutable
    {
        public BaseVMAkka() : this(ActorRefs.Nobody) { }
        public BaseVMAkka(IActorRef vmActor)
        {
            VmActor = vmActor;
        }

        private IActorRef _vmActor;
        public IActorRef VmActor
        {
            get { return _vmActor; }
            set { _vmActor = value; VmActorSet(); }
        }

        public override void OnPropertyChanged(object sender, PropertyChangedEventArgs args, object oldValue, object newValue)
        {
            // notify actor system
            if (VmActor != ActorRefs.Nobody)
                VmActor.Tell(new VMCommands.ChangeProperty(args.PropertyName, oldValue, newValue));

            // notify ui elements
            base.OnPropertyChanged(sender, args, oldValue, newValue);
        }

        protected virtual void VmActorSet()
        {
            if (VmActor != ActorRefs.Nobody)
                VmActor.Tell(new VMCommands.SetVM(GetVM()));
        }

        protected abstract object GetVM();
    }
}
