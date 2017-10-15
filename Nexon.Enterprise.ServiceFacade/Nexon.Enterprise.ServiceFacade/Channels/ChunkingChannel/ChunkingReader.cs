using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Xml;

namespace Nexon.Enterprise.ServiceFacade.Channels.ChunkingChannel
{
	internal class ChunkingReader : XmlDictionaryReader
	{
		internal ChunkingReader(Message startMessage, int maxBufferedChunks, TimeoutHelper receiveTimeout)
		{
			this.innerReader = startMessage.GetReaderAtBodyContents();
			this.lockobject = new object();
			this.messageId = ChunkingUtils.GetMessageHeader<Guid>(startMessage, "MessageId", "http://bam.nexon.com/chunking");
			this.bufferedChunks = new SynchronizedQueue<Message>(maxBufferedChunks);
			this.receiveTimeout = receiveTimeout;
			this.nextChunkNum = 1;
		}

		public override int ReadContentAsBase64(byte[] buffer, int index, int count)
		{
			int num = this.innerReader.ReadContentAsBase64(buffer, index, count);
			if (num != 0)
			{
				return num;
			}
			this.GetReaderFromNextChunk(this.receiveTimeout);
			if (this.isLastChunk)
			{
				return 0;
			}
			num = this.innerReader.ReadContentAsBase64(buffer, index, count);
			if (num == 0)
			{
				throw new CommunicationException("Received chunk contains no data");
			}
			return num;
		}

		public override XmlNodeType NodeType
		{
			get
			{
				if (this.innerReader.NodeType == XmlNodeType.EndElement && !this.isLastChunk)
				{
					this.GetReaderFromNextChunk(this.receiveTimeout);
					return this.innerReader.NodeType;
				}
				return this.innerReader.NodeType;
			}
		}

		public override bool Read()
		{
			return this.innerReader.NodeType != XmlNodeType.None && this.innerReader.Read();
		}

		internal void AddMessage(Message message)
		{
			if (!this.bufferedChunks.TryEnqueue(this.receiveTimeout, message))
			{
				QuotaExceededException innerException = new QuotaExceededException(string.Format("Number of buffered chunks exceeded MaxBufferedChunks setting which is {0}. Consider increasing the setting or processing received data faster", this.bufferedChunks.MaxLength));
				throw new CommunicationException("Quota error while receiving message", innerException);
			}
		}

		internal XmlDictionaryReader InnerReader
		{
			set
			{
				this.innerReader = value;
			}
		}

		internal bool IsLastChunk
		{
			set
			{
				this.isLastChunk = true;
			}
		}

		private void GetReaderFromNextChunk(TimeoutHelper timeouthelper)
		{
			Message message = null;
			if (this.bufferedChunks.TryDequeue(timeouthelper, out message))
			{
				int header = message.Headers.GetHeader<int>("ChunkNumber", "http://bam.nexon.com/chunking");
				Console.WriteLine(" < Received chunk {0} of message {1}", header, this.messageId);
				if (header != this.nextChunkNum)
				{
					throw new CommunicationException(string.Format("Received chunk number {0} but expected chunk number {1}", header, this.nextChunkNum));
				}
				this.isLastChunk = (message.Headers.FindHeader("ChunkingEnd", "http://bam.nexon.com/chunking") > -1);
				this.innerReader = message.GetReaderAtBodyContents();
				if (this.isLastChunk)
				{
					this.innerReader.ReadStartElement();
					this.innerReader.ReadStartElement();
				}
				else
				{
					this.innerReader.ReadStartElement("chunk", "http://bam.nexon.com/chunking");
				}
				lock (this.lockobject)
				{
					this.nextChunkNum++;
					return;
				}
			}
			throw new TimeoutException(string.Format("ChunkingReader timed out while waiting for chunk message number {0}", this.nextChunkNum));
		}

		public override int ReadValueChunk(char[] buffer, int index, int count)
		{
			throw new NotImplementedException("Operation not implemented");
		}

