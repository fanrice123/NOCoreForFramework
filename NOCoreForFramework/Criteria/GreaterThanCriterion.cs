using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkObservabilityCore.Criteria
{
	/// <summary>
	/// A **Criterion** object checks if an attribute is 
	/// greater than a criterion specified.
	/// </summary>
	public class GreaterThanCriterion : ICriterion
	{
		/// <summary>
		/// Default initialises an **GreaterThanCriterion**.
		/// </summary>
		/// <param name="attrName">Name of attribute.</param>
		/// <param name="criterion">Criterion of <see cref="Check"/>.</param>
		public GreaterThanCriterion(String attrName, IComparable criterion)
		{
			Attribute = attrName;
			Criterion = criterion;
			Check = (IComparable value) => { return value.CompareTo(Criterion) > 0; };
		}

		/// <summary>
		/// Initialises an **GreaterThanCriterion** with a custom checker.
		/// </summary>
		/// <param name="attrName">Name of attribute.</param>
		/// <param name="criterion">Criterion of <see cref="Check"/>.</param>
		/// <param name="checker">A function checks if attribute meets criterion.</param>
		public GreaterThanCriterion(String attrName,
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
		/// Function checks if attribute specified is greater than criterion.
		/// </summary>
		public Func<IComparable, bool> Check
		{
			get;
			set;
		}
	}
}
