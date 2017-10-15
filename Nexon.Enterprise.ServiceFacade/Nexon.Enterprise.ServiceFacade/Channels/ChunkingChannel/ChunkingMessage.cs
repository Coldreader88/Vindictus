using System;
using System.ServiceModel.Channels;
using System.Xml;

namespace Nexon.Enterprise.ServiceFacade.Channels.ChunkingChannel
{
	public class ChunkingMessage : Message
	{
		internal ChunkingMessage(MessageVersion version, string action, ChunkingReader reader, Guid messageId)
		{
			this.version = version;
			this.chunkReader = reader;
			this.properties = new MessageProperties();
			this.headers = new MessageHeaders(this.version);
			this.headers.Action = action;
			this.messageId = messageId;
		}

		protected override XmlDictionaryReader OnGetReaderAtBodyContents()
		{
			return this.chunkReader;
		}

		protected override void OnClose()
		{
			this.chunkReader.Close();
		}

		public override MessageHeaders Headers
		{
			get
			{
				return this.headers;
			}
		}

		protected override void OnWriteBodyContents(XmlDictionaryWriter writer)
		{
		}

		public override MessageProperties Properties
		{
			get
			{
				return this.properties;
			}
		}

		public override MessageVersion Version
		{
			get
			{
				return this.version;
			}
		}

		public Guid MessageId
		{
			get
			{
				return this.messageId;
			}
		}

		internal ChunkingReader UnderlyingReader
		{
			get
			{
				return this.chunkReader;
			}
		}

		private MessageVersion version;

		private ChunkingReader chunkReader;

		private MessageProperties properties;

		private MessageHeaders headers;

		private Guid messageId;
	}
}
