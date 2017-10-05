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
		/// 
		/// </summary>
		String Attribute { get; }

		Func<IComparable, bool> Check { get; }

	}
}
