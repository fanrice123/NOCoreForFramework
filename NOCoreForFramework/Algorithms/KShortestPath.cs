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
			: this(graph, src, Constraint<INode>.Default, Constraint<IEdge>.Default)
		{
		}

		public KShortestPath(IGraph graph, INode src, Constraint<INode> cNode)
			: this(graph, src, cNode, Constraint<IEdge>.Default)
		{
		}

		public KShortestPath(IGraph graph, INode src, Constraint<IEdge> cEdge)
			: this(graph, src, Constraint<INode>.Default, cEdge)
		{
		}

		public KShortestPath(IGraph graph, INode src, Constraint<INode> cNode, Constraint<IEdge> cEdge)
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

				var newStates = CreateNewStates(state, cNode, cEdge);
				foreach (var newState in newStates)
				{
					var newNode = newState.CurrentNode;
					var newPath = newState.CurrentRoute;

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

		private static readonly List<Route> emptyList = new List<Route>();

		public IList<Route> PathsTo(INode node)
		{

			if (pathsDict.ContainsKey(node))
				return pathsDict[node];
			else
				return emptyList;
		}

		private IEnumerable<State> CreateNewStates(State currentState, 
												   Constraint<INode> cNode,
												   Constraint<IEdge> cEdge)
		{
			var currentPath = currentState.CurrentRoute;
			LinkedList<State> states = new LinkedList<State>();

			foreach (var edge in currentState.CurrentNode.ConnectTo)
			{
				if (cEdge.Validate(edge) && cNode.Validate(edge.To))
				{
					var newPath = currentPath.Clone();
					newPath.Add(edge);
					states.AddLast(new State(newPath));
				}
			}

			return states;
		}


	}
}
