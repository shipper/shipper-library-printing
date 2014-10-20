using System;

internal static class ExceptionExtensions
{
	public static void Log(this Exception ex){
		Console.WriteLine (@"
Expection: {0}.{1}
Message: {2}
Stack Trace: 
{3}
", 
           ex.GetType().Namespace, 
           ex.GetType().Name, 
           ex.Message, 
           ex.StackTrace);
	}
}

