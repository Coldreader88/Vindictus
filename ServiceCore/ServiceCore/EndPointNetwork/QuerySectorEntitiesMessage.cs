using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class QuerySectorEntitiesMessage : IMessage
	{
		public string Sector
		{
			get
			{
				return this.sector;
			}
		}

		public QuerySectorEntitiesMessage(string sector)
		{
			this.sector = sector;
		}

		public override string ToString()
		{
			return string.Format("QuerySectorEntitiesMessage[ sector = {0} ]", this.sector);
		}

		private string sector;
	}
}
