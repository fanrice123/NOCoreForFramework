﻿using System;
using System.Collections;
using System.Collections.Generic;
using NetworkObservabilityCore.Criteria;

namespace NetworkObservabilityCore
{
	public class Constraint<T> : IEnumerable<ICriterion> where T : IConstrainable
	{

		#region Properties
		public static Constraint<T> Default => new Constraint<T>();

		public IEnumerable<ICriterion> Criteria { get; private set; }
		#endregion

		#region Constructors
		public Constraint()
			: this(new HashSet<ICriterion>())
		{
		}

		public Constraint(IEnumerable<ICriterion> criteria) 
		{
			Criteria = criteria;
		}
		#endregion

		public bool Validate(T constrainable)
		{
			foreach (var criterion in Criteria)
			{
				if (constrainable.HasNumericAttribute(criterion.Attribute))
				{
					IComparable attribute = constrainable.NumericAttributes[criterion.Attribute];
					if (!criterion.Check(attribute))
						return false;
				}
				else
				{
					return false;
				}
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
