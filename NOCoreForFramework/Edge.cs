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
		public IDictionary<String, Double> NumericAttributes
		{
			get;
			set;
		}

		/// <inheritdoc />
		public IDictionary<String, String> DescriptiveAttributes
		{
			get;
			set;
		}

		/// <inheritdoc />
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
		/// Default initialises an **Edge**.
		/// </summary>
		public Edge()
			: this(new AttributePair<Double>[0], new AttributePair<String>[0])
		{
		}

		/// <summary>
		/// Initialises an **Edge** with numeric attributes <see cref="AttributePair{T}"/>
		/// where type parameter is <see cref="Double"/>.
		/// </summary>
		public Edge(params AttributePair<Double>[] numericAttr)
			: this(numericAttr, new AttributePair<String>[0])
		{
		}

		/// <summary>
		/// Initialises an **Edge** with <see cref="AttributePair{T}"/> 
		/// where type parameter is <see cref="String"/>.
		/// </summary>
		public Edge(AttributePair<Double>[] numericAttr, AttributePair<String>[] descriptiveAttr)
		{
			Id = IdGenerator.GenerateEdgeId();
			Label = Id;
			IsBlocked = false;
			NumericAttributes = new Dictionary<string, Double>();
			foreach (var attrPair in numericAttr)
			{
				NumericAttributes[attrPair.Name] = attrPair.Attribute;
			}

			DescriptiveAttributes = new Dictionary<string, String>();
			foreach (var attrPair in descriptiveAttr)
			{
				DescriptiveAttributes[attrPair.Name] = attrPair.Attribute;
			}
		}
		#endregion

		#region Members

		/// <inheritdoc />
		public bool HasNumericAttribute(String name)
		{
			return NumericAttributes.ContainsKey(name);
		}

		/// <inheritdoc />
		public bool HasDescriptiveAttribute(String name)
		{
			return DescriptiveAttributes.ContainsKey(name);
		}

		/// <summary>
		/// Compares **IEdge** to other with their <see cref="Id"/>.
		/// </summary>
		/// <param name="other">An <see cref="IEdge"/>.</param>
		/// <returns>Returns `true` if they are equal.</returns>
		public virtual bool Equals(IEdge other)
		{
			return Id == other.Id;
		}
		#endregion

		#region Object methods
		/// <summary>
		/// Get hash code based on <see cref="Id"/>.
		/// </summary>
		/// <returns>Hash code.</returns>
		public override int GetHashCode()
		{
			return Id.GetHashCode();
		}

		/// <summary>
		/// Compares **IEdge** with other object.
		/// > [!Note]
		/// > Note that this method make use of <see cref="Equals(IEdge)"/>.
		/// > If `obj` is <see cref="IEdge"/> then it will be compared using
		/// > <see cref="Equals(IEdge)"/>.
		/// </summary>
		/// <param name="obj"></param>
		/// <returns>`false` if matches one of the following cases, othwerise
		/// returns `true`:
		/// * obj is not <see cref="IEdge"/>.
		/// * obj is <see cref="IEdge"/> but <see cref="Equals(IEdge)"/> returns `false`.
		/// </returns>
		public override bool Equals(object obj)
		{
			return obj is IEdge && Equals(obj as IEdge);
		}

		/// <summary>
		/// String representation of <see cref="INode"/>.
		/// </summary>
		public override string ToString()
		{
			return String.Format("{0}: {1}, From {2} To {3}", Id, Label, From.Id, To.Id);
		}
		#endregion

	}
}
