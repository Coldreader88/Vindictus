using System;
using System.Collections.Generic;
using Utility;

namespace ServiceCore.CharacterServiceOperations.RandomMissionOperations
{
	[Serializable]
	public class MissionQuest : ICloneable
	{
		public int TotalProgress
		{
			get
			{
				int num = 0;
				foreach (MissionQuestInfo missionQuestInfo in this.Goals)
				{
					num += missionQuestInfo.TargetCount;
				}
				return num;
			}
		}

		public ProgressInfo ToProgressInfo()
		{
			return new ProgressInfo
			{
				ID = this.ID,
				Title = this.Title,
				Current = this.Progress,
				Max = this.TotalProgress
			};
		}

		public Func<int, bool> CompleteChecker()
		{
			return (int _progress) => this.TotalProgress <= _progress;
		}

		public bool IsComplete()
		{
			if (this.TotalProgress < this.progress)
			{
				Log<MissionQuest>.Logger.ErrorFormat("progress [{1}] is bigger than TotalProgress [{0}]", this.TotalProgress, this.progress);
				this.progress = this.TotalProgress;
			}
			return this.TotalProgress == this.progress;
		}

		public int Progress
		{
			get
			{
				return this.progress;
			}
			set
			{
				this.progress = value;
			}
		}

		public object Clone()
		{
			MissionQuest missionQuest = new MissionQuest
			{
				ID = this.ID,
				Progress = this.Progress,
				Title = this.Title,
				IsPartyMission = this.IsPartyMission
			};
			foreach (MissionQuestInfo missionQuestInfo in this.GameSettings)
			{
				missionQuest.GameSettings.Add(missionQuestInfo.Clone() as MissionQuestInfo);
			}
			foreach (MissionQuestInfo missionQuestInfo2 in this.Goals)
			{
				missionQuest.Goals.Add(missionQuestInfo2.Clone() as MissionQuestInfo);
			}
			return missionQuest;
		}

		public override string ToString()
		{
			string text = string.Format("ID {0}   Title {1}  Progress {2} IsPartyMission {3}", new object[]
			{
				this.ID,
				this.Title,
				this.Progress,
				this.IsPartyMission
			});
			text += "GameSettings\n";
			foreach (MissionQuestInfo arg in this.GameSettings)
			{
				text = text + arg + " ";
			}
			text += "\nGoals\n";
			foreach (MissionQuestInfo arg2 in this.Goals)
			{
				text = text + arg2 + " ";
			}
			text += "\n";
			return text;
		}

		public int ID;

		public string Title;

		public bool IsPartyMission;

		public IList<MissionQuestInfo> GameSettings = new List<MissionQuestInfo>();

		public IList<MissionQuestInfo> Goals = new List<MissionQuestInfo>();

		private int progress;
	}
}
