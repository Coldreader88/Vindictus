using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.MicroPlayServiceOperations
{
	[Serializable]
	public sealed class MoveSector : Operation
	{
		public string TriggerName { get; set; }

		public int TargetGroup { get; set; }

		public bool HasCheatPermission { get; set; }

		public List<int> HolyProps { get; set; }

		public MoveSector(string triggerName, int targetGroup, bool hasCheatPermission)
		{
			this.TriggerName = triggerName;
			this.TargetGroup = targetGroup;
			this.HasCheatPermission = hasCheatPermission;
			this.HolyProps = new List<int>();
		}

		public MoveSector(string triggerName, int targetGroup, List<int> holyProps, bool hasCheatPermission)
		{
			this.TriggerName = triggerName;
			this.TargetGroup = targetGroup;
			this.HasCheatPermission = hasCheatPermission;
			this.HolyProps = holyProps;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
