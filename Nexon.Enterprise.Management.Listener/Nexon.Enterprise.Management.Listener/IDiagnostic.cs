using System;
using System.Runtime.InteropServices;

namespace Nexon.Enterprise.Management.Listener
{
	[Guid("9D16F637-18AE-4b69-A30C-D7D96BA7DB2C")]
	public interface IDiagnostic
	{
		void InitializeRawValue(string instanceName, int nexonCounterTypeId, long rawValue);

		void IncrementBy(string instanceName, int nexonCounterTypeId, long value);
	}
}
