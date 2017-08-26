using System;
using System.Collections.Generic;
using System.Text;

namespace NetworkObservabilityCore
{
	public class Edge : IEdge
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

		public bool IsBlocked
		{
			get;
			set;
		}

		public double Weight
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
		public Edge()
			: this(1)
		{
		}

		public Edge(int weight)
		{
			Id = IdGenerator.GenerateEdgeIndex();
			Label = Id;
			Weight = weight;
			IsBlocked = false;
			Attributes = new Dictionary<string, IComparable>();
		}
		#endregion

		public IComparable this[String key]
		{
			get
			{
				return Attributes[key];
			}
			set
			{
				Attributes[key] = value;
			}
		}

		public bool HasAttribute(String name)
		{
			return Attributes.ContainsKey(name);
		}

		public override string ToString()
		{
			return String.Format("{0}: {1}, From {2} To {3}", Id, Label, From.Id, To.Id);
		}

		public override int GetHashCode()
		{
			return Id.GetHashCode();
		}

		bool IEquatable<IEdge>.Equals(IEdge other)
		{
			return Id == other.Id;
		}
	}
}
