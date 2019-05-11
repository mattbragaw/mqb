using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Akka.Actors
{
    public class RequestActor : ReceiveActor
    {
        #region Static Methods

        public static string GetUniqueName() => Path.GetUniqueName(ConstantsAkka.REQUEST);

        #endregion

        #region Command Definitions

        public class Request : Cmd
        {
            public Request(IActorRef requestor, object requestorMessage, IActorRef target, object targetMessage) : 
                this(requestor, requestorMessage, target, targetMessage, 10) { }
            public Request(IActorRef requestor, object requestorMessage, IActorRef target, object targetMessage, int timeoutSeconds)
            {
                Requestor = requestor;
                RequestorMessage = requestorMessage;
                Target = target;
                TargetMessage = targetMessage;
                TimeoutSeconds = timeoutSeconds;
            }

            public IActorRef Requestor { get; }
            public object RequestorMessage { get; }
            public IActorRef Target { get; }
            public object TargetMessage { get; }
            public int TimeoutSeconds { get; }
        }

        #endregion

        #region Event Definitions

        public class RequestCompleted : Evnt
        {
            public RequestCompleted(Request request, object result)
            {
                Request = request;
                Result = result;
            }

            public Request Request { get; }
            public object Result { get; }
        }
        public class RequestTimedOut : Evnt
        {
            public RequestTimedOut(Request request)
            {
                Request = request;
            }

            public Request Request { get; }
        }

        #endregion

        public RequestActor()
        {
            Become(Starting);
        }

        #region Properties

        protected IActorRef Owner { get; set; }
        protected Request RequestMsg { get; set; }

        #endregion

        #region State Methods

        private void Starting()
        {
            Receive<Request>(cmd =>
            {
                Owner = Sender;
                RequestMsg = cmd;

                Become(Requesting);
            });
        }
        private void Requesting()
        {
            // set timeout if no response
            SetReceiveTimeout(TimeSpan.FromSeconds(RequestMsg.TimeoutSeconds));

            // tell target messsage
            RequestMsg.Target.Tell(RequestMsg.TargetMessage);

            // handle messages
            Receive<ReceiveTimeout>(cmd =>
            {
                Owner.Tell(new RequestTimedOut(RequestMsg));

                Context.Stop(Self);
            });
            ReceiveAny(responseMsg =>
            {
                Owner.Tell(new RequestCompleted(RequestMsg, responseMsg));

                Context.Stop(Self);
            });
        }

        #endregion
    }
}
