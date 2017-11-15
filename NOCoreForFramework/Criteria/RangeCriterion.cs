using System;

namespace NetworkObservabilityCore.Criteria
{
	/// <summary>
	/// A **criterion** object checks if an attribute is in between 2 criteria.
	/// </summary>
	public class RangeCriterion : ICriterion
	{
		/// <summary>
		/// Default initialises an **EqualCriterion**.
		/// </summary>
		/// <param name="attrName">Name of attribute.</param>
		/// <param name="minInclusive">Criterion that attribute must not be
		/// less than.</param>
		/// <param name="maxInclusive">Criterion that attribute must not be
		/// greater than.</param>
		public RangeCriterion(String attrName,
							  IComparable minInclusive,
							  IComparable maxInclusive)
		{
			Attribute = attrName;
			MinInclusive = minInclusive;
			MaxInclusive = maxInclusive;

			Check = (IComparable value) =>
			{
				//			MinInclusive <= value &&& value <= MaxInclusive
				return MinInclusive.CompareTo(value) <= 0 && value.CompareTo(MaxInclusive) <= 0;
			};

		}

		/// <summary>
		/// Initialises an **EqualCriterion** with a custom checker.
		/// </summary>
		/// <param name="attrName">Name of attribute.</param>
		/// <param name="minInclusive">Criterion that attribute must not be
		/// less than.</param>
		/// <param name="maxInclusive">Criterion that attribute must not be
		/// greater than.</param>
		/// <param name="checker">Function checks if criterion is met.</param>
		public RangeCriterion(String attrName,
							  IComparable minInclusive,
							  IComparable maxInclusive,
							  Func<IComparable, bool> checker)
		{
			Attribute = attrName;
			MinInclusive = minInclusive;
			MaxInclusive = maxInclusive;
			Check = checker;
		}

		#region Properties

		/// <inheritdoc />
		public String Attribute
		{
			get;
			set;
		}

		/// <summary>
		/// Specified attribute must not be greater than this criterion.
		/// </summary>
		public IComparable MaxInclusive
		{
			get;
			set;
		}

		/// <summary>
		/// Specified attribute must not be less than this criterion.
		/// </summary>
		public IComparable MinInclusive
		{
			get;
			set;
		}

		/// <summary>
		/// Function checks if a specified attribute is in between a range.
		/// </summary>
		public Func<IComparable, bool> Check
		{
			get;
			set;
		}

		#endregion
	}
}
