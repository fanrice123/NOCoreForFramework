using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkObservabilityCore
{
	public class KShortestPath
	{
		private Dictionary<INode, List<Route>> pathsDict;

		public KShortestPath(IGraph graph, INode src)
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

				var newStates = CreateNewStates(state);
				foreach (var newState in newStates)
				{
					var newNode = newState.CurrentNode;
					var newPath = newState.CurrentPath;

					if (pathsDict.ContainsKey(newNode))
					{
						var paths = pathsDict[newNode];
						Route oldPath = paths[0]; // all paths has same cost

						if (newPath.PathCost > oldPath.PathCost)
							continue;

						if (newPath.PathCost < oldPath.PathCost)
						{
							paths.Clear();
							paths.Add(newPath);
						}
						else if (newPath.PathCost == oldPath.PathCost)
						{
							paths.Add(newPath);
						}

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

		public IList<Route> PathsTo(INode node)
		{
			return pathsDict[node];
		}

		private IEnumerable<State> CreateNewStates(State currentState)
		{
			var currentPath = currentState.CurrentPath;
			LinkedList<State> states = new LinkedList<State>();

			foreach (var edge in currentState.CurrentNode.ConnectTo)
			{
				var newPath = currentPath.Clone();
				newPath.Add(edge);
				states.AddLast(new State(newPath));
			}

			return states;
		}

		private class State
		{
			public State(Route path)
			{
				CurrentPath = path;
			}

			public Route CurrentPath
			{
				get;
				private set;
			}

			public INode CurrentNode => CurrentPath.Destination;
		}
	}
}
