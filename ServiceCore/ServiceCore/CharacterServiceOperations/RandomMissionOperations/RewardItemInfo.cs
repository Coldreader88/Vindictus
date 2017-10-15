using System;
using System.Collections.Generic;
using System.Linq;

namespace ServiceCore.CharacterServiceOperations.RandomMissionOperations
{
	[Serializable]
	public class RewardItemInfo : ICloneable
	{
		public List<string> RewardItemIDs { get; set; }

		public List<int> RewardItemNums { get; set; }

		public RewardItemInfo(List<string> ids, List<int> nums)
		{
			this.RewardItemIDs = new List<string>();
			this.RewardItemNums = new List<int>();
			this.RewardItemIDs.AddRange(ids);
			this.RewardItemNums.AddRange(nums);
		}

		public RewardItemInfo()
		{
			this.RewardItemIDs = new List<string>();
			this.RewardItemNums = new List<int>();
		}

		public void Set(string id, int num)
		{
			this.RewardItemIDs.Add(id);
			this.RewardItemNums.Add(num);
		}

		public int Count
		{
			get
			{
				return this.RewardItemIDs.Count;
			}
		}

		public string GetID(int idx)
		{
			return this.RewardItemIDs[idx];
		}

		public int GetNum(int idx)
		{
			return this.RewardItemNums[idx];
		}

		public override string ToString()
		{
			string text = "";
			for (int i = 0; i < this.RewardItemIDs.Count<string>(); i++)
			{
				object obj = text;
				text = string.Concat(new object[]
				{
					obj,
					" RewardItem ",
					this.RewardItemIDs[i],
					" ",
					this.RewardItemNums[i],
					"\n"
				});
			}
			return text;
		}

		public object Clone()
		{
			return new RewardItemInfo(this.RewardItemIDs, this.RewardItemNums);
		}
	}
}
