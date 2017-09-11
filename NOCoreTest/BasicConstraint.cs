using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetworkObservabilityCore;
using NetworkObservabilityCore.Criteria;

namespace NOCoreTest
{
	class BasicConstraint : Constraint<Edge>
	{
		BasicConstraint()
			: base()
		{
			Criteria.Add(new EqualCriterion("test", 5));
		}

		public void te()
		{
			var ec = new Constraint<Edge>();

			ec.Criteria.Add(new EqualCriterion("Weight", 5));
		}
	}
}
