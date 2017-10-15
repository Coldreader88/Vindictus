using System;
using System.ServiceModel.Channels;
using System.Xml;

namespace Nexon.Enterprise.ServiceFacade.Channels.ChunkingChannel
{
	internal class ChunkingWriter : XmlDictionaryWriter
	{
		internal ChunkingWriter(Message originalMessage, TimeoutHelper chunkingTimeout, IOutputChannel outputChannel)
		{
			this.version = originalMessage.Version;
			this.originalMessage = originalMessage;
			this.chunkingTimeout = chunkingTimeout;
			this.outputChannel = outputChannel;
			this.startState = new StartChunkState();
			this.chunkNum = 1;
		}

		private void WriteChunkCallback(XmlDictionaryWriter writer, object state)
		{
			ChunkState chunkState = (ChunkState)state;
			writer.WriteStartElement("chunk", "http://bam.nexon.com/chunking");
			writer.WriteBase64(chunkState.Buffer, 0, chunkState.Count);
			writer.WriteEndElement();
		}

		private void SetStartMessageHeaders(Message message, Message chunk)
		{
			this.messageId = Guid.NewGuid();
			this.messageIdHeader = MessageHeader.CreateHeader("MessageId", "http://bam.nexon.com/chunking", this.messageId.ToString(), true);
			chunk.Headers.Add(this.messageIdHeader);
			MessageHeader header = MessageHeader.CreateHeader("ChunkingStart", "http://bam.nexon.com/chunking", null, true);
			chunk.Headers.Add(header);
			MessageHeaders headers = message.Headers;
			string addressingNamespace = ChunkingUtils.GetAddressingNamespace(message.Version);
			for (int i = 0; i < headers.Count; i++)
			{
				if (headers[i].Name == "Action" && headers[i].Namespace == addressingNamespace)
				{
					string action = message.Headers.Action;
					MessageHeader header2 = MessageHeader.CreateHeader("OriginalAction", "http://bam.nexon.com/chunking", action);
					chunk.Headers.Add(header2);
				}
				else
				{
					chunk.Headers.CopyHeaderFrom(headers, i);
				}
			}
		}

		private void CreateStartChunk()
		{
			ChunkBodyWriter body = new ChunkBodyWriter(new ChunkBodyWriter.WriteBody(this.WriteStartChunkCallback), this.startState);
			this.startMessage = Message.CreateMessage(this.version, "http://bam.nexon.com/chunkingAction", body);
			this.SetStartMessageHeaders(this.originalMessage, this.startMessage);
			this.outputChannel.Send(this.startMessage, this.chunkingTimeout.RemainingTime());
		}

		private void WriteStartChunkCallback(XmlDictionaryWriter writer, object state)
		{
			StartChunkState startChunkState = (StartChunkState)state;
			writer.WriteStartElement(startChunkState.OperationName, startChunkState.OperationNs);
			writer.WriteStartElement(startChunkState.ParamName, startChunkState.ParamNs);
			writer.WriteEndElement();
			writer.WriteEndElement();
		}

		public override void Close()
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public override void Flush()
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public override string LookupPrefix(string ns)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public override void WriteBase64(byte[] buffer, int index, int count)
		{
			if (this.chunkState == null)
			{
				this.chunkState = new ChunkState();
			}
			int i = this.chunkState.AppendBytes(buffer, index, count);
			if (this.chunkState.Count == 4096)
			{
				this.CreateChunkMessage();
			}
			int num = index + count - 1;
			while (i > 0)
			{
				int index2 = num - i + 1;
				i = this.chunkState.AppendBytes(buffer, index2, i);
				if (this.chunkState.Count == 4096)
				{
					this.CreateChunkMessage();
				}
			}
		}

		private void CreateChunkMessage()
		{
			ChunkBodyWriter body = new ChunkBodyWriter(new ChunkBodyWriter.WriteBody(this.WriteChunkCallback), this.chunkState);
			Message message = Message.CreateMessage(this.version, "http://bam.nexon.com/chunkingAction", body);
			message.Headers.Add(this.messageIdHeader);
			message.Headers.Add(MessageHeader.CreateHeader("ChunkNumber", "http://bam.nexon.com/chunking", this.chunkNum, true));
			this.outputChannel.Send(message, this.chunkingTimeout.RemainingTime());
			Console.WriteLine(" > Sent chunk {0} of message {1}", this.chunkNum, this.messageId);
			this.chunkNum++;
			this.chunkState = new ChunkState();
		}

