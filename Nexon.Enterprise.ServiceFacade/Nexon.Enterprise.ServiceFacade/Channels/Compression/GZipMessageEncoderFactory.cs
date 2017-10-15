using System;
using System.IO;
using System.IO.Compression;
using System.ServiceModel.Channels;

namespace Nexon.Enterprise.ServiceFacade.Channels.Compression
{
	internal class GZipMessageEncoderFactory : MessageEncoderFactory
	{
		public GZipMessageEncoderFactory(MessageEncoderFactory messageEncoderFactory)
		{
			if (messageEncoderFactory == null)
			{
				throw new ArgumentNullException("messageEncoderFactory", "A valid message encoder factory must be passed to the GZipEncoder");
			}
			this.encoder = new GZipMessageEncoderFactory.GZipMessageEncoder(messageEncoderFactory.Encoder);
		}

		public override MessageEncoder Encoder
		{
			get
			{
				return this.encoder;
			}
		}

		public override MessageVersion MessageVersion
		{
			get
			{
				return this.encoder.MessageVersion;
			}
		}

		private MessageEncoder encoder;

		private class GZipMessageEncoder : MessageEncoder
		{
			internal GZipMessageEncoder(MessageEncoder messageEncoder)
			{
				if (messageEncoder == null)
				{
					throw new ArgumentNullException("messageEncoder", "A valid message encoder must be passed to the GZipEncoder");
				}
				this.innerEncoder = messageEncoder;
			}

			public override string ContentType
			{
				get
				{
					return GZipMessageEncoderFactory.GZipMessageEncoder.GZipContentType;
				}
			}

			public override string MediaType
			{
				get
				{
					return GZipMessageEncoderFactory.GZipMessageEncoder.GZipContentType;
				}
			}

			public override MessageVersion MessageVersion
			{
				get
				{
					return this.innerEncoder.MessageVersion;
				}
			}

			private static ArraySegment<byte> CompressBuffer(ArraySegment<byte> buffer, BufferManager bufferManager, int messageOffset)
			{
				MemoryStream memoryStream = new MemoryStream();
				memoryStream.Write(buffer.Array, 0, messageOffset);
				using (GZipStream gzipStream = new GZipStream(memoryStream, CompressionMode.Compress, true))
				{
					gzipStream.Write(buffer.Array, messageOffset, buffer.Count);
				}
				byte[] array = memoryStream.ToArray();
				byte[] array2 = bufferManager.TakeBuffer(array.Length);
				Array.Copy(array, 0, array2, 0, array.Length);
				bufferManager.ReturnBuffer(buffer.Array);
				ArraySegment<byte> result = new ArraySegment<byte>(array2, messageOffset, array2.Length - messageOffset);
				return result;
			}

			private static ArraySegment<byte> DecompressBuffer(ArraySegment<byte> buffer, BufferManager bufferManager)
			{
				MemoryStream stream = new MemoryStream(buffer.Array, buffer.Offset, buffer.Count - buffer.Offset);
				MemoryStream memoryStream = new MemoryStream();
				int num = 0;
				int num2 = 1024;
				byte[] buffer2 = bufferManager.TakeBuffer(num2);
				using (GZipStream gzipStream = new GZipStream(stream, CompressionMode.Decompress))
				{
					for (;;)
					{
						int num3 = gzipStream.Read(buffer2, 0, num2);
						if (num3 == 0)
						{
							break;
						}
						memoryStream.Write(buffer2, 0, num3);
						num += num3;
					}
				}
				bufferManager.ReturnBuffer(buffer2);
				byte[] array = memoryStream.ToArray();
				byte[] array2 = bufferManager.TakeBuffer(array.Length + buffer.Offset);
				Array.Copy(buffer.Array, 0, array2, 0, buffer.Offset);
				Array.Copy(array, 0, array2, buffer.Offset, array.Length);
				ArraySegment<byte> result = new ArraySegment<byte>(array2, buffer.Offset, array.Length);
				bufferManager.ReturnBuffer(buffer.Array);
				return result;
			}

			public override Message ReadMessage(ArraySegment<byte> buffer, BufferManager bufferManager, string contentType)
			{
				ArraySegment<byte> buffer2 = GZipMessageEncoderFactory.GZipMessageEncoder.DecompressBuffer(buffer, bufferManager);
				Message message = this.innerEncoder.ReadMessage(buffer2, bufferManager);
				message.Properties.Encoder = this;
				return message;
			}

			public override ArraySegment<byte> WriteMessage(Message message, int maxMessageSize, BufferManager bufferManager, int messageOffset)
			{
				ArraySegment<byte> buffer = this.innerEncoder.WriteMessage(message, maxMessageSize, bufferManager, messageOffset);
				return GZipMessageEncoderFactory.GZipMessageEncoder.CompressBuffer(buffer, bufferManager, messageOffset);
			}

			public override Message ReadMessage(Stream stream, int maxSizeOfHeaders, string contentType)
			{
				GZipStream stream2 = new GZipStream(stream, CompressionMode.Decompress, true);
				return this.innerEncoder.ReadMessage(stream2, maxSizeOfHeaders);
			}

			public override void WriteMessage(Message message, Stream stream)
			{
				using (GZipStream gzipStream = new GZipStream(stream, CompressionMode.Compress, true))
				{
					this.innerEncoder.WriteMessage(message, gzipStream);
				}
				stream.Flush();
			}

			private static string GZipContentType = "application/x-gzip";

			private MessageEncoder innerEncoder;
		}
	}
}
