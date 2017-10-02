using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetworkObservabilityCore;
using NetworkObservabilityCore.Utils;

namespace NOCoreTest
{
	public class TestSubEdge : Edge
	{
		public TestSubEdge()
			: this(1)
		{
		}

		public TestSubEdge(int weight)
			: base(new AttributePair<Double> { Name = "Weight", Attribute = weight })
		{
		}
	}
}
