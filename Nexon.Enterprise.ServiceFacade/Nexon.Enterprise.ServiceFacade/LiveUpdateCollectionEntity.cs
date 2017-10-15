using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nexon.Enterprise.ServiceFacade
{
	[DataContract]
	public sealed class LiveUpdateCollectionEntity
	{
		[DataMember]
		public string Key { get; private set; }

		[DataMember]
		public List<LiveUpdateEntity> Data { get; private set; }

		public LiveUpdateCollectionEntity()
		{
			this.Key = Guid.NewGuid().ToString();
			this.Data = new List<LiveUpdateEntity>();
		}
	}
}
