using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetworkObservabilityCore;
using NetworkObservabilityCore.Utils;

namespace NOCoreTest
{
    [TestClass]
    public class TestForRouteClass
    {
        Node a;
        Node b;
        Node c;
        Node d;
        Node e;

        Edge aToB;
        Edge bToC;
        Edge cToD;
        Edge dToE;

        [TestInitialize]
        public void TestInitialise()
        {
            a = new Node();
            b = new Node();
            b.IsObserver = true;
            c = new Node();
            d = new Node();
            e = new Node();

            aToB = new Edge();
            aToB.Weight = 1;
            bToC = new Edge();
            bToC.Weight = 5;
            cToD = new Edge();
            cToD.Weight = 1;
            dToE = new Edge();
            dToE.Weight = 1;

            a.ConnectOut.Add(aToB);
            b.ConnectIn.Add(aToB);
            aToB.From = a;
            aToB.To = b;

            b.ConnectOut.Add(bToC);
            c.ConnectIn.Add(bToC);
            bToC.From = b;
            bToC.To = c;


            c.ConnectOut.Add(cToD);
            d.ConnectIn.Add(cToD);
            cToD.From = c;
            cToD.To = d;

            d.ConnectOut.Add(dToE);
            e.ConnectIn.Add(dToE);
            dToE.From = d;
            dToE.To = e;
        }

        [TestMethod]
        public void TestPathCost()
        {
            Route myRoute = new Route(a, "Weight");
            Assert.AreEqual(0, myRoute.PathCost);
        }

        [TestMethod]
        public void testIfContainsObserver()
        {
            Route myRoute = new Route(a, "Weight");
            myRoute.Add(aToB);
            myRoute.Add(bToC);
            myRoute.Add(cToD);
            //Assert.IsTrue(myRoute.ContainsObserver());
            //Assert.AreEqual(3, myRoute.PathCost);

            //Assert.AreEqual(d, myRoute.Destination);
        }
    }
}
