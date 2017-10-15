using System;
using System.Collections.Generic;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class AllUserGoalEventMessage : IMessage
	{
		public IDictionary<int, int> AllUserGoalInfo { get; set; }

		public AllUserGoalEventMessage(IDictionary<int, int> AllUserGoalInfo)
		{
			this.AllUserGoalInfo = AllUserGoalInfo;
		}

		public override string ToString()
		{
			return string.Format("AllUserGoalEventMessage [ count = {0} ]", this.AllUserGoalInfo.Count);
		}
	}
}
