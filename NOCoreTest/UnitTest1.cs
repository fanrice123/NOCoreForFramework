using System;
using NetworkObservabilityCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace NOCoreTest
{
	[TestClass]
	public class UnitTest1
	{
		[TestMethod]
		public void TestMethod1()
		{
			Graph graph = new Graph();

			var A = new Node() { Label = "A" };
			var B = new Node() { Label = "B" };
			var C = new Node() { Label = "C" };
			var D = new Node() { Label = "D" };
			var E = new Node() { Label = "E" };
			graph.AddNode(A);
			graph.AddNode(B);
			graph.AddNode(C);
			graph.AddNode(D);
			graph.AddNode(E);

			graph.ConnectNodeToWith(A, B, new Edge(8));
			graph.ConnectNodeToWith(A, D, new Edge(9));
			graph.ConnectNodeToWith(A, E, new Edge(4));
			graph.ConnectNodeToWith(B, C, new Edge(1));
			graph.ConnectNodeToWith(C, B, new Edge(2));
			graph.ConnectNodeToWith(C, D, new Edge(3));
			graph.ConnectNodeToWith(D, C, new Edge(2));
			graph.ConnectNodeToWith(D, E, new Edge(7));
			graph.ConnectNodeToWith(E, C, new Edge(1));

			B.IsObserver = true;
			var observers = new List<INode>();
			observers.Add(B);

			var result = graph.ObserveConnectivity(observers);
		}
	}
}
