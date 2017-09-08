using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetworkObservabilityCore;

namespace NOCoreTest
{
    [TestClass]
    public class TestNodeClass
    {
        [TestMethod]
        public void TestMethod1()
        {
			var node1 = new Node();

			var node2 = new Node();

			Assert.IsFalse(node1.Equals(node2));
        }
    }
}
