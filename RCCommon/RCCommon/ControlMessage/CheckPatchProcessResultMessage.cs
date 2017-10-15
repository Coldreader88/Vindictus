using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace RemoteControlSystem.ControlMessage
{
	[Guid("7A290175-36C4-4a91-92BC-0EC30EB925AC")]
	[Serializable]
	public sealed class CheckPatchProcessResultMessage
	{
		public CheckPatchProcessResultMessage(List<string> checkPatchLog)
		{
			this.CheckPatchLog = checkPatchLog;
		}

		public List<string> CheckPatchLog;
	}
}
