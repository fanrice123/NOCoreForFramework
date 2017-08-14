using System;
using NetworkObservabilityCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace NOCoreTest
{
	[TestClass]
	public class KShortestPathTest
	{
		[TestMethod]
		public void TestFindKShortestPath()
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

			graph.ConnectNodeToWith(A, B, new Edge(4));
			graph.ConnectNodeToWith(B, E, new Edge(5));
			graph.ConnectNodeToWith(A, C, new Edge(5));
			graph.ConnectNodeToWith(C, E, new Edge(4));
			graph.ConnectNodeToWith(A, D, new Edge(9));
			graph.ConnectNodeToWith(D, E, new Edge(7));

			/*
			graph.ConnectNodeToWith(B, C, new Edge()(1));
			graph.ConnectNodeToWith(C, D, new Edge()(3));
			graph.ConnectNodeToWith(D, C, new Edge()(2));
			graph.ConnectNodeToWith(E, C, new Edge()(1));
			*/

			KShortestPath ksp = new KShortestPath(graph, A);

			Assert.AreEqual(2, ksp.PathsTo(E).Count);
		}

		[TestMethod]
		public void TestFindKShortestPath2()
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

			graph.ConnectNodeToWith(A, B, new Edge(4));
			graph.ConnectNodeToWith(B, E, new Edge(5));
			graph.ConnectNodeToWith(A, C, new Edge(5));
			graph.ConnectNodeToWith(C, E, new Edge(4));
			graph.ConnectNodeToWith(A, D, new Edge(1));
			graph.ConnectNodeToWith(D, E, new Edge(7));

			/*
			graph.ConnectNodeToWith(B, C, new Edge()(1));
			graph.ConnectNodeToWith(C, D, new Edge()(3));
			graph.ConnectNodeToWith(D, C, new Edge()(2));
			graph.ConnectNodeToWith(E, C, new Edge()(1));
			*/

			KShortestPath ksp = new KShortestPath(graph, A);

			Assert.AreEqual(1, ksp.PathsTo(E).Count);
		}

		[TestMethod]
		public void TestFindKShortestPath3()
		{
			Graph graph = new Graph();

			var A = new Node() { Label = "A" };
			var B = new Node() { Label = "B" };
			var C = new Node() { Label = "C" };
			var D = new Node() { Label = "D" };
			var E = new Node() { Label = "E" };
			var F = new Node() { Label = "F" };
			var G = new Node() { Label = "G" };
			var H = new Node() { Label = "H" };
			graph.AddNode(H);
			graph.AddNode(A);
			graph.AddNode(B);
			graph.AddNode(C);
			graph.AddNode(D);
			graph.AddNode(E);
			graph.AddNode(F);
			graph.AddNode(G);

			graph.ConnectNodeToWith(A, H, new Edge());
			graph.ConnectNodeToWith(H, F, new Edge());
			graph.ConnectNodeToWith(A, B, new Edge());
			graph.ConnectNodeToWith(B, D, new Edge());
			graph.ConnectNodeToWith(A, C, new Edge());
			graph.ConnectNodeToWith(C, D, new Edge());
			graph.ConnectNodeToWith(D, E, new Edge());
			graph.ConnectNodeToWith(E, F, new Edge());
			graph.ConnectNodeToWith(D, G, new Edge());
			graph.ConnectNodeToWith(G, F, new Edge());

			KShortestPath ksp = new KShortestPath(graph, A);

			Assert.AreEqual(1, ksp.PathsTo(F).Count);
		}

		[TestMethod]
		public void TestFindKShortestPath4()
		{
			Graph graph = new Graph();

			var A = new Node() { Label = "A" };
			var B = new Node() { Label = "B" };
			var C = new Node() { Label = "C" };
			var D = new Node() { Label = "D" };
			var E = new Node() { Label = "E" };
			var F = new Node() { Label = "F" };
			var G = new Node() { Label = "G" };
			graph.AddNode(A);
			graph.AddNode(B);
			graph.AddNode(C);
			graph.AddNode(D);
			graph.AddNode(E);
			graph.AddNode(F);
			graph.AddNode(G);

			graph.ConnectNodeToWith(A, B, new Edge());
			graph.ConnectNodeToWith(B, D, new Edge());
			graph.ConnectNodeToWith(A, C, new Edge());
			graph.ConnectNodeToWith(C, D, new Edge());
			graph.ConnectNodeToWith(D, E, new Edge());
			graph.ConnectNodeToWith(E, F, new Edge());
			graph.ConnectNodeToWith(D, G, new Edge());
			graph.ConnectNodeToWith(G, F, new Edge());

			KShortestPath ksp = new KShortestPath(graph, A);

			Assert.AreEqual(4, ksp.PathsTo(F).Count);
			Assert.AreEqual(2, ksp.PathsTo(D).Count);
			Assert.AreEqual(2, ksp.PathsTo(E).Count);
		}
	}
}
