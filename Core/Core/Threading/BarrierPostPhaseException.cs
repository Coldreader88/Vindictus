using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace Devcat.Core.Threading
{
	[ComVisible(true)]
	[Serializable]
	public class BarrierPostPhaseException : Exception
	{
		public BarrierPostPhaseException() : this((string)null)
        {
		}

		public BarrierPostPhaseException(Exception innerException) : this(null, innerException)
		{
		}

		public BarrierPostPhaseException(string message) : this(message, null)
		{
		}

		[SecurityCritical]
		protected BarrierPostPhaseException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		public BarrierPostPhaseException(string message, Exception innerException) : base((message == null) ? "BarrierPostPhaseException" : message, innerException)
		{
		}
	}
}
