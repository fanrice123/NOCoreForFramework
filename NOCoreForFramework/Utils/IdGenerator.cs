using System;

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

		/// <summary>
		/// Returns a new Id for *Node* object.
		/// </summary>
		/// <returns>Id with format "N{XXXXXXX}"</returns>
		public static String GenerateNodeId()
		{
			return String.Format("N{0:0000000}", nodeIdIndex++);
		}

		/// <summary>
		/// Returns a new Id for *Edge* object.
		/// </summary>
		/// <returns>Id with format "E{XXXXXXX}"</returns>
		public static String GenerateEdgeId()
		{
			return String.Format("E{0:0000000}", edgeIdIndex++);
		}
		 
		/// <summary>
		/// Reset Id index for *Node* object to start from number given.
		/// </summary>
		/// <param name="index">The number of next Id of node start from.</param>
		public static void SetNodeIdStartFrom(int index)
		{
			nodeIdIndex = index;
		}

		/// <summary>
		/// Reset Id index for *Edge* object to start from number given.
		/// </summary>
		/// <param name="index">The number of next Id of edge start from.</param>
		public static void SetEdgeIdStartFrom(int index)
		{
			edgeIdIndex = index;
		}
	}
}
