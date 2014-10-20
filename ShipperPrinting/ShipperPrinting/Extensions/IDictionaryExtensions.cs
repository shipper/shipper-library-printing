using System;
using System.Linq;
using System.Collections.Generic;
using Charles.Shipper.Printing.Core.Extensions.Models;

internal static class IDictionaryExtensions
{
	public static IDictionary<TKey, Pair<T1, T2>> Pair<TKey, T1, T2>(
		this IDictionary<TKey, T1> dictA, 
		IDictionary<TKey, T2> dictB, Func<TKey, TKey, bool> compare = null){
		IDictionary<TKey, Pair<T1, T2>> res = new Dictionary<TKey, Pair<T1, T2>> ();
		foreach (TKey keyA in dictA.Keys) {
			foreach (TKey keyB in dictB.Keys) {
				bool equal = false;
				if (compare != null) {
					equal = compare (keyA, keyB);
				} else {
					equal = keyA.Equals(keyB);
				}
				if (!equal) {
					continue;
				}
				res.Add (keyA, new Pair<T1, T2> {
					Value1 = dictA[keyA],
					Value2 = dictB[keyB]
				});
			}
		}

		return res;
	}
}

