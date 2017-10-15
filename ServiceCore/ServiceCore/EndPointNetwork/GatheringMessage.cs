using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class GatheringMessage : IMessage
	{
		public string EntityName
		{
			get
			{
				return this.entityName;
			}
		}

		public int GatherTag
		{
			get
			{
				return this.gatherTag;
			}
		}

		public GatheringMessage(string eName, int gTag)
		{
			this.entityName = eName;
			this.gatherTag = gTag;
		}

		public override string ToString()
		{
			return string.Format("PropBrokenMessage[ entityName = {0} gatherTag = {1} ]", this.entityName, this.gatherTag);
		}

		private string entityName;

		private int gatherTag;
	}
}
