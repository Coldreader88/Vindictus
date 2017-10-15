using System;
using System.Collections.Generic;
using Utility;

namespace ServiceCore.CharacterServiceOperations
{
	public static class StatDictExtension
	{
		public static Stats? GetPlayerStatTypeIndex(string statName)
		{
			for (int i = 0; i < StatDictExtension.Values.Length; i++)
			{
				if (statName == StatDictExtension.Values.GetValue(i).ToString())
				{
					return new Stats?((Stats)StatDictExtension.Values.GetValue(i));
				}
			}
			return null;
		}

		public static string GetPlayerStatTypeName(int statIndex)
		{
			for (int i = 0; i < StatDictExtension.Values.Length; i++)
			{
				if (statIndex == (int)StatDictExtension.Values.GetValue(i))
				{
					return StatDictExtension.Values.GetValue(i).ToString();
				}
			}
			return null;
		}

		public static bool ApplyNewStat(this IDictionary<string, int> oldStat, IDictionary<string, int> newStat)
		{
			bool result = false;
			foreach (string key in CharacterStats.StatNames.Keys)
			{
				int num = oldStat.TryGetValue(key);
				int num2 = newStat.TryGetValue(key);
				if (num != num2)
				{
					result = true;
					oldStat[key] = num2;
				}
			}
			return result;
		}

		public static Array Values = Enum.GetValues(typeof(Stats));
	}
}
