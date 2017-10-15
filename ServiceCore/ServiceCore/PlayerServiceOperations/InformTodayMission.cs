using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.PlayerServiceOperations
{
	[Serializable]
	public class InformTodayMission : Operation
	{
		public List<int> MissionIDList { get; set; }

		public InformTodayMission()
		{
			this.MissionIDList = new List<int>();
		}

		public bool HasMissinInfo()
		{
			return this.MissionIDList.Count > 0;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
