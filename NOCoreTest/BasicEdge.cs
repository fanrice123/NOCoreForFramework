using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetworkObservabilityCore;

namespace NOCoreTest
{
	public class BasicEdge : Edge
	{
		public BasicEdge()
			: this(1)
		{
		}

		public BasicEdge(double value)
			: base()
		{
			NumericAttributes["test"] = value;
		}

	}
}
