using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace NetworkObservabilityCore
{
	using FromTo = Tuple<INode, INode>;
	using ObserveFromToThrough = Dictionary<Tuple<INode, INode, Route>, bool>;
	using ObserveConnectivityPercent = Dictionary<Tuple<INode, INode>, double>;

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
			from.ConnectTo.Add(edge);
			to.ConnectFrom.Add(edge);
			edge.From = from;
			edge.To = to;
		}

		public ObserveConnectivityPercent
		ObserveConnectivityPercentage(ICollection<INode> observers)
		{
			var result = new Dictionary<FromTo, double>();

			foreach (var fromNode in AllNodes)
			{
				var from = fromNode.Value;
				if (observers.Contains(from) && !from.IsObserverInclusive)
					continue;

				//KShortestPath shortestPath = new KShortestPath(this, from);
				AllPaths shortestPath = new AllPaths(this, from);

				foreach (var to in AllNodes.Values.Where(to => !to.Equals(from)))
				{
					var paths = shortestPath.PathsTo(to);
					double observed = 0;
					foreach (Route path in paths)
					{
						foreach (var observer in observers)
						{
							if (path.Contains(observer))
							{
								++observed;
								break;
							}
						}

						result[Tuple.Create(from, to)] = observed != 0 ? observed / paths.Count : 0;
					}
				}
			}

			return result;
		}


		public ObserveFromToThrough
		ObserveConnectivity(ICollection<INode> observers)
		{
			var result = new Dictionary<Tuple<INode, INode, Route>, bool>();

			foreach (var fromNode in AllNodes)
			{
				var from = fromNode.Value;
				if (observers.Contains(from) && !from.IsObserverInclusive)
					continue;

				//KShortestPath shortestPath = new KShortestPath(this, from);
				AllPaths shortestPath = new AllPaths(this, from);

				foreach (var to in AllNodes.Values.Where(to => !to.Equals(from)))
				{
					var paths = shortestPath.PathsTo(to);
					foreach (Route path in paths)
					{
						bool observed = false;
						foreach (var observer in observers)
						{
							if (path.Contains(observer))
							{
								observed = true;
								break;
							}
						}
						result[new Tuple<INode, INode, Route>(from, to, path)] = observed;
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
				while (item.ConnectFrom.Count != 0)
				{
					Remove(item.ConnectFrom[0]);
				}
				while (item.ConnectTo.Count != 0)
				{
					Remove(item.ConnectTo[0]);
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
