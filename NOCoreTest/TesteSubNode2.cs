using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetworkObservabilityCore;
using NetworkObservabilityCore.Utils;

namespace NOCoreTest
{
	class TesteSubNode2 : INode
	{
		public string Id
		{
			get;
			protected set;
		}

		public string Label
		{
			get;
			set;
		}

		public List<IEdge> ConnectOut
		{
			get;
			set;
		}

		public List<IEdge> ConnectIn
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

		public IDictionary<string, IComparable> Attributes
		{
			get;
			set;
		}

		public bool IsBlocked { get; set; }

		public double Value { get; set; }


		public TesteSubNode2()
		{
			Id = IdGenerator.GenerateNodeId();
			Label = Id;
			ConnectOut = new List<IEdge>();
			ConnectIn = new List<IEdge>();
			IsObserver = IsObserverInclusive = false;
			Attributes = new Dictionary<string, IComparable>();
		}

		public bool Equals(INode other)
		{
			return Id == other.Id;
		}

		public override int GetHashCode()
		{
			return Id.GetHashCode();
		}

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

		public bool HasAttribute(string name)
		{
			return Attributes.ContainsKey(name);
		}
	}
}
