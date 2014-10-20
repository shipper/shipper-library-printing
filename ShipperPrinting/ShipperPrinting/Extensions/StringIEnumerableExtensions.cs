using System;
using System.Collections.Generic;
using System.Linq;

internal static class StringIEnumerableExtensions
{
	public static string[] MoveUp(this string[] values)
	{
		return values.MoveUp (null);
	}

	public static string[] MoveUp(this string[] values, Func<string, bool> whereCallback)
	{
		for (int i = 0; i < values.Length - 1; i++)
		{
			if (String.IsNullOrWhiteSpace(values[i]))
			{
				for (int z = i + 1; z < values.Length; z++)
				{
					if (!String.IsNullOrWhiteSpace(values.ElementAt(z)))
					{
						values[i] = values.ElementAt(z);
						values[z] = String.Empty;
						break;
					}
				}
			}
		}
		if (whereCallback == null)
		{
			return values;
		}
		for (int i = 0; i < values.Length - 1; i++)
		{
			if (String.IsNullOrWhiteSpace(values[i]))
			{
				break;
			}
			if (whereCallback(values[i]))
			{
				continue;
			}
			if (String.IsNullOrWhiteSpace(values[values.Length - 1]))
			{
				//Move everything down
				if (i < values.Length - 2)
				{
					for (int j = i + 2; j < values.Length; j++)
					{
						values[j] = values[j - 1];
					}
					int maxLength = 1;
					for (; maxLength < values[i].Length; maxLength++)
					{
						if (!whereCallback(values[i].Substring(0, maxLength)))
						{
							break;
						}
					}
					string a = values[i].Substring(0, maxLength - 1);
					string b = values[i].Substring(maxLength - 1);
					values[i] = a;
					values[i + 1] = b;
					continue;
				}
			}
			//We tried
		}
		return values;
	}
}

