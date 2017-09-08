using System;
using System.Collections.Generic;

namespace NetworkObservabilityCore
{

	/// <summary>
	/// **Graph** is an [**ADT**](https://en.wikipedia.org/wiki/Graph_(abstract_data_type))
	/// This **Graph** is implemented using [adjacency list](https://en.wikipedia.org/wiki/Adjacency_list)
	/// representation. this ADT contains vertex and arc.
	/// See also vertex representation <see cref="INode"/> and
	/// arc representation <see cref="IEdge"/>.
	/// </summary>
	public class Graph : IGraph
    {

		#region PrivateFields
		/// <summary>
		/// The field of AllNodes.
		/// See <see cref="AllNodes"/>
		/// </summary>
		protected internal Dictionary<String, INode> allNodes;

		/// <summary>
		/// The field of AllEdges.
		/// See <see cref="AllEdges"/>
		/// </summary>
		protected internal Dictionary<String, IEdge> allEdges;
		#endregion

		#region Constructors

		/// <summary>
		/// The constructor of **Graph**.
		/// <see cref="AllNodes"/> and <see cref="AllEdges"/> will be initialised
		/// when this constructor being called.
		/// are initialised.
		/// </summary>
		public Graph()
		{
			allNodes = new Dictionary<string, INode>();
			allEdges = new Dictionary<string, IEdge>();
		}
		#endregion

		#region Properties
		/// <inheritdoc />
		public IReadOnlyDictionary<String, INode> AllNodes => allNodes;

		/// <inheritdoc />
		public IReadOnlyDictionary<String, IEdge> AllEdges => allEdges;

		/// <inheritdoc />
		public int NodeCount => allNodes.Count;

		/// <inheritdoc />
		public int EdgeCount => allNodes.Count;

		/// <inheritdoc />
		public IEnumerable<INode> NodeEnumerable => allNodes.Values;

		/// <inheritdoc />
		public IEnumerable<IEdge> EdgeEnumerable => allEdges.Values;
		#endregion

		#region Methods
		/// <summary>
		/// By calling this method, a node is being added into **Graph**.
		/// </summary>
		/// <param name="node"></param>
		/// <exception cref="InvalidOperationException">
		/// Thrown when node is added already added into **Graph**.
		/// </exception>
		/// <exception cref="ArgumentNullException">
		/// Thrown when argument is null value.</exception>
		public void Add(INode node)
		{
			if (node == null)
				throw new ArgumentNullException();
			if (allNodes.ContainsKey(node.Id))
				throw new InvalidOperationException("Item already existed in Graph.");
			allNodes[node.Id] = node;
		}

		/// <summary>
		/// This method connects 2 nodes with a directed edge.
		/// </summary>
		/// <param name="from">A node where a directed edge starts from.</param>
		/// <param name="to">A node where a directed edge connects to.</param>
		/// <param name="edge">A directed edges connects up <paramref name="from"/> and <paramref name="to"/></param>
		/// <exception cref="ArgumentNullException">Thrown if one of arguments is null</exception>
		/// <exception cref="InvalidOperationException">Thrown if :
		///		* **from** or **to** Node does not exist in graph.
		///		* The **edge** found in **Graph**.
		/// </exception>
		public void ConnectNodeToWith(INode from, INode to, IEdge edge)
		{
			if (from == null || to == null || edge == null)
				throw new ArgumentNullException();
			if (!Contains(from) || !Contains(to) || Contains(edge))
				throw new InvalidOperationException("Node does not belong to Graph.");
			allEdges[edge.Id] = edge;
			from.ConnectOut.Add(edge);
			to.ConnectIn.Add(edge);
			edge.From = from;
			edge.To = to;
		}

		/// <inheritdoc />
		public bool Contains(INode node)
		{
			return allNodes.ContainsKey(node.Id);
		}

		/// <inheritdoc />
		public bool Contains(IEdge edge)
		{
			return allEdges.ContainsKey(edge.Id);
		}


		/// <inheritdoc />
		public void Clear()
		{
			allNodes.Clear();
			allEdges.Clear();
		}

		/// <inheritdoc />
		public bool Remove(INode item)
		{
			if (allNodes.ContainsKey(item.Id))
			{
				item.ConnectIn.ForEach(edge => edge.From.ConnectOut.Remove(edge));
				item.ConnectOut.ForEach(edge => edge.To.ConnectIn.Remove(edge));
				item.ConnectIn.Clear();
				item.ConnectOut.Clear();
				return allNodes.Remove(item.Id);
			}
			else
			{
				return false;
			}
		}

		/// <inheritdoc />
		public bool Remove(IEdge edge)
		{
			if (allEdges.ContainsKey(edge.Id))
			{
				edge.From.ConnectOut.Remove(edge);
				edge.To.ConnectIn.Remove(edge);
				return allEdges.Remove(edge.Id);
			}
			else
			{
				return false;
			}
		}
		#endregion
	}
}
