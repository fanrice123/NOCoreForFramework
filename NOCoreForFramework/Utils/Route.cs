using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

namespace NetworkObservabilityCore.Utils
{
	/// <summary>
	/// A path between 2 **Node**s.
	/// </summary>
    public class Route : IEnumerable<INode>, IComparable<Route>, IEquatable<Route>
    {
        private LinkedList<INode> nodeSequence;
		private String edgeKeyAttr;

		#region Properties

		/// <summary>
		/// Starting point of **Route**.
		/// </summary>
		public INode From =>  nodeSequence.First.Value;

		/// <summary>
		/// Ending point of **Route**.
		/// </summary>
		public INode To => nodeSequence.Last.Value;

		/// <summary>
		/// The path cost of **Route** from starting point
		/// to ending point.
		/// </summary>
        public double PathCost
        {
            get;
            private set;
        }

		public bool IsReadOnly => false;

		public int Count => nodeSequence.Count;

		#endregion

		#region Constructors

		/// <summary>
		/// Instantiates a **Route** object.
		/// </summary>
		/// <param name="src">Starting point of **Route**.</param>
		/// <param name="edgeAttr">Attribute to calculate path cost.</param>
		public Route(INode src, String edgeAttr)
        {
            nodeSequence = new LinkedList<INode>();
			nodeSequence.AddLast(src);
			edgeKeyAttr = edgeAttr;
            PathCost = 0;
        }
		
		/// <summary>
		/// Copy constructor of **Route**.
		/// </summary>
		/// <param name="path"></param>
		private Route(Route path)
        {
			nodeSequence = new LinkedList<INode>(path.nodeSequence);
			edgeKeyAttr = path.edgeKeyAttr;
			PathCost = path.PathCost;
        }

		#endregion

		#region Methods

		/// <summary>
		/// Concatenates a **Route** with an <see cref="IEdge"/>.
		/// </summary>
		/// <param name="edge">The new portion of **Route**.</param>
		public void Add(IEdge edge)
        {
            nodeSequence.AddLast(edge.To);
            PathCost += Convert.ToDouble(edge[edgeKeyAttr]);
        }

		/// <summary>
		/// Checks if **Route** contains the given <see cref="INode"/>.
		/// </summary>
		/// <param name="node">A **Node** object.</param>
		/// <returns>Returns `true` if contains the given **Node**.</returns>
		public bool Contains(INode node)
		{
			return nodeSequence.Contains(node);
		}

		/// <summary>
		/// Checks if contains any observer.
		/// </summary>
		/// <returns>`true` for containing observer regardless of quantity.</returns>
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

		/// <summary>
		/// Compares 2 **Route** with their <see cref="PathCost"/>.
		/// </summary>
		/// <param name="other"></param>
		/// <returns></returns>
		public int CompareTo(Route other)
		{
			return PathCost.CompareTo(other.PathCost);
		}

		/// <summary>
		/// Checks if equals to **Route** given.
		/// </summary>
		/// <param name="other">Targeted **Route**.</param>
		/// <returns>
		/// `true` if all of the **Node**s in both **Route** 
		/// are exact same.
		/// </returns>
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

		/// <summary>
		/// Clones a **Route** object.
		/// </summary>
		/// <returns></returns>
        public Route Clone()
		{
			return new Route(this);
		}

		#endregion

	}
}