using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utility;

namespace ServiceCore.CharacterServiceOperations
{
	[Serializable]
	public sealed class CharacterStats : IEnumerable<KeyValuePair<string, int>>, IEnumerable
	{
		static CharacterStats()
		{
			foreach (object obj in Enum.GetValues(typeof(Stats)))
			{
				Stats stats = (Stats)obj;
				if (stats != Stats.INVALID && stats != Stats.STATS_NUM)
				{
					CharacterStats.StatNames[stats.ToString()] = stats;
				}
			}
		}

		IEnumerator<KeyValuePair<string, int>> IEnumerable<KeyValuePair<string, int>>.GetEnumerator()
		{
			return this.stats.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.stats.GetEnumerator();
		}

		public int this[Stats kind]
		{
			get
			{
				return this.stats.TryGetValue(kind.ToString());
			}
			set
			{
				this.stats[kind.ToString()] = value;
			}
		}

		public CharacterStats()
		{
			this.stats = CharacterStats.StatNames.Keys.ToDictionary((string x) => x, (string x) => 0, StringComparer.OrdinalIgnoreCase);
		}

		public CharacterStats(CharacterStats source) : this()
		{
			foreach (KeyValuePair<string, int> keyValuePair in ((IEnumerable<KeyValuePair<string, int>>)source))
			{
				this.stats[keyValuePair.Key] = keyValuePair.Value;
			}
		}

		public CharacterStats(Dictionary<string, int> dict) : this()
		{
			foreach (KeyValuePair<string, int> keyValuePair in dict)
			{
				this.stats[keyValuePair.Key] = keyValuePair.Value;
			}
		}

		public CharacterStats(int s, int d, int i, int w, int l, int h, int st) : this()
		{
			this.stats[Stats.STR.ToString()] = s;
			this.stats[Stats.DEX.ToString()] = d;
			this.stats[Stats.INT.ToString()] = i;
			this.stats[Stats.WILL.ToString()] = w;
			this.stats[Stats.LUCK.ToString()] = l;
			this.stats[Stats.HP.ToString()] = h;
			this.stats[Stats.STAMINA.ToString()] = st;
		}

		public void AddStat(Dictionary<string, int> stat)
		{
			foreach (KeyValuePair<string, int> keyValuePair in stat)
			{
				if (this.stats.ContainsKey(keyValuePair.Key))
				{
					Dictionary<string, int> dictionary;
					string key;
					(dictionary = this.stats)[key = keyValuePair.Key] = dictionary[key] + keyValuePair.Value;
				}
				else
				{
					this.stats.Add(keyValuePair.Key, keyValuePair.Value);
				}
			}
		}

		public void SubtractStat(Dictionary<string, int> stat)
		{
			foreach (KeyValuePair<string, int> keyValuePair in stat)
			{
				if (this.stats.ContainsKey(keyValuePair.Key))
				{
					Dictionary<string, int> dictionary;
					string key;
					(dictionary = this.stats)[key = keyValuePair.Key] = dictionary[key] - keyValuePair.Value;
				}
				else
				{
					this.stats.Add(keyValuePair.Key, -keyValuePair.Value);
				}
			}
		}

		public static CharacterStats operator +(CharacterStats lhs, CharacterStats rhs)
		{
			return new CharacterStats
			{
				lhs,
				rhs
			};
		}

		public static CharacterStats operator -(CharacterStats lhs, CharacterStats rhs)
		{
			CharacterStats characterStats = new CharacterStats();
			characterStats.Add(lhs);
			characterStats.Subtract(rhs);
			return characterStats;
		}

		public CharacterStats Add(CharacterStats rhs)
		{
			foreach (KeyValuePair<string, int> keyValuePair in ((IEnumerable<KeyValuePair<string, int>>)rhs))
			{
				int num;
				if (this.stats.TryGetValue(keyValuePair.Key, out num))
				{
					this.stats[keyValuePair.Key] = num + keyValuePair.Value;
				}
				else
				{
					this.stats[keyValuePair.Key] = keyValuePair.Value;
				}
			}
			return this;
		}

		public CharacterStats Subtract(CharacterStats rhs)
		{
			foreach (KeyValuePair<string, int> keyValuePair in ((IEnumerable<KeyValuePair<string, int>>)rhs))
			{
				int num;
				if (this.stats.TryGetValue(keyValuePair.Key, out num))
				{
					this.stats[keyValuePair.Key] = num - keyValuePair.Value;
					if (this.stats[keyValuePair.Key] == 0)
					{
						this.stats.Remove(keyValuePair.Key);
					}
				}
				else
				{
					this.stats[keyValuePair.Key] = -keyValuePair.Value;
				}
			}
			return this;
		}

		public void Multiply(double ratio)
		{
			foreach (string key in this.stats.Keys.ToList<string>())
			{
				this.stats[key] = (int)((double)this.stats[key] * ratio);
			}
		}

		public void Override(Dictionary<string, int> overrider)
		{
			if (overrider == null)
			{
				return;
			}
			foreach (KeyValuePair<string, int> keyValuePair in overrider)
			{
				this.stats[keyValuePair.Key] = keyValuePair.Value;
			}
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (KeyValuePair<string, int> keyValuePair in this.stats)
			{
				stringBuilder.AppendFormat("{0}[{1}] ", keyValuePair.Key, keyValuePair.Value);
			}
			return stringBuilder.ToString();
		}

		public static Dictionary<string, Stats> StatNames = new Dictionary<string, Stats>(StringComparer.OrdinalIgnoreCase);

		private Dictionary<string, int> stats;
	}
}
