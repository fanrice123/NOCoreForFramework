using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace NetworkObservabilityCore
{
    public class Route : IEnumerable<INode>, IComparable<Route>, IEquatable<Route>
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
            PathCost += edge.Weight;
        }

        public bool ContainsObserver()
        {
            foreach(var node in nodeSequence)
            {
                if (node.IsObserver)
                {
                    return true;
                }
            }
            return false;
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

		bool IEquatable<Route>.Equals(Route other)
		{
			var flagPairs = other.Zip(this, (lhs, rhs) => lhs.Equals(rhs));

			foreach (var flag in flagPairs)
			{
				if (!flag)
					return false;
			}

			return true;
		}

		public override bool Equals(object obj)
		{
			return obj is Route && Equals(obj as Route);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				int hash = 486187739;
				foreach (var node in nodeSequence)
				{
					hash = hash * 17 + node.GetHashCode();
				}
				return hash;
			}
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