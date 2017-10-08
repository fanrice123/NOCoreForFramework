using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetworkObservabilityCore.Utils;
using NetworkObservabilityCore.Algorithms;

namespace NetworkObservabilityCore
{

	public class ConnectivityObserver
	{

		public Dictionary<FromToThrough<INode, INode, Route>, bool>
		Observe(IGraph graph, ICollection<INode> observers, Tuple<String, Constraint<IEdge>> weightAndConstraint)
		{
			var result = new Dictionary<FromToThrough<INode, INode, Route>, bool>();

			foreach (var fromNode in graph.AllNodes)
			{
				var from = fromNode.Value;
				if (observers.Contains(from) && !from.IsObserverInclusive)
					continue;

				//KShortestPath shortestPath = new KShortestPath(this, from);
				AllPaths shortestPath = new AllPaths(graph, from, weightAndConstraint.Item1, weightAndConstraint.Item2);

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
						result[FromToThrough.Create(from, to, path)] = observed;
					}
				}
			}

			return result;
		}

	}
}
