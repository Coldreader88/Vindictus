using System;
using System.Configuration;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;

namespace Nexon.Enterprise.ServiceFacade.Channels.Compression
{
	public class GZipMessageEncodingElement : BindingElementExtensionElement
	{
		public override Type BindingElementType
		{
			get
			{
				return typeof(GZipMessageEncodingBindingElement);
			}
		}

		[ConfigurationProperty("innerMessageEncoding", DefaultValue = "textMessageEncoding")]
		public string InnerMessageEncoding
		{
			get
			{
				return (string)base["innerMessageEncoding"];
			}
			set
			{
				base["innerMessageEncoding"] = value;
			}
		}

		public override void ApplyConfiguration(BindingElement bindingElement)
		{
			GZipMessageEncodingBindingElement gzipMessageEncodingBindingElement = (GZipMessageEncodingBindingElement)bindingElement;
			PropertyInformationCollection properties = base.ElementInformation.Properties;
			string innerMessageEncoding;
			if (properties["innerMessageEncoding"].ValueOrigin != PropertyValueOrigin.Default && (innerMessageEncoding = this.InnerMessageEncoding) != null)
			{
				if (innerMessageEncoding == "textMessageEncoding")
				{
					gzipMessageEncodingBindingElement.InnerMessageEncodingBindingElement = new TextMessageEncodingBindingElement();
					return;
				}
				if (!(innerMessageEncoding == "binaryMessageEncoding"))
				{
					return;
				}
				gzipMessageEncodingBindingElement.InnerMessageEncodingBindingElement = new BinaryMessageEncodingBindingElement();
			}
		}

		protected override BindingElement CreateBindingElement()
		{
			GZipMessageEncodingBindingElement gzipMessageEncodingBindingElement = new GZipMessageEncodingBindingElement();
			this.ApplyConfiguration(gzipMessageEncodingBindingElement);
			return gzipMessageEncodingBindingElement;
		}
	}
}
