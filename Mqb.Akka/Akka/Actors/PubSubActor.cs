using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Akka.Actors
{
    public class PubSubActor : ReceiveActor
    {
        #region Command Definitions

        public class RegisterOwner : Cmd { }
        public class Subscribe : Cmd { }
        public class Unsubscribe : Cmd { }

        #endregion

        public PubSubActor()
        {
            Become(Starting);
        }

        #region Properties

        public IActorRef Owner { get; protected set; }
        public HashSet<IActorRef> Subscribers = new HashSet<IActorRef>();

        #endregion

        #region State Methods

        private void Starting()
        {
            Receive<RegisterOwner>(cmd =>
            {
                Owner = Sender;
                Become(Ready);
            });
        }
        private void Ready()
        {
            Receive<Subscribe>(cmd => Subscribers.Add(Sender));
            Receive<Unsubscribe>(cmd => Subscribers.Remove(Sender));
            ReceiveAny(cmd =>
            {
                TellSubscribers(cmd);
            });
        }

        #endregion

        #region Utility Methods

        private void TellSubscribers(object msg)
        {
            foreach (var subscriber in Subscribers) subscriber.Tell(msg, Owner);
        }

        #endregion
    }
}
