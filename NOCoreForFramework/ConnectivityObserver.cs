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

        public Dictionary<FromTo<INode, INode>, Tuple<ISet<Route>, ISet<Route>>>
        Observe(IGraph graph, ICollection<INode> observers, Tuple<String, Constraint<IEdge>> weightAndConstraint)
        {
            var result = new Dictionary<FromTo<INode, INode>, Tuple<ISet<Route>, ISet<Route>>>();

            foreach (var fromNode in graph.AllNodes)
            {
                var from = fromNode.Value;
                if (observers.Contains(from) && !from.IsObserverInclusive)
                    continue;

                //KShortestPath shortestPath = new KShortestPath(this, from);
                AllPaths shortestPath = new AllPaths(graph, from, weightAndConstraint.Item1, weightAndConstraint.Item2);

                foreach (var to in graph.AllNodes.Values.Where(to => !to.Equals(from)))
                {
					if (observers.Contains(to) && !to.IsObserverInclusive)
						continue;
                    var fromTo = FromTo.Create(from, to);
                    var observedRoutes = new HashSet<Route>();
                    var unobservedRoutes = new HashSet<Route>();

                    var paths = shortestPath.PathsTo(to);
                    foreach (Route path in paths)
                    {
                        foreach (var observer in observers)
                        {
                            if (path.Contains(observer))
                            {
                                observedRoutes.Add(path);
								if (unobservedRoutes.Contains(path))
									unobservedRoutes.Remove(path);
                                break;
                            }
                            else
                            {
                                unobservedRoutes.Add(path);
                            }
                        }
                    }
                    result[fromTo] = new Tuple<ISet<Route>, ISet<Route>>(observedRoutes, unobservedRoutes);
                }
            }

            return result;
        }
        
        /*
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
        */

	}
}
