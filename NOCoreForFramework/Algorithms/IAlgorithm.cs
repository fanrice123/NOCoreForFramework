using System;
using System.Collections.Generic;
using NetworkObservabilityCore.Utils;

namespace NetworkObservabilityCore.Algorithms
{
	public interface IAlgorithm
	{

		INode Source
		{
			get;
			set;
		}

		Constraint<INode> NodeConstraint
		{
			get;
			set;
		}

		Constraint<IEdge> EdgeConstraint
		{
			get;
			set;
		}

		String SelectedEdgeAttribute
		{
			get;
			set;
		}

		void Setup(INode src, String edgeAttr, Constraint<INode> cNode, Constraint<IEdge> cEdge);

		void Run();

		IList<Route> PathsTo(INode node);
	}
}
