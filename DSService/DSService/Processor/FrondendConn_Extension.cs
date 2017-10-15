using System;
using ServiceCore.EndPointNetwork;
using ServiceCore.FrontendServiceOperations;
using UnifiedNetwork.Entity;

namespace DSService.Processor
{
	public static class FrondendConn_Extension
	{
		public static void SendConsole(this IEntityProxy frontendConn, object format, params object[] arg)
		{
			frontendConn.RequestOperation(SendPacket.Create<SystemMessage>(new SystemMessage(SystemMessageCategory.Console, format.ToString(), arg)));
		}
	}
}
