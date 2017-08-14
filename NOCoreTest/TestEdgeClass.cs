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
            Edge edge1 = new Edge();
            Edge edge2 = new Edge();
            Assert.AreNotEqual(edge1.Id, edge2.Id);
        }
        

        [TestMethod]
        public void TestValue()
        {
            Edge edge = new Edge(3);//////////////////////////////////
            Assert.AreEqual(3, edge.Value);
        }
    }
}
