using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Xml.Linq;
using System.IO;
using System.Reflection;
using System.Diagnostics;
using System.Runtime;
using NetworkObservabilityCore.Utils;

namespace NetworkObservabilityCore.Xml
{
    public class GraphXML
    {
		public XDocument File
		{
			get;
			protected set;
		}

		public Dictionary<String, Assembly> DependencyMap
		{
			get;
			set;
		}

		public GraphXML()
			: this("1.0", "utf-8", "false")
		{
		}
		
		public GraphXML(String version, String encoding, String standalone)
		{
			File = new XDocument(new XDeclaration(version, encoding, standalone));
			DependencyMap = new Dictionary<String, Assembly>();
		}

		public void Save(String path, IGraph graph)
		{
			Save(path, graph, "NetworkObservabilityCore");
		}
		
		public void Save(String path, IGraph graph, String rootName)
		{
			XElement root = new XElement(rootName);
			DumpTo(graph, ref root);
			File.Add(root);

			// New way to save
			File.Save(path);

			// Old way to save
			/*
			var xml = System.IO.File.Create(path);
			var writer = new StreamWriter(xml);
			File.Save(writer);
			writer.Dispose();
			*/
		}

		protected virtual void DumpTo(IGraph graph, ref XElement root)
		{
			XElement dependenciesNode = new XElement("Dependencies");

			Type graphType = graph.GetType();
			XElement graphNode = new XElement("Graph", new XAttribute("Type", graphType.FullName));
			DependencyMap[graphType.FullName] = graphType.GetTypeInfo().Assembly;

			XElement nodes = new XElement("Nodes");
			foreach (var pair in graph.AllNodes)
			{
				INode node = pair.Value;
				nodes.Add(CreateXElement(node));
			}
			graphNode.Add(nodes);

			XElement edges = new XElement("Edges");
			foreach (var pair in graph.AllEdges)
			{
				IEdge edge = pair.Value;
				edges.Add(CreateXElement(edge));
			}
			graphNode.Add(edges);

			var dependencies = DependencyMap.Values.Distinct();
			foreach (var dependency in dependencies)
			{
				var types = DependencyMap.Where(type => type.Value.FullName == dependency.FullName);
				XElement dependencyNode = new XElement("Dependency", new XAttribute("Name", dependency.ManifestModule.Name));
				foreach(var typePair in types)
				{
					dependencyNode.Add(new XElement("Type", typePair.Key));
				}
				dependenciesNode.Add(dependencyNode);
			}

			XElement indexGeneratorNode = CreateXElementIdGenerator();

			root.Add(dependenciesNode);
			root.Add(indexGeneratorNode);
			root.Add(graphNode);
		}

		protected virtual XElement CreateXElement(INode node)
		{
			Type nodeType = node.GetType();
			XElement xelement = new XElement("Node", new XAttribute("Id", node.Id), new XAttribute("Type", nodeType.FullName));
			DependencyMap[nodeType.FullName] = nodeType.GetTypeInfo().Assembly;

			var isObserver = new XElement("IsObserver", node.IsObserver);
			var isObserverInclusive = new XElement("IsObserverInclusive", node.IsObserverInclusive);
			var isVisible = new XElement("IsVisible", node.IsVisible);
			var isBlocked = new XElement("IsBlocked", node.IsBlocked);
			var label = new XElement("Label", node.Label);
			var attributes = CreateAttributes(node.Properties);

			xelement.Add(isObserver);
			xelement.Add(isObserverInclusive);
			xelement.Add(isVisible);
			xelement.Add(isBlocked);
			xelement.Add(label);
			xelement.Add(attributes);

			return xelement;
		}

		protected virtual XElement CreateXElement(IEdge edge)
		{
			Type edgeType = edge.GetType();
			XElement xelement = new XElement("Edge", new XAttribute("Id", edge.Id), new XAttribute("Type", edgeType.FullName));
			DependencyMap[edgeType.FullName] = edgeType.GetTypeInfo().Assembly;

			var from = new XElement("From", edge.From.Id);
			var label = new XElement("Label", edge.Label);
			var to = new XElement("To", edge.To.Id);
			var isBlocked = new XElement("IsBlocked", edge.IsBlocked);
			var attributes = CreateAttributes(edge.Properties);

			xelement.Add(from);
			xelement.Add(label);
			xelement.Add(to);
			xelement.Add(isBlocked);
			xelement.Add(attributes);

			return xelement;
		}

		protected XElement CreateXElementIdGenerator()
		{
			XElement element = new XElement("IdGenerator", new XElement("NodeIdIndex", IdGenerator.nodeIdIndex.ToString()),
														   new XElement("EdgeIdIndex", IdGenerator.edgeIdIndex.ToString()));

			return element;
		}

		protected XElement CreateAttributes<K, V>(IDictionary<K, V> attributes)
		{
			XElement xelement = new XElement("Attributes");
			foreach (var pair in attributes)
			{
				var xattribute = new XElement("Attribute");
				var xkey = new XAttribute("Key", pair.Key);
				var xvalue = new XAttribute("Value", pair.Value);
				Type valueType = pair.Value.GetType();
				var typeFullName = valueType.FullName;
				var xvalueType = new XAttribute("ValueType", typeFullName);
				if (!typeFullName.Contains("System"))
					DependencyMap[typeFullName] = valueType.GetTypeInfo().Assembly;
				xattribute.Add(xkey, xvalue, xvalueType);

				xelement.Add(xattribute);
			}
			return xelement;
		}

