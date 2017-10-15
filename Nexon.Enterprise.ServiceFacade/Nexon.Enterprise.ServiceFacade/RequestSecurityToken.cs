using System;
using System.IdentityModel.Tokens;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Security.Tokens;
using System.Xml;

namespace Nexon.Enterprise.ServiceFacade
{
	public class RequestSecurityToken : RequestSecurityTokenBase
	{
		public RequestSecurityToken() : this(string.Empty, string.Empty, string.Empty, 0, "http://schemas.xmlsoap.org/ws/2005/02/trust/SymmetricKey", null, null, null)
		{
		}

		public RequestSecurityToken(string context, string tokenType, string requestType, int keySize, string keyType, SecurityToken proofKey, SecurityToken entropy, EndpointAddress appliesTo) : base(context, tokenType, keySize, appliesTo)
		{
			this.keyType = keyType;
			this.proofKey = proofKey;
			this.requestType = requestType;
			this.requestorEntropy = entropy;
		}

		public string RequestType
		{
			get
			{
				return this.requestType;
			}
			set
			{
				this.requestType = value;
			}
		}

		public string KeyType
		{
			get
			{
				return this.keyType;
			}
			set
			{
				this.keyType = value;
			}
		}

		public SecurityToken ProofKey
		{
			get
			{
				return this.proofKey;
			}
			set
			{
				this.proofKey = value;
			}
		}

		public SecurityToken RequestorEntropy
		{
			get
			{
				return this.requestorEntropy;
			}
			set
			{
				this.requestorEntropy = value;
			}
		}

		public bool IsProofKeyAsymmetric()
		{
			return "http://schemas.xmlsoap.org/ws/2005/02/trust/PublicKey" == this.keyType;
		}

		public static RequestSecurityToken CreateFrom(XmlReader xr)
		{
			return RequestSecurityToken.ProcessRequestSecurityTokenElement(xr);
		}

		protected override void OnWriteBodyContents(XmlDictionaryWriter writer)
		{
			writer.WriteStartElement("RequestSecurityToken", "http://schemas.xmlsoap.org/ws/2005/02/trust");
			if (base.TokenType != null && base.TokenType.Length > 0)
			{
				writer.WriteStartElement("TokenType", "http://schemas.xmlsoap.org/ws/2005/02/trust");
				writer.WriteString(base.TokenType);
				writer.WriteEndElement();
			}
			if (this.requestType != null && this.requestType.Length > 0)
			{
				writer.WriteStartElement("RequestType", "http://schemas.xmlsoap.org/ws/2005/02/trust");
				writer.WriteString(this.requestType);
				writer.WriteEndElement();
			}
			if (base.AppliesTo != null)
			{
				writer.WriteStartElement("AppliesTo", "http://schemas.xmlsoap.org/ws/2004/09/policy");
				base.AppliesTo.WriteTo(AddressingVersion.WSAddressing10, writer);
				writer.WriteEndElement();
			}
			if (this.requestorEntropy != null)
			{
				writer.WriteStartElement("Entropy", "http://schemas.xmlsoap.org/ws/2005/02/trust");
				BinarySecretSecurityToken binarySecretSecurityToken = this.requestorEntropy as BinarySecretSecurityToken;
				if (binarySecretSecurityToken != null)
				{
					writer.WriteStartElement("BinarySecret", "http://schemas.xmlsoap.org/ws/2005/02/trust");
					byte[] keyBytes = binarySecretSecurityToken.GetKeyBytes();
					writer.WriteBase64(keyBytes, 0, keyBytes.Length);
					writer.WriteEndElement();
				}
				writer.WriteEndElement();
			}
			if (this.keyType != null && this.keyType.Length > 0)
			{
				writer.WriteStartElement("KeyType", "http://schemas.xmlsoap.org/ws/2005/02/trust");
				writer.WriteString(this.keyType);
				writer.WriteEndElement();
			}
			if (base.KeySize > 0)
			{
				writer.WriteStartElement("KeySize", "http://schemas.xmlsoap.org/ws/2005/02/trust");
				writer.WriteValue(base.KeySize);
				writer.WriteEndElement();
			}
			writer.WriteEndElement();
		}

