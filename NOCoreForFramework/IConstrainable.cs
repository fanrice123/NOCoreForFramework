using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkObservabilityCore
{
	public interface IConstrainable
	{
		Dictionary<String, IComparable> Attributes { get; }

		bool HasAttribute(String name);
	}
}
