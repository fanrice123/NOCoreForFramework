using System;

namespace NetworkObservabilityCore.Criteria
{
	/// <summary>
	/// A **criterion** object checks if attribute is less than a criterion.
	/// </summary>
	public class LessThanCriterion : ICriterion
	{
		/// <summary>
		/// Default initialises an **LessthanCriterion**.
		/// </summary>
		/// <param name="attrName">Name of attribute.</param>
		/// <param name="criterion">Criterion of <see cref="Check"/>.</param>
		public LessThanCriterion(String attrName, IComparable criterion)
		{
			Attribute = attrName;
			Criterion = criterion;
			Check = (IComparable value) => { return value.CompareTo(Criterion) < 0; };
		}

		/// <summary>
		/// Initialises an **LessThanCriterion** with a custom checker.
		/// </summary>
		/// <param name="attrName">Name of attribute.</param>
		/// <param name="criterion">Criterion of <see cref="Check"/>.</param>
		/// <param name="checker">A function checks if attribute meets criterion.</param>
		public LessThanCriterion(String attrName,
								 IComparable criterion,
								 Func<IComparable, bool> checker)
		{
			Attribute = attrName;
			Criterion = criterion;
			Check = checker;
		}

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

		/// <summary>
		/// Function checks if attribute specified is less than criterion.
		/// </summary>
		public Func<IComparable, bool> Check
		{
			get;
			set;
		}
	}
}
