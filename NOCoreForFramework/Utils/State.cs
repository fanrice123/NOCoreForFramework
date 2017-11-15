
namespace NetworkObservabilityCore.Utils
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
