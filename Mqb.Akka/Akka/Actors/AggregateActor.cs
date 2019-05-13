using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mqb.Akka.Actors
{
    public class AggregateActor : ReceiveActor
    {
        #region Static Methods

        public static string GetUniqueName() => Path.GetUniqueName(ConstantsAkka.AGGREGATE);

        #endregion

        #region Command Definitions

        public abstract class BaseGetCmd : Cmd
        {
            public BaseGetCmd(IActorRef requestor,
                object originalCommand,
                ISet<IActorRef> targets,
                object targetCommand,
                Type expectedResponseType) : this(requestor, originalCommand, targets, targetCommand, expectedResponseType, 10, true)
            {
            }
            public BaseGetCmd(IActorRef requestor,
                object originalCommand,
                ISet<IActorRef> targets,
                object targetCommand,
                Type expectedResponseType,
                int timeoutSeconds,
                bool tellRequestor) : this(requestor, originalCommand, targets.Count, expectedResponseType, timeoutSeconds, tellRequestor)
            {
                Targets = targets;
                TargetCommand = targetCommand;
            }
            public BaseGetCmd(IActorRef requestor,
                object originalCommand,
                int targetCount,
                Type expectedResponseType) : this (requestor, originalCommand, targetCount, expectedResponseType, 10, true)
            {
            }
            public BaseGetCmd(
                IActorRef requestor, 
                object originalCommand, 
                int targetCount,
                Type expectedResponseType,
                int timeoutSeconds,
                bool tellRequestor)
            {
                Requestor = requestor;
                OriginalCommand = originalCommand;
                TargetCount = targetCount;
                ExpectedResponseType = expectedResponseType;
                TimeoutSeconds = timeoutSeconds;
                TellRequestor = tellRequestor;
            }

            public IActorRef Requestor { get; }
            public object OriginalCommand { get; }
            public int TargetCount { get; }
            public ISet<IActorRef> Targets { get; }
            public object TargetCommand { get; }
            public Type ExpectedResponseType { get; }
            public int TimeoutSeconds { get; }
            public bool TellRequestor { get; }
        }
        public abstract class BaseGetAddCmd : Cmd
        {
            public BaseGetAddCmd(IActorRef target, object cmd)
            {
                Target = target;
                Cmd = cmd;
            }

            public IActorRef Target { get; }
            public object Cmd { get; }
        }
        public class GetAll : BaseGetCmd
        {
            public GetAll(
                IActorRef requestor,
                object originalCommand,
                int targetCount,
                Type expectedResponseType) :
                base(requestor, originalCommand, targetCount, expectedResponseType)
            {
            }
            public GetAll(
                IActorRef requestor,
                object originalCommand,
                int targetCount,
                Type expectedResponseType,
                int timeoutSeconds, bool tellRequestor) :
                base(requestor, originalCommand, targetCount, expectedResponseType, timeoutSeconds, tellRequestor)
            {
            }
            public GetAll(
                IActorRef requestor, 
                object originalCommand, 
                ISet<IActorRef> targets, 
                object targetCommand, 
                Type expectedResponseType) : 
                base(requestor, originalCommand, targets, targetCommand, expectedResponseType)
            {
            }

            public GetAll(
                IActorRef requestor, 
                object originalCommand, 
                ISet<IActorRef> targets, 
                object targetCommand, 
                Type expectedResponseType, 
                int timeoutSeconds, bool tellRequestor) : 
                base(requestor, originalCommand, targets, targetCommand, expectedResponseType, timeoutSeconds, tellRequestor)
            {
            }
        }
        public class GetAllAdd : BaseGetAddCmd
        {
            public GetAllAdd(IActorRef target, object cmd) : base(target, cmd)
            {
            }
        }
        public class GetSingle : BaseGetCmd {
            public GetSingle(
                IActorRef requestor,
                object originalCommand,
                int targetCount,
                Type matchResponseType,
                Type noMatchResponseType) :
                base(requestor, originalCommand, targetCount, matchResponseType)
            {
                NoMatchResponseType = noMatchResponseType;
            }

            public GetSingle(
                IActorRef requestor,
                object originalCommand,
                int targetCount,
                Type matchResponseType,
                Type noMatchResponseType,
                int timeoutSeconds,
                bool tellRequestor) :
                base(requestor, originalCommand, targetCount, matchResponseType, timeoutSeconds, tellRequestor)
            {
                NoMatchResponseType = noMatchResponseType;
            }
            public GetSingle(
                IActorRef requestor,
                object originalCommand,
                ISet<IActorRef> targets,
                object targetCommand,
                Type matchResponseType,
                Type noMatchResponseType) :
                base(requestor, originalCommand, targets, targetCommand, matchResponseType)
            {
                NoMatchResponseType = noMatchResponseType;
            }

            public GetSingle(
                IActorRef requestor,
                object originalCommand,
                ISet<IActorRef> targets,
                object targetCommand,
                Type matchResponseType,
                Type noMatchResponseType,
                int timeoutSeconds,
                bool tellRequestor) :
                base(requestor, originalCommand, targets, targetCommand, matchResponseType, timeoutSeconds, tellRequestor)
            {
                NoMatchResponseType = noMatchResponseType;
            }

            public Type NoMatchResponseType { get; }
        }
        public class GetSingleAdd : BaseGetAddCmd
        {
            public GetSingleAdd(IActorRef target, object cmd) : base(target, cmd)
            {
            }
        }
        public class GetIf : BaseGetCmd
        {
            public GetIf(
                IActorRef requestor,
                object originalCommand,
                int targetCount,
                Type matchResponseType,
                Type noMatchResponseType) :
                base(requestor, originalCommand, targetCount, matchResponseType)
            {
                NoMatchResponseType = noMatchResponseType;
            }
            public GetIf(
                IActorRef requestor,
                object originalCommand,
                int targetCount,
                Type matchResponseType,
                Type noMatchResponseType,
                int timeoutSeconds,
                bool tellRequestor) :
                base(requestor, originalCommand, targetCount, matchResponseType, timeoutSeconds, tellRequestor)
            {
                NoMatchResponseType = noMatchResponseType;
            }
            public GetIf(
                IActorRef requestor, 
                object originalCommand, 
                ISet<IActorRef> targets, 
                object targetCommand,
                Type matchResponseType,
                Type noMatchResponseType) : 
                base(requestor, originalCommand, targets, targetCommand, matchResponseType)
            {
                NoMatchResponseType = noMatchResponseType;
            }
            public GetIf(
                IActorRef requestor, 
                object originalCommand, 
                ISet<IActorRef> targets, 
                object targetCommand, 
                Type matchResponseType,
                Type noMatchResponseType,
                int timeoutSeconds, 
                bool tellRequestor) : 
                base(requestor, originalCommand, targets, targetCommand, matchResponseType, timeoutSeconds, tellRequestor)
            {
                NoMatchResponseType = noMatchResponseType;
            }

            public Type NoMatchResponseType { get; }
        }
        public class GetIfAdd : BaseGetAddCmd
        {
            public GetIfAdd(IActorRef target, object cmd) : base(target, cmd)
            {
            }
        }
        public class ReduceTargetCount : Cmd
        {
            public ReduceTargetCount() : this(1) { }
            public ReduceTargetCount(int reduction)
            {
                Reduction = reduction;
            }

            public int Reduction { get; }
        }

        #endregion

        #region Event Definitions

        public class GetAllCompletedEvnt : Evnt
        {
            public GetAllCompletedEvnt(GetAll cmd, IEnumerable<object> results)
            {
                Cmd = cmd;
                Results = results;
            }

            public GetAll Cmd { get; }
            public IEnumerable<object> Results { get; }
        }
        public class GetIfCompletedEvnt : Evnt {
            public GetIfCompletedEvnt(GetIf cmd, IEnumerable<object> results)
            {
                Cmd = cmd;
                Results = results;
            }

            public GetIf Cmd { get; }
            public IEnumerable<object> Results { get; }
        }
        public class GetSingleCompletedEvnt : Evnt {
            public GetSingleCompletedEvnt(GetSingle cmd, object result)
            {
                Cmd = cmd;
                Result = result;
            }

            public GetSingle Cmd { get; }
            public object Result { get; }
        }
        
        #endregion

        public AggregateActor()
        {
            Become(Ready);
        }

        #region Properties

        public IActorRef Owner { get; protected set; }
        public IActorRef Requestor { get; protected set; }
        public object OriginalCommand { get; protected set; }
        public int TargetCount { get; protected set; }
        public int RemainingCount { get; protected set; }
        public ISet<IActorRef> Targets { get; protected set; }
        public object TargetCommand { get; protected set; }
        public Type ExpectedResponseType { get; protected set; }
        public Type NoMatchResponseType { get; protected set; }
        public int TimeoutSeconds { get; protected set; }
        public bool TellRequestor { get; protected set; }
        public List<object> Responses { get; set; } = new List<object>();
        public IEnumerable<object> ResponsesOut { get { return Responses; } }

        protected GetAll GetAllMsg { get; set; }
        protected GetIf GetIfMsg { get; set; }
        protected GetSingle GetSingleMsg { get; set; }

        #endregion

        #region State Definitions

        private void Ready()
        {
            // configure from requesting actor
            Receive<GetAll>(cmd => GetAllCmd(cmd, true));
            Receive<GetIf>(cmd => GetIfCmd(cmd, true));
            Receive<GetSingle>(cmd => GetSingleCmd(cmd, true));
        }
        private void Aggregating()
        {
            // query targets with message if all provided
            if (TargetCount > 0)
                if (Targets != null)
                    foreach (var t in Targets) t.Tell(TargetCommand);
            else
                Become(Replying);

            // set timeout
            SetReceiveTimeout(TimeSpan.FromSeconds(TimeoutSeconds));

            // handle timeout message
            Receive<ReceiveTimeout>(cmd => Become(Replying));

            // incrementing target count and targets dynamically
            if (Targets == null)
            {
                Targets = new HashSet<IActorRef>();

                if (GetAllMsg != null)
                {
                    Receive<GetAll>(cmd => GetAllCmd(cmd, false));
                    Receive<GetAllAdd>(cmd => AddAndTell(cmd.Target, cmd.Cmd));
                }
                else if (GetIfMsg != null)
                {
                    Receive<GetIf>(cmd => GetIfCmd(cmd, false));
                    Receive<GetIfAdd>(cmd => AddAndTell(cmd.Target, cmd.Cmd));
                }   
                else if (GetSingleMsg != null)
                {
                    Receive<GetSingle>(cmd => GetSingleCmd(cmd, false));
                    Receive<GetSingleAdd>(cmd => AddAndTell(cmd.Target, cmd.Cmd));
                }   

                Receive<ReduceTargetCount>(cmd => ReduceTargetCountCmd(cmd));
            }

            // receive responses of the expected match type or expected no match type
            ReceiveAny(cmd =>
            {
                if (cmd.GetType() == ExpectedResponseType)
                {
                    if (Targets.Remove(Sender))
                    {
                        Responses.Add(cmd);
                        RemainingCount -= 1;
                    }
                }
                else if (cmd.GetType() == NoMatchResponseType)
                {
                    if (Targets.Remove(Sender))
                    {
                        RemainingCount -= 1;
                    }
                }
                else
                {
                    Unhandled(cmd);
                    return;
                }

                if (RemainingCount < 1) Become(Replying);
            });
        }
        private void Replying()
        {
            if (GetSingleMsg == null)
                Reply();
            else
                ReplySingle();

            Context.Stop(Self);
        }

        #endregion

        #region Command Handlers

        private void GetAllCmd(GetAll cmd, bool firstExecution)
        {
            if (firstExecution)
            {
                Owner = Sender;
                Requestor = cmd.Requestor;
                OriginalCommand = cmd.OriginalCommand;
                TargetCount = cmd.TargetCount;
                RemainingCount = cmd.TargetCount;
                Targets = cmd.Targets;
                TargetCommand = cmd.TargetCommand;
                ExpectedResponseType = cmd.ExpectedResponseType;
                TimeoutSeconds = cmd.TimeoutSeconds;
                TellRequestor = cmd.TellRequestor;
                
                GetAllMsg = cmd;

                Become(Aggregating);
            }
            else
            {
                TargetCount += cmd.TargetCount;
                RemainingCount += cmd.TargetCount;
            }
        }
        private void GetIfCmd(GetIf cmd, bool firstExecution)
        {
            if (firstExecution)
            {
                Owner = Sender;
                Requestor = cmd.Requestor;
                OriginalCommand = cmd.OriginalCommand;
                TargetCount = cmd.TargetCount;
                RemainingCount = cmd.TargetCount;
                Targets = cmd.Targets;
                TargetCommand = cmd.TargetCommand;
                ExpectedResponseType = cmd.ExpectedResponseType;
                NoMatchResponseType = cmd.NoMatchResponseType;
                TimeoutSeconds = cmd.TimeoutSeconds;
                TellRequestor = cmd.TellRequestor;

                GetIfMsg = cmd;

                Become(Aggregating);
            }
            else
            {
                TargetCount += cmd.TargetCount;
                RemainingCount += cmd.TargetCount;
            }   
        }
        private void GetSingleCmd(GetSingle cmd, bool firstExecution)
        {
            if (firstExecution)
            {
                Owner = Sender;
                Requestor = cmd.Requestor;
                OriginalCommand = cmd.OriginalCommand;
                TargetCount = cmd.TargetCount;
                RemainingCount = cmd.TargetCount;
                Targets = cmd.Targets;
                TargetCommand = cmd.TargetCommand;
                ExpectedResponseType = cmd.ExpectedResponseType;
                NoMatchResponseType = cmd.NoMatchResponseType;
                TimeoutSeconds = cmd.TimeoutSeconds;
                TellRequestor = cmd.TellRequestor;

                GetSingleMsg = cmd;

                Become(Aggregating);
            }
            else
            {
                TargetCount += cmd.TargetCount;
                RemainingCount += cmd.TargetCount;
            }   
        }
        private void ReduceTargetCountCmd(ReduceTargetCount cmd)
        {
            RemainingCount -= cmd.Reduction;

            if (RemainingCount < 1)
                Become(Replying);
        }

        #endregion

        #region Utility Methods

        private void AddAndTell(IActorRef actorRef, object cmd)
        {
            Targets.Add(actorRef);
            actorRef.Tell(cmd);
        }
        private void Reply()
        {
            if (TellRequestor)
                Requestor.Tell(ResponsesOut, Owner);
            else
                if (GetAllMsg != null)
                    Owner.Tell(new GetAllCompletedEvnt(GetAllMsg, ResponsesOut));
                else
                    Owner.Tell(new GetIfCompletedEvnt(GetIfMsg, ResponsesOut));
        }
        private void ReplySingle()
        {
            if (TellRequestor)
                Requestor.Tell(Responses.FirstOrDefault(), Owner);
            else
                Owner.Tell(new GetSingleCompletedEvnt(GetSingleMsg, Responses.FirstOrDefault()));
        }

        #endregion
    }
}
