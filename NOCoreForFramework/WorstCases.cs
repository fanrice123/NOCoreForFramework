using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkObservabilityCore
{
    public class WorstCases
    {
        // The two final sets to be subtracted from each other
        private Dictionary<INode, List<Route>> kShortest;
        private Dictionary<INode, List<Route>> allPaths;

        public WorstCases(IGraph graph)
        {
			List<KShortestPath> allShortestPaths = new List<KShortestPath>();
			List<AllPaths> allPossiblePaths = new List<AllPaths>();

            allPaths = new Dictionary<INode, List<Route>>();
            kShortest = new Dictionary<INode, List<Route>>();

            foreach (var node in graph)
            {
                KShortestPath kShortest = new KShortestPath(graph, node);
                allShortestPaths.Add(kShortest);

                AllPaths allPaths = new AllPaths(graph, node);
                allPossiblePaths.Add(allPaths);
            }

            // Append all possible paths 
            foreach (var node in graph)
            {
                // Find all the posible paths and store them
                foreach (var tempAllPaths in allPossiblePaths)
                {
                    allPaths.Add(node, new List<Route>(tempAllPaths.PathsTo(node)));
                }

                // Find all the shortest paths and store them
                foreach (var tempAllPaths in allShortestPaths)
                {
                    kShortest.Add(node, new List<Route>(tempAllPaths.PathsTo(node)));
                }
            }

            // Do the subtraction
            foreach (KeyValuePair<INode, List<Route>> pair in kShortest)
            {
                var longPaths = allPaths[pair.Key];
                foreach (Route route in pair.Value)
                {
                    if (longPaths.Contains(route) || route.ContainsObserver())
                    {
                        allPaths[pair.Key].Remove(route);
                    }
                }

            }
        }

        public Dictionary<INode, List<Route>> GetAllWorstRoutes()
        {
            return allPaths;
        }
    }
}
