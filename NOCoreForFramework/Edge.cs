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
		/// <remarks>
		/// See also <seealso cref="Edge(int)"/>
		/// </remarks>
		public double Weight
		{
			get;
			set;
		}

		/// <inheritdoc />
		public IDictionary<string, IComparable> Attributes
		{
			get;
			set;
		}

		public IComparable this[String key]
		{
			get
			{
				return Attributes[key];
			}
			set
			{
				Attributes[key] = value;
			}
		}

		#endregion

		#region Constructors

		/// <summary>
		/// This equals to 
		/// ```cs
		/// new Edge(1)
		/// ```
		/// See also <seealso cref="Edge(double)"/>
		/// </summary>
		public Edge()
			: this(1)
		{
		}

		/// <summary>
		/// Initialises an **Edge** with <see cref="Weight"/> value of `1`.
		/// </summary>
		/// <param name="weight"></param>
		public Edge(double weight)
		{
			Id = IdGenerator.GenerateEdgeId();
			Label = Id;
			Weight = weight;
			IsBlocked = false;
			Attributes = new Dictionary<string, IComparable>();
		}
		#endregion

		public bool HasAttribute(String name)
		{
			return Attributes.ContainsKey(name);
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
