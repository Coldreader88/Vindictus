using System;
using System.Collections.Generic;

namespace RemoteControlSystem.ServerMessage
{
	public static class RCClientMessages
	{
		public static IEnumerable<Type> Types
		{
			get
			{
				yield return typeof(PingMessage);
				yield return typeof(ClientInitMessage);
				yield return typeof(ServerInfoMessage);
				yield return typeof(AddProcessMessage);
				yield return typeof(ModifyProcessMessage);
				yield return typeof(RemoveProcessMessage);
				yield return typeof(StartProcessMessage);
				yield return typeof(CheckPatchProcessMessage);
				yield return typeof(StopProcessMessage);
				yield return typeof(KillProcessMessage);
				yield return typeof(UpdateProcessMessage);
				yield return typeof(KillUpdateProcessMessage);
				yield return typeof(StandardInProcessMessage);
				yield return typeof(StateChangeProcessMessage);
				yield return typeof(LogProcessMessage);
				yield return typeof(ClientSelfUpdateMessage);
				yield return typeof(FileDistributeMessage);
				yield return typeof(PerformanceUpdateMessage);
				yield break;
			}
		}

		public static IDictionary<Type, int> TypeConverters
		{
			get
			{
				return RCClientMessages.Types.GetConverter(8192);
			}
		}
	}
}
