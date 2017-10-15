using System;
using System.Collections.Generic;
using ServiceCore.CharacterServiceOperations.RandomMissionOperations;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class MissionMessage : IMessage
	{
		private int GetRemainTimeFromNow(DateTime date)
		{
			double totalSeconds = (date - DateTime.UtcNow).TotalSeconds;
			int num = (2147483647.0 < totalSeconds) ? int.MaxValue : ((int)totalSeconds);
			if (num < 0)
			{
				num = 0;
			}
			return num;
		}

		private void SetProgressInfo(List<ProgressInfo> progressInfoList)
		{
			this.QuestTitle0 = "";
			this.Progress0 = -1;
			this.TotalProgress0 = -1;
			this.QuestTitle1 = "";
			this.Progress1 = -1;
			this.TotalProgress1 = -1;
			this.QuestTitle2 = "";
			this.Progress2 = -1;
			this.TotalProgress2 = -1;
			this.QuestTitle3 = "";
			this.Progress3 = -1;
			this.TotalProgress3 = -1;
			this.QuestTitle4 = "";
			this.Progress4 = -1;
			this.TotalProgress4 = -1;
			foreach (ProgressInfo progressInfo in progressInfoList)
			{
				switch (progressInfo.ID)
				{
				case 0:
					this.QuestTitle0 = progressInfo.Title;
					this.Progress0 = progressInfo.Current;
					this.TotalProgress0 = progressInfo.Max;
					break;
				case 1:
					this.QuestTitle1 = progressInfo.Title;
					this.Progress1 = progressInfo.Current;
					this.TotalProgress1 = progressInfo.Max;
					break;
				case 2:
					this.QuestTitle2 = progressInfo.Title;
					this.Progress2 = progressInfo.Current;
					this.TotalProgress2 = progressInfo.Max;
					break;
				case 3:
					this.QuestTitle3 = progressInfo.Title;
					this.Progress3 = progressInfo.Current;
					this.TotalProgress3 = progressInfo.Max;
					break;
				case 4:
					this.QuestTitle4 = progressInfo.Title;
					this.Progress4 = progressInfo.Current;
					this.TotalProgress4 = progressInfo.Max;
					break;
				}
			}
		}

		public MissionMessage(MissionInfo mission)
		{
			this.MID = mission.MID;
			this.ID = mission.ID;
			this.Category = mission.Category;
			this.Title = mission.Title;
			this.Location = mission.Location;
			this.Description = mission.Description;
			this.RequiredLevel = mission.RequiredLevel;
			this.RewardAP = mission.RewardAP;
			this.RewardEXP = mission.RewardEXP;
			this.RewardGold = mission.RewardGold;
			this.RewardItemIDs = new List<string>();
			this.RewardItemIDs.AddRange(mission.RewardItems.RewardItemIDs);
			this.RewardItemNums = new List<int>();
			this.RewardItemNums.AddRange(mission.RewardItems.RewardItemNums);
			this.ExpirationTime = this.GetRemainTimeFromNow(mission.ExpireDate);
			this.ExpirationPeriod = mission.ExpirationPeriod;
			this.ModifiedExpirationTime = this.GetRemainTimeFromNow(mission.ModifiedExpirationDate);
			this.Complete = mission.Complete;
			this.SetProgressInfo(mission.ProgressInfoList);
		}

		public string MID { get; set; }

		public long ID { get; set; }

		public string Category { get; set; }

		public string Title { get; set; }

		public string Location { get; set; }

		public string Description { get; set; }

		public int RequiredLevel { get; set; }

		public int RewardAP { get; set; }

		public int RewardEXP { get; set; }

		public int RewardGold { get; set; }

		public List<string> RewardItemIDs { get; set; }

		public List<int> RewardItemNums { get; set; }

		public int ModifiedExpirationTime { get; set; }

		public int ExpirationTime { get; set; }

		public int ExpirationPeriod { get; set; }

		public bool Complete { get; set; }

		public string QuestTitle0 { get; set; }

		public int Progress0 { get; set; }

		public int TotalProgress0 { get; set; }

		public string QuestTitle1 { get; set; }

		public int Progress1 { get; set; }

		public int TotalProgress1 { get; set; }

		public string QuestTitle2 { get; set; }

		public int Progress2 { get; set; }

		public int TotalProgress2 { get; set; }

		public string QuestTitle3 { get; set; }

		public int Progress3 { get; set; }

		public int TotalProgress3 { get; set; }

		public string QuestTitle4 { get; set; }

		public int Progress4 { get; set; }

		public int TotalProgress4 { get; set; }

		public override string ToString()
		{
			return string.Format("ID {2} MID {0} {1} ", this.MID, this.Title, this.ID);
		}
	}
}
