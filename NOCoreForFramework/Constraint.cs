using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkObservabilityCore
{
	public class Constraint<CType> : IEnumerable<ICriterion> where CType : IConstrainable
	{

		#region Properties
		public static Constraint<CType> Default => new Constraint<CType>();

		public ISet<ICriterion> Criteria { get; private set; }
		#endregion

		#region Constructors
		public Constraint()
			: this(new HashSet<ICriterion>())
		{
		}

		public Constraint(ISet<ICriterion> criteria) 
		{
			Criteria = criteria;
		}
		#endregion

		public bool Validate(CType constrainable)
		{
			foreach (var criterion in Criteria)
			{
				IComparable attribute = constrainable.Attributes[criterion.Attribute];
				criterion.Check(attribute);
			}

			return true;
		}

		public IEnumerator<ICriterion> GetEnumerator()
		{
			return Criteria.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
