using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetworkObservabilityCore;
using NetworkObservabilityCore.Criteria;

namespace NOCoreTest
{
	[TestClass]
	public class ConstraintTest
	{
		[TestMethod]
		public void TestConstraint()
		{
			Constraint<IEdge> ce = new Constraint<IEdge>();

			ce.Criteria.Add(new RangeCriterion("Value", 9, 100));
			ce.Criteria.Add(new EqualCriterion("Weight", 35.99));
			ce.Criteria.Add(new LessThanCriterion("Value", 33));
			ce.Criteria.Add(new GreaterThanCriterion("Weight", 30.0));
			IEdge edge = new Edge();
			edge["Value"] = 32;
			edge["Weight"] = 35.99;

			Assert.IsTrue(ce.Validate(edge));
		}

		[TestMethod]
		public void TestConstraint2()
		{
			Constraint<IEdge> ce = new Constraint<IEdge>();

			ce.Criteria.Add(new RangeCriterion("Value", 9, 100));
			ce.Criteria.Add(new EqualCriterion("Weight", 35.99));
			ce.Criteria.Add(new LessThanCriterion("Value", 33));
			ce.Criteria.Add(new GreaterThanCriterion("Weight", 30.0));
			IEdge edge = new Edge();
			edge["Value"] = 3;
			edge["Weight"] = 35.99;

			Assert.IsFalse(ce.Validate(edge));
		}
	}
}
