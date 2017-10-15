using System;
using System.EnterpriseServices;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace Nexon.Enterprise.Management.Listener
{
	[EventTrackingEnabled(true)]
	[Guid("34F0773D-DCA1-4603-9044-21E2D68E3030")]
	[ObjectPooling(Enabled = true)]
	[EventClass(FireInParallel = true)]
	[JustInTimeActivation(true)]
	[Transaction(TransactionOption.Required, Isolation = TransactionIsolationLevel.ReadCommitted)]
	[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
	public class MessageListener : ServicedComponent, IMessage
	{
		[AutoComplete(true)]
		public void WriteException(string appDomainFriendlyName, int traceEventType, int priority, string category, string message, Exception e)
		{
		}

		[AutoComplete(true)]
		public void WriteMessage(string appDomainFriendlyName, int traceEventType, int priority, string category, string message, string[] args)
		{
		}
	}
}
