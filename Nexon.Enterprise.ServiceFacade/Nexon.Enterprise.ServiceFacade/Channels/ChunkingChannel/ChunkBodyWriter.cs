using System;
using System.ServiceModel.Channels;
using System.Xml;

namespace Nexon.Enterprise.ServiceFacade.Channels.ChunkingChannel
{
	internal class ChunkBodyWriter : BodyWriter
	{
		protected override void OnWriteBodyContents(XmlDictionaryWriter writer)
		{
			this.writeBodyCallback(writer, this.state);
		}

		internal ChunkBodyWriter(ChunkBodyWriter.WriteBody writeBodyCallback, object state) : base(true)
		{
			this.writeBodyCallback = writeBodyCallback;
			this.state = state;
		}

		private ChunkBodyWriter.WriteBody writeBodyCallback;

		private object state;

		internal delegate void WriteBody(XmlDictionaryWriter writer, object state);
	}
}
