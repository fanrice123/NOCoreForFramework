using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetworkObservabilityCore.Utils;
using NetworkObservabilityCore.Criteria;

namespace NetworkObservabilityCore.Algorithms
{
	public class AllPaths : IAlgorithm
	{
		private Dictionary<INode, List<Route>> pathsDict;

		#region Properties

		public INode Source
		{
			get;
			set;
		}

		public Constraint<INode> NodeConstraint
		{
			get;
			set;
		}

		public Constraint<IEdge> EdgeConstraint
		{
			get;
			set;
		}

		public String SelectedEdgeAttribute
		{
			get;
			set;
		}

		#endregion

		public AllPaths()
		{
		}


		public void Setup(INode src, String edgeAttr)
		{
			Setup(src, edgeAttr, Constraint<INode>.Default, Constraint<IEdge>.Default);
		}

		public void Setup(INode src, String edgeAttr, Constraint<INode> cNode)
		{
			Setup(src, edgeAttr, cNode, Constraint<IEdge>.Default);
		}

		public void Setup(INode src, String edgeAttr, Constraint<IEdge> cEdge)
		{
			Setup(src, edgeAttr, Constraint<INode>.Default, cEdge);
		}

		public void Setup(INode src, String edgeAttr, Constraint<INode> cNode, Constraint<IEdge> cEdge)
		{
			Source = src;
			SelectedEdgeAttribute = edgeAttr;
			NodeConstraint = cNode;
			EdgeConstraint = cEdge;
		}

		public void Run()
		{
			pathsDict = new Dictionary<INode, List<Route>>();
			Stack<State> stack = new Stack<State>();

			var srcPath = new Route(Source, SelectedEdgeAttribute);
			var srcPaths = new List<Route>() { srcPath };

			pathsDict[Source] = srcPaths;

			stack.Push(new State(srcPath));

			while (stack.Count != 0)
			{
				State state = stack.Pop();


				var newStates = CreateLooplessNewStates(state, NodeConstraint, EdgeConstraint);
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
