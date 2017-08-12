using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace NetworkObservabilityCore
{
    public class Graph : IGraph 
    {


		public Dictionary<String, INode> AllNodes
		{
			get;
			internal set;
		}

		public Dictionary<String, IEdge> AllEdges
		{
			get;
			internal set;
		}

		public int Count => AllNodes.Count;

		public bool IsReadOnly => throw new NotImplementedException();

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
			from.Links.Add(edge);
			to.ConnectFrom.Add(edge);
			edge.From = from;
			edge.To = to;
		}

		public Dictionary<Tuple<INode, INode>, bool>
		ObserveConnectivity(ICollection<INode> observers)
		{
			var result = new Dictionary<Tuple<INode, INode>, bool>();

			foreach (var fromNode in AllNodes)
			{
				var from = fromNode.Value;
				if (observers.Contains(from) && !from.IsObserverInclusive)
					continue;

				Dijkstra dijkstra = new Dijkstra(this, from);

				foreach (var toNode in AllNodes)
				{
					var to = toNode.Value;
					if (from.Equals(to))
						continue;

					foreach (var observer in observers)
					{
						var paths = dijkstra.PathTo(to);
						bool flag = paths.Contains(observer);
						result[new Tuple<INode, INode>(from, to)] = flag;
					}
				}
			}

			return result;
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
				edge.From.Links.Remove(edge);
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
				while (item.ConnectFrom.Count != 0)
				{
					Remove(item.ConnectFrom[0]);
				}
				while (item.Links.Count != 0)
				{
					Remove(item.Links[0]);
				}
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
