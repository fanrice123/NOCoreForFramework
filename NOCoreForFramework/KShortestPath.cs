using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetworkObservabilityCore;

namespace NOCoreForFramework
{
	public class KShortestPath
	{
		private Dictionary<INode, List<Path>> paths;

		public KShortestPath(IGraph graph, INode src)
		{
			paths = new Dictionary<INode, List<Path>>();


			
		}
	}
}
