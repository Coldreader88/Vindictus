using System;
using System.Collections.Generic;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class EvilCoreInfo
	{
		public string EvilCoreEntityName { get; set; }

		public EvilCoreType EvilCoreType { get; set; }

		public int Winner { get; set; }

		public List<int> AdditionalRareCoreTagList { get; set; }

		public EvilCoreInfo(string evilCoreEntityName, EvilCoreType type, int winner)
		{
			this.EvilCoreEntityName = evilCoreEntityName;
			this.EvilCoreType = type;
			this.Winner = winner;
			this.AdditionalRareCoreTagList = new List<int>();
		}

		public EvilCoreInfo(string evilCoreEntityName, EvilCoreType type, int winner, bool isRareCore)
		{
			this.EvilCoreEntityName = evilCoreEntityName;
			this.EvilCoreType = type;
			this.Winner = winner;
			this.AdditionalRareCoreTagList = new List<int>();
			if (isRareCore && winner != -1)
			{
				this.AdditionalRareCoreTagList.Add(winner);
			}
		}

		public EvilCoreInfo(string evilCoreEntityName, EvilCoreType type, int winner, List<int> rareCoreTagList)
		{
			this.EvilCoreEntityName = evilCoreEntityName;
			this.EvilCoreType = type;
			this.Winner = winner;
			this.AdditionalRareCoreTagList = new List<int>();
			if (winner == -1)
			{
				foreach (int item in rareCoreTagList)
				{
					this.AdditionalRareCoreTagList.Add(item);
				}
			}
		}
	}
}
