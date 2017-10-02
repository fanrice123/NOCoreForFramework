using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetworkObservabilityCore;
using NetworkObservabilityCore.Utils;

namespace NOCoreTest
{
	class TesteSubNode2 : INode
	{
		public string Id
		{
			get;
			protected set;
		}

		public string Label
		{
			get;
			set;
		}

		public List<IEdge> ConnectOut
		{
			get;
			set;
		}

		public List<IEdge> ConnectIn
		{
			get;
			set;
		}

		public bool IsObserver
		{
			get;
			set;
		}

		public bool IsObserverInclusive
		{
			get;
			set;
		}

		public bool IsVisible
		{
			get;
			set;
		}

		public IDictionary<string, Double> NumericAttributes
		{
			get;
			set;
		}

		public IDictionary<string, String> DescriptiveAttributes
		{
			get;
			set;
		}

		public bool IsBlocked { get; set; }

		public double Value { get; set; }


		public TesteSubNode2()
		{
			Id = IdGenerator.GenerateNodeId();
			Label = Id;
			ConnectOut = new List<IEdge>();
			ConnectIn = new List<IEdge>();
			IsObserver = IsObserverInclusive = false;
			NumericAttributes = new Dictionary<string, Double>();
			DescriptiveAttributes = new Dictionary<string, String>();
		}

		public bool Equals(INode other)
		{
			return Id == other.Id;
		}

		public override int GetHashCode()
		{
			return Id.GetHashCode();
		}

		public Double this[String key]
		{
			get
			{
				return NumericAttributes[key];
			}
			set
			{
				NumericAttributes[key] = value;
			}
		}

		public bool HasNumericAttribute(string name)
		{
			return NumericAttributes.ContainsKey(name);
		}

		public bool HasDescriptiveAttribute(string name)
		{
			return DescriptiveAttributes.ContainsKey(name);
		}
	}
}
