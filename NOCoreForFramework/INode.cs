using System;
using System.Collections.Generic;
using System.Text;

namespace NetworkObservabilityCore
{
	/// <summary>
	/// To Implement INode, you must create a field named id.
	/// </summary>
	public interface INode : IEquatable<INode>
	{
		String Id { get; }

		String Label { get; set; }

		HashSet<IEdge> Links { get; set; }

		bool IsObserver { get; set; }

		bool IsObserverInclusive { get; set; }

	}
}
