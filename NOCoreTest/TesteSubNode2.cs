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
		
		public bool IsObserverInclusive {
			get;
			set;
		}

		public TesteSubNode2()
		{
			Id = IdGenerator.GenerateNodeIndex();
			Label = Id;
			Links = new HashSet<IEdge>();
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
