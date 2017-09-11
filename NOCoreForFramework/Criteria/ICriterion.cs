using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkObservabilityCore.Criteria
{
	public interface ICriterion
	{
		String Attribute { get; }

		Func<IComparable, bool> Check { get; }

	}
}
