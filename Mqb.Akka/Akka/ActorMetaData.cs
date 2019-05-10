using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Akka
{
    /// <summary>
    /// Meta-data class. Nested/child actors can build path 
    /// based on their parent(s) / position in hierarchy.
    /// Adapter from Petabridge class: https://github.com/petabridge/akkadotnet-helpers/blob/master/AkkaHelpers/ActorMetaData.cs
    /// </summary>
    public class ActorMetaData
    {
        public ActorMetaData(ActorMetaData parent = null) : this(string.Empty, parent) { }
        public ActorMetaData(string name, ActorMetaData parent = null)
        {
            if (!string.IsNullOrEmpty(name))
                Name = name;

            PlaceholderCount = parent != null ? parent.PlaceholderCount : 0;
            if (string.IsNullOrEmpty(name))
            {
                PlaceholderCount++;
                Name = string.Concat("{", PlaceholderCount - 1, "}");
            }

            Parent = parent;

            // if no parent, we assume a top-level actor
            var parentPath = parent != null ? parent.Path : string.Format("/{0}", Constants.USER_GUARDIAN);

            Path = string.Format("{0}/{1}", parentPath, Name);
        }

        public string Name { get; }
        public ActorMetaData Parent { get; }
        public string Path { get; }
        public int PlaceholderCount { get; }
    }
    public class RootActorMetaData : ActorMetaData
    {
        public RootActorMetaData(string name) : base(name, null)
        {
        }
    }
    
}
