using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Reflection;
using NetworkObservabilityCore.Utils;

namespace NetworkObservabilityCore.Xml
{
	/// <summary>
	/// **GraphXML** is a special designed tool to read/load.
	/// See <see cref="IGraph"/> from/to xml file.
	/// </summary>
    public class GraphXML
    { 
		/// <summary>
		/// The actual Xml file.
		/// </summary>
		public XDocument File
		{
			get;
			protected set;
		}

		/// <summary>
		/// Mapping full name of a type to an <see cref="Assembly"/>.
		/// </summary>
		public Dictionary<String, Assembly> DependencyMap
		{
			get;
			set;
		}

		#region Constructors
		/// <summary>
		/// Default constructor of **GraphXML**.
		/// </summary>
		public GraphXML()
			: this("1.0", "utf-8", "false")
		{
		}

		/// <summary>
		/// A constructor allow user to define specification of Xml.
		/// See also <seealso cref="XDeclaration(string, string, string)"/>
		/// </summary>
		/// <param name="version">The version of the Xml, usually "1.0".</param>
		/// <param name="encoding">The encoding of Xml document.</param>
		/// <param name="standalone">A string containing "yes" or "no" that specifies whether the Xml is standalone or requires external entities tobe resolved.</param>
		public GraphXML(String version, String encoding, String standalone)
		{
			File = new XDocument(new XDeclaration(version, encoding, standalone));
			DependencyMap = new Dictionary<String, Assembly>();
		}
		#endregion

		#region Methods
		/// <summary>
		/// Saves a **graph** object as Xml file at given path with
		/// a default root name. See <see cref="Save(string, IGraph, string)"/>
		/// for more details.
		/// </summary>
		/// <param name="path">File path to store **graph**.</param>
		/// <param name="graph">Graph object to be saved.</param>
		public void Save(String path, IGraph graph)
		{
			Save(path, graph, "NetworkObservabilityCore");
		}
		
		/// <summary>
		/// Saves a **graph** object as Xml file at given path with
		/// a root name specified.
		/// </summary>
		/// <param name="path">File path to store **graph**.</param>
		/// <param name="graph">Graph object to be saved.</param>
		/// <param name="rootName">Root name of the Xml file.</param>
		/// <remarks>
		/// <see cref="DumpTo(IGraph, ref XElement)"/> will be used
		/// to store the **graph** object.
		/// </remarks>
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

		/// <summary>
		/// Reads Xml file.
		/// </summary>
		/// <param name="path">File path of the Xml file.</param>
		/// <returns><see cref="IGraph"/> object stored in the Xml file.</returns>
		/// <remarks>
		/// > [!Note]
		/// > <see cref="Dump(XElement)"/> is used to load the <see cref="IGraph"/> 
		/// > from **XElement** xnode.
		/// </remarks>
		public IGraph Read(String path)
		{
			File = XDocument.Load(path);
			IGraph graph = Dump(File.Root);

			return graph;
		}

		#region Save helper methods
		/// <summary>
		/// Dumps a <see cref="IGraph"/> into root node in
		/// <see cref="XElement"/> representation.
		/// </summary>
		/// <param name="graph">Graph object to be dumped.</param>
		/// <param name="root">The root node where the **graph** will be saved at.</param>
		/// <remarks>
		/// > [!Note]
		/// > This method utilises <see cref="CreateXElement(IEdge)"/>,
		/// > <see cref="CreateXElement(INode)"/>, <see cref="CreateDependencyXElement"/>
		/// > and <see cref="CreateXElementIdGenerator"/> to store all the details
		/// > of a **graph**.
		/// </remarks>
		protected virtual void DumpTo(IGraph graph, ref XElement root)
		{

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

			XElement dependenciesNode = CreateDependencyXElement();

			XElement indexGeneratorNode = CreateXElementIdGenerator();

			root.Add(dependenciesNode);
			root.Add(indexGeneratorNode);
			root.Add(graphNode);
		}

		/// <summary>
		/// Creates an XElement representation of <see cref="INode"/>.
		/// </summary>
		/// <param name="node">
		/// A <see cref="INode"/> object to be serialized.
		/// </param>
		/// <returns>**XElement** representation of **node**.</returns>
		/// <remarks>
		/// > [!Note]
		/// > This method uses <see cref="CreateAttributes{K, V}(IDictionary{K, V}, string)"/>
		/// > to serialize <see cref="IConstrainable.DescriptiveAttributes"/> and
		/// > <see cref="IConstrainable.NumericAttributes"/>. <see cref="DependencyMap"/> is
		/// > also updated when serialising the **node**.
		/// </remarks>
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
			var numericAttributes = CreateAttributes(node.NumericAttributes, "NumericAttributes");
			var descriptiveAttributes = CreateAttributes(node.DescriptiveAttributes, "DescriptiveAttributes");

