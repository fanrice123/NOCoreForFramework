using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetworkObservabilityCore;
using System.Collections.Generic;
using NetworkObservabilityCore.Utils;

namespace NOCoreTest
{
    [TestClass]
    public class GraphTest
    {
        [TestMethod]
        public void TestObserveConnectivity()
        {
            Graph graph = new Graph();

            var A = new Node() { Label = "A" };
            var B = new Node() { Label = "B" };
            var C = new Node() { Label = "C" };
            var D = new Node() { Label = "D" };
            var E = new Node() { Label = "E" };
            graph.Add(A);
            graph.Add(B);
            graph.Add(C);
            graph.Add(D);
            graph.Add(E);

            graph.ConnectNodeToWith(A, B, new Edge(4));
            graph.ConnectNodeToWith(B, E, new Edge(5));
            graph.ConnectNodeToWith(A, C, new Edge(5));
            graph.ConnectNodeToWith(C, E, new Edge(4));
            graph.ConnectNodeToWith(A, D, new Edge(9));
            graph.ConnectNodeToWith(D, E, new Edge(7));
            B.IsObserver = true;
			/*
			graph.ConnectNodeToWith(B, C, new Edge()(1));
			graph.ConnectNodeToWith(C, D, new Edge()(3));
			graph.ConnectNodeToWith(D, C, new Edge()(2));
			graph.ConnectNodeToWith(E, C, new Edge()(1));
			*/

			var result = new ConnectivityObserver().ObserveConnectivity(graph, new List<INode>() { B }, "Weight");

			var routes = new HashSet<Route>();
			foreach (var key in result.Keys)
			{
				var route = key.Item3;
				if (!routes.Contains(route))
					routes.Add(route);
				else
					Assert.Fail();
			}

            
        }
    }
}
