
namespace NetworkObservabilityCore
{
	/// <summary>
	/// An interface that possess proterpies "From" and "To".
	/// </summary>
	public interface IConnection
	{

		/// <summary>
		/// Gets <see cref="INode"/> where a connection starts from.
		/// </summary>
		INode From { get; set; }

		/// <summary>
		/// Gets <see cref="INode"/> where a connection links to.
		/// </summary>
		INode To { get; set; }
	}
}
