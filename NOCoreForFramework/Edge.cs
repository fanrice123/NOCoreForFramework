using System;
using System.Collections.Generic;
using System.Text;
using NetworkObservabilityCore.Utils;

namespace NetworkObservabilityCore
{
	/// <summary>
	/// **Edge** implements <see cref="IEdge"/> and <see cref="IConstrainable"/>.
	/// </summary>
	public class Edge : IEdge
	{

		#region Properties

		/// <summary>
		/// Every **Edge** has its own unique Id.
		/// **Unless** you **know** what are you doing, otherwise **DO NOT**
		/// change the Id.
		/// See also <seealso cref="IdGenerator"/>
		/// > [!Important]
		/// > Changing Id in the middle of object life cycle
		/// > will result undefined behaviour.
		/// </summary>
		public String Id
		{
			get;
			protected set;
		}

		/// <inheritdoc />
		/// <remarks>
		/// The default value of a node's label is its <see cref="Id"/>.
		/// </remarks>
		public String Label
		{
			get;
			set;
		}

		/// <inheritdoc />
		public INode From
		{
			get;
			set;
		}

		/// <inheritdoc />
		public INode To
		{
			get;
			set;
		}

		/// <inheritdoc />
		/// <remarks>
		/// `false` by default.
		/// </remarks>
		public bool IsBlocked
		{
			get;
			set;
		}

		/// <inheritdoc />
		public IDictionary<String, Property> Properties
		{
			get;
			set;
		}

		public IComparable this[String key]
		{
			get
			{
                var property = Properties[key];
                var value = property.Value;
                IComparable retVal = null;
                switch (property.Type)
                {
                    case "System.Double":
                        retVal = Convert.ToDouble(value);
                        break;
                    case "System.Boolean":
                        retVal = Convert.ToBoolean(value);
                        break;
                    case "System.String":
                        retVal = value;
                        break;
                }
                return retVal;
			}
			set
			{
                var rawValue = value;
                var type = rawValue.GetType().FullName;
                var property = new Property()
                {
                    Type = rawValue.GetType().FullName,
                    Value = String.Format("{0}", rawValue),
                    Key = key
                };
                Properties[key] = property;
			}
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Initialises an **Edge**
		/// </summary>
		public Edge()
			: this(new AttributePair[0])
		{
		}

		/// <summary>
		/// Initialises an **Edge** with <see cref="AttributePair"/>
		/// </summary>
		public Edge(params Property[] properties)
		{
			Id = IdGenerator.GenerateEdgeId();
			Label = Id;
			IsBlocked = false;
			Properties = new Dictionary<string, Property>();
			foreach (var attrPair in properties)
			{
				Properties[attrPair.Name] = attrPair.Attribute;
			}
		}
		#endregion

		public bool HasAttribute(String name)
		{
			return Properties.ContainsKey(name);
		}

		public override int GetHashCode()
		{
			return Id.GetHashCode();
		}

		bool IEquatable<IEdge>.Equals(IEdge other)
		{
			return Id == other.Id;
		}

		public override string ToString()
		{
			return String.Format("{0}: {1}, From {2} To {3}", Id, Label, From.Id, To.Id);
		}

	}
}
