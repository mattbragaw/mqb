using Akka.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Akka.Actors
{
    public abstract class AutoSnapshotActor : ReceivePersistentActor
    {
        #region Commands
        
        public class SetEventsBetweenSnapshots
        {
            public SetEventsBetweenSnapshots(int eventCount)
            {
                EventCount = eventCount;
            }

            public int EventCount { get; }
        }
        
        #endregion

        #region State Definition

        public abstract class PersistentState
        {
            public int EventCountToSnapshot { get; set; } = 10;
            public int EventsSinceSnapshot { get; set; } = 0;
        }

        #endregion
    }
    public abstract class AutoSnapshotActor<TState> : AutoSnapshotActor
        where TState : AutoSnapshotActor.PersistentState, new()
    {

        public AutoSnapshotActor()
        {
            HandleSnapshotEvents();
            HandleSnapshotConfigurationCommands();
        }

        protected void HandleSnapshotEvents()
        {
            Recover<SnapshotOffer>(evnt => State = (TState)evnt.Snapshot);
        }
        protected void HandleSnapshotConfigurationCommands()
        {
            Command<SetEventsBetweenSnapshots>(cmd => State.EventCountToSnapshot = cmd.EventCount);
        }

        public TState State { get; set; } = new TState();

        protected void PersistAndTrack<T>(T evnt, Action<T> actionAfterPersist)
        {
            Persist(evnt, actionAfterPersist);

            State.EventsSinceSnapshot++;

            if (State.EventsSinceSnapshot >= State.EventCountToSnapshot)
            {
                State.EventsSinceSnapshot = 0;

                SaveSnapshot(State);
            }
        }
    }
}
