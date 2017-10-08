using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetworkObservabilityCore;
using NetworkObservabilityCore.Utils;

namespace NOCoreTest
{
	[TestClass]
	public class ConnectivityObserverTest
	{
		ConnectivityObserver co;
		IGraph graph;
		List<INode> observers;

		[TestInitialize]
		public void Initialize()
		{
			co = new ConnectivityObserver();
			graph = new Graph();

			var A = new TestSubNode() { Label = "A" };
			var B = new Node() { Label = "B" };
			var C = new Node() { Label = "C" };
			var D = new TestSubNode() { Label = "D" };
			var E = new TesteSubNode2() { Label = "E" };
			E.DescriptiveAttributes["test"] = "test";
			B["dummy"] = 15;
			C.DescriptiveAttributes["time"] = "NOthing";
			A["3"] = 0.99;

			graph.Add(A);
			graph.Add(B);
			graph.Add(C);
			graph.Add(D);
			graph.Add(E);

			graph.ConnectNodeToWith(A, B, new Edge(new AttributePair<Double> { Name = "Weight", Attribute = 8 }));
			graph.ConnectNodeToWith(A, D, new TestSubEdge(9));
			graph.ConnectNodeToWith(A, E, new Edge(new AttributePair<Double> { Name = "Weight", Attribute = 4 }));
			graph.ConnectNodeToWith(B, C, new Edge(new AttributePair<Double> { Name = "Weight", Attribute = 1 }));
			graph.ConnectNodeToWith(C, B, new Edge(new AttributePair<Double> { Name = "Weight", Attribute = 2 }));
			graph.ConnectNodeToWith(C, D, new TestSubEdge(3));
			graph.ConnectNodeToWith(D, C, new Edge(new AttributePair<Double> { Name = "Weight", Attribute = 2 }));
			graph.ConnectNodeToWith(D, E, new Edge(new AttributePair<Double> { Name = "Weight", Attribute = 7 }));
			graph.ConnectNodeToWith(E, C, new Edge(new AttributePair<Double> { Name = "Weight", Attribute = 1 }));
			observers = new List<INode>();
			observers.Add(B);
			observers.Add(C);
		}

		[TestMethod]
		public void TestObserve()
		{
			co.Observe(graph, observers, Tuple.Create("Weight", Constraint<IEdge>.Default));
		}
	}
}
