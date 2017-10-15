using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class CustomSectorQuestMessage : IMessage
	{
		public string TargetSector
		{
			get
			{
				return this.targetSector;
			}
		}

		public CustomSectorQuestMessage(string targetSector)
		{
			this.targetSector = targetSector;
		}

		public override string ToString()
		{
			return string.Format("CustomSectorQuestMessage[ targetSector = {0} ]", this.targetSector);
		}

		private string targetSector;
	}
}
