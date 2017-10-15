using System;
using System.Runtime.InteropServices;

namespace Nexon.Enterprise.Management.Listener
{
	[Guid("BC030126-35FA-4c88-9980-FDD452DF6246")]
	public interface IMessage
	{
		void WriteException(string appDomainFriendlyName, int traceEventType, int priority, string category, string message, Exception e);

		void WriteMessage(string appDomainFriendlyName, int traceEventType, int priority, string category, string message, string[] args);
	}
}
