using System;
using System.Collections.Generic;
using System.Text;

namespace NetworkObservabilityCore
{
    public class Node : INode
    {

		#region Property
		public String Id
		{
			get;
			protected set;
		}

		public String Label
		{
			get;
			set;
		}

		public HashSet<IEdge> Links
		{
			get;
			set;
		}

		public bool IsObserver
		{
			get;
			set;
		}

		public bool IsObserverInclusive
		{
			get;
			set;
		}

		#endregion

		#region Constructors
		public Node()
		{
			Id = IdGenerator.GenerateNodeIndex();
			Label = Id;
			Links = new HashSet<IEdge>();
			IsObserver = IsObserverInclusive = false;
		}
		#endregion

		public override String ToString()
		{
			return String.Format("{0}: {1}", Id, Label);
		}

		public override int GetHashCode()
		{
			return Id.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			return Id == (obj as Node).Id;
		}

		bool IEquatable<INode>.Equals(INode other)
		{
			return other is Node && Equals(other);
		}
	}
}
