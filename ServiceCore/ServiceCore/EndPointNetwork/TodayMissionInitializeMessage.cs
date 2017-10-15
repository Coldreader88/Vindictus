using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class TodayMissionInitializeMessage : IMessage
	{
		public TodayMissionInitializeMessage(Dictionary<int, TodayMissinState> statesDic, int remainResetMinute)
		{
			this.MissionStates = statesDic;
			this.RemainResetMinute = remainResetMinute;
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (KeyValuePair<int, TodayMissinState> keyValuePair in this.MissionStates)
			{
				stringBuilder.AppendFormat("{0}\n", keyValuePair.Value.ToString());
			}
			return stringBuilder.ToString();
		}

		private Dictionary<int, TodayMissinState> MissionStates;

		private int RemainResetMinute;
	}
}
