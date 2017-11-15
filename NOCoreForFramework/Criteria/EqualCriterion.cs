using System;

namespace NetworkObservabilityCore.Criteria
{

	/// <summary>
	/// A **criterion** checks if an attribute is equal to a specified
	/// value.
	/// </summary>
	public class EqualCriterion : ICriterion
	{
		/// <summary>
		/// Default initialises an **EqualCriterion**.
		/// </summary>
		/// <param name="attrName">Name of attribute.</param>
		/// <param name="criterion">Criterion of <see cref="Check"/>.</param>
		public EqualCriterion(String attrName, IComparable criterion)
		{
			Attribute = attrName;
			Criterion = criterion;

			Check = value => Criterion.CompareTo(value) == 0;

		}

		/// <summary>
		/// Initialises an **EqualCriterion** with a custom checker.
		/// </summary>
		/// <param name="attrName">Name of attribute.</param>
		/// <param name="criterion">Criterion of <see cref="Check"/>.</param>
		/// <param name="checker">A function checks if it meets criterion.</param>
		public EqualCriterion(String attrName,
							  IComparable criterion,
							  Func<IComparable, bool> checker)
		{
			Attribute = attrName;
			Criterion = criterion;
			Check = checker;
		}

		#region Properties

		/// <inheritdoc />
		public String Attribute
		{
			get;
			set;
		}

		/// <inheritdoc />
		public IComparable Criterion
		{
			get;
			set;
		}

		/// <inheritdoc />
		public Func<IComparable, bool> Check
		{
			get;
			set;
		}

		#endregion
	}
}
