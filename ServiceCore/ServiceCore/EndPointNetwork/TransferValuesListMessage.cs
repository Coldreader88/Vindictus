using System;
using System.Collections.Generic;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class TransferValuesListMessage : IMessage
	{
		public TransferValuesListMessage(ICollection<TransferValues> transferInfos)
		{
			this.TransferInfos = transferInfos;
		}

		public ICollection<TransferValues> TransferInfos { get; private set; }
	}
}