			xelement.Add(isObserver);
			xelement.Add(isObserverInclusive);
			xelement.Add(isVisible);
			xelement.Add(isBlocked);
			xelement.Add(label);
			xelement.Add(numericAttributes);
			xelement.Add(descriptiveAttributes);

			return xelement;
		}

		/// <summary>
		/// Creates an XElement representation of <see cref="IEdge"/>.
		/// </summary>
		/// <param name="edge">
		/// A <see cref="IEdge"/> object to be serialized.
		/// </param>
		/// <returns>**XElement** representation of **edge**.</returns>
		/// <remarks>
		/// > [!Note]
		/// > This method uses <see cref="CreateAttributes{K, V}(IDictionary{K, V}, string)"/>
		/// > to serialize <see cref="IConstrainable.DescriptiveAttributes"/> and
		/// > <see cref="IConstrainable.NumericAttributes"/>. <see cref="DependencyMap"/> is
		/// > also updated when serialising the **edge**.
		/// </remarks>
		protected virtual XElement CreateXElement(IEdge edge)
		{
			Type edgeType = edge.GetType();
			XElement xelement = new XElement("Edge", new XAttribute("Id", edge.Id), new XAttribute("Type", edgeType.FullName));
			DependencyMap[edgeType.FullName] = edgeType.GetTypeInfo().Assembly;

			var from = new XElement("From", edge.From.Id);
			var label = new XElement("Label", edge.Label);
			var to = new XElement("To", edge.To.Id);
			var isBlocked = new XElement("IsBlocked", edge.IsBlocked);
			var numericAttributes = CreateAttributes(edge.NumericAttributes, "NumericAttributes");
			var descriptiveAttributes = CreateAttributes(edge.DescriptiveAttributes, "DescriptiveAttributes");

			xelement.Add(from);
			xelement.Add(label);
			xelement.Add(to);
			xelement.Add(isBlocked);
			xelement.Add(numericAttributes);
			xelement.Add(descriptiveAttributes);

			return xelement;
		}

		/// <summary>
		/// Serialises <see cref="DependencyMap"/> to Xml representation.
		/// </summary>
		/// <returns>**XElement** representation of <see cref="DependencyMap"/>.</returns>
		protected virtual XElement CreateDependencyXElement()
		{
			XElement dependenciesNode = new XElement("Dependencies");

			var dependencies = DependencyMap.Values.Distinct();
			foreach (var dependency in dependencies)
			{
				var types = DependencyMap.Where(type => type.Value.FullName == dependency.FullName);
				XElement dependencyNode = new XElement("Dependency", new XAttribute("Name", dependency.ManifestModule.Name));
				foreach (var typePair in types)
				{
					dependencyNode.Add(new XElement("Type", typePair.Key));
				}
				dependenciesNode.Add(dependencyNode);
			};

			return dependenciesNode;
		}

		/// <summary>
		/// Saves <see cref="IdGenerator"/> as **XElement**.
		/// </summary>
		/// <returns>**XElement** representation of <see cref="IdGenerator"/>.</returns>
		protected XElement CreateXElementIdGenerator()
		{
			XElement element = new XElement("IdGenerator", new XElement("NodeIdIndex", IdGenerator.nodeIdIndex.ToString()),
														   new XElement("EdgeIdIndex", IdGenerator.edgeIdIndex.ToString()));

			return element;
		}

		/// <summary>
		/// Serialises an attributes object.
		/// </summary>
		/// <typeparam name="K">The key type. Usually a **string**.</typeparam>
		/// <typeparam name="V">The value type. Usually a **string** or **double**.</typeparam>
		/// <param name="attributes">An attributes object.</param>
		/// <param name="name">The name of **XElement** node.</param>
		/// <returns>A **XElement** representation of attributes object.</returns>
		protected XElement CreateAttributes<K, V>(IDictionary<K, V> attributes, String name)
		{
			XElement xelement = new XElement(name);
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
		#endregion

		#region Read helper methods
		/// <summary>
		/// Restores from a **XElement** node to
		/// a <see cref="IGraph"/>.
		/// </summary>
		/// <param name="root">
		/// The root node where the Xml representation of **graph** is stored at.
		/// </param>
		/// <remarks>
		/// > [!Note]
		/// > This method utilises <see cref="LoadNode(XElement)"/>,
		/// > <see cref="LoadEdge(XElement)"/>, <see cref="SetIdsStartFrom(XElement)"/>
		/// > and <see cref="CreateXElementIdGenerator"/> to restore all the details
		/// > of a **graph**.
		/// </remarks>
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
				var from = graph.AllNodes[tuple.From];
				var to = graph.AllNodes[tuple.To];
				var edge = tuple.Through;
				graph.ConnectNodeToWith(from, to, edge);
			}

			SetIdsStartFrom(root);

