using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkObservabilityCore.Utils
{
    public class FromTo : IConnection
    {
        public FromTo(INode from, INode to)
        {
            From = from;
            To = to;
        }

        public INode From { get; set; }
        public INode To { get; set; }
    }
}
