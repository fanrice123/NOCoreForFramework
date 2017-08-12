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

		List<IEdge> Neighbours { get; set; }

		List<IEdge> ConnectFrom { get; set; }

		bool IsObserver { get; set; }

		bool IsObserverInclusive { get; set; }
		
		bool IsVisible { get; set; }

	}
}
