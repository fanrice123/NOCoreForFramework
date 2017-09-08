using System;
using System.Collections.Generic;

namespace NetworkObservabilityCore
{

	/// <summary>
	/// An arbitrary Graph type should always implement this interface to work with
	/// <see cref="INode"/> and <see cref="IEdge"/>
	/// </summary>
	public interface IGraph
	{
		/// <summary>
		/// This property is a collection of <see cref="INode"/> of **Graph**.
		/// Use Node ID to retrieve the Node.
		/// </summary>
		/// <example>
		/// <code>
		/// public INode getNode(String id)
		/// {
		///		return graph.AllNodes[id];
		/// }
		/// </code>
		/// </example>
		IReadOnlyDictionary<String, INode> AllNodes { get; }

		/// <summary>
		/// This property is a collection of <see cref="IEdge"/> of **Graph**
		/// </summary>
		/// <example>
		/// <code>
		/// public INode getEdge(String id)
		/// {
		///		return graph.AllEdges[id];
		/// }
		/// </code>
		/// </example>
		IReadOnlyDictionary<String, IEdge> AllEdges { get; }

		/// <summary>
		/// Returns the number of **Node** in **Graph**.
		/// See also <seealso cref="INode"/>
		/// </summary>
		int NodeCount { get; }

		/// <summary>
		/// Returns the number of **Edge** in **Graph**.
		/// See also <seealso cref="IEdge"/>
		/// </summary>
		int EdgeCount { get; }

		/// <summary>
		/// Gets `IEnumerable` of <see cref="INode"/>.
		/// </summary>
		IEnumerable<INode> NodeEnumerable { get; }

		/// <summary>
		/// Gets `IEnumerable` of <see cref="IEdge"/>.
		/// </summary>
		IEnumerable<IEdge> EdgeEnumerable { get; }
		/// <summary>
		/// By calling this method, a node is being added into **Graph**.
		/// </summary>
		/// <param name="node">Node being added.</param>
		void Add(INode node);

		/// <summary>
		/// This method connects 2 nodes with a directed edge.
		/// </summary>
		/// <param name="from">A node where a directed edge starts from.</param>
		/// <param name="to">A node where a directed edge connects to.</param>
		/// <param name="edge">A directed edges connects up <paramref name="from"/> and <paramref name="to"/></param>
		void ConnectNodeToWith(INode from, INode to, IEdge edge);

		/// <summary>
		/// This method checks if **Graph** contains **node**.
		/// </summary>
		/// <param name="node"></param>
		/// <returns>Returns true if **Graph** contains the **node**.</returns>
		bool Contains(INode node);

		/// <summary>
		/// This method checks if **Graph** contains **edge**.
		/// </summary>
		/// <param name="edge"></param>
		/// <returns>Returns true if **Graph** contains the **edge**.</returns>
		bool Contains(IEdge edge);

		/// <summary>
		/// This method emptys all its nodes and edges.
		/// <see cref="AllNodes"/> and <see cref="AllEdges"/> will turn into empty 
		/// after this method being executed.
		/// </summary>
		void Clear();

		/// <summary>
		/// This methods removes an node from **Graph**.
		/// It also removes edges connecting to the given node.
		/// </summary>
		/// <param name="edge"></param>
		/// <returns>Returns true if the remove operation succeeded otherwise false.
		/// Thsi method returns false also if the node not found.</returns>
		bool Remove(INode node);

		/// <summary>
		/// This methods removes an edge from **Graph**.
		/// </summary>
		/// <param name="edge"></param>
		/// <returns>Returns true if the remove operation succeeded otherwise false.
		/// Thsi method returns false also if the edge not found.</returns>
		bool Remove(IEdge edge);

	}
}
