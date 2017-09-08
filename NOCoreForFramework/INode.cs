using System;
using System.Collections.Generic;

namespace NetworkObservabilityCore
{
	/// <summary>
	/// **INode** is an interface working with <see cref="IEdge"/> and <see cref="IGraph"/>.
	/// A type implements this interface plays the role of vertex in a [**Graph**](https://en.wikipedia.org/wiki/Graph_(abstract_data_type))
	/// </summary>
	public interface INode : IConstrainable, IEquatable<INode>
	{
		/// <summary>
		/// Every Node type has its unique Id.
		/// This Id is also used to calculate [hash code](https://docs.microsoft.com/en-us/dotnet/api/system.object.gethashcode?view=netframework-4.6.1#System_Object_GetHashCode).
		/// > [!Note] 
		/// > Note that every type that implements **INode** should
		/// > always use <see cref="NetworkObservabilityCore.Utils.IdGenerator"/> to get an Id.
		/// </summary>
		String Id { get; }

		/// <summary>
		/// **Label** is a property that allow user to assign
		/// a nickname whatever he want.
		/// </summary>
		String Label { get; set; }

		/// <summary>
		/// Get a collection of <see cref="IEdge"/> connecting from
		/// this node to other nodes.
		/// </summary>
		List<IEdge> ConnectOut { get; set; }

		/// <summary>
		/// Get a collection of <see cref="IEdge"/> connecting from
		/// other nodes to this node.
		/// </summary>
		List<IEdge> ConnectIn { get; set; }

		/// <summary>
		/// Indicates if this node contains observer.
		/// </summary>
		bool IsObserver { get; set; }

		/// <summary>
		/// Indicates if this node is being observed
		/// while being observer at the same time.
		/// </summary>
		bool IsObserverInclusive { get; set; }
		
		/// <summary>
		/// Indicates if this node is visible to observer.
		/// </summary>
		bool IsVisible { get; set; }

		/// <summary>
		/// Indicates if the connection through this node is 
		/// being blocked.
		/// </summary>
		bool IsBlocked { get; set; }
	}
}
