using System;

namespace NetworkObservabilityCore
{
	/// <summary>
	/// **IEdge** is an interface working with <see cref="INode"/> and <see cref="IGraph"/>.
	/// A type implements this interface plays the role of arc in a [**Graph**](https://en.wikipedia.org/wiki/Graph_(abstract_data_type))
	/// </summary>
	public interface IEdge : IConstrainable, IEquatable<IEdge>
	{
		/// <summary>
		/// Every Edge type has its unique Id.
		/// This Id is also used to calculate [hash code](https://docs.microsoft.com/en-us/dotnet/api/system.object.gethashcode?view=netframework-4.6.1#System_Object_GetHashCode).
		/// > [!Note] 
		/// > Note that every type that implements **IEdge** should
		/// > always use <see cref="NetworkObservabilityCore.Utils.IdGenerator"/> to get an Id.
		/// </summary>
		String Id { get; }

		/// <summary>
		/// **Label** is a property that allow user to assign
		/// a nickname whatever he want.
		/// </summary>
		String Label { get; set; }

		/// <summary>
		/// Gets <see cref="INode"/> where a connection starts from.
		/// </summary>
		INode From { get; set; }

		/// <summary>
		/// Gets <see cref="INode"/> where a connection links to.
		/// </summary>
		INode To { get; set; }

		/// <summary>
		/// Indicates if the connection through this edge is 
		/// being blocked.
		/// </summary>
		bool IsBlocked { get; set; }

		/// <summary>
		/// This property has been deprecated.
		/// > [!Warning]
		/// > This property is about to be removed in the coming release.
		/// </summary>
		double Weight { get; set; }
	}
}
