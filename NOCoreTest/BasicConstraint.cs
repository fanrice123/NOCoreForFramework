using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetworkObservabilityCore;

namespace NOCoreTest
{
	class BasicConstraint : Constraint<Edge>
	{
		BasicConstraint()
			: base()
		{
			Criteria.Add(new EqualCriterion("test", 5));
		}
	}
}
