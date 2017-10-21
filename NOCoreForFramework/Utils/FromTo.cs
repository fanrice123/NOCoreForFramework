using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkObservabilityCore.Utils
{
    public class FromTo
    {
        public static FromTo<F, T>
        Create<F, T>(F from, T to)
        {
            return new FromTo<F, T>(from, to);
        }
    }

    public class FromTo<FType, TType>
    {
        public FromTo(FType from, TType to)
        {
            From = from;
            To = to;
        }

        public FType From { get; set; }
        public TType To { get; set; }
    }
}
