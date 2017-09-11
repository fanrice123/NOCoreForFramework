using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkObservabilityCore.Criteria
{
	public class GreaterThanCriterion : ICriterion
	{
		public GreaterThanCriterion(String attrName, IComparable criterion)
		{
			Attribute = attrName;
			Criterion = criterion;
			Check = (IComparable value) => { return value.CompareTo(Criterion) > 0; };
		}

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
	}
}