		public override byte[] ReadContentAsBase64()
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public override int AttributeCount
		{
			get
			{
				return this.innerReader.AttributeCount;
			}
		}

		public override string BaseURI
		{
			get
			{
				return this.innerReader.BaseURI;
			}
		}

		public override void Close()
		{
			this.innerReader.Close();
		}

		public override int Depth
		{
			get
			{
				return this.innerReader.Depth;
			}
		}

		public override bool EOF
		{
			get
			{
				return this.innerReader.EOF;
			}
		}

		public override string GetAttribute(int i)
		{
			return this.innerReader.GetAttribute(i);
		}

		public override string GetAttribute(string name, string namespaceURI)
		{
			return this.innerReader.GetAttribute(name, namespaceURI);
		}

		public override string GetAttribute(string name)
		{
			return this.innerReader.GetAttribute(name);
		}

		public override bool HasValue
		{
			get
			{
				return this.innerReader.HasValue;
			}
		}

		public override bool IsEmptyElement
		{
			get
			{
				return this.innerReader.IsEmptyElement;
			}
		}

		public override string LocalName
		{
			get
			{
				return this.innerReader.LocalName;
			}
		}

		public override string LookupNamespace(string prefix)
		{
			return this.innerReader.LookupNamespace(prefix);
		}

		public override bool MoveToAttribute(string name, string ns)
		{
			return this.innerReader.MoveToAttribute(name, ns);
		}

		public override bool MoveToAttribute(string name)
		{
			return this.innerReader.MoveToAttribute(name);
		}

		public override bool MoveToElement()
		{
			return this.innerReader.MoveToElement();
		}

		public override bool MoveToFirstAttribute()
		{
			return this.innerReader.MoveToFirstAttribute();
		}

		public override bool MoveToNextAttribute()
		{
			return this.innerReader.MoveToNextAttribute();
		}

		public override XmlNameTable NameTable
		{
			get
			{
				return this.innerReader.NameTable;
			}
		}

		public override string NamespaceURI
		{
			get
			{
				return this.innerReader.NamespaceURI;
			}
		}

		public override string Prefix
		{
			get
			{
				return this.innerReader.Prefix;
			}
		}

		public override void ReadStartElement(string localname, string ns)
		{
			this.innerReader.ReadStartElement(localname, ns);
		}

		public override bool ReadAttributeValue()
		{
			return this.innerReader.ReadAttributeValue();
		}

		public override ReadState ReadState
		{
			get
			{
				return this.innerReader.ReadState;
			}
		}

		public override void ResolveEntity()
		{
			this.innerReader.ResolveEntity();
		}

		public override string Value
		{
			get
			{
				return this.innerReader.Value;
			}
		}

		public override bool CanReadValueChunk
		{
			get
			{
				return false;
			}
		}

		public override void EndCanonicalization()
		{
			throw new NotImplementedException("not implemented");
		}

		public override string GetAttribute(XmlDictionaryString localName, XmlDictionaryString namespaceUri)
		{
			throw new NotImplementedException("not implemented");
		}

		public override int IndexOfLocalName(string[] localNames, string namespaceUri)
		{
			throw new NotImplementedException("not implemented");
		}

		public override int IndexOfLocalName(XmlDictionaryString[] localNames, XmlDictionaryString namespaceUri)
		{
			throw new NotImplementedException("not implemented");
		}

		public override bool IsLocalName(string localName)
		{
			throw new NotImplementedException("not implemented");
		}

		public override bool IsLocalName(XmlDictionaryString localName)
		{
			throw new NotImplementedException("not implemented");
		}

		public override bool IsNamespaceUri(string namespaceUri)
		{
			throw new NotImplementedException("not implemented");
		}

		public override bool IsNamespaceUri(XmlDictionaryString namespaceUri)
		{
			throw new NotImplementedException("not implemented");
		}

		public override bool IsStartArray(out Type type)
		{
			return this.innerReader.IsStartArray(out type);
		}

