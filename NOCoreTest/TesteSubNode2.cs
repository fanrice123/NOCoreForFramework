using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetworkObservabilityCore;

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

		public List<IEdge> Links
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

		public TesteSubNode2()
		{
			Id = IdGenerator.GenerateNodeIndex();
			Label = Id;
			Links = new List<IEdge>();
			ConnectFrom = new List<IEdge>();
			IsObserver = IsObserverInclusive = false;
		}

		public bool Equals(INode other)
		{
			return Id == other.Id;
		}

		public override int GetHashCode()
		{
			return Id.GetHashCode();
		}
	}
}
