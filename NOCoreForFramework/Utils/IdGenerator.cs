using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkObservabilityCore.Utils
{
	/// <summary>
	/// Every Class that implements INode or IEdge should always get their
	/// id with this static Generator.
	/// </summary>
	public static class IdGenerator
	{
		internal static int nodeIdIndex = 0;
		internal static int edgeIdIndex = 0;

		public static String GenerateNodeId()
		{
			return String.Format("N{0:0000000}", nodeIdIndex++);
		}

		public static String GenerateEdgeId()
		{
			return String.Format("E{0:0000000}", edgeIdIndex++);
		}

		public static void SetNodeIdStartFrom(int index)
		{
			nodeIdIndex = index;
		}

		public static void SetEdgeIdStartFrom(int index)
		{
			edgeIdIndex = index;
		}
	}
}
