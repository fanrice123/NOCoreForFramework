using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkObservabilityCore.Utils
{
	public class AttributePair<T>
	{
		public String Name
		{
			get;
			set;
		}

		public T Attribute
		{
			get;
			set;
		}
	}
}
