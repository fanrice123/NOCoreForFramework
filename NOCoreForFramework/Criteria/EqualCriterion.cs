using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkObservabilityCore.Criteria
{

	public class EqualCriterion : ICriterion
	{
		public EqualCriterion(String attrName, IComparable criterion)
		{
			Attribute = attrName;
			Criterion = criterion;

			Check = (IComparable value) => { return Criterion.CompareTo(value) == 0; };

		}

		#region Properties

		public String Attribute
		{
			get;
			set;
		}

		public IComparable Criterion
		{
			get;
			set;
		}

		public Func<IComparable, bool> Check
		{
			get;
			set;
		}

		#endregion
	}
}
