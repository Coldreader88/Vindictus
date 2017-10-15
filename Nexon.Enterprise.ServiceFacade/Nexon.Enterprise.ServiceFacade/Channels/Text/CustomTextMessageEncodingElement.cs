using System;
using System.ComponentModel;
using System.Configuration;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;
using System.Xml;

namespace Nexon.Enterprise.ServiceFacade.Channels.Text
{
	public class CustomTextMessageEncodingElement : BindingElementExtensionElement
	{
		public override void ApplyConfiguration(BindingElement bindingElement)
		{
			base.ApplyConfiguration(bindingElement);
			CustomTextMessageBindingElement customTextMessageBindingElement = (CustomTextMessageBindingElement)bindingElement;
			customTextMessageBindingElement.MessageVersion = this.MessageVersion;
			customTextMessageBindingElement.MediaType = this.MediaType;
			customTextMessageBindingElement.Encoding = this.Encoding;
			this.ApplyConfiguration(customTextMessageBindingElement.ReaderQuotas);
		}

		private void ApplyConfiguration(XmlDictionaryReaderQuotas readerQuotas)
		{
			if (readerQuotas == null)
			{
				throw new ArgumentNullException("readerQuotas");
			}
			if (this.ReaderQuotasElement.MaxDepth != 0)
			{
				readerQuotas.MaxDepth = this.ReaderQuotasElement.MaxDepth;
			}
			if (this.ReaderQuotasElement.MaxStringContentLength != 0)
			{
				readerQuotas.MaxStringContentLength = this.ReaderQuotasElement.MaxStringContentLength;
			}
			if (this.ReaderQuotasElement.MaxArrayLength != 0)
			{
				readerQuotas.MaxArrayLength = this.ReaderQuotasElement.MaxArrayLength;
			}
			if (this.ReaderQuotasElement.MaxBytesPerRead != 0)
			{
				readerQuotas.MaxBytesPerRead = this.ReaderQuotasElement.MaxBytesPerRead;
			}
			if (this.ReaderQuotasElement.MaxNameTableCharCount != 0)
			{
				readerQuotas.MaxNameTableCharCount = this.ReaderQuotasElement.MaxNameTableCharCount;
			}
		}

		public override Type BindingElementType
		{
			get
			{
				return typeof(CustomTextMessageBindingElement);
			}
		}

		protected override BindingElement CreateBindingElement()
		{
			CustomTextMessageBindingElement customTextMessageBindingElement = new CustomTextMessageBindingElement();
			this.ApplyConfiguration(customTextMessageBindingElement);
			return customTextMessageBindingElement;
		}

		[TypeConverter(typeof(MessageVersionConverter))]
		[ConfigurationProperty("messageVersion", DefaultValue = "Soap12WSAddressing10")]
		public MessageVersion MessageVersion
		{
			get
			{
				return (MessageVersion)base["messageVersion"];
			}
			set
			{
				base["messageVersion"] = value;
			}
		}

		[ConfigurationProperty("mediaType", DefaultValue = "text/xml")]
		public string MediaType
		{
			get
			{
				return (string)base["mediaType"];
			}
			set
			{
				base["mediaType"] = value;
			}
		}

		[ConfigurationProperty("encoding", DefaultValue = "utf-8")]
		public string Encoding
		{
			get
			{
				return (string)base["encoding"];
			}
			set
			{
				base["encoding"] = value;
			}
		}

		[ConfigurationProperty("readerQuotas")]
		public XmlDictionaryReaderQuotasElement ReaderQuotasElement
		{
			get
			{
				return (XmlDictionaryReaderQuotasElement)base["readerQuotas"];
			}
		}
	}
}
