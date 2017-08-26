using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkObservabilityCore
{
	public class RangeCriterion : ICriterion
	{

		public RangeCriterion(String attrName, IComparable minInclusive, IComparable maxInclusive)
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

		#region Properties

		public String Attribute
		{
			get;
			set;
		}

		public IComparable MaxInclusive
		{
			get;
			set;
		}

		public IComparable MinInclusive
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
