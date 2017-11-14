﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkObservabilityCore
{
	public interface IConnection
	{

		/// <summary>
		/// Gets <see cref="INode"/> where a connection starts from.
		/// </summary>
		INode From { get; set; }

		/// <summary>
		/// Gets <see cref="INode"/> where a connection links to.
		/// </summary>
		INode To { get; set; }
	}
}
