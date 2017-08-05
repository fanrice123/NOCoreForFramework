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
						bool flag = dijkstra.PathTo(to).Contains(observer);
						result[new Tuple<INode, INode>(from, to)] = flag;
					}
				}
			}

			return result;
		}

		public void Add(INode item)
		{
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

		public bool Remove(INode item)
		{
			throw new NotImplementedException();
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
