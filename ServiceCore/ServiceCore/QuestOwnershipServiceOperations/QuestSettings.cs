using System;
using System.Collections.Generic;
using System.Linq;
using Utility;

namespace ServiceCore.QuestOwnershipServiceOperations
{
	[Serializable]
	public sealed class QuestSettings
	{
		public string QuestID { get; private set; }

		public int Difficulty { get; private set; }

		public MultiValueDictionary<string, string> Settings { get; private set; }

		public QuestSettings(string questID, int difficulty)
		{
			this.QuestID = questID;
			this.Difficulty = difficulty;
			this.Settings = new MultiValueDictionary<string, string>();
		}

		public int TutorStampCount
		{
			get
			{
				List<string> list;
				if (this.Settings.TryGetValue("TutorStamp", out list))
				{
					return list.Count;
				}
				return 0;
			}
		}

		public float QuickSlotMultiplier
		{
			get
			{
				if (this.Settings.Keys.Contains("SetQuickSlotLimit"))
				{
					return 0f;
				}
				float num = 1f;
				foreach (KeyValuePair<string, string> keyValuePair in this.Settings)
				{
					if (keyValuePair.Key == "QuickSlotMultiplier")
					{
						num *= float.Parse(keyValuePair.Value);
					}
				}
				return num;
			}
		}

		public int QuickSlotLimit
		{
			get
			{
				List<string> source;
				if (this.Settings.TryGetValue("SetQuickSlotLimit", out source))
				{
					return int.Parse(source.First<string>());
				}
				return 0;
			}
		}

		public int CalculateQuickSlotLimit(int baseLimit)
		{
			List<string> source;
			if (this.Settings.TryGetValue("SetQuickSlotLimit", out source))
			{
				return int.Parse(source.First<string>());
			}
			float num = 1f;
			foreach (KeyValuePair<string, string> keyValuePair in this.Settings)
			{
				if (keyValuePair.Key == "QuickSlotMultiplier")
				{
					num *= float.Parse(keyValuePair.Value);
				}
			}
			return (int)(num * (float)baseLimit);
		}

		public float SubWeaponMultiplier
		{
			get
			{
				if (this.Settings.Keys.Contains("SetSubWeaponLimit"))
				{
					return 0f;
				}
				float num = 1f;
				foreach (KeyValuePair<string, string> keyValuePair in this.Settings)
				{
					if (keyValuePair.Key == "SubWeaponMultiplier")
					{
						num *= float.Parse(keyValuePair.Value);
					}
				}
				return num;
			}
		}

		public int SubWeaponLimit
		{
			get
			{
				List<string> source;
				if (this.Settings.TryGetValue("SetSubWeaponLimit", out source))
				{
					return int.Parse(source.First<string>());
				}
				return 0;
			}
		}

		public int CalculateSubWeaponLimit(int baseLimit)
		{
			List<string> source;
			if (this.Settings.TryGetValue("SetSubWeaponLimit", out source))
			{
				return int.Parse(source.First<string>());
			}
			float num = 1f;
			foreach (KeyValuePair<string, string> keyValuePair in this.Settings)
			{
				if (keyValuePair.Key == "SubWeaponMultiplier")
				{
					num *= float.Parse(keyValuePair.Value);
				}
			}
			return (int)(num * (float)baseLimit);
		}

		public string SelectedSubWeapon
		{
			get
			{
				List<string> source;
				if (this.Settings.TryGetValue("SelectedSubWeapon", out source))
				{
					return source.First<string>();
				}
				return null;
			}
		}

		public bool UseFishingModule
		{
			get
			{
				return this.Settings.Keys.Contains("Fishing");
			}
		}

		public bool UseHuntingModule
		{
			get
			{
				return this.Settings.Keys.Contains("Hunting");
			}
		}

		public bool UseTowerModule
		{
			get
			{
				return this.Settings.Keys.Contains("Tower");
			}
		}

		public bool ConsumableFree
		{
			get
			{
				return this.Settings.Keys.Contains("ConsumableFree");
			}
		}

		public bool IsNoAssist
		{
			get
			{
				return this.Settings.Keys.Contains("NoAssist");
			}
		}

		public Dictionary<string, int> TriggeredDropDic
		{
			get
			{
				Dictionary<string, int> dictionary = new Dictionary<string, int>();
				List<string> list;
				if (this.Settings.TryGetValue("TriggeredDrop", out list))
				{
					char[] separator = new char[]
					{
						'='
					};
					foreach (string text in list)
					{
						string[] array = text.Split(separator);
						if (array.Length == 2)
						{
							dictionary.Add(array[0], int.Parse(array[1]));
						}
					}
				}
				return dictionary;
			}
		}

		public bool IsRequiredItemForAll
		{
			get
			{
				return this.Settings.Keys.Contains("RequiredItemForAll");
			}
		}

		public bool IsNoHugeLucky
		{
			get
			{
				return this.Settings.Keys.Contains("NoHugeLucky");
			}
		}
	}
}
