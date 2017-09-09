using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetworkObservabilityCore;
using NetworkObservabilityCore.Utils;

namespace NOCoreTest
{
	[TestClass]
	public class RouteTest
	{
		private IGraph graph;
		private INode src;
		private Route route1, route2;
		private INode[] nodes;

		[TestInitialize]
		public void Initialize()
		{
			src = new Node() { Label = "A" };
			graph = new Graph();
			graph.Add(src);
			nodes = new Node[5];
			nodes[0] = src;
			for (int i = 1; i != nodes.Length; ++i)
			{
				int val = 'A';
				nodes[i] = new Node
				{
					Label = Convert.ToString(Convert.ToChar(val + i))
				};
				graph.Add(nodes[i]);
			}
			route1 = new Route(src, "Weight");
			route2 = new Route(src, "Weight");
		}

		[TestMethod]
		public void TestMethod1()
		{
			IEdge edge1 = new Edge();
			IEdge edge2 = new Edge();
			graph.ConnectNodeToWith(src, nodes[1], edge1);
			graph.ConnectNodeToWith(nodes[1], nodes[2], edge2);

			route1.Add(edge1);
			route1.Add(edge2);
			Assert.IsFalse(route1.Equals(route2));
		}

	}
}
