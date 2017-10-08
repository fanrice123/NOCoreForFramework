using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkObservabilityCore.Criteria
{
	/// <summary>
	/// **ICriterion** checks a <see cref="IConstrainable"/> object if
	/// it meets a criterion.
	/// </summary>
	public interface ICriterion
	{
		/// <summary>
		/// The name of attribute to be checked.
		/// </summary>
		String Attribute { get; }

		/// <summary>
		/// Function returns `true` if <see cref="Attribute"/> meets
		/// the criterion specified.
		/// </summary>
		Func<IComparable, bool> Check { get; }

	}
}
