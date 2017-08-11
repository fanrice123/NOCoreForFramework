using System;
using System.Collections.Generic;
using System.Text;

namespace NetworkObservabilityCore
{
	public interface IGraph : ICollection<INode>
	{
		Dictionary<String, INode> AllNodes { get; }

		Dictionary<String, IEdge> AllEdges { get; }

		void ConnectNodeToWith(INode from, INode to, IEdge edge);

		bool Remove(IEdge edge);


    }
}
