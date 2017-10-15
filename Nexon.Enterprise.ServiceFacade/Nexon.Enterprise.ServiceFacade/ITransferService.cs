using System;
using System.IO;
using System.ServiceModel;
using Nexon.Enterprise.ServiceFacade.Channels.ChunkingChannel;

namespace Nexon.Enterprise.ServiceFacade
{
	[ServiceContract(Namespace = "http://bam.nexon.com")]
	public interface ITransferService
	{
		[ChunkingBehavior(ChunkingAppliesTo.Both)]
		[OperationContract]
		Stream EchoStream(Stream stream);

		[OperationContract]
		bool CheckLiveUpdate(string key, string hashCode);

		[OperationContract]
		[ChunkingBehavior(ChunkingAppliesTo.OutMessage)]
		Stream DownloadStream(string key);

		[OperationContract(IsOneWay = true)]
		[ChunkingBehavior(ChunkingAppliesTo.InMessage)]
		void UploadStream(Stream stream);
	}
}
