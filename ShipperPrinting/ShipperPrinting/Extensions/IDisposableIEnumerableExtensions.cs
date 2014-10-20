using System;
using System.Collections.Generic;

internal static class IDisposableIEnumerableExtensions
{
	public static void Dispose(this IEnumerable<IDisposable> disposable){
		if (disposable == null) {
			return;
		}
		foreach (IDisposable dis in disposable) {
			if (dis == null) {
				continue;
			}
			try{
				dis.Dispose();
			}catch{

			}
		}
	}
}