		public override bool IsStartElement(XmlDictionaryString localName, XmlDictionaryString namespaceUri)
		{
			throw new NotImplementedException("not implemented");
		}

		protected new bool IsTextNode(XmlNodeType nodeType)
		{
			throw new NotImplementedException("not implemented");
		}

		public override void MoveToStartElement()
		{
			throw new NotImplementedException("not implemented");
		}

		public override void MoveToStartElement(string name)
		{
			throw new NotImplementedException("not implemented");
		}

		public override void MoveToStartElement(string localName, string namespaceUri)
		{
			throw new NotImplementedException("not implemented");
		}

		public override void MoveToStartElement(XmlDictionaryString localName, XmlDictionaryString namespaceUri)
		{
			throw new NotImplementedException("not implemented");
		}

		public override int ReadArray(string localName, string namespaceUri, bool[] array, int offset, int count)
		{
			throw new NotImplementedException("not implemented");
		}

		public override int ReadArray(string localName, string namespaceUri, DateTime[] array, int offset, int count)
		{
			throw new NotImplementedException("not implemented");
		}

		public override int ReadArray(string localName, string namespaceUri, decimal[] array, int offset, int count)
		{
			throw new NotImplementedException("not implemented");
		}

		public override int ReadArray(string localName, string namespaceUri, double[] array, int offset, int count)
		{
			throw new NotImplementedException("not implemented");
		}

		public override int ReadArray(string localName, string namespaceUri, float[] array, int offset, int count)
		{
			throw new NotImplementedException("not implemented");
		}

		public override int ReadArray(string localName, string namespaceUri, Guid[] array, int offset, int count)
		{
			throw new NotImplementedException("not implemented");
		}

		public override int ReadArray(string localName, string namespaceUri, int[] array, int offset, int count)
		{
			throw new NotImplementedException("not implemented");
		}

		public override int ReadArray(string localName, string namespaceUri, long[] array, int offset, int count)
		{
			throw new NotImplementedException("not implemented");
		}

		public override int ReadArray(string localName, string namespaceUri, short[] array, int offset, int count)
		{
			throw new NotImplementedException("not implemented");
		}

		public override int ReadArray(string localName, string namespaceUri, TimeSpan[] array, int offset, int count)
		{
			throw new NotImplementedException("not implemented");
		}

		public override int ReadArray(XmlDictionaryString localName, XmlDictionaryString namespaceUri, bool[] array, int offset, int count)
		{
			throw new NotImplementedException("not implemented");
		}

		public override int ReadArray(XmlDictionaryString localName, XmlDictionaryString namespaceUri, DateTime[] array, int offset, int count)
		{
			throw new NotImplementedException("not implemented");
		}

		public override int ReadArray(XmlDictionaryString localName, XmlDictionaryString namespaceUri, decimal[] array, int offset, int count)
		{
			throw new NotImplementedException("not implemented");
		}

		public override int ReadArray(XmlDictionaryString localName, XmlDictionaryString namespaceUri, double[] array, int offset, int count)
		{
			throw new NotImplementedException("not implemented");
		}

		public override int ReadArray(XmlDictionaryString localName, XmlDictionaryString namespaceUri, float[] array, int offset, int count)
		{
			throw new NotImplementedException("not implemented");
		}

		public override int ReadArray(XmlDictionaryString localName, XmlDictionaryString namespaceUri, Guid[] array, int offset, int count)
		{
			throw new NotImplementedException("not implemented");
		}

		public override int ReadArray(XmlDictionaryString localName, XmlDictionaryString namespaceUri, int[] array, int offset, int count)
		{
			throw new NotImplementedException("not implemented");
		}

		public override int ReadArray(XmlDictionaryString localName, XmlDictionaryString namespaceUri, long[] array, int offset, int count)
		{
			throw new NotImplementedException("not implemented");
		}

		public override int ReadArray(XmlDictionaryString localName, XmlDictionaryString namespaceUri, short[] array, int offset, int count)
		{
			throw new NotImplementedException("not implemented");
		}

