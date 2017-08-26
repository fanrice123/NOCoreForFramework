using System;
using System.Collections.Generic;
using System.Text;

namespace NetworkObservabilityCore
{
    public class Node : INode
    {

		#region Properties
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

		public List<IEdge> ConnectTo
		{
			get;
			set;
		}

		public List<IEdge> ConnectFrom
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

		public bool IsVisible
		{
			get;
			set;
		}

		public bool IsBlocked
		{
			get;
			set;
		}

		public Dictionary<string, IComparable> Attributes
		{
			get;
			protected set;
		}

		#endregion

		#region Constructors
		public Node()
		{
			Id = IdGenerator.GenerateNodeIndex();
			Label = Id;
			ConnectTo = new List<IEdge>();
			ConnectFrom = new List<IEdge>();
			IsObserver = IsObserverInclusive = IsBlocked = false;
			IsVisible = true;
			Attributes = new Dictionary<string, IComparable>();
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
			return obj is INode && Equals(obj as INode);
		}

		bool IEquatable<INode>.Equals(INode other)
		{
			return Id == other.Id;
		}

		public bool HasAttribute(string name)
		{
			return Attributes.ContainsKey(name);
		}
	}
}
