using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace NetworkObservabilityCore
{
    public class Path : IEnumerable<INode>, IComparable<Path>
    {
        private LinkedList<INode> nodeSequence;

        public Path(INode src)
        {
            nodeSequence = new LinkedList<INode>();
			nodeSequence.AddLast(src);
            PathCost = 0;
        }
		
		private Path(Path path)
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

		public int CompareTo(Path other)
		{
			return PathCost.CompareTo(other.PathCost);
		}

		public Path Clone()
		{
			return new Path(this);
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
