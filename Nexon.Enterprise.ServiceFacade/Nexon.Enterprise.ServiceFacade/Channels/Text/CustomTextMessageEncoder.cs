using System;
using System.IO;
using System.ServiceModel.Channels;
using System.Text;
using System.Xml;

namespace Nexon.Enterprise.ServiceFacade.Channels.Text
{
	public class CustomTextMessageEncoder : MessageEncoder
	{
		public CustomTextMessageEncoder(CustomTextMessageEncoderFactory factory)
		{
			this.factory = factory;
			this.writerSettings = new XmlWriterSettings();
			this.writerSettings.Encoding = Encoding.GetEncoding(factory.CharSet);
			this.contentType = string.Format("{0}; charset={1}", this.factory.MediaType, this.writerSettings.Encoding.HeaderName);
		}

		public override string ContentType
		{
			get
			{
				return this.contentType;
			}
		}

		public override string MediaType
		{
			get
			{
				return this.factory.MediaType;
			}
		}

		public override MessageVersion MessageVersion
		{
			get
			{
				return this.factory.MessageVersion;
			}
		}

		public override Message ReadMessage(ArraySegment<byte> buffer, BufferManager bufferManager, string contentType)
		{
			byte[] array = new byte[buffer.Count];
			Array.Copy(buffer.Array, buffer.Offset, array, 0, array.Length);
			bufferManager.ReturnBuffer(buffer.Array);
			MemoryStream stream = new MemoryStream(array);
			return base.ReadMessage(stream, int.MaxValue);
		}

		public override Message ReadMessage(Stream stream, int maxSizeOfHeaders, string contentType)
		{
			XmlReader envelopeReader = XmlReader.Create(stream);
			return Message.CreateMessage(envelopeReader, maxSizeOfHeaders, this.MessageVersion);
		}

		public override ArraySegment<byte> WriteMessage(Message message, int maxMessageSize, BufferManager bufferManager, int messageOffset)
		{
			MemoryStream memoryStream = new MemoryStream();
			XmlWriter xmlWriter = XmlWriter.Create(memoryStream, this.writerSettings);
			message.WriteMessage(xmlWriter);
			xmlWriter.Close();
			byte[] buffer = memoryStream.GetBuffer();
			int num = (int)memoryStream.Position;
			memoryStream.Close();
			int bufferSize = num + messageOffset;
			byte[] array = bufferManager.TakeBuffer(bufferSize);
			Array.Copy(buffer, 0, array, messageOffset, num);
			ArraySegment<byte> result = new ArraySegment<byte>(array, messageOffset, num);
			return result;
		}

		public override void WriteMessage(Message message, Stream stream)
		{
			XmlWriter xmlWriter = XmlWriter.Create(stream, this.writerSettings);
			message.WriteMessage(xmlWriter);
			xmlWriter.Close();
		}

		private CustomTextMessageEncoderFactory factory;

		private XmlWriterSettings writerSettings;

		private string contentType;
	}
}
