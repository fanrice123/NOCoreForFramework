using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetworkObservabilityCore;
using NetworkObservabilityCore.Xml;
using NetworkObservabilityCore.Utils;
using System.Collections.Generic;
using System.Xml.Linq;

namespace NOCoreTest
{
	[TestClass]
	public class GraphXMLTest
	{
		GraphXML writer;
		IGraph graph;
		String emptyGraphPath;
		String graph1NodePath;
		String graph1Path;
		String graph2Path;
		String graphExoticNodePath;
		String extendableGraphPath;

		[TestInitialize]
		public void Initialize()
		{
			writer = new GraphXML();
			IdGenerator.SetNodeIdStartFrom(0);
			IdGenerator.SetEdgeIdStartFrom(0);
			emptyGraphPath = "empty graph.xml";
			graph1NodePath = "graph.xml";
			graph1Path = "graph1.xml";
			graph2Path = "graph2.xml";
			graphExoticNodePath = "exotic graph.xml";
			extendableGraphPath = "extendable graph.xml";
		}

		[TestMethod]
		public void TestConstructor()
		{
			writer = new GraphXML();
		}

		[TestMethod]
		public void TestSaveEmptyGraph()
		{
			writer.Save(emptyGraphPath, new Graph());
		}

		[TestMethod]
		public void TestSaveGraphWithOneNode()
		{
			graph = new Graph();
			graph.Add(new Node());
			writer.Save(graph1NodePath, graph);

		}

		[TestMethod]
		public void TestSaveNormalSizeGraph1()
		{
			graph = new Graph();

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

			graph.ConnectNodeToWith(A, B, new Edge(8));
			graph.ConnectNodeToWith(A, D, new Edge(9));
			graph.ConnectNodeToWith(A, E, new Edge(4));
			graph.ConnectNodeToWith(B, C, new Edge(1));
			graph.ConnectNodeToWith(C, B, new Edge(2));
			graph.ConnectNodeToWith(C, D, new Edge(3));
			graph.ConnectNodeToWith(D, C, new Edge(2));
			graph.ConnectNodeToWith(D, E, new Edge(7));
			graph.ConnectNodeToWith(E, C, new Edge(1));

			writer.Save(graph1Path, graph);
		}

		[TestMethod]
		public void TestSaveNormalSizeGraph2()
		{
			graph = new Graph();

			var zero = new Node() { Label = "0" };
			var one = new Node() { Label = "1" };
			var two = new Node() { Label = "2" };
			var three = new Node() { Label = "3" };
			var four = new Node() { Label = "4" };
			var five = new Node() { Label = "5" };
			var six = new Node() { Label = "6" };
			var seven = new Node() { Label = "7" };
			var eight = new Node() { Label = "8" };
			graph.Add(zero);
			graph.Add(one);
			graph.Add(two);
			graph.Add(three);
			graph.Add(four);
			graph.Add(five);
			graph.Add(six);
			graph.Add(seven);
			graph.Add(eight);

			graph.ConnectNodeToWith(zero, one, new Edge(4));
			graph.ConnectNodeToWith(one, zero, new Edge(4));
			graph.ConnectNodeToWith(zero, seven, new Edge(8));
			graph.ConnectNodeToWith(seven, zero, new Edge(8));
			graph.ConnectNodeToWith(one, seven, new Edge(11));
			graph.ConnectNodeToWith(seven, one, new Edge(11));
			graph.ConnectNodeToWith(one, two, new Edge(8));
			graph.ConnectNodeToWith(two, one, new Edge(8));
			graph.ConnectNodeToWith(two, eight, new Edge(2));
			graph.ConnectNodeToWith(eight, two, new Edge(2));
			graph.ConnectNodeToWith(seven, eight, new Edge(7));
			graph.ConnectNodeToWith(eight, seven, new Edge(7));
			graph.ConnectNodeToWith(seven, six, new Edge(1));
			graph.ConnectNodeToWith(six, seven, new Edge(1));
			graph.ConnectNodeToWith(eight, six, new Edge(6));
			graph.ConnectNodeToWith(six, eight, new Edge(6));
			graph.ConnectNodeToWith(two, three, new Edge(7));
			graph.ConnectNodeToWith(three, two, new Edge(7));
			graph.ConnectNodeToWith(two, five, new Edge(4));
			graph.ConnectNodeToWith(five, two, new Edge(4));
			graph.ConnectNodeToWith(six, five, new Edge(2));
			graph.ConnectNodeToWith(five, six, new Edge(2));
			graph.ConnectNodeToWith(three, five, new Edge(14));
			graph.ConnectNodeToWith(five, three, new Edge(14));
			graph.ConnectNodeToWith(three, four, new Edge(9));
			graph.ConnectNodeToWith(four, three, new Edge(9));
			graph.ConnectNodeToWith(five, four, new Edge(10));
			graph.ConnectNodeToWith(four, five, new Edge(10));

			writer.Save(graph2Path, graph);

		}

		[TestMethod]
		public void TestSaveGraphWithUnknownNode()
		{
			graph = new Graph();

			var A = new TestSubNode() { Label = "A" };
			var B = new Node() { Label = "B" };
			var C = new Node() { Label = "C" };
			var D = new TestSubNode() { Label = "D" };
			var E = new TesteSubNode2() { Label = "E" };
			graph.Add(A);
			graph.Add(B);
			graph.Add(C);
			graph.Add(D);
			graph.Add(E);

			graph.ConnectNodeToWith(A, B, new Edge(8));
			graph.ConnectNodeToWith(A, D, new TestSubEdge(9));
			graph.ConnectNodeToWith(A, E, new Edge(4));
			graph.ConnectNodeToWith(B, C, new Edge(1));
			graph.ConnectNodeToWith(C, B, new Edge(2));
			graph.ConnectNodeToWith(C, D, new TestSubEdge(3));
			graph.ConnectNodeToWith(D, C, new Edge(2));
			graph.ConnectNodeToWith(D, E, new Edge(7));
			graph.ConnectNodeToWith(E, C, new Edge(1));

			writer.Save(graphExoticNodePath, graph);
		}