		private static RequestSecurityToken ProcessRequestSecurityTokenElement(XmlReader xr)
		{
			if (xr == null)
			{
				throw new ArgumentNullException("xr");
			}
			if (xr.IsEmptyElement)
			{
				throw new ArgumentException("wst:RequestSecurityToken element was empty. Unable to create RequestSecurityToken object");
			}
			int depth = xr.Depth;
			string attribute = xr.GetAttribute("Context", string.Empty);
			string tokenType = string.Empty;
			string text = string.Empty;
			int keySize = 0;
			string text2 = "http://schemas.xmlsoap.org/ws/2005/02/trust/SymmetricKey";
			EndpointAddress appliesTo = null;
			SecurityToken entropy = null;
			SecurityToken securityToken = null;
			while (xr.Read())
			{
				if (XmlNodeType.Element == xr.NodeType)
				{
					if ("http://schemas.xmlsoap.org/ws/2005/02/trust" == xr.NamespaceURI)
					{
						if ("RequestType" == xr.LocalName && !xr.IsEmptyElement)
						{
							xr.Read();
							text = xr.ReadContentAsString();
						}
						else if ("TokenType" == xr.LocalName && !xr.IsEmptyElement)
						{
							xr.Read();
							tokenType = xr.ReadContentAsString();
						}
						else if ("KeySize" == xr.LocalName && !xr.IsEmptyElement)
						{
							xr.Read();
							keySize = xr.ReadContentAsInt();
						}
						else if ("KeyType" == xr.LocalName && !xr.IsEmptyElement)
						{
							xr.Read();
							text2 = xr.ReadContentAsString();
						}
						else if ("Entropy" == xr.LocalName && !xr.IsEmptyElement)
						{
							entropy = RequestSecurityToken.ProcessEntropyElement(xr);
						}
						else
						{
							Console.WriteLine("Not processing element: {0}:{1}", xr.NamespaceURI, xr.LocalName);
						}
					}
					else if ("http://schemas.xmlsoap.org/ws/2004/09/policy" == xr.NamespaceURI)
					{
						if ("AppliesTo" == xr.LocalName && !xr.IsEmptyElement)
						{
							appliesTo = RequestSecurityToken.ProcessAppliesToElement(xr);
						}
						else
						{
							Console.WriteLine("Not processing element: {0}:{1}", xr.NamespaceURI, xr.LocalName);
						}
					}
					else
					{
						Console.WriteLine("Not processing element: {0}:{1}", xr.NamespaceURI, xr.LocalName);
					}
				}
				if ("RequestSecurityToken" == xr.LocalName && "http://schemas.xmlsoap.org/ws/2005/02/trust" == xr.NamespaceURI && xr.Depth == depth && XmlNodeType.EndElement == xr.NodeType)
				{
					break;
				}
			}
			return new RequestSecurityToken(attribute, tokenType, text, keySize, text2, securityToken, entropy, appliesTo);
		}

		private static SecurityToken ProcessEntropyElement(XmlReader xr)
		{
			if (xr == null)
			{
				throw new ArgumentNullException("xr");
			}
			if (xr.IsEmptyElement)
			{
				throw new ArgumentException("wst:Entropy element was empty. Unable to create SecurityToken object");
			}
			int depth = xr.Depth;
			SecurityToken result = null;
			while (xr.Read())
			{
				if ("BinarySecret" == xr.LocalName && "http://schemas.xmlsoap.org/ws/2005/02/trust" == xr.NamespaceURI && !xr.IsEmptyElement && XmlNodeType.Element == xr.NodeType)
				{
					byte[] array = new byte[1024];
					xr.Read();
					int num = xr.ReadContentAsBase64(array, 0, array.Length);
					byte[] array2 = new byte[num];
					for (int i = 0; i < num; i++)
					{
						array2[i] = array[i];
					}
					result = new BinarySecretSecurityToken(array2);
				}
				if ("Entropy" == xr.LocalName && "http://schemas.xmlsoap.org/ws/2005/02/trust" == xr.NamespaceURI && xr.Depth == depth && XmlNodeType.EndElement == xr.NodeType)
				{
					break;
				}
			}
			return result;
		}

		private static EndpointAddress ProcessAppliesToElement(XmlReader xr)
		{
			if (xr == null)
			{
				throw new ArgumentNullException("xr");
			}
			if (xr.IsEmptyElement)
			{
				throw new ArgumentException("wsp:AppliesTo element was empty. Unable to create EndpointAddress object");
			}
			int depth = xr.Depth;
			EndpointAddress result = null;
			while (xr.Read())
			{
				if ("EndpointReference" == xr.LocalName && "http://www.w3.org/2005/08/addressing" == xr.NamespaceURI && !xr.IsEmptyElement && XmlNodeType.Element == xr.NodeType)
				{
					DataContractSerializer dataContractSerializer = new DataContractSerializer(typeof(EndpointAddress10));
					EndpointAddress10 endpointAddress = (EndpointAddress10)dataContractSerializer.ReadObject(xr, false);
					result = endpointAddress.ToEndpointAddress();
				}
				else if ("EndpointReference" == xr.LocalName && "http://schemas.xmlsoap.org/ws/2004/08/addressing" == xr.NamespaceURI && !xr.IsEmptyElement && XmlNodeType.Element == xr.NodeType)
				{
					DataContractSerializer dataContractSerializer2 = new DataContractSerializer(typeof(EndpointAddressAugust2004));
					EndpointAddressAugust2004 endpointAddressAugust = (EndpointAddressAugust2004)dataContractSerializer2.ReadObject(xr, false);
					result = endpointAddressAugust.ToEndpointAddress();
				}
				if ("AppliesTo" == xr.LocalName && "http://schemas.xmlsoap.org/ws/2004/09/policy" == xr.NamespaceURI && xr.Depth == depth && XmlNodeType.EndElement == xr.NodeType)
				{
					break;
				}
			}
			return result;
		}

		private string keyType;

		private string requestType;

		private SecurityToken requestorEntropy;

		private SecurityToken proofKey;
	}
}
