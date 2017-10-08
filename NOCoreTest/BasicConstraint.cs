using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetworkObservabilityCore.Utils;
using NetworkObservabilityCore;
using NetworkObservabilityCore.Criteria;

namespace NOCoreTest
{
	class BasicConstraint : Constraint<Edge>
	{
		BasicConstraint()
			:base(new List<ICriterion>() { new EqualCriterion("test", 5) })
		{
		}
	}
}
