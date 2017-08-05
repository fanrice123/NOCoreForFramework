using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetworkObservabilityCore;

namespace NOCoreTest
{
	public class TestSubEdge : Edge
	{
		public TestSubEdge()
			: this(1)
		{
		}

		public TestSubEdge(int weight)
			: base(weight)
		{
		}
	}
}
