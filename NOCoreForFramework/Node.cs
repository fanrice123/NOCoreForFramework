using System;
using System.Collections.Generic;
using System.Text;
using NetworkObservabilityCore.Utils;

namespace NetworkObservabilityCore
{
	/// <summary>
	/// **Node** implements <see cref="INode"/> and <see cref="IConstrainable"/>.
	/// </summary>
    public class Node : INode
    {

		#region Properties

		/// <summary>
		/// Every **Node** has its own unique Id.
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
		public List<IEdge> ConnectOut
		{
			get;
			set;
		}

		/// <inheritdoc />
		public List<IEdge> ConnectIn
		{
			get;
			set;
		}

		/// <inheritdoc />
		/// <remarks>
		/// `false` by default.
		/// </remarks>
		public bool IsObserver
		{
			get;
			set;
		}

		/// <inheritdoc />
		/// <remarks>
		/// `false` by default.
		/// </remarks>
		public bool IsObserverInclusive
		{
			get;
			set;
		}

		/// <inheritdoc />
		/// <remarks>
		/// `true` by default.
		/// </remarks>
		public bool IsVisible
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
		public IDictionary<string, Double> NumericAttributes
		{
			get;
			set;
		}

		/// <inheritdoc />
		public IDictionary<string, String> DescriptiveAttributes
		{
			get;
			set;
		}

		/// <inheritdoc />
		/// <exception cref="KeyNotFoundException">Thrown if the attribute does not exist.</exception>
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

		#endregion

		#region Constructors

		/// <summary>
		/// Initialises an **Node** with <see cref="AttributePair{T}"/> 
		/// where type parameter is <see cref="String"/>.
		/// </summary>
		/// <param name="numericAttr">Attribute pairs.</param>
		/// <param name="descriptiveAttr">Attribute pairs.</param>
		public Node()
			: this(new AttributePair<Double>[0], new AttributePair<String>[0])
		{
		}

		/// <summary>
		/// Initialises an **Node** with numeric attributes <see cref="AttributePair{T}"/>
		/// where type parameter is <see cref="Double"/>.
		/// </summary>
		public Node(params AttributePair<Double>[] numericAttr)
			: this(numericAttr, new AttributePair<String>[0])
		{
		}

		/// <summary>
		/// Initialises an **Node** with descriptive attributes <see cref="AttributePair{T}"/>
		/// where type parameter is <see cref="String"/>.
		/// </summary>
		public Node(params AttributePair<String>[] descriptiveAttr)
			: this(new AttributePair<Double>[0], descriptiveAttr)
		{
		}


		/// <summary>
		/// Initialises an **Edge** with <see cref="AttributePair{T}"/> 
		/// where type parameter is <see cref="String"/>.
		/// </summary>
		/// <param name="numericAttr">Attribute pairs.</param>
		/// <param name="descriptiveAttr">Attribute pairs.</param
		public Node(AttributePair<Double>[] numericAttr, AttributePair<String>[] descriptiveAttr)
		{
			Id = IdGenerator.GenerateNodeId();
			Label = Id;
			ConnectOut = new List<IEdge>();
			ConnectIn = new List<IEdge>();
			IsObserver = IsObserverInclusive = IsBlocked = false;
			IsVisible = true;
			NumericAttributes = new Dictionary<string, Double>();
			DescriptiveAttributes = new Dictionary<string, String>();
		}
		#endregion

		#region Methods

		/// <inheritdoc />
		public bool HasNumericAttribute(string name)
		{
			return NumericAttributes.ContainsKey(name);
		}

		/// <inheritdoc />
		public bool HasDescriptiveAttribute(string name)
		{
			return DescriptiveAttributes.ContainsKey(name);
		}

		/// <summary>
		/// Compares **INode** to other with their <see cref="Id"/>.
		/// </summary>
		/// <param name="other">An <see cref="INode"/>.</param>
		/// <returns>Returns `true` if they are equal.</returns>
		public virtual bool Equals(INode other)
		{
			return Id == other.Id;
		}

		#endregion

		#region Object methods
		/// <summary>
		/// Gets hash value based on <see cref="Id"/>
		/// </summary>
		/// <returns>Hash code.</returns>
		public override int GetHashCode()
		{
			return Id.GetHashCode();
		}

		/// <summary>
		/// Compares **INode** with other object.
		/// > [!Note]
		/// > Note that this method make use of <see cref="Equals(INode)"/>.
		/// > If `obj` is <see cref="INode"/> then it will be compared using
		/// > <see cref="Equals(INode)"/>.
		/// </summary>
		/// <param name="obj"></param>
		/// <returns>`false` if matches one of the following cases, othwerise
		/// returns `true`:
		/// * obj is not <see cref="INode"/>.
		/// * obj is <see cref="INode"/> but <see cref="Equals(INode)"/> returns `false`.
		/// </returns>
		public override bool Equals(object obj)
		{
			return obj is INode && Equals(obj as INode);
		}

		/// <summary>
		/// String representation of <see cref="INode"/>.
		/// </summary>
		public override String ToString()
		{
			return String.Format("{0}: {1}", Id, Label);
		}

		#endregion
	}
}
