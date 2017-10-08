using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetworkObservabilityCore;
using System.Collections.Generic;
using NetworkObservabilityCore.Utils;
using NetworkObservabilityCore.Criteria;

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

			graph.ConnectNodeToWith(A, B, new Edge(new AttributePair<Double> { Name = "Weight", Attribute = 4 }));
            graph.ConnectNodeToWith(B, E, new Edge(new AttributePair<Double> { Name = "Weight", Attribute = 5 }));
            graph.ConnectNodeToWith(A, C, new Edge(new AttributePair<Double> { Name = "Weight", Attribute = 5 }));
            graph.ConnectNodeToWith(C, E, new Edge(new AttributePair<Double> { Name = "Weight", Attribute = 4 }));
            graph.ConnectNodeToWith(A, D, new Edge(new AttributePair<Double> { Name = "Weight", Attribute = 9 }));
            graph.ConnectNodeToWith(D, E, new Edge(new AttributePair<Double> { Name = "Weight", Attribute = 7 }));
            B.IsObserver = true;
			/*
			graph.ConnectNodeToWith(B, C, new Edge({Name="Weight", Attribute=)(1));
			graph.ConnectNodeToWith(C, D, new Edge({Name="Weight", Attribute=)(3));
			graph.ConnectNodeToWith(D, C, new Edge({Name="Weight", Attribute=)(2));
			graph.ConnectNodeToWith(E, C, new Edge({Name="Weight", Attribute=)(1));
			*/

			var result = new ConnectivityObserver().Observe(graph, new List<INode>() { B }, Tuple.Create("Weight", Constraint<IEdge>.Default));

			var routes = new HashSet<Route>();
			foreach (var key in result.Keys)
			{
				var route = key.Through;
				if (!routes.Contains(route))
					routes.Add(route);
				else
					Assert.Fail();
			}

            
        }
    }
}
