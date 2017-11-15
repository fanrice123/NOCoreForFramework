
namespace NetworkObservabilityCore.Utils
{
	/// <summary>
	/// A tuple contains 2 **Node** objects.
	/// </summary>
    public class FromTo<T>
    {
		/// <summary>
		/// Instantiates a tuple.
		/// </summary>
		/// <param name="from">Node.</param>
		/// <param name="to">Another node. :)</param>
        public FromTo(T from, T to)
        {
            From = from;
            To = to;
        }

        public T From { get; set; }

        public T To { get; set; }
    }
}
