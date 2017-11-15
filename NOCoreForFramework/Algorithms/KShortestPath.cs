using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetworkObservabilityCore.Utils;
using NetworkObservabilityCore.Criteria;

namespace NetworkObservabilityCore.Algorithms
{
	/// <summary>
	/// An algorithm finds all the shortest paths from origin to every single nodes in a graph.
	/// </summary>
	public class KShortestPath : IAlgorithm
	{
		private Dictionary<INode, List<Route>> pathsDict;
		private static readonly List<Route> emptyList = new List<Route>();

		#region Properties

		/// <inheritdoc />
		public INode Source
		{
			get;
			set;
		}

		/// <inheritdoc />
		public Constraint<INode> NodeConstraint
		{
			get;
			set;
		}

		/// <inheritdoc />
		public Constraint<IEdge> EdgeConstraint
		{
			get;
			set;
		}

		/// <inheritdoc />
		public String SelectedEdgeAttribute
		{
			get;
			set;
		}

		/// <inheritdoc />
		public bool IsSet
		{
			get;
			set;
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Instantiates a *KShortestPath* algorithm.
		/// </summary>
		public KShortestPath()
		{
			IsSet = false;
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// See <see cref="Setup(INode, string, Constraint{INode}, Constraint{IEdge})"/>.
		/// </summary>
		public void Setup(INode src, String edgeAttr)
		{
			Setup(src, edgeAttr, Constraint<INode>.Default, Constraint<IEdge>.Default);
		}

		/// <summary>
		/// See <see cref="Setup(INode, string, Constraint{INode}, Constraint{IEdge})"/>.
		/// </summary>
		public void Setup(INode src, String edgeAttr, Constraint<INode> cNode)
		{
			Setup(src, edgeAttr, cNode, Constraint<IEdge>.Default);
		}

		/// <summary>
		/// See <see cref="Setup(INode, string, Constraint{INode}, Constraint{IEdge})"/>.
		/// </summary>
		public void Setup(INode src, String edgeAttr, Constraint<IEdge> cEdge)
		{
			Setup(src, edgeAttr, Constraint<INode>.Default, cEdge);
		}

		/// <inheritdoc />
		public void Setup(INode src, String edgeAttr, Constraint<INode> cNode, Constraint<IEdge> cEdge)
		{
			Source = src;
			SelectedEdgeAttribute = edgeAttr;
			NodeConstraint = cNode;
			EdgeConstraint = cEdge;
			IsSet = true;
		}

		/// <inheritdoc />
		public void Run()
		{
			if (!IsSet)
				throw new InvalidOperationException("The algorithm is not set. Call method Setup before running the algorithm.");

			pathsDict = new Dictionary<INode, List<Route>>();
			Queue<State> queue = new Queue<State>();

			var srcPath = new Route(Source, SelectedEdgeAttribute);
			var srcPaths = new List<Route>() { srcPath };

			pathsDict[Source] = srcPaths;

			queue.Enqueue(new State(srcPath));

			while (queue.Count != 0)
			{
				State state = queue.Dequeue();

				var newStates = CreateNewStates(state, NodeConstraint, EdgeConstraint);
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

		/// <inheritdoc />
		public IList<Route> PathsTo(INode node)
		{

			if (pathsDict.ContainsKey(node))
				return pathsDict[node];
			else
				return emptyList;
		}

		#endregion

		#region Private Methods

		private IEnumerable<State> CreateNewStates(State currentState, 
												   Constraint<INode> cNode,
												   Constraint<IEdge> cEdge)
		{
			var currentPath = currentState.CurrentRoute;
			LinkedList<State> states = new LinkedList<State>();

			foreach (var edge in currentState.CurrentNode.ConnectOut)
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

		#endregion

	}
}
