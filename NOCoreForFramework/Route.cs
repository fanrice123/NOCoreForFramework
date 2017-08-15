using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace NetworkObservabilityCore
{
    public class Route : IEnumerable<INode>, IComparable<Route>
    {
        private LinkedList<INode> nodeSequence;

        public Route(INode src)
        {
            nodeSequence = new LinkedList<INode>();
			nodeSequence.AddLast(src);
            PathCost = 0;
        }
		
		private Route(Route path)
        {
			nodeSequence = new LinkedList<INode>(path.nodeSequence);
			PathCost = path.PathCost;
        }

        public void Add(IEdge edge)
        {
            nodeSequence.AddLast(edge.To);
            PathCost += edge.Value;
        }

		public IEnumerator<INode> GetEnumerator()
		{
			return nodeSequence.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return nodeSequence.GetEnumerator();
		}

		public int CompareTo(Route other)
		{
			return PathCost.CompareTo(other.PathCost);
		}

		public Route Clone()
		{
			return new Route(this);
		}

		public INode Source => nodeSequence.First.Value;

        public INode Destination => nodeSequence.Last.Value;

        public double PathCost
        {
            get;
            private set;
        }

    }
}
