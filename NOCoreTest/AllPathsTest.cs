using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetworkObservabilityCore;
using NetworkObservabilityCore.Algorithms;
using NetworkObservabilityCore.Utils;
using NetworkObservabilityCore.Criteria;

namespace NOCoreTest
{
	[TestClass]
	public class AllPathsTest
	{
		[TestMethod]
		public void TestAllPaths1()
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

			AllPaths ap = new AllPaths(graph, A, "Weight");

			Assert.AreEqual(3, ap.PathsTo(E).Count);
		}

		[TestMethod]
		public void TestAllPaths2()
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

			AllPaths ap = new AllPaths(graph, A, "Weight");

			Assert.AreEqual(4, ap.PathsTo(F).Count);
			Assert.AreEqual(2, ap.PathsTo(D).Count);
			Assert.AreEqual(2, ap.PathsTo(E).Count);
		}

		[TestMethod]
		public void TestAllPaths3()
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

			AllPaths ksp = new AllPaths(graph, A, "Weight");

			Assert.AreEqual(3, ksp.PathsTo(D).Count);
		}

		[TestMethod]
		public void TestAllPaths4()
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

			graph.ConnectNodeToWith(A, B, new Edge(new AttributePair { Name = "Weight", Attribute = 4 },
												   new AttributePair { Name = "Value", Attribute = 3 }));
			graph.ConnectNodeToWith(B, D, new Edge(new AttributePair { Name = "Weight", Attribute = 5 },
												   new AttributePair { Name = "Value", Attribute = 29 }));
			graph.ConnectNodeToWith(A, C, new Edge(new AttributePair { Name = "Weight", Attribute = 7 },
												   new AttributePair { Name = "Value", Attribute = 10 }));
			graph.ConnectNodeToWith(C, D, new Edge(new AttributePair { Name = "Weight", Attribute = 2 },
												   new AttributePair { Name = "Value", Attribute = 32 }));
			graph.ConnectNodeToWith(A, E, new Edge(new AttributePair { Name = "Weight", Attribute = 1 }));
			graph.ConnectNodeToWith(E, F, new Edge(new AttributePair { Name = "Weight", Attribute = 11 }));
			graph.ConnectNodeToWith(F, G, new Edge(new AttributePair { Name = "Weight", Attribute = 3 }));
			graph.ConnectNodeToWith(G, D, new Edge(new AttributePair { Name = "Weight", Attribute = 2 }));

			Constraint<IEdge> constraint = new Constraint<IEdge>();
			constraint.Criteria.Add(new RangeCriterion("Value", 9, 100));
			constraint.Criteria.Add(new LessThanCriterion("Value", 33));
			constraint.Criteria.Add(new GreaterThanCriterion("Weight", 1));
			IEdge edge = new Edge();

			AllPaths ksp = new AllPaths(graph, A, "Weight", constraint);

			Assert.AreEqual(1, ksp.PathsTo(D).Count);
		}

		[TestMethod]
		public void TestAllPaths5()
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

			graph.ConnectNodeToWith(A, B, new Edge(new AttributePair { Name = "Weight", Attribute = 4 },
												   new AttributePair { Name = "Value", Attribute = 3 }));
			graph.ConnectNodeToWith(B, D, new Edge(new AttributePair { Name = "Weight", Attribute = 5 },
												   new AttributePair { Name = "Value", Attribute = 29 }));
			graph.ConnectNodeToWith(A, C, new Edge(new AttributePair { Name = "Weight", Attribute = 7 },
												   new AttributePair { Name = "Value", Attribute = 10 }));
			graph.ConnectNodeToWith(C, D, new Edge(new AttributePair { Name = "Weight", Attribute = 2 },
												   new AttributePair { Name = "Value", Attribute = 33 }));
			graph.ConnectNodeToWith(A, E, new Edge(new AttributePair { Name = "Weight", Attribute = 1 }));
			graph.ConnectNodeToWith(E, F, new Edge(new AttributePair { Name = "Weight", Attribute = 11 }));
			graph.ConnectNodeToWith(F, G, new Edge(new AttributePair { Name = "Weight", Attribute = 3 }));
			graph.ConnectNodeToWith(G, D, new Edge(new AttributePair { Name = "Weight", Attribute = 2 }));

			Constraint<IEdge> constraint = new Constraint<IEdge>();
			constraint.Criteria.Add(new RangeCriterion("Value", 9, 100));
			constraint.Criteria.Add(new LessThanCriterion("Value", 33));
			constraint.Criteria.Add(new GreaterThanCriterion("Weight", 1));
			IEdge edge = new Edge();

			AllPaths ksp = new AllPaths(graph, A, "Weight", constraint);

			Assert.AreEqual(0, ksp.PathsTo(D).Count);
		}
	}
}
