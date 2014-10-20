using System;

namespace Charles.Shipper.Printing.Core.Extensions.Models
{
	public struct Pair<T1, T2>
	{
		public T1 Value1 { get; set; }
		public T2 Value2 { get; set; }
	}
}

