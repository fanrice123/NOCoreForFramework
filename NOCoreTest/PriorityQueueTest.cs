using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetworkObservabilityCore;
using System.Collections;
using System.Collections.Generic;

namespace NOCoreTest
{
    [TestClass]
    public class PriorityQueueTest
    {

        private PriorityQueue<int> testIntegerObj;

        [TestInitialize]
        public void Initialize()
        {
            testIntegerObj = new PriorityQueue<int>(5);
        }

        [TestMethod]
        public void AddTheFirstFiveInt()
        {
            testIntegerObj.Enqueue(5);
            testIntegerObj.Enqueue(6);
            testIntegerObj.Enqueue(7);
            testIntegerObj.Enqueue(8);
            testIntegerObj.Enqueue(9);
            Assert.AreEqual(5, testIntegerObj.Count);
        }
        
        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]/////////////////////
        public void AddMoreThanEnoughInt()
        {
            PriorityQueue<int> testInt = new PriorityQueue<int>(5);
            testInt.Enqueue(5);
            testInt.Enqueue(6);
            testInt.Enqueue(7);
            testInt.Enqueue(8);
            testInt.Enqueue(9);
            testInt.Enqueue(10);
        }

        [TestMethod]
        public void TestCount()
        {
            PriorityQueue<int> testInt = new PriorityQueue<int>(5);
            Assert.AreEqual(0, testInt.Count);
        }


        [TestMethod]
        public void TestCapacity()
        {
            PriorityQueue<int> testInt = new PriorityQueue<int>(5);
            Assert.AreEqual(5, testInt.Capacity);
        }

        [TestMethod]
        public void TestFirstObj()
        {
            PriorityQueue<int> testInt = new PriorityQueue<int>(5);
            testInt.Enqueue(5);
            testInt.Enqueue(6);
            testInt.Enqueue(7);
            testInt.Enqueue(8);
            testInt.Enqueue(9);
            Assert.AreEqual(5, testInt.Front);
        }

        [TestMethod]
        public void TestFailFirstObj()
        {
            PriorityQueue<int> testInt = new PriorityQueue<int>(5);
            Assert.AreEqual(0, testInt.Front);///////////
        }

        [TestMethod]
        public void TestSort()
        {
            PriorityQueue<int> testInt = new PriorityQueue<int>(5);
            testInt.Enqueue(7);
            testInt.Enqueue(5);
            testInt.Enqueue(9);
            testInt.Enqueue(8);
            testInt.Enqueue(6);
            testInt.Sort();
            Assert.AreEqual(9, testInt.Front);
        }

        [TestMethod]
        public void TestDequeue()
        {
            PriorityQueue<int> testInt = new PriorityQueue<int>(5);
            testInt.Enqueue(7);
            testInt.Enqueue(5);
            testInt.Enqueue(9);
            testInt.Enqueue(8);
            testInt.Enqueue(6);
            testInt.Sort();
            Assert.AreEqual(9, testInt.Dequeue());
            Assert.AreEqual(5, testInt.Capacity);
        }


        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void TestDequeueUnderflow()
        {
            PriorityQueue<int> testInt = new PriorityQueue<int>(5);
            testInt.Enqueue(7);
            testInt.Enqueue(5);
            testInt.Enqueue(9);
            testInt.Enqueue(8);
            testInt.Enqueue(6);
            testInt.Dequeue();
            testInt.Dequeue();
            testInt.Dequeue();
            testInt.Dequeue();
            testInt.Dequeue();
            testInt.Dequeue();
        }

        // BuildMinHeap
        // MinHeap is used -> lower key, higher priority
        //Once sorted, the heap is messed up so we build it again

                
    }
}
