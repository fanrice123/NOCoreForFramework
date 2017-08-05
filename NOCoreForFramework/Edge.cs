using System;
using System.Collections.Generic;
using System.Text;

namespace NetworkObservabilityCore
{
    public class Edge : IEdge
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

		public int Weight
		{
			get;
			set;
		}

		public INode From
		{
			get;
			set;
		}

		public INode To
		{
			get;
			set;
		}

		#endregion

		#region Constructors
		public Edge()
			: this(1)
		{
		}

		public Edge(int weight)
		{
			Id = IdGenerator.GenerateEdgeIndex();
			Label = Id;
			Weight = weight;
		}
		#endregion

		public override string ToString()
		{
			return String.Format("{0}: {1}, From {2} To {3}", Id, Label, From.Id, To.Id);
		}

		public override int GetHashCode()
		{
			return Id.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			return Id == (obj as Edge).Id;
		}

		bool IEquatable<IEdge>.Equals(IEdge other)
		{
			return other is Edge && Equals(other);
		}
	}
}
