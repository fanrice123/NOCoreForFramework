using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetworkObservabilityCore.Utils;

namespace NetworkObservabilityCore.Algorithms
{
	public class AllPaths
	{
		private Dictionary<INode, List<Route>> pathsDict;
		private String edgeKeyAttr;

		public AllPaths(IGraph graph, INode src, String edgeAttr)
			: this(graph, src, edgeAttr, Constraint<INode>.Default, Constraint<IEdge>.Default)
		{
		}

		public AllPaths(IGraph graph, INode src, String edgeAttr, Constraint<INode> cNode)
			: this(graph, src, edgeAttr, cNode, Constraint<IEdge>.Default)
		{
		}

		public AllPaths(IGraph graph, INode src, String edgeAttr, Constraint<IEdge> cEdge)
			: this(graph, src, edgeAttr, Constraint<INode>.Default, cEdge)
		{
		}

		public AllPaths(IGraph graph, INode src, String edgeAttr, Constraint<INode> cNode, Constraint<IEdge> cEdge)
		{
			pathsDict = new Dictionary<INode, List<Route>>();
			edgeKeyAttr = edgeAttr;
			Stack<State> stack = new Stack<State>();

			var srcPath = new Route(src, edgeAttr);
			var srcPaths = new List<Route>() { srcPath };

			pathsDict[src] = srcPaths;

			stack.Push(new State(srcPath));

			while (stack.Count != 0)
			{
				State state = stack.Pop();


				var newStates = CreateLooplessNewStates(state, cNode, cEdge);
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
					stack.Push(newState);
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

		private IEnumerable<State> CreateLooplessNewStates(State currentState, 
														   Constraint<INode> cNode, 
														   Constraint<IEdge> cEdge)
		{
			var currentRoute = currentState.CurrentRoute;
			LinkedList<State> states = new LinkedList<State>();

			foreach (var edge in currentState.CurrentNode.ConnectOut)
			{
				if (!currentRoute.Contains(edge.To) && cNode.Validate(edge.To) &&
													   cEdge.Validate(edge))
				{
					var newPath = currentRoute.Clone();
					newPath.Add(edge);
					states.AddLast(new State(newPath));
				}
			}

			return states;
		}
	}
}
