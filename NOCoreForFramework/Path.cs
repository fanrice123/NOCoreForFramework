using NetworkObservabilityCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NOCoreForFramework
{
    class Path
    {
        public Queue<INode> nodeSequence;

        public Path()
        {
            nodeSequence = new Queue<INode>();
            PathCost = 0;
        }

        //public Path(Queue<INode> nodelist)
        //{
        //    nodeSequence = new Queue<INode>(nodelist);
        //    pathTotalCost = 0;

        //}

        public void Add(IEdge edge)
        {
            nodeSequence.Enqueue(edge.To);
            PathCost += edge.Value;
        }


        public INode Source => nodeSequence.First();

        public INode Destination => nodeSequence.Last();

        public double PathCost
        {
            get;
            private set;
        }

    }
}