		public override int ReadArray(XmlDictionaryString localName, XmlDictionaryString namespaceUri, TimeSpan[] array, int offset, int count)
		{
			throw new NotImplementedException("not implemented");
		}

		public override bool[] ReadBooleanArray(string localName, string namespaceUri)
		{
			throw new NotImplementedException("not implemented");
		}

		public override bool[] ReadBooleanArray(XmlDictionaryString localName, XmlDictionaryString namespaceUri)
		{
			throw new NotImplementedException("not implemented");
		}

		public override byte[] ReadContentAsBinHex()
		{
			throw new NotImplementedException("not implemented");
		}

		protected new byte[] ReadContentAsBinHex(int maxByteArrayContentLength)
		{
			throw new NotImplementedException("not implemented");
		}

		public override int ReadContentAsChars(char[] chars, int offset, int count)
		{
			throw new NotImplementedException("not implemented");
		}

		public override decimal ReadContentAsDecimal()
		{
			throw new NotImplementedException("not implemented");
		}

		public override Guid ReadContentAsGuid()
		{
			throw new NotImplementedException("not implemented");
		}

		public override void ReadContentAsQualifiedName(out string localName, out string namespaceUri)
		{
			throw new NotImplementedException("not implemented");
		}

		public override string ReadContentAsString()
		{
			throw new NotImplementedException("not implemented");
		}

		protected new string ReadContentAsString(int maxStringContentLength)
		{
			throw new NotImplementedException("not implemented");
		}

		public override string ReadContentAsString(string[] strings, out int index)
		{
			throw new NotImplementedException("not implemented");
		}

		public override string ReadContentAsString(XmlDictionaryString[] strings, out int index)
		{
			throw new NotImplementedException("not implemented");
		}

		public override TimeSpan ReadContentAsTimeSpan()
		{
			throw new NotImplementedException("not implemented");
		}

		public override UniqueId ReadContentAsUniqueId()
		{
			throw new NotImplementedException("not implemented");
		}

		public override DateTime[] ReadDateTimeArray(string localName, string namespaceUri)
		{
			throw new NotImplementedException("not implemented");
		}

		public override DateTime[] ReadDateTimeArray(XmlDictionaryString localName, XmlDictionaryString namespaceUri)
		{
			throw new NotImplementedException("not implemented");
		}

		public override decimal[] ReadDecimalArray(string localName, string namespaceUri)
		{
			throw new NotImplementedException("not implemented");
		}

		public override decimal[] ReadDecimalArray(XmlDictionaryString localName, XmlDictionaryString namespaceUri)
		{
			throw new NotImplementedException("not implemented");
		}

		public override double[] ReadDoubleArray(string localName, string namespaceUri)
		{
			throw new NotImplementedException("not implemented");
		}

		public override double[] ReadDoubleArray(XmlDictionaryString localName, XmlDictionaryString namespaceUri)
		{
			throw new NotImplementedException("not implemented");
		}

		public override byte[] ReadElementContentAsBase64()
		{
			throw new NotImplementedException("not implemented");
		}

		public override byte[] ReadElementContentAsBinHex()
		{
			throw new NotImplementedException("not implemented");
		}

		public override bool ReadElementContentAsBoolean()
		{
			throw new NotImplementedException("not implemented");
		}

		public override DateTime ReadElementContentAsDateTime()
		{
			throw new NotImplementedException("not implemented");
		}

		public override decimal ReadElementContentAsDecimal()
		{
			throw new NotImplementedException("not implemented");
		}

		public override double ReadElementContentAsDouble()
		{
			throw new NotImplementedException("not implemented");
		}

		public override Guid ReadElementContentAsGuid()
		{
			throw new NotImplementedException("not implemented");
		}

		public override int ReadElementContentAsInt()
		{
			throw new NotImplementedException("not implemented");
		}

		public override long ReadElementContentAsLong()
		{
			throw new NotImplementedException("not implemented");
		}

		public override string ReadElementContentAsString()
		{
			throw new NotImplementedException("not implemented");
		}

