using System;
using System.Collections.Generic;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class AcceptQuestMessage : IMessage
	{
		public string QuestID
		{
			get
			{
				return this.questID;
			}
		}

		public string Title
		{
			get
			{
				return this.title;
			}
		}

		public int SwearID
		{
			get
			{
				return this.swearID;
			}
		}

		public ShipOptionInfo Option
		{
			get
			{
				return this.option;
			}
		}

		public AcceptQuestMessage(string questID, string title, int swearID, ShipOptionInfo option, bool isSeason2, ICollection<int> selectedBossQuestIDInfos)
		{
			this.questID = questID;
			this.title = title;
			this.swearID = swearID;
			this.option = option;
		}

		public override string ToString()
		{
			return string.Format("AcceptQuestMessage [ questID = {0} difficulty = {1} title = {2} swearID = {3} ]", new object[]
			{
				this.questID,
				this.option.Difficulty,
				this.title,
				this.swearID
			});
		}

		private string questID;

		private string title;

		private int swearID;

		private ShipOptionInfo option;
	}
}
