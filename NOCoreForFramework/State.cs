using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkObservabilityCore
{
	public class State
	{
		public State(Route route)
		{
			CurrentRoute = route;
		}

		public Route CurrentRoute
		{
			get;
			private set;
		}

		public INode CurrentNode => CurrentRoute.Destination;
	}
}
