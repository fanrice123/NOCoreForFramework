
namespace NetworkObservabilityCore.Utils
{
	/// <summary>
	/// An auxiliary class to instantiate <see cref="FromToThrough{FType, TType, ThrType}"/>.
	/// </summary>
	public static class FromToThrough
	{
		/// <summary>
		/// Instantiates <see cref="FromToThrough{FType, TType, ThrType}"/>.
		/// </summary>
		/// <param name="from">Origin node.</param>
		/// <param name="to">Destitation node.</param>
		/// <param name="through">Connection between 2 nodes.</param>
		/// <returns>A tuple object.</returns>
		public static FromToThrough<T, Thr>
		Create<T, Thr>(T from, T to, Thr through)
		{
			return new FromToThrough<T, Thr>(from, to, through);
		}
	}

	/// <summary>
	/// A tuple consists of 2 nodes and a connection between 2 nodes.
	/// </summary>
	public class FromToThrough<PType, ThrType>
	{
		/// <summary>
		/// Instantiates tuple.
		/// </summary>
		/// <param name="from">A point.</param>
		/// <param name="to">Another point. :(</param>
		/// <param name="through">Connection between 2 points.</param>
		public FromToThrough(PType from, PType to, ThrType through)
		{
			From = from;
			To = to;
			Through = through;
		}

		public PType From { get; set; }

		public PType To { get; set; }

		public ThrType Through { get; set; }

	}
}