		[TestMethod]
		public void TestReadGraphWithUnknownNode()
		{
			graph = writer.Read(graphExoticNodePath) as Graph;
			Assert.AreEqual(5, graph.NodeCount);
		}

		[TestMethod]
		public void TestReadEmptyGraph()
		{
			graph = writer.Read(emptyGraphPath) as Graph;
			Assert.AreEqual(graph.NodeCount, 0);
		}

		[TestMethod]
		public void TestReadGraphWithOneNode()
		{
			graph = writer.Read(graph1NodePath) as Graph;
			Assert.AreEqual(1, graph.NodeCount);
			var fakeNode = new Node();
			Assert.AreEqual(1, ParseId(fakeNode.Id));
			var fakeEdge = new Edge();
			Assert.AreEqual(0, ParseId(fakeEdge.Id));
		}

		[TestMethod]
		public void TestReadNormalSizeGraph1()
		{
			graph = writer.Read(graph1Path) as Graph;
			Assert.AreEqual(5, graph.NodeCount);
			var fakeNode = new Node();
			var fakeEdge = new Edge();
			Assert.AreEqual(5, ParseId(fakeNode.Id));
			Assert.AreEqual(9, ParseId(fakeEdge.Id));

		}

		[TestMethod]
		public void TestReadNormalSizeGraph2()
		{
			graph = writer.Read(graph2Path) as Graph;
			Assert.AreEqual(9, graph.NodeCount);
			var fakeNode = new Node();
			var fakeEdge = new Edge();
			Assert.AreEqual(9, ParseId(fakeNode.Id));
			Assert.AreEqual(28, ParseId(fakeEdge.Id));
		}

		[TestMethod]
		public void TestWrite50000NodeGraph()
		{
			graph = new Graph();
			List<INode> nodes = new List<INode>();

			for (int i = 0; i != 50000; ++i)
			{
				var node = new TesteSubNode2();
				nodes.Add(node);
				graph.Add(node);
			}
			Assert.AreEqual(nodes.Count, graph.NodeCount);
			Assert.AreEqual(50000, graph.NodeCount);

			Random random = new Random();

			for (int i = 0; i != 50000; ++i)
			{
				var index1 = random.Next(0, 50000);
				var index2 = random.Next(0, 50000);
				while (index2 == index1)
					index2 = random.Next(0, 50000);
				graph.ConnectNodeToWith(nodes[index1], nodes[index2], new Edge(random.Next(1, 600)));
			}
			Assert.AreEqual(graph.AllEdges.Count, 50000);

			writer.Save("Large Graph2.xml", graph);
		}

		[TestMethod]
		public void TestWriter1000NodeGraph()
		{
			graph = new Graph();
			List<INode> nodes = new List<INode>();

			for (int i = 0; i != 1000; ++i)
			{
				var node = new TesteSubNode2();
				nodes.Add(node);
				graph.Add(node);
			}
			Assert.AreEqual(nodes.Count, graph.NodeCount);
			Assert.AreEqual(1000, graph.NodeCount);

			Random random = new Random();

			for (int i = 0; i != 1000; ++i)
			{
				var index1 = random.Next(0, 1000);
				var index2 = random.Next(0, 1000);
				while (index2 == index1)
					index2 = random.Next(0, 1000);
				graph.ConnectNodeToWith(nodes[index1], nodes[index2], new Edge(random.Next(1, 600)));
			}
			Assert.AreEqual(graph.AllEdges.Count, 1000);

			writer.Save("Large Graph.xml", graph);
		}

		[TestMethod]
		public void TestRead1000NodeGraph()
		{
			graph = writer.Read("Large Graph.xml") as Graph;
		}

		[TestMethod]
		public void TestRead50000NodeGraph()
		{
			graph = writer.Read("Large Graph2.xml") as Graph;
		}

		[TestMethod]
		public void TestWriteExtendableGraph()
		{
			graph = new Graph();

			var A = new TestSubNode() { Label = "A" };
			var B = new Node() { Label = "B" };
			var C = new Node() { Label = "C" };
			var D = new TestSubNode() { Label = "D" };
			var E = new TesteSubNode2() { Label = "E" };
			E["test"] = "test";
			B["dummy"] = 15;
			C["time"] = "NOthing";
			A["3"] = 0.99;

			graph.Add(A);
			graph.Add(B);
			graph.Add(C);
			graph.Add(D);
			graph.Add(E);

			graph.ConnectNodeToWith(A, B, new Edge(8));
			graph.ConnectNodeToWith(A, D, new TestSubEdge(9));
			graph.ConnectNodeToWith(A, E, new Edge(4));
			graph.ConnectNodeToWith(B, C, new Edge(1));
			graph.ConnectNodeToWith(C, B, new Edge(2));
			graph.ConnectNodeToWith(C, D, new TestSubEdge(3));
			graph.ConnectNodeToWith(D, C, new Edge(2));
			graph.ConnectNodeToWith(D, E, new Edge(7));
			graph.ConnectNodeToWith(E, C, new Edge(1));

			writer.Save(extendableGraphPath, graph);
		}

		[TestMethod]
		public void TestReadExtendableGraph()
		{
			graph = writer.Read(extendableGraphPath) as Graph;
		}

		private int ParseId(String id)
		{
			return Int32.Parse(id.Substring(1, id.Length - 1));
		}

	}
}
