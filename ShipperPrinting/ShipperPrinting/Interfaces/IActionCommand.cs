using System;

namespace Charles.Shipper.Printing.Core.Interfaces
{
	internal interface IActionCommand
	{
		void Call();
	}

	internal interface IActionCommand<T1> : IActionCommand
	{
		void Call(T1 t1);
	}

	internal interface IActionCommand<T1, T2> : IActionCommand<T1>
	{
		void Call (T1 t1, T2 t2);
	}

	internal interface IActionCommand<T1, T2, T3> : IActionCommand<T1, T2>
	{
		void Call (T1 t1, T2 t2, T3 t3);
	}

	internal interface IActionCommand<T1, T2, T3, T4> : IActionCommand<T1, T2, T3>
	{
		void Call (T1 t1, T2 t2, T3 t3, T4 t4);
	}

	internal interface IActionCommand<T1, T2, T3, T4, T5> : IActionCommand<T1, T2, T3, T4>
	{
		void Call (T1 t1, T2 t2, T3 t3, T4 t4, T5 t5);
	}

	internal interface IActionCommand<T1, T2, T3, T4, T5, T6> : IActionCommand<T1, T2, T3, T4, T5>
	{
		void Call (T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6);
	}

	internal interface IActionCommand<T1, T2, T3, T4, T5, T6, T7> : IActionCommand<T1, T2, T3, T4, T5, T6>
	{
		void Call (T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7);
	}
}

