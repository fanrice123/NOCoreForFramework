using System;
using NetworkObservabilityCore;
using NetworkObservabilityCore.Algorithms;
using NetworkObservabilityCore.Utils;
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
			graph.Add(A);
			graph.Add(B);
			graph.Add(C);
			graph.Add(D);
			graph.Add(E);

			graph.ConnectNodeToWith(A, B, new Edge(new AttributePair { Name = "Weight", Attribute = 4 }));
			graph.ConnectNodeToWith(B, E, new Edge(new AttributePair { Name = "Weight", Attribute = 5 }));
			graph.ConnectNodeToWith(A, C, new Edge(new AttributePair { Name = "Weight", Attribute = 5 }));
			graph.ConnectNodeToWith(C, E, new Edge(new AttributePair { Name = "Weight", Attribute = 4 }));
			graph.ConnectNodeToWith(A, D, new Edge(new AttributePair { Name = "Weight", Attribute = 9 }));
			graph.ConnectNodeToWith(D, E, new Edge(new AttributePair { Name = "Weight", Attribute = 7 }));

			/*
			graph.ConnectNodeToWith(B, C, new Edge(new AttributePair { Name="Weight", Attribute=)(1));
			graph.ConnectNodeToWith(C, D, new Edge(new AttributePair { Name="Weight", Attribute=)(3));
			graph.ConnectNodeToWith(D, C, new Edge(new AttributePair { Name="Weight", Attribute=)(2));
			graph.ConnectNodeToWith(E, C, new Edge(new AttributePair { Name="Weight", Attribute=)(1));
			*/

			KShortestPath ksp = new KShortestPath(graph, A, "Weight");

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
			graph.Add(A);
			graph.Add(B);
			graph.Add(C);
			graph.Add(D);
			graph.Add(E);

			graph.ConnectNodeToWith(A, B, new Edge(new AttributePair { Name = "Weight", Attribute = 4 }));
			graph.ConnectNodeToWith(B, E, new Edge(new AttributePair { Name = "Weight", Attribute = 5 }));
			graph.ConnectNodeToWith(A, C, new Edge(new AttributePair { Name = "Weight", Attribute = 5 }));
			graph.ConnectNodeToWith(C, E, new Edge(new AttributePair { Name = "Weight", Attribute = 4 }));
			graph.ConnectNodeToWith(A, D, new Edge(new AttributePair { Name = "Weight", Attribute = 1 }));
			graph.ConnectNodeToWith(D, E, new Edge(new AttributePair { Name = "Weight", Attribute = 7 }));

			/*
			graph.ConnectNodeToWith(B, C, new Edge(new AttributePair { Name="Weight", Attribute=)(1));
			graph.ConnectNodeToWith(C, D, new Edge(new AttributePair { Name="Weight", Attribute=)(3));
			graph.ConnectNodeToWith(D, C, new Edge(new AttributePair { Name="Weight", Attribute=)(2));
			graph.ConnectNodeToWith(E, C, new Edge(new AttributePair { Name="Weight", Attribute=)(1));
			*/

			KShortestPath ksp = new KShortestPath(graph, A, "Weight");

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
			graph.Add(H);
			graph.Add(A);
			graph.Add(B);
			graph.Add(C);
			graph.Add(D);
			graph.Add(E);
			graph.Add(F);
			graph.Add(G);

			graph.ConnectNodeToWith(A, H, new Edge(new AttributePair { Name = "Weight", Attribute = 1 }));
			graph.ConnectNodeToWith(H, F, new Edge(new AttributePair { Name = "Weight", Attribute = 1 }));
			graph.ConnectNodeToWith(A, B, new Edge(new AttributePair { Name = "Weight", Attribute = 1 }));
			graph.ConnectNodeToWith(B, D, new Edge(new AttributePair { Name = "Weight", Attribute = 1 }));
			graph.ConnectNodeToWith(A, C, new Edge(new AttributePair { Name = "Weight", Attribute = 1 }));
			graph.ConnectNodeToWith(C, D, new Edge(new AttributePair { Name = "Weight", Attribute = 1 }));
			graph.ConnectNodeToWith(D, E, new Edge(new AttributePair { Name = "Weight", Attribute = 1 }));
			graph.ConnectNodeToWith(E, F, new Edge(new AttributePair { Name = "Weight", Attribute = 1 }));
			graph.ConnectNodeToWith(D, G, new Edge(new AttributePair { Name = "Weight", Attribute = 1 }));
			graph.ConnectNodeToWith(G, F, new Edge(new AttributePair { Name = "Weight", Attribute = 1 }));

			KShortestPath ksp = new KShortestPath(graph, A, "Weight");

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
			graph.Add(A);
			graph.Add(B);
			graph.Add(C);
			graph.Add(D);
			graph.Add(E);
			graph.Add(F);
			graph.Add(G);

			graph.ConnectNodeToWith(A, B, new Edge(new AttributePair { Name = "Weight", Attribute = 1 }));
			graph.ConnectNodeToWith(B, D, new Edge(new AttributePair { Name = "Weight", Attribute = 1 }));
			graph.ConnectNodeToWith(A, C, new Edge(new AttributePair { Name = "Weight", Attribute = 1 }));
			graph.ConnectNodeToWith(C, D, new Edge(new AttributePair { Name = "Weight", Attribute = 1 }));
			graph.ConnectNodeToWith(D, E, new Edge(new AttributePair { Name = "Weight", Attribute = 1 }));
			graph.ConnectNodeToWith(E, F, new Edge(new AttributePair { Name = "Weight", Attribute = 1 }));
			graph.ConnectNodeToWith(D, G, new Edge(new AttributePair { Name = "Weight", Attribute = 1 }));
			graph.ConnectNodeToWith(G, F, new Edge(new AttributePair { Name = "Weight", Attribute = 1 }));

			KShortestPath ksp = new KShortestPath(graph, A, "Weight");

			Assert.AreEqual(4, ksp.PathsTo(F).Count);
			Assert.AreEqual(2, ksp.PathsTo(D).Count);
			Assert.AreEqual(2, ksp.PathsTo(E).Count);
		}

		[TestMethod]
		public void TestFindKShortestPath5()
		{
			Graph graph = new Graph();

			var A = new Node() { Label = "A" };
			var B = new Node() { Label = "B" };
			var C = new Node() { Label = "C" };
			var D = new Node() { Label = "D" };
			var E = new Node() { Label = "E" };
			var F = new Node() { Label = "F" };
			var G = new Node() { Label = "G" };
			graph.Add(A);
			graph.Add(B);
			graph.Add(C);
			graph.Add(D);
			graph.Add(E);
			graph.Add(F);
			graph.Add(G);

			graph.ConnectNodeToWith(A, B, new Edge(new AttributePair { Name = "Weight", Attribute = 4 }));
			graph.ConnectNodeToWith(B, D, new Edge(new AttributePair { Name = "Weight", Attribute = 5 }));
			graph.ConnectNodeToWith(A, C, new Edge(new AttributePair { Name = "Weight", Attribute = 7 }));
			graph.ConnectNodeToWith(C, D, new Edge(new AttributePair { Name = "Weight", Attribute = 2 }));
			graph.ConnectNodeToWith(A, E, new Edge(new AttributePair { Name = "Weight", Attribute = 1 }));
			graph.ConnectNodeToWith(E, F, new Edge(new AttributePair { Name = "Weight", Attribute = 1 }));
			graph.ConnectNodeToWith(F, G, new Edge(new AttributePair { Name = "Weight", Attribute = 1 }));
			graph.ConnectNodeToWith(G, D, new Edge(new AttributePair { Name = "Weight", Attribute = 1 }));

			KShortestPath ksp = new KShortestPath(graph, A, "Weight");

			Assert.AreEqual(1, ksp.PathsTo(D).Count);
		}
	}
}