		public override void WriteCData(string text)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public override void WriteCharEntity(char ch)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public override void WriteChars(char[] buffer, int index, int count)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public override void WriteComment(string text)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public override void WriteDocType(string name, string pubid, string sysid, string subset)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public override void WriteEndAttribute()
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public override void WriteEndDocument()
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public override void WriteEndElement()
		{
			if (this.currentElementName.Name == this.startState.ParamName && this.currentElementName.Namespace == this.startState.ParamNs)
			{
				this.currentElementName = new XmlQualifiedName(this.startState.OperationName, this.startState.OperationNs);
				return;
			}
			if (this.currentElementName.Name == this.startState.OperationName && this.currentElementName.Namespace == this.startState.OperationNs)
			{
				this.CreateChunkMessage();
				this.CreateEndChunk();
				return;
			}
			throw new InvalidOperationException("Chunking channel supports only messages with the format ...<soap:Body><operationElement><paramElement>data to be chunked</paramElement></operationElement></soap:Body>. Check that the service operation has one input parameter and/or one output parameter or return value.");
		}

		private void CreateEndChunk()
		{
			ChunkBodyWriter body = new ChunkBodyWriter(new ChunkBodyWriter.WriteBody(this.WriteStartChunkCallback), this.startState);
			Message message = Message.CreateMessage(this.version, "http://bam.nexon.com/chunkingAction", body);
			message.Headers.Add(this.messageIdHeader);
			message.Headers.Add(MessageHeader.CreateHeader("ChunkingEnd", "http://bam.nexon.com/chunking", null, true));
			message.Headers.Add(MessageHeader.CreateHeader("ChunkNumber", "http://bam.nexon.com/chunking", this.chunkNum, true));
			this.outputChannel.Send(message, this.chunkingTimeout.RemainingTime());
			Console.WriteLine(" > Sent chunk {0} of message {1}", this.chunkNum, this.messageId);
		}

		public override void WriteEntityRef(string name)
		{
			throw new NotImplementedException();
		}

		public override void WriteFullEndElement()
		{
			throw new NotImplementedException();
		}

		public override void WriteProcessingInstruction(string name, string text)
		{
			throw new NotImplementedException();
		}

		public override void WriteRaw(string data)
		{
			throw new NotImplementedException();
		}

		public override void WriteRaw(char[] buffer, int index, int count)
		{
			throw new NotImplementedException();
		}

		public override void WriteStartAttribute(string prefix, string localName, string ns)
		{
			throw new NotImplementedException();
		}

		public override void WriteStartDocument(bool standalone)
		{
			throw new NotImplementedException();
		}

		public override void WriteStartDocument()
		{
			throw new NotImplementedException();
		}

		public override void WriteStartElement(string prefix, string localName, string ns)
		{
			if (this.startState.OperationName == null)
			{
				this.startState.OperationName = localName;
				this.startState.OperationNs = ns;
				this.currentElementName = new XmlQualifiedName(localName, ns);
				return;
			}
			if (this.startState.ParamName == null)
			{
				this.startState.ParamName = localName;
				this.startState.ParamNs = ns;
				this.CreateStartChunk();
				this.currentElementName = new XmlQualifiedName(localName, ns);
				return;
			}
			throw new InvalidOperationException("Chunking channel supports only messages with the format ...<soap:Body><operationElement><paramElement>data to be chunked</paramElement></operationElement></soap:Body>. Check that the service operation has one input parameter and/or one output parameter or return value.");
		}

		public override WriteState WriteState
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public override void WriteString(string text)
		{
			throw new NotImplementedException();
		}

		public override void WriteSurrogateCharEntity(char lowChar, char highChar)
		{
			throw new NotImplementedException();
		}

		public override void WriteWhitespace(string ws)
		{
			throw new NotImplementedException();
		}

		private StartChunkState startState;

		private ChunkState chunkState;

		private Message startMessage;

		private MessageVersion version;

		private Message originalMessage;

		private MessageHeader messageIdHeader;

		private Guid messageId;

		private int chunkNum;

		private XmlQualifiedName currentElementName;

		private TimeoutHelper chunkingTimeout;

		private IOutputChannel outputChannel;
	}
}
