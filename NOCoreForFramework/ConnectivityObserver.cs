using System;
using System.Collections.Generic;
using System.Linq;
using NetworkObservabilityCore.Utils;
using NetworkObservabilityCore.Algorithms;

namespace NetworkObservabilityCore
{

	/// <summary>
	/// This object observes connectivity of a *Graph*.
	/// </summary>
	public class ConnectivityObserver
	{

		/// <summary>
		/// Observes a *Graph*.
		/// </summary>
		/// <param name="graph">Graph to be observed.</param>
		/// <param name="observers"></param>
		/// <param name="weightAndConstraint"></param>
		/// <returns></returns>
        public Dictionary<FromTo, Tuple<ISet<Route>, ISet<Route>>>
        Observe(IGraph graph, 
				ICollection<INode> observers, 
				Tuple<String, Constraint<IEdge>> weightAndConstraint, 
				IAlgorithm algorithm)
        {
            var result = new Dictionary<FromTo, Tuple<ISet<Route>, ISet<Route>>>();

            foreach (var fromNode in graph.AllNodes)
            {
                var from = fromNode.Value;
                if (observers.Contains(from) && !from.IsObserverInclusive)
                    continue;

                //KShortestPath shortestPath = new KShortestPath(this, from);
                algorithm.Setup(from, weightAndConstraint.Item1, Constraint<INode>.Default, weightAndConstraint.Item2);
				algorithm.Run();

                foreach (var to in graph.AllNodes.Values.Where(to => !to.Equals(from)))
                {
					if (observers.Contains(to) && !to.IsObserverInclusive)
						continue;
                    var fromTo = new FromTo(from, to);
                    var observedRoutes = new HashSet<Route>();
                    var unobservedRoutes = new HashSet<Route>();

                    var paths = algorithm.PathsTo(to);
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
