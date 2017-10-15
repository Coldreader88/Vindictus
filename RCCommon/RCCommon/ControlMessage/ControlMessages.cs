using System;
using System.Collections.Generic;

namespace RemoteControlSystem.ControlMessage
{
	public static class ControlMessages
	{
		public static IEnumerable<Type> Types
		{
			get
			{
				yield return typeof(ChildProcessLogRequestMessage);
				yield return typeof(ChildProcessLogReplyMessage);
				yield return typeof(ChildProcessLogListRequestMessage);
				yield return typeof(ChildProcessLogListReplyMessage);
				yield return typeof(ChildProcessLogConnectMessage);
				yield return typeof(ChildProcessLogDisconnectMessage);
				yield return typeof(ChildProcessLogMessage);
				yield return typeof(ExeInfoRequestMessage);
				yield return typeof(ExeInfoReplyMessage);
				yield return typeof(FunctionRequestMessage);
				yield return typeof(FunctionReplyMessage);
				yield return typeof(CheckPatchProcessResultMessage);
				yield break;
			}
		}

		public static IDictionary<Type, int> TypeConverters
		{
			get
			{
				return ControlMessages.Types.GetConverter(12288);
			}
		}
	}
}
