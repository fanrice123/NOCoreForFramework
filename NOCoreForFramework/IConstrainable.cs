using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkObservabilityCore
{
	/// <summary>
	/// A type implements **IConstrainable** implies
	/// that it has <see cref="Attributes"/> which allows it
	/// to work with <see cref="Constraint{T}"/>
	/// </summary>
	public interface IConstrainable
	{
		/// <summary>
		/// Gets attributes with its name.
		/// </summary>
        IDictionary<String, Double> NumericAttributes { get; set; }

		/// <summary>
		/// Gets attributes with its name.
		/// </summary>
        IDictionary<String, String> DescriptiveAttributes { get; set; }

		/// <summary>
		/// Checks if it has certain numeric attribute.
		/// </summary>
		/// <param name="name">Name of attribute</param>
		/// <returns>Returns true if it exists.</returns>
		/// <exception cref="ArgumentNullException">Thrown if the name is null.</exception>
		bool HasNumericAttribute(String name);

		/// <summary>
		/// Checks if it has certain descriptive attribute.
		/// </summary>
		/// <param name="name">Name of attribute</param>
		/// <returns>Returns true if it exists.</returns>
		/// <exception cref="ArgumentNullException">Thrown if the name is null.</exception>
		bool HasDescriptiveAttribute(String name);

		/// <summary>
		/// This indexer returns <see cref="NumericAttributes"/> with the name specified.
		/// </summary>
		/// <param name="key">Name of <see cref="NumericAttributes"/></param>
		/// <returns>The specified Attributes.</returns>
		Double this[String key] { get; set; }
	}
}
