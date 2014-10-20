using System;
using Charles.Shipper.Printing.Core.Interfaces;

namespace Charles.Shipper.Printing.Core
{
	
	internal abstract class ActionCommand : IActionCommand
	{
		public abstract void Call();
	}

	internal class ActionCommand<T1> : ActionCommand, IActionCommand<T1>
	{
		public Action<T1> Action { get; private set; }
		public T1 FirstParameter { get; private set; }

		public ActionCommand(Action<T1> action, T1 t1){
			Action = action;
			FirstParameter = t1;
		}

		public override void Call(){
			Action (FirstParameter);
		}

		public void Call(T1 parameter){
			Action (parameter);
		}
	}

	internal class ActionCommand<T1, T2> : ActionCommand, IActionCommand<T1, T2>
	{
		public Action<T1, T2> Action { get; private set; }
		public T1 FirstParameter { get; private set; }
		public T2 SecondParameter { get; private set; }

		public ActionCommand (Action<T1, T2> action, T1 t1, T2 t2)
		{
			Action = action;
			FirstParameter = t1;
			SecondParameter = t2;
		}

		public override void Call(){
			Action (FirstParameter, 
			        SecondParameter);
		}

		public void Call(T1 parameter){
			Action (parameter, 
			        SecondParameter);
		}

		public void Call(T1 p1, T2 p2){
			Action (p1, 
			        p2);
		}
	}

	internal class ActionCommand<T1, T2, T3> : ActionCommand, IActionCommand<T1, T2, T3>
	{
		public Action<T1, T2, T3> Action { get; private set; }
		public T1 FirstParameter { get; private set; }
		public T2 SecondParameter { get; private set; }
		public T3 ThirdParameter { get; private set; }

		public ActionCommand(Action<T1, T2, T3> action, T1 t1, T2 t2, T3 t3){
			Action = action;
			FirstParameter = t1;
			SecondParameter = t2;
			ThirdParameter = t3;
		}

		public override void Call(){
			Action (FirstParameter, 
			        SecondParameter, 
			        ThirdParameter);
		}

		public void Call(T1 parameter){
			Action (parameter, 
			        SecondParameter,
			        ThirdParameter);
		}

		public void Call(T1 p1, T2 p2){
			Action (p1, 
			        p2,
			        ThirdParameter);
		}

		public void Call(T1 p1, T2 p2, T3 p3){
			Action (p1, 
			        p2,
			        p3);
		}
	}

	internal class ActionCommand<T1, T2, T3, T4> : ActionCommand, IActionCommand<T1, T2, T3, T4>
	{
		public Action<T1, T2, T3, T4> Action { get; private set; }
		public T1 FirstParameter { get; private set; }
		public T2 SecondParameter { get; private set; }
		public T3 ThirdParameter { get; private set; }
		public T4 FourthParameter { get; private set; }

		public ActionCommand(Action<T1, T2, T3, T4> action, T1 t1, T2 t2, T3 t3, T4 t4){
			Action = action;
			FirstParameter = t1;
			SecondParameter = t2;
			ThirdParameter = t3;
			FourthParameter = t4;
		}

		public override void Call(){
			Action (FirstParameter, 
			        SecondParameter, 
			        ThirdParameter, 
			        FourthParameter);
		}

		public void Call(T1 parameter){
			Action (parameter, 
			        SecondParameter,
			        ThirdParameter, 
			        FourthParameter);
		}

		public void Call(T1 p1, T2 p2){
			Action (p1, 
			        p2,
			        ThirdParameter, 
			        FourthParameter);
		}

		public void Call(T1 p1, T2 p2, T3 p3){
			Action (p1, 
			        p2,
			        p3, 
			        FourthParameter);
		}

		public void Call(T1 p1, T2 p2, T3 p3, T4 p4){
			Action (p1, 
			        p2,
			        p3, 
			        p4);
		}
	}

	internal class ActionCommand<T1, T2, T3, T4, T5> : ActionCommand, IActionCommand<T1, T2, T3, T4, T5>
	{
		public Action<T1, T2, T3, T4, T5> Action { get; private set; }
		public T1 FirstParameter { get; private set; }
		public T2 SecondParameter { get; private set; }
		public T3 ThirdParameter { get; private set; }
		public T4 FourthParameter { get; private set; }
		public T5 FithParameter { get; private set; }

		public ActionCommand(Action<T1, T2, T3, T4, T5> action, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5){
			Action = action;
			FirstParameter = t1;
			SecondParameter = t2;
			ThirdParameter = t3;
			FourthParameter = t4;
			FithParameter = t5;
		}

		public override void Call(){
			Action (FirstParameter, 
			        SecondParameter, 
			        ThirdParameter, 
			        FourthParameter, 
			        FithParameter);
		}

		public void Call(T1 p1){
			Action (p1, 
			        SecondParameter,
			        ThirdParameter, 
			        FourthParameter, 
			        FithParameter);
		}

		public void Call(T1 p1, T2 p2){
			Action (p1, 
			        p2,
			        ThirdParameter, 
			        FourthParameter, 
			        FithParameter);
		}

		public void Call(T1 p1, T2 p2, T3 p3){
			Action (p1, 
			        p2,
			        p3, 
			        FourthParameter, 
			        FithParameter);
		}

		public void Call(T1 p1, T2 p2, T3 p3, T4 p4){
			Action (p1, 
			        p2,
			        p3, 
			        p4, 
			        FithParameter);
		}

		public void Call(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5){
			Action (p1, 
			        p2,
			        p3, 
			        p4, 
			        p5);
		}
	}
}

