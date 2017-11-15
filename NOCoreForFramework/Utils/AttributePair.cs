using System;

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
