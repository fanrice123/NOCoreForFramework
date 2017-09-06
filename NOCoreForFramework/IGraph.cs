using System;
using System.Collections.Generic;

namespace NetworkObservabilityCore
{

	/// <summary>
	/// An arbitrary Graph type should always implement this interface.
	/// </summary>
	public interface IGraph : ICollection<INode>
	{
		/// <summary>
		/// This property is a collection of <see cref="INode"/> of **IGraph**
		/// </summary>
		Dictionary<String, INode> AllNodes { get; }

		Dictionary<String, IEdge> AllEdges { get; }

		/// <summary>
		/// This method connects 2 nodes with a directed edge.
		/// </summary>
		/// <param name="from">A node where a directed edge starts from.</param>
		/// <param name="to">A node where a directed edge connects to.</param>
		/// <param name="edge">A directed edges connects up <paramref name="from"/> and <paramref name="to"/></param>
		void ConnectNodeToWith(INode from, INode to, IEdge edge);

		/// <summary>
		/// This methods removes an edge from **IGraph**.
		/// </summary>
		/// <param name="edge"></param>
		/// <returns></returns>
		bool Remove(IEdge edge);

    }
}
