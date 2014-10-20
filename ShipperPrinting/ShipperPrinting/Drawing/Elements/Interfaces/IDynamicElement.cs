using System;
using System.Collections.Generic;

namespace Charles.Shipper.Printing.Core
{
	public interface IDynamicElement
	{
		string Id { get; set; }
		string Content { get; set; }
	}
}

