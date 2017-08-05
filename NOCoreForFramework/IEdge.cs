using System;
using System.Collections.Generic;
using System.Text;

namespace NetworkObservabilityCore
{
	public interface IEdge : IEquatable<IEdge>
	{
		String Id { get; }

		String Label { get; set; }

		INode From { get; set; }

		INode To { get; set; }
	}
}
