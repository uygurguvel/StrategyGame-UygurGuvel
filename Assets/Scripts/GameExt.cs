using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class GameExt
{
	public static int IndexOf<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key)
	{
		int i = 0;
		foreach (var pair in dictionary)
		{
			if (pair.Key.Equals(key))
			{
				return i;
			}
			i++;
		}
		return -1;
	}

	public static int GetModulo(this int k, int n)
	{
		return ((k %= n) < 0) ? k + n : k;
	}

	public static void AdjustImage(this AspectRatioFitter aspectRatioFitter, Rect rect)
	{
		float aspectRatio = rect.width / rect.height;
		aspectRatioFitter.aspectRatio = aspectRatio;
	}
}