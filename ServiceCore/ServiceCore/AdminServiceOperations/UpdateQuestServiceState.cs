using System;
using System.Collections.Generic;

namespace ServiceCore.AdminServiceOperations
{
	[Serializable]
	public sealed class UpdateQuestServiceState
	{
		public int RunningQuestCount
		{
			get
			{
				return this.runningQuestCount;
			}
		}

		public IEnumerable<string> RunningQuestList
		{
			get
			{
				return this.runningQuestList;
			}
		}

		public UpdateQuestServiceState(int count, IEnumerable<string> list)
		{
			this.runningQuestCount = count;
			this.runningQuestList = list;
		}

		private int runningQuestCount;

		private IEnumerable<string> runningQuestList;
	}
}
