using System;
using System.Collections.Generic;

namespace ServiceCore.CharacterServiceOperations.RandomMissionOperations
{
	[Serializable]
	public class MissionInfo
	{
		public string MID
		{
			get
			{
				return this.mid;
			}
			set
			{
				this.mid = value;
			}
		}

		public long ID
		{
			get
			{
				return this.id;
			}
			set
			{
				this.id = value;
			}
		}

		public string Category
		{
			get
			{
				return this.category;
			}
			set
			{
				this.category = value;
			}
		}

		public string Title
		{
			get
			{
				return this.title;
			}
			set
			{
				this.title = value;
			}
		}

		public string Location
		{
			get
			{
				return this.location;
			}
			set
			{
				this.location = value;
			}
		}

		public string Description
		{
			get
			{
				return this.description;
			}
			set
			{
				this.description = value;
			}
		}

		public int RequiredLevel
		{
			get
			{
				return this.requiredLevel;
			}
			set
			{
				this.requiredLevel = value;
			}
		}

		public int RewardAP
		{
			get
			{
				return this.rewardAP;
			}
			set
			{
				this.rewardAP = value;
			}
		}

		public int RewardEXP
		{
			get
			{
				return this.rewardEXP;
			}
			set
			{
				this.rewardEXP = value;
			}
		}

		public int RewardGold
		{
			get
			{
				return this.rewardGold;
			}
			set
			{
				this.rewardGold = value;
			}
		}

		public RewardItemInfo RewardItems
		{
			get
			{
				return this.rewardItems;
			}
			set
			{
				this.rewardItems = value;
			}
		}

		public List<ProgressInfo> ProgressInfoList
		{
			get
			{
				return this.progressInfoList;
			}
			set
			{
				this.progressInfoList = value;
			}
		}

		public DateTime AssignedDate
		{
			get
			{
				return this.assignedDate;
			}
			set
			{
				this.assignedDate = value;
			}
		}

		public DateTime ExpireDate
		{
			get
			{
				return this.expireDate;
			}
			set
			{
				this.expireDate = value;
			}
		}

		public int ExpirationPeriod
		{
			get
			{
				return this.expirationPeriod;
			}
			set
			{
				this.expirationPeriod = value;
			}
		}

		public DateTime ModifiedExpirationDate
		{
			get
			{
				return this.modifiedExpirationDate;
			}
			set
			{
				this.modifiedExpirationDate = value;
			}
		}

		public bool Complete
		{
			get
			{
				return this.complete;
			}
			set
			{
				this.complete = value;
			}
		}

		public override string ToString()
		{
			string text = "";
			text = text + " MID " + this.MID + "\n";
			object obj = text;
			text = string.Concat(new object[]
			{
				obj,
				" ID ",
				this.ID,
				"\n"
			});
			text = text + " Category " + this.Category + "\n";
			text = text + " Title " + this.Title + "\n";
			text = text + " Location " + this.Location + "\n";
			text = text + " Description " + this.Description + "\n";
			object obj2 = text;
			text = string.Concat(new object[]
			{
				obj2,
				" RequiredLevel ",
				this.RequiredLevel,
				"\n"
			});
			object obj3 = text;
			text = string.Concat(new object[]
			{
				obj3,
				" RewardAP ",
				this.RewardAP,
				"\n"
			});
			object obj4 = text;
			text = string.Concat(new object[]
			{
				obj4,
				" RewardEXP ",
				this.RewardEXP,
				"\n"
			});
			object obj5 = text;
			text = string.Concat(new object[]
			{
				obj5,
				" RewardGold ",
				this.RewardGold,
				"\n"
			});
			text += this.rewardItems.ToString();
			foreach (ProgressInfo progressInfo in this.progressInfoList)
			{
				text += progressInfo.ToString();
			}
			object obj6 = text;
			text = string.Concat(new object[]
			{
				obj6,
				" AssignedDate ",
				this.AssignedDate,
				"\n"
			});
			object obj7 = text;
			text = string.Concat(new object[]
			{
				obj7,
				" ExpireDate ",
				this.ExpireDate,
				"\n"
			});
			object obj8 = text;
			text = string.Concat(new object[]
			{
				obj8,
				" ExpirationPeriod ",
				this.ExpirationPeriod,
				"\n"
			});
			object obj9 = text;
			text = string.Concat(new object[]
			{
				obj9,
				" ModifiedExpirationDate ",
				this.ModifiedExpirationDate,
				"\n"
			});
			object obj10 = text;
			text = string.Concat(new object[]
			{
				obj10,
				" Complete ",
				this.Complete,
				"\n"
			});
			return text;
		}

		public string ToStringRewardVer()
		{
			string text = "";
			if (this.RewardAP > 0)
			{
				text = text + this.RewardAP + "AP ";
			}
			if (this.RewardEXP > 0)
			{
				text = text + this.RewardEXP + "EXP ";
			}
			if (this.RewardGold > 0)
			{
				text = text + this.RewardEXP + "GOLD ";
			}
			return string.Format("{0} 을 완료하였습니다. 보상으로 {1}를 획득하였습니다.", this.title, text);
		}

		private string mid;

		private long id;

		private string category;

		private string title;

		private string location;

		private string description;

		private int requiredLevel;

		private int rewardAP;

		private int rewardEXP;

		private int rewardGold;

		private RewardItemInfo rewardItems;

		private List<ProgressInfo> progressInfoList = new List<ProgressInfo>();

		private DateTime assignedDate;

		private DateTime expireDate = DateTime.MaxValue;

		private int expirationPeriod = int.MaxValue;

		private DateTime modifiedExpirationDate = DateTime.MaxValue;

		private bool complete;
	}
}
