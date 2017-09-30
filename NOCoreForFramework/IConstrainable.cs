using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkObservabilityCore
{
	/// <summary>
	/// A type implements **IConstrainable** implies
	/// that it has <see cref="Properties"/> which allows it
	/// to work with <see cref="Constraint{T}"/>
	/// </summary>
	public interface IConstrainable
	{
		/// <summary>
		/// Gets attributes with its name.
		/// </summary>
        IDictionary<String, Property> Properties { get; set; }

		/// <summary>
		/// Checks if it has certain attribute.
		/// </summary>
		/// <param name="name">Name of attribute</param>
		/// <returns>Returns true if it exists.</returns>
		/// <exception cref="ArgumentNullException">Thrown if the name is null.</exception>
		bool HasAttribute(String name);

		/// <summary>
		/// This indexer returns <see cref="Properties"/> with the name specified.
		/// </summary>
		/// <param name="key">Name of <see cref="Properties"/></param>
		/// <returns>The specified Attributes.</returns>
		IComparable this[String key] { get; set; }
	}
}