			return graph;
		}

		/// <summary>
		/// Sets <see cref="IdGenerator"/> indices to start from the values
		/// stores in the root node of Xml file.
		/// </summary>
		/// <param name="root">The root node of Xml file.</param>
		protected void SetIdsStartFrom(XElement root)
		{
			var xnode = root.Element("IdGenerator");
			IdGenerator.SetNodeIdStartFrom(Int32.Parse(xnode.Element("NodeIdIndex").Value));
			IdGenerator.SetEdgeIdStartFrom(Int32.Parse(xnode.Element("EdgeIdIndex").Value));

		}

		/// <summary>
		/// Loads <see cref="INode"/> object from a **XElement** representation of a **node**.
		/// </summary>
		/// <param name="xnode">A **XElement** representation of **node**.</param>
		/// <returns>A <see cref="INode"/> object.</returns>
		/// <remarks>
		/// > [!Note]
		/// > This method utilises <see cref="LoadNumericAttributes(XElement)"/> and
		/// > <see cref="LoadDescriptiveAttributes(XElement)"/> to load attributes of
		/// > <see cref="IConstrainable.NumericAttributes"/> and
		/// > <see cref="IConstrainable.DescriptiveAttributes"/> respectively.
		/// </remarks>
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
			node.NumericAttributes = LoadNumericAttributes(xnode.Element("NumericAttributes"));
			node.DescriptiveAttributes = LoadDescriptiveAttributes(xnode.Element("DescriptiveAttributes"));
			node.ConnectOut = new List<IEdge>();
			node.ConnectIn = new List<IEdge>();

			return node;
		}

		/// <summary>
		/// Loads <see cref="IEdge"/> object from a **XElement** representation of a **edge**.
		/// </summary>
		/// <param name="xedge">**XElement** representation of an **edge**.</param>
		/// <returns>A <see cref="FromToThrough"/> consists of starting and ending point of <see cref="IEdge"/>
		/// and the <see cref="IEdge"/> itself.
		/// </returns>
		/// <remarks>
		/// > [!Note]
		/// > This method utilises <see cref="LoadNumericAttributes(XElement)"/> and
		/// > <see cref="LoadDescriptiveAttributes(XElement)"/> to load attributes of
		/// > <see cref="IConstrainable.NumericAttributes"/> and
		/// > <see cref="IConstrainable.DescriptiveAttributes"/> respectively.
		/// </remarks>
		protected virtual FromToThrough<String, String, IEdge> LoadEdge(XElement xedge)
		{
			var edgeTypeName = xedge.Attribute("Type").Value;
			Type edgeType = DependencyMap[edgeTypeName].GetType(edgeTypeName);
			IEdge edge = Activator.CreateInstance(edgeType) as IEdge;

			PropertyInfo property = edgeType.GetProperty("Id", BindingFlags.NonPublic | 
				BindingFlags.Public | BindingFlags.Instance);
			property.SetValue(edge, xedge.Attribute("Id").Value);
			edge.Label = xedge.Element("Label").Value;
			edge.IsBlocked = Boolean.Parse(xedge.Element("IsBlocked").Value);
			edge.NumericAttributes = LoadNumericAttributes(xedge.Element("NumericAttributes"));
			edge.DescriptiveAttributes = LoadDescriptiveAttributes(xedge.Element("DescriptiveAttributes"));

			var from = xedge.Element("From").Value;
			var to = xedge.Element("To").Value;

			return FromToThrough.Create(from, to, edge);
		}

		/// <summary>
		/// Loads from a **XElement** representation of an numerical attributes object.
		/// </summary>
		/// <param name="xelement">**XElement** which the attributes object is serialised to.</param>
		/// <returns>A dictionary object. See <see cref="IConstrainable.NumericAttributes"/>.
		/// </returns>
		protected IDictionary<String, Double> LoadNumericAttributes(XElement xelement)
		{
			var attributes = new Dictionary<String, Double>();
			
			foreach (var xattribute in xelement.Elements())
			{
				var xkey = xattribute.Attribute("Key").Value;
				var xvalue = xattribute.Attribute("Value").Value;
				attributes[xkey] = Double.Parse(xvalue);
			}
			return attributes;
		}

		/// <summary>
		/// Loads from a **XElement** representation of a descriptive attributes object.
		/// </summary>
		/// <param name="xelement">**XElement** which the attributes object is serilaised to.</param>
		/// <returns>A dictionary object. See <see cref="IConstrainable.DescriptiveAttributes"/>.
		/// </returns>
		protected IDictionary<String, String> LoadDescriptiveAttributes(XElement xelement)
		{
			var attributes = new Dictionary<String, String>();
			
			foreach (var xattribute in xelement.Elements())
			{
				var xkey = xattribute.Attribute("Key").Value;
				var xvalue = xattribute.Attribute("Value").Value;
				attributes[xkey] = xvalue;
			}
			return attributes;
		}

		private bool HasConstructor(Type type)
		{
			var hasConstructor = type.GetConstructor(BindingFlags.Default, null, Type.EmptyTypes, null) != null;
			return !(type.IsPrimitive || hasConstructor);
		}
		#endregion
		#endregion
	}
}