		public override TimeSpan ReadElementContentAsTimeSpan()
		{
			throw new NotImplementedException("not implemented");
		}

		public override UniqueId ReadElementContentAsUniqueId()
		{
			throw new NotImplementedException("not implemented");
		}

		public override void ReadFullStartElement()
		{
			throw new NotImplementedException("not implemented");
		}

		public override void ReadFullStartElement(string name)
		{
			throw new NotImplementedException("not implemented");
		}

		public override void ReadFullStartElement(string localName, string namespaceUri)
		{
			throw new NotImplementedException("not implemented");
		}

		public override void ReadFullStartElement(XmlDictionaryString localName, XmlDictionaryString namespaceUri)
		{
			throw new NotImplementedException("not implemented");
		}

		public override Guid[] ReadGuidArray(string localName, string namespaceUri)
		{
			throw new NotImplementedException("not implemented");
		}

		public override Guid[] ReadGuidArray(XmlDictionaryString localName, XmlDictionaryString namespaceUri)
		{
			throw new NotImplementedException("not implemented");
		}

		public override short[] ReadInt16Array(string localName, string namespaceUri)
		{
			throw new NotImplementedException("not implemented");
		}

		public override short[] ReadInt16Array(XmlDictionaryString localName, XmlDictionaryString namespaceUri)
		{
			throw new NotImplementedException("not implemented");
		}

		public override int[] ReadInt32Array(string localName, string namespaceUri)
		{
			throw new NotImplementedException("not implemented");
		}

		public override int[] ReadInt32Array(XmlDictionaryString localName, XmlDictionaryString namespaceUri)
		{
			throw new NotImplementedException("not implemented");
		}

		public override long[] ReadInt64Array(string localName, string namespaceUri)
		{
			throw new NotImplementedException("not implemented");
		}

		public override long[] ReadInt64Array(XmlDictionaryString localName, XmlDictionaryString namespaceUri)
		{
			throw new NotImplementedException("not implemented");
		}

		public override float[] ReadSingleArray(string localName, string namespaceUri)
		{
			throw new NotImplementedException("not implemented");
		}

		public override float[] ReadSingleArray(XmlDictionaryString localName, XmlDictionaryString namespaceUri)
		{
			throw new NotImplementedException("not implemented");
		}

		public override void ReadStartElement(XmlDictionaryString localName, XmlDictionaryString namespaceUri)
		{
			throw new NotImplementedException("not implemented");
		}

		public override string ReadString()
		{
			throw new NotImplementedException("not implemented");
		}

		protected new string ReadString(int maxStringContentLength)
		{
			throw new NotImplementedException("not implemented");
		}

		public override TimeSpan[] ReadTimeSpanArray(string localName, string namespaceUri)
		{
			throw new NotImplementedException("not implemented");
		}

		public override TimeSpan[] ReadTimeSpanArray(XmlDictionaryString localName, XmlDictionaryString namespaceUri)
		{
			throw new NotImplementedException("not implemented");
		}

		public override int ReadValueAsBase64(byte[] buffer, int offset, int count)
		{
			throw new NotImplementedException("not implemented");
		}

		public override bool TryGetArrayLength(out int count)
		{
			throw new NotImplementedException("not implemented");
		}

		public override bool TryGetBase64ContentLength(out int length)
		{
			throw new NotImplementedException("not implemented");
		}

		public override bool TryGetLocalNameAsDictionaryString(out XmlDictionaryString localName)
		{
			return this.innerReader.TryGetLocalNameAsDictionaryString(out localName);
		}

		public override bool TryGetNamespaceUriAsDictionaryString(out XmlDictionaryString namespaceUri)
		{
			throw new NotImplementedException("not implemented");
		}

		private XmlDictionaryReader innerReader;

		private bool isLastChunk;

		private SynchronizedQueue<Message> bufferedChunks;

		private int nextChunkNum;

		private object lockobject;

		private Guid messageId;

		private TimeoutHelper receiveTimeout;
	}
}
