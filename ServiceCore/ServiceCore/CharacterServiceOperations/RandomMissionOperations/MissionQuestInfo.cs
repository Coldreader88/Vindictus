using System;

namespace ServiceCore.CharacterServiceOperations.RandomMissionOperations
{
	[Serializable]
	public class MissionQuestInfo : ICloneable
	{
		public MissionQuestInfo()
		{
			this.IsPositive = "1";
			this.IsParty = "0";
			this.Setting = MissionQuestInfo.GameSetting.Regex;
			this.TargetCount = 1;
		}

		public MissionQuestInfo.GameSetting Setting { get; set; }

		public int SettingIntValue { get; set; }

		public string SettingStringValue { get; set; }

		public string Target { get; set; }

		public int TargetCount { get; set; }

		public string IsPositive { get; set; }

		public string IsParty { get; set; }

		public object Clone()
		{
			return new MissionQuestInfo
			{
				Setting = this.Setting,
				SettingIntValue = this.SettingIntValue,
				SettingStringValue = this.SettingStringValue,
				Target = this.Target,
				TargetCount = this.TargetCount,
				IsPositive = this.IsPositive,
				IsParty = this.IsParty
			};
		}

		public override string ToString()
		{
			string str = "";
			return str + string.Format("{0} {1} {2} {3} {4} {5} {6} ", new object[]
			{
				this.Setting,
				this.SettingIntValue,
				this.SettingStringValue,
				this.Target,
				this.TargetCount,
				this.IsPositive,
				this.IsParty
			});
		}

		public enum GameSetting
		{
			Regex,
			SoloMode,
			PartyLimit,
			TimeLimit,
			ItemLimit,
			HardMode,
			Difficulty,
			GearLimit,
			PlayerDead,
			Stage
		}
	}
}
