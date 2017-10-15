using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.Xml;

namespace Nexon.Enterprise.ServiceFacade.Channels.Compression
{
	public sealed class GZipMessageEncodingBindingElement : MessageEncodingBindingElement, IPolicyExportExtension
	{
		public GZipMessageEncodingBindingElement() : this(new TextMessageEncodingBindingElement())
		{
		}

		public GZipMessageEncodingBindingElement(MessageEncodingBindingElement messageEncoderBindingElement)
		{
			this.innerBindingElement = messageEncoderBindingElement;
		}

		public MessageEncodingBindingElement InnerMessageEncodingBindingElement
		{
			get
			{
				return this.innerBindingElement;
			}
			set
			{
				this.innerBindingElement = value;
			}
		}

		public override MessageEncoderFactory CreateMessageEncoderFactory()
		{
			return new GZipMessageEncoderFactory(this.innerBindingElement.CreateMessageEncoderFactory());
		}

		public override MessageVersion MessageVersion
		{
			get
			{
				return this.innerBindingElement.MessageVersion;
			}
			set
			{
				this.innerBindingElement.MessageVersion = value;
			}
		}

		public override BindingElement Clone()
		{
			return new GZipMessageEncodingBindingElement(this.innerBindingElement);
		}

		public override T GetProperty<T>(BindingContext context)
		{
			if (typeof(T) == typeof(XmlDictionaryReaderQuotas))
			{
				return this.innerBindingElement.GetProperty<T>(context);
			}
			return base.GetProperty<T>(context);
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

		void IPolicyExportExtension.ExportPolicy(MetadataExporter exporter, PolicyConversionContext policyContext)
		{
			if (policyContext == null)
			{
				throw new ArgumentNullException("policyContext");
			}
			XmlDocument xmlDocument = new XmlDocument();
			policyContext.GetBindingAssertions().Add(xmlDocument.CreateElement("gzip", "GZipEncoding", "http://schemas.microsoft.com/ws/06/2004/mspolicy/netgzip1"));
		}

		private MessageEncodingBindingElement innerBindingElement;
	}
}
