using System;
using System.Runtime.Serialization;

namespace Nexon.Enterprise.ServiceFacade
{
	[DataContract]
	public sealed class LiveUpdateEntity
	{
		[DataMember]
		public string FilePath { get; private set; }

		[DataMember]
		public string FileHashCode { get; private set; }

		public LiveUpdateEntity(string path, string hashCode)
		{
			this.FilePath = path;
			this.FileHashCode = hashCode;
		}
	}
}
