using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkObservabilityCore
{
	public interface IConstrainable
	{
		IDictionary<String, IComparable> Attributes { get; set; }

		bool HasAttribute(String name);
	}
}
