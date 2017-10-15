using System;
using System.ServiceModel.Channels;

namespace Nexon.Enterprise.ServiceFacade.Channels.ChunkingChannel
{
	public class ChunkingUtils
	{
		public static T GetMessageHeader<T>(Message message, string name, string ns)
		{
			int num = message.Headers.FindHeader(name, ns);
			if (num > -1)
			{
				return message.Headers.GetHeader<T>(num);
			}
			return default(T);
		}

		public static string GetAddressingNamespace(MessageVersion version)
		{
			if (version.Addressing == AddressingVersion.WSAddressing10)
			{
				return "http://www.w3.org/2005/08/addressing";
			}
			return "http://schemas.xmlsoap.org/ws/2004/08/addressing";
		}

		public const int ChunkSize = 4096;

		public const int MaxBufferedChunksDefault = 10;

		public const string ChunkElement = "chunk";

		public const string ChunkHeaderWrapperElement = "OriginalHeaders";

		public const string ChunkNs = "http://bam.nexon.com/chunking";

		public const string ChunkNumberHeader = "ChunkNumber";

		public const string ChunkingEndHeader = "ChunkingEnd";

		public const string MessageIdHeader = "MessageId";

		public const string ChunkingStartHeader = "ChunkingStart";

		public const string OriginalAction = "OriginalAction";

		public const string ChunkAction = "http://bam.nexon.com/chunkingAction";

		public const string WsAddressing10Ns = "http://www.w3.org/2005/08/addressing";

		public const string WsAddressingAugust2004Ns = "http://schemas.xmlsoap.org/ws/2004/08/addressing";

		public static readonly TimeSpan ChunkReceiveTimeout = TimeSpan.FromMinutes(3.0);
	}
}
