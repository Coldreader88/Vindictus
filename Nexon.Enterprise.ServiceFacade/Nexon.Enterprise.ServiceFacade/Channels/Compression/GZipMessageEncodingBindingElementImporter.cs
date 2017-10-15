using System;
using System.Collections.Generic;
using System.ServiceModel.Description;
using System.Xml;

namespace Nexon.Enterprise.ServiceFacade.Channels.Compression
{
	public class GZipMessageEncodingBindingElementImporter : IPolicyImportExtension
	{
		void IPolicyImportExtension.ImportPolicy(MetadataImporter importer, PolicyConversionContext context)
		{
			if (importer == null)
			{
				throw new ArgumentNullException("importer");
			}
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			ICollection<XmlElement> bindingAssertions = context.GetBindingAssertions();
			foreach (XmlElement xmlElement in bindingAssertions)
			{
				if (xmlElement.NamespaceURI == "http://schemas.microsoft.com/ws/06/2004/mspolicy/netgzip1" && xmlElement.LocalName == "GZipEncoding")
				{
					bindingAssertions.Remove(xmlElement);
					context.BindingElements.Add(new GZipMessageEncodingBindingElement());
					break;
				}
			}
		}
	}
}
