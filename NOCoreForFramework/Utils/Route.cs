using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace NetworkObservabilityCore.Utils
{
    public class Route : IEnumerable<INode>, IComparable<Route>, IEquatable<Route>
    {
        private LinkedList<INode> nodeSequence;
		private String edgeKeyAttr;

        public Route(INode src, String edgeAttr)
        {
            nodeSequence = new LinkedList<INode>();
			nodeSequence.AddLast(src);
			edgeKeyAttr = edgeAttr;
            PathCost = 0;
        }
		
		private Route(Route path)
        {
			nodeSequence = new LinkedList<INode>(path.nodeSequence);
			edgeKeyAttr = path.edgeKeyAttr;
			PathCost = path.PathCost;
        }

        public void Add(IEdge edge)
        {
            nodeSequence.AddLast(edge.To);
            PathCost += Convert.ToDouble(edge[edgeKeyAttr]);
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

		public bool Equals(Route other)
		{
			if (nodeSequence.Count != other.nodeSequence.Count)
				return false;
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

        public override string ToString()
        {
            String ret_val = "";
            int i = 0;
            foreach (INode node in nodeSequence)
            {
                if (i++ != 0)
                    ret_val += " --> ";
                ret_val += node.Label;
            }

            return ret_val;
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