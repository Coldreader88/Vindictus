using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class PvpTeamInfoMessage : IMessage
	{
		public Dictionary<int, int> TeamInfo { get; set; }

		public PvpTeamInfoMessage(Dictionary<int, int> teaminfo)
		{
			this.TeamInfo = teaminfo;
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder("PvpTeamInfoMessage[ ");
			foreach (KeyValuePair<int, int> keyValuePair in this.TeamInfo)
			{
				stringBuilder.AppendFormat("{0} : {1} ", keyValuePair.Key, keyValuePair.Value);
			}
			stringBuilder.AppendFormat("]", new object[0]);
			return stringBuilder.ToString();
		}
	}
}
