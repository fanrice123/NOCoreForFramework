using NetworkObservabilityCore;
using NetworkObservabilityCore.Algorithms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkObservabilityCore.Utils
{
	using FromTo = Tuple<INode, INode>;
	using FromToThrough = Tuple<INode, INode, Route>;

	public class ConnectivityObserver
	{
		public Dictionary<FromTo, double>
		ObserveConnectivityPercentage(IGraph graph, ICollection<INode> observers, Tuple<String, Constraint<IEdge>> tuple)
		{
			var result = new Dictionary<FromTo, double>();

			foreach (var fromNode in graph.AllNodes)
			{
				var from = fromNode.Value;
				// if from is one of the observers & it has to be observed as well
				if (observers.Contains(from) && !from.IsObserverInclusive)
					continue;

				//KShortestPath shortestPath = new KShortestPath(this, from);
				AllPaths shortestPath = new AllPaths(graph, from, tuple.Item1, tuple.Item2);

				// 
				foreach (var to in graph.AllNodes.Values.Where(to => !to.Equals(from)))
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


		public Dictionary<FromToThrough, bool>
		ObserveConnectivity(IGraph graph, ICollection<INode> observers, Tuple<String, Constraint<IEdge>> tuple)
		{
			var result = new Dictionary<FromToThrough, bool>();

			foreach (var fromNode in graph.AllNodes)
			{
				var from = fromNode.Value;
				if (observers.Contains(from) && !from.IsObserverInclusive)
					continue;

				//KShortestPath shortestPath = new KShortestPath(this, from);
				AllPaths shortestPath = new AllPaths(graph, from, tuple.Item1, tuple.Item2);

				foreach (var to in graph.AllNodes.Values.Where(to => !to.Equals(from)))
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
						result[new FromToThrough(from, to, path)] = observed;
					}
				}
			}

			return result;
		}
	}
}
