using System;
using System.Runtime.Serialization;

namespace Devcat.Core.Diagnostics
{
	[Serializable]
	public class AssumptionErrorException : Exception
	{
		public AssumptionErrorException(string responsiblePerson) : this(responsiblePerson, null)
		{
		}

		public AssumptionErrorException(string responsiblePerson, Exception innerException) : base(string.Format("Program logic assumption error. Contact {0} for more information.", responsiblePerson), innerException)
		{
		}

		public AssumptionErrorException(SerializationInfo si, StreamingContext sc) : base(si, sc)
		{
		}
	}
}
