using System;
using System.EnterpriseServices;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace Nexon.Enterprise.Management.Listener
{
	[Guid("CA5BFF54-2CCA-4242-8470-786639D49C86")]
	[EventTrackingEnabled(true)]
	[JustInTimeActivation(true)]
	[ObjectPooling(Enabled = true)]
	[EventClass(FireInParallel = true)]
	[Transaction(TransactionOption.Required, Isolation = TransactionIsolationLevel.ReadCommitted)]
	[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
	public class DiagnosticListener : ServicedComponent, IDiagnostic
	{
		public void InitializeRawValue(string instanceName, int nexonCounterTypeId, long rawValue)
		{
		}

		public void IncrementBy(string instanceName, int nexonCounterTypeId, long value)
		{
		}
	}
}
