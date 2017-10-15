using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class GameStartGrantedMessage : IMessage
	{
		public int QuestLevel { get; set; }

		public int QuestTime { get; set; }

		public int HardMode { get; set; }

		public int SoloSector { get; set; }

		public QuestSectorInfos QuestSectorInfos { get; set; }

		public bool IsHuntingQuest { get; set; }

		public int InitGameTime { get; set; }

		public int SectorMoveGameTime { get; set; }

		public int Difficulty { get; set; }

		public bool IsTimerDecreasing { get; set; }

		public int QuestStartedPlayerCount { get; set; }

		public int NewSlot { get; set; }

		public int NewKey { get; set; }

		public bool IsUserDedicated { get; set; }

		public override string ToString()
		{
			return string.Format("GameStartGrantedMessage [ QuestLevel = {0} QuestTime = {1} hardmode = {2} soloSector = {3} IsHuntingQuest = {4} InitGameTime = {5} SectorMoveGameTime = {6} IsUserDedicated = {7}]", new object[]
			{
				this.QuestLevel,
				this.QuestTime,
				this.HardMode,
				this.SoloSector,
				this.IsHuntingQuest,
				this.InitGameTime,
				this.SectorMoveGameTime,
				this.IsUserDedicated
			});
		}
	}
}
