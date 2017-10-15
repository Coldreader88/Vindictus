using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.Xml;

namespace Nexon.Enterprise.ServiceFacade.Channels.Text
{
	public class CustomTextMessageBindingElement : MessageEncodingBindingElement, IWsdlExportExtension
	{
		private CustomTextMessageBindingElement(CustomTextMessageBindingElement binding) : this(binding.Encoding, binding.MediaType, binding.MessageVersion)
		{
			this.readerQuotas = new XmlDictionaryReaderQuotas();
			binding.ReaderQuotas.CopyTo(this.readerQuotas);
		}

		public CustomTextMessageBindingElement(string encoding, string mediaType, MessageVersion msgVersion)
		{
			if (encoding == null)
			{
				throw new ArgumentNullException("encoding");
			}
			if (mediaType == null)
			{
				throw new ArgumentNullException("mediaType");
			}
			if (msgVersion == null)
			{
				throw new ArgumentNullException("msgVersion");
			}
			this.msgVersion = msgVersion;
			this.mediaType = mediaType;
			this.encoding = encoding;
			this.readerQuotas = new XmlDictionaryReaderQuotas();
		}

		public CustomTextMessageBindingElement(string encoding, string mediaType) : this(encoding, mediaType, MessageVersion.Soap11WSAddressing10)
		{
		}

		public CustomTextMessageBindingElement(string encoding) : this(encoding, "text/xml")
		{
		}

		public CustomTextMessageBindingElement() : this("UTF-8")
		{
		}

		public override MessageVersion MessageVersion
		{
			get
			{
				return this.msgVersion;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.msgVersion = value;
			}
		}

		public string MediaType
		{
			get
			{
				return this.mediaType;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.mediaType = value;
			}
		}

		public string Encoding
		{
			get
			{
				return this.encoding;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.encoding = value;
			}
		}

		public XmlDictionaryReaderQuotas ReaderQuotas
		{
			get
			{
				return this.readerQuotas;
			}
		}

		public override MessageEncoderFactory CreateMessageEncoderFactory()
		{
			return new CustomTextMessageEncoderFactory(this.MediaType, this.Encoding, this.MessageVersion);
		}

		public override BindingElement Clone()
		{
			return new CustomTextMessageBindingElement(this);
		}

		public override IChannelFactory<TChannel> BuildChannelFactory<TChannel>(BindingContext context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			context.BindingParameters.Add(this);
			return context.BuildInnerChannelFactory<TChannel>();
		}

		public override bool CanBuildChannelFactory<TChannel>(BindingContext context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			return context.CanBuildInnerChannelFactory<TChannel>();
		}

		public override IChannelListener<TChannel> BuildChannelListener<TChannel>(BindingContext context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			context.BindingParameters.Add(this);
			return context.BuildInnerChannelListener<TChannel>();
		}

		public override bool CanBuildChannelListener<TChannel>(BindingContext context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			context.BindingParameters.Add(this);
			return context.CanBuildInnerChannelListener<TChannel>();
		}

		public override T GetProperty<T>(BindingContext context)
		{
			if (typeof(T) == typeof(XmlDictionaryReaderQuotas))
			{
				return (T)((object)this.readerQuotas);
			}
			return base.GetProperty<T>(context);
		}

		void IWsdlExportExtension.ExportContract(WsdlExporter exporter, WsdlContractConversionContext context)
		{
		}

		void IWsdlExportExtension.ExportEndpoint(WsdlExporter exporter, WsdlEndpointConversionContext context)
		{
			((IWsdlExportExtension)new TextMessageEncodingBindingElement
			{
				MessageVersion = this.msgVersion
			}).ExportEndpoint(exporter, context);
		}

		private MessageVersion msgVersion;

		private string mediaType;

		private string encoding;

		private XmlDictionaryReaderQuotas readerQuotas;
	}
}
