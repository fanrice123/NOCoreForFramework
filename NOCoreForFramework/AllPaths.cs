using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkObservabilityCore
{
	public class AllPaths
	{
		private Dictionary<INode, List<Route>> pathsDict;

		public AllPaths(IGraph graph, INode src)
		{
			pathsDict = new Dictionary<INode, List<Route>>();
			Queue<State> queue = new Queue<State>();

			var srcPath = new Route(src);
			var srcPaths = new List<Route>() { srcPath };

			pathsDict[src] = srcPaths;

			queue.Enqueue(new State(srcPath));

			while (queue.Count != 0)
			{
				State state = queue.Dequeue();


				var newStates = CreateLooplessNewStates(state);
				foreach (var newState in newStates)
				{
					var newNode = newState.CurrentNode;
					var newPath = newState.CurrentRoute;

					if (pathsDict.ContainsKey(newNode))
					{
						pathsDict[newNode].Add(newPath);
					}
					else
					{
						var paths = new List<Route>() { newPath };
						pathsDict[newNode] = paths;
					}
					queue.Enqueue(newState);
				}
			}
		}

		private static readonly List<Route> emptyList = new List<Route>();

		public IList<Route> PathsTo(INode node)
		{

			if (pathsDict.ContainsKey(node))
				return pathsDict[node];
			else
				return emptyList;
		}

		private IEnumerable<State> CreateLooplessNewStates(State currentState)
		{
			var currentRoute = currentState.CurrentRoute;
			LinkedList<State> states = new LinkedList<State>();

			foreach (var edge in currentState.CurrentNode.ConnectTo)
			{
				if (currentRoute.Contains(edge.To))
					continue;
				var newPath = currentRoute.Clone();
				newPath.Add(edge);
				states.AddLast(new State(newPath));
			}

			return states;
		}
	}
}
