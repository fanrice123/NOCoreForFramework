using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetworkObservabilityCore;

namespace NOCoreTest
{
    [TestClass]
    public class TestEdgeClass
    {
        private Edge edge;
        [TestInitialize]
        public void Initialize()
        {
            edge = new Edge();
        }

        [TestMethod]
        public void TestId()
        {
            //Assert.AreEqual(1, )
        }
    }
}
