using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace NetworkObservabilityCore
{

    public class Graph : IGraph 
    {

		/// <inheritdoc />
		public Dictionary<String, INode> AllNodes
		{
			get;
			internal set;
		}

		/// <inheritdoc />
		public Dictionary<String, IEdge> AllEdges
		{
			get;
			internal set;
		}

		public int Count => AllNodes.Count;

		public bool IsReadOnly => false;

		public Graph()
		{
			AllNodes = new Dictionary<string, INode>();
			AllEdges = new Dictionary<string, IEdge>();
		}

		public void AddNode(INode node)
		{
			Add(node);
		}

		public void ConnectNodeToWith(INode from, INode to, IEdge edge)
		{
			AllEdges[edge.Id] = edge;
			from.ConnectTo.Add(edge);
			to.ConnectFrom.Add(edge);
			edge.From = from;
			edge.To = to;
		}

		public void Add(INode item)
		{
			if (AllNodes.ContainsKey(item.Id))
				throw new InvalidOperationException("Item already existed in Graph.");
			AllNodes[item.Id] = item;
		}

		public void Clear()
		{
			AllNodes.Clear();
			AllEdges.Clear();
		}

		public void CopyTo(INode[] array, int arrayIndex)
		{
			throw new NotImplementedException();
		}

		public bool Remove(IEdge edge)
		{
			if (AllEdges.ContainsKey(edge.Id))
			{
				edge.From.ConnectTo.Remove(edge);
				edge.To.ConnectFrom.Remove(edge);
				AllEdges.Remove(edge.Id);
				return true;
			}
			else
			{
				return false;
			}
		}

		public bool Remove(INode item)
		{
			if (AllNodes.ContainsKey(item.Id))
			{
				item.ConnectFrom.ForEach(edge => edge.From.ConnectTo.Remove(edge));
				item.ConnectTo.ForEach(edge => edge.To.ConnectFrom.Remove(edge));
				item.ConnectFrom.Clear();
				item.ConnectTo.Clear();
				AllNodes.Remove(item.Id);
				return true;
			}
			else
			{
				return false;
			}
		}

		public IEnumerator<INode> GetEnumerator()
		{
			return AllNodes.Values.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return AllNodes.GetEnumerator();
		}

		public bool Contains(INode item)
		{
			return AllNodes.ContainsKey(item.Id);
		}

	}
}
