using System;
using System.Collections.Generic;

namespace NetworkObservabilityCore
{
	public class Dijkstra
    {
		readonly List<INode> nodes;
		private double[] dist;
		private int[] prev;
		private Comparison<AuxNode<double, int>> compare;
		Dictionary<INode, int> dict;

		public Dijkstra(IGraph graph, INode src)
			: this(graph, src, Comparer<AuxNode<double, int>>.Default)
		{
		}

		public Dijkstra(IGraph g, INode src, IComparer<AuxNode<double, int>> comp)
		{
			compare = comp.Compare;
			List<AuxNode<double, int>> auxNodes = new List<AuxNode<double, int>>();
			nodes = new List<INode>(g.AllNodes.Values);
			var pq = new PriorityQueue<AuxNode<double, int>>(nodes.Count, comp.Compare);
			dict = Dictionarize(nodes);
			dist = new double[nodes.Count];
			prev = new int[nodes.Count];

			for (int i = 0; i != nodes.Count; ++i)
			{
				if (src != nodes[i])
				{
					dist[i] = Double.PositiveInfinity;
				}
				else
				{
					dist[i] = 0;
				}
				prev[i] = int.MinValue;
				var auxNode = new AuxNode<double, int>(dist[i], i);
				auxNodes.Add(auxNode);
				pq.Enqueue(auxNode);

			}


			while (pq.Count != 0)
			{
				var auxNode = pq.Dequeue();
				var edges = nodes[auxNode.Item].Neighbours;
				foreach (Edge edge in edges)
				{
					var idxFrom = dict[edge.From];
					var idxTo = dict[edge.To];
					var newDist = dist[idxFrom] + edge.Value;
					if (newDist < dist[idxTo])
					{
						dist[idxTo] = newDist;
						prev[idxTo] = idxFrom;
						var newKey = new AuxNode<double, int>(newDist, idxTo);
						pq.DecreaseKey(auxNodes[idxTo], newKey);
						auxNodes[idxTo] = newKey;
					}
				}
			}
		}

		public ICollection<INode> PathTo(INode to)
		{
			Stack<INode> path = new Stack<INode>();
			path.Push(to);
			var prevIndex = prev[dict[to]];
			while (prevIndex >= 0)
			{
				path.Push(nodes[prevIndex]);
				prevIndex = prev[prevIndex];
			}

			return path.ToArray();
		}

		private Dictionary<INode, int> Dictionarize(IList<INode> nodes)
		{
			var dict = new Dictionary<INode, int>();

			for (int i = 0; i != nodes.Count; ++i)
				dict[nodes[i]] = i;

			return dict;
		}

		#region AuxiliaryClass

		public class AuxNode<TKey, TItem> : IComparable<AuxNode<TKey, TItem>>
			//, IEquatable<AuxNode<TKey, TItem>> 
			where TKey : IComparable<TKey>
		{
			public TKey Weight { get; set; }
			public TItem Item { get; set; }

			public AuxNode(TKey weight, TItem item)
			{
				Weight = weight;
				Item = item;
			}
			
			int IComparable<AuxNode<TKey, TItem>>.CompareTo(AuxNode<TKey, TItem> other)
			{
				return Weight.CompareTo(other.Weight);
			}

			/*
			bool IEquatable<AuxNode<TKey, TItem>>.Equals(AuxNode<TKey, TItem> other)
			{
				return Weight.CompareTo(other.Weight) == 0 &&
					Item.Equals(other.Item);
			}
			*/
		}

		#endregion
	}
}
