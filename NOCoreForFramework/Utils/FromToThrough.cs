using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkObservabilityCore.Utils
{
	public static class FromToThrough
	{
		public static FromToThrough<F, T, Thr>
		Create<F, T, Thr>(F from, T to, Thr through) where Thr : IConnection
		{
			return new FromToThrough<F, T, Thr>(from, to, through);
		}
	}

	public class FromToThrough<FType, TType, ThrType> where ThrType : IConnection
	{
		public FromToThrough(FType from, TType to, ThrType through)
		{
			From = from;
			To = to;
			Through = through;
		}

		public FType From { get; set; }
		public TType To { get; set; }
		public ThrType Through { get; set; }

	}
}
