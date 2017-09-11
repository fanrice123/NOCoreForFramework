using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetworkObservabilityCore.Criteria;

namespace NOCoreTest
{
	[TestClass]
	public class CriteriaTest
	{
		[TestMethod]
		public void TestEqualCriterion()
		{
			EqualCriterion ec = new EqualCriterion("Test", 256.77);
			Assert.IsTrue(ec.Check(256.77));
		}

		[TestMethod]
		public void TestEqualCriterion2()
		{
			EqualCriterion ec = new EqualCriterion("Test", 256.0);
			Assert.IsFalse(ec.Check(256.77));

		}

		[TestMethod]
		public void TestEqualCriterion3()
		{
			EqualCriterion ec = new EqualCriterion("Test", "A string");
			Assert.IsTrue(ec.Check("A string"));

		}

		[TestMethod]
		public void TestEqualCriterion4()
		{
			EqualCriterion ec = new EqualCriterion("Test", "A string");
			Assert.IsFalse(ec.Check("Not a string"));

		}

		[TestMethod]
		public void TestGreaterThanCriterion()
		{
			GreaterThanCriterion ec = new GreaterThanCriterion("Test", 256.77);
			Assert.IsTrue(ec.Check(256.88));
		}

		[TestMethod]
		public void TestGreaterThanCriterion2()
		{
			GreaterThanCriterion ec = new GreaterThanCriterion("Test", 256.0);
			Assert.IsFalse(ec.Check(56.7));

		}

		[TestMethod]
		public void TestLessThanCriterion()
		{
			LessThanCriterion ec = new LessThanCriterion("Test", 256.77);
			Assert.IsTrue(ec.Check(256.0));
		}

		[TestMethod]
		public void TestLessThanCriterion2()
		{
			LessThanCriterion ec = new LessThanCriterion("Test", 256);
			Assert.IsFalse(ec.Check(567));

		}

		[TestMethod]
		public void TestRangeCriterion()
		{
			RangeCriterion ec = new RangeCriterion("Test", 256.77, 900.0);
			Assert.IsTrue(ec.Check(596.0));
		}

		[TestMethod]
		public void TestRangeCriterion2()
		{
			RangeCriterion ec = new RangeCriterion("Test", 256.77, 900.0);
			Assert.IsFalse(ec.Check(5670.0));

		}

		[TestMethod]
		public void TestRangeCriterion3()
		{
			RangeCriterion ec = new RangeCriterion("Test", 256.77, 900.0);
			Assert.IsFalse(ec.Check(70.0));
		}

		[TestMethod]
		public void TestRangeCriterion4()
		{
			RangeCriterion ec = new RangeCriterion("Test", 256.77, 900.0);
			Assert.IsTrue(ec.Check(900.0));
		}

		[TestMethod]
		public void TestRangeCriterion5()
		{
			RangeCriterion ec = new RangeCriterion("Test", 256.77, 900.0);
			Assert.IsTrue(ec.Check(256.77));
		}
		
		[TestMethod]
		[ExpectedException(typeof(InvalidOperationException))]
		public void TestRangeCriterion6()
		{
			RangeCriterion ec = new RangeCriterion("Test", 900.0, 256.77);
		}

	}
}