		public IGraph Read(String path)
		{
			File = XDocument.Load(path);
			IGraph graph = Dump(File.Root);

			return graph;
		}

		protected virtual IGraph Dump(XElement root)
		{
			XElement dependencies = root.Element("Dependencies");
			foreach (var dependency in dependencies.Elements())
			{
				String assemPath = dependency.Attribute("Name").Value;

				foreach (var type in dependency.Elements("Type"))
				{
					DependencyMap[type.Value] = Assembly.LoadFrom(assemPath);
				}
			}

			XElement xgraph = root.Element("Graph");

			var graphTypeName = xgraph.Attribute("Type").Value;
			Type graphType = DependencyMap[graphTypeName].GetType(graphTypeName);

			IGraph graph = Activator.CreateInstance(graphType) as IGraph;

			IEnumerable<XElement> xnodes = xgraph.Element("Nodes").Elements();

			foreach (XElement xnode in xnodes)
			{
				graph.Add(LoadNode(xnode));
			}

			IEnumerable<XElement> xedges = xgraph.Element("Edges").Elements();

			foreach (XElement xedge in xedges)
			{
				var tuple = LoadEdge(xedge);
				var from = graph.AllNodes[tuple.Item1];
				var to = graph.AllNodes[tuple.Item2];
				var edge = tuple.Item3;
				graph.ConnectNodeToWith(from, to, edge);
			}

			SetIdsStartFrom(root);

			return graph;
		}

		protected void SetIdsStartFrom(XElement root)
		{
			var xnode = root.Element("IdGenerator");
			IdGenerator.SetNodeIdStartFrom(Int32.Parse(xnode.Element("NodeIdIndex").Value));
			IdGenerator.SetEdgeIdStartFrom(Int32.Parse(xnode.Element("EdgeIdIndex").Value));

		}

		protected virtual INode LoadNode(XElement xnode)
		{
			var nodeTypeName = xnode.Attribute("Type").Value;
			Type nodeType = DependencyMap[nodeTypeName].GetType(nodeTypeName);
			INode node = Activator.CreateInstance(nodeType) as INode;

			PropertyInfo property = nodeType.GetProperty("Id", BindingFlags.NonPublic | 
				BindingFlags.Public | BindingFlags.Instance);
			property.SetValue(node, xnode.Attribute("Id").Value);
			node.IsObserver = Boolean.Parse(xnode.Element("IsObserver").Value);
			node.IsObserverInclusive = Boolean.Parse(xnode.Element("IsObserverInclusive").Value);
			node.IsVisible = Boolean.Parse(xnode.Element("IsVisible").Value);
			node.Label = xnode.Element("Label").Value;
			node.IsBlocked = Boolean.Parse(xnode.Element("IsBlocked").Value);
			node.Properties = LoadAttributes(xnode.Element("Attributes"));
			node.ConnectOut = new List<IEdge>();
			node.ConnectIn = new List<IEdge>();

			return node;
		}

		protected virtual Tuple<String, String, IEdge> LoadEdge(XElement xedge)
		{
			var edgeTypeName = xedge.Attribute("Type").Value;
			Type edgeType = DependencyMap[edgeTypeName].GetType(edgeTypeName);
			IEdge edge = Activator.CreateInstance(edgeType) as IEdge;

			PropertyInfo property = edgeType.GetProperty("Id", BindingFlags.NonPublic | 
				BindingFlags.Public | BindingFlags.Instance);
			property.SetValue(edge, xedge.Attribute("Id").Value);
			edge.Label = xedge.Element("Label").Value;
			edge.IsBlocked = Boolean.Parse(xedge.Element("IsBlocked").Value);
			edge.Properties = LoadAttributes(xedge.Element("Attributes"));

			var from = xedge.Element("From").Value;
			var to = xedge.Element("To").Value;

			return Tuple.Create(from, to, edge);
		}

		protected IDictionary<String, IComparable> LoadAttributes(XElement xelement)
		{
			var attributes = new Dictionary<String, IComparable>();
			
			foreach (var xattribute in xelement.Elements())
			{
				var xkey = xattribute.Attribute("Key").Value;
				var xvalue = xattribute.Attribute("Value").Value;
				var valueTypeName = xattribute.Attribute("ValueType").Value;
				Type valueType;
				IComparable attribute;
				if (valueTypeName.Contains("System"))
				{
					valueType = Type.GetType(valueTypeName);
				}
				else
				{
					valueType = DependencyMap[valueTypeName].GetType(valueTypeName);
				}
				var value = ChangeType(xvalue, valueType);
				if (valueType.Equals(typeof(String)))
				{
					attribute = value;
				}
				else if (HasConstructor(valueType))
				{
					attribute = Activator.CreateInstance(valueType, value) as IComparable;
				}
				else
				{
					attribute = Activator.CreateInstance(valueType) as IComparable;
					attribute = value;
				}
				attributes[xkey] = attribute;
			}
			return attributes;
		}

		private bool HasConstructor(Type type)
		{
			var hasConstructor = type.GetConstructor(BindingFlags.Default, null, Type.EmptyTypes, null) != null;
			return !(type.IsPrimitive || hasConstructor);
		}

		private IComparable ChangeType(String str, Type type)
		{
			return Convert.ChangeType(str, type) as IComparable;
		}

	}
}
