
namespace NetworkObservabilityCore.Utils
{
	/// <summary>
	/// An auxiliary class representing a state in graph search algorithm.
	/// </summary>
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

		public INode CurrentNode => CurrentRoute.To;
	}
}
