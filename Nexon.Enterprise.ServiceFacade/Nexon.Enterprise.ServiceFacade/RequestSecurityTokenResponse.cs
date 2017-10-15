using System;
using System.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Security;
using System.Xml;

namespace Nexon.Enterprise.ServiceFacade
{
	public class RequestSecurityTokenResponse : RequestSecurityTokenBase
	{
		public RequestSecurityTokenResponse() : this(string.Empty, string.Empty, 0, null, null, null, false)
		{
		}

		public RequestSecurityTokenResponse(string context, string tokenType, int keySize, EndpointAddress appliesTo, SecurityToken requestedSecurityToken, SecurityToken requestedProofToken, bool computeKey) : base(context, tokenType, keySize, appliesTo)
		{
			this.m_requestedSecurityToken = requestedSecurityToken;
			this.m_requestedProofToken = requestedProofToken;
			this.m_computeKey = computeKey;
		}

		public SecurityToken RequestedSecurityToken
		{
			get
			{
				return this.m_requestedSecurityToken;
			}
			set
			{
				this.m_requestedSecurityToken = value;
			}
		}

		public SecurityToken RequestedProofToken
		{
			get
			{
				return this.m_requestedProofToken;
			}
			set
			{
				this.m_requestedProofToken = value;
			}
		}

		public SecurityKeyIdentifierClause RequestedAttachedReference
		{
			get
			{
				return this.m_requestedAttachedReference;
			}
			set
			{
				this.m_requestedAttachedReference = value;
			}
		}

		public SecurityKeyIdentifierClause RequestedUnattachedReference
		{
			get
			{
				return this.m_requestedUnattachedReference;
			}
			set
			{
				this.m_requestedUnattachedReference = value;
			}
		}

		public SecurityToken IssuerEntropy
		{
			get
			{
				return this.m_issuerEntropy;
			}
			set
			{
				this.m_issuerEntropy = value;
			}
		}

		public bool ComputeKey
		{
			get
			{
				return this.m_computeKey;
			}
			set
			{
				this.m_computeKey = value;
			}
		}

		public static byte[] ComputeCombinedKey(byte[] requestorEntropy, byte[] issuerEntropy, int keySize)
		{
			if (keySize < 64 || keySize > 4096)
			{
				throw new ArgumentOutOfRangeException("keySize");
			}
			KeyedHashAlgorithm keyedHashAlgorithm = new HMACSHA1(requestorEntropy, true);
			byte[] array = new byte[keySize / 8];
			byte[] array2 = issuerEntropy;
			byte[] array3 = new byte[keyedHashAlgorithm.HashSize / 8 + array2.Length];
			int i = 0;
			while (i < array.Length)
			{
				keyedHashAlgorithm.Initialize();
				array2 = keyedHashAlgorithm.ComputeHash(array2);
				array2.CopyTo(array3, 0);
				issuerEntropy.CopyTo(array3, array2.Length);
				keyedHashAlgorithm.Initialize();
				byte[] array4 = keyedHashAlgorithm.ComputeHash(array3);
				int num = 0;
				while (num < array4.Length && i < array.Length)
				{
					array[i++] = array4[num];
					num++;
				}
			}
			return array;
		}

		protected override void OnWriteBodyContents(XmlDictionaryWriter writer)
		{
			writer.WriteStartElement("RequestSecurityTokenResponse", "http://schemas.xmlsoap.org/ws/2005/02/trust");
			if (base.TokenType != null && base.TokenType.Length > 0)
			{
				writer.WriteStartElement("TokenType", "http://schemas.xmlsoap.org/ws/2005/02/trust");
				writer.WriteString(base.TokenType);
				writer.WriteEndElement();
			}
			WSSecurityTokenSerializer wssecurityTokenSerializer = new WSSecurityTokenSerializer();
			if (this.RequestedSecurityToken != null)
			{
				writer.WriteStartElement("RequestedSecurityToken", "http://schemas.xmlsoap.org/ws/2005/02/trust");
				wssecurityTokenSerializer.WriteToken(writer, this.RequestedSecurityToken);
				writer.WriteEndElement();
			}
			if (this.RequestedAttachedReference != null)
			{
				writer.WriteStartElement("RequestedAttachedReference", "http://schemas.xmlsoap.org/ws/2005/02/trust");
				wssecurityTokenSerializer.WriteKeyIdentifierClause(writer, this.RequestedAttachedReference);
				writer.WriteEndElement();
			}
			if (this.RequestedUnattachedReference != null)
			{
				writer.WriteStartElement("RequestedUnattachedReference", "http://schemas.xmlsoap.org/ws/2005/02/trust");
				wssecurityTokenSerializer.WriteKeyIdentifierClause(writer, this.RequestedUnattachedReference);
				writer.WriteEndElement();
			}
			if (base.AppliesTo != null)
			{
				writer.WriteStartElement("AppliesTo", "http://schemas.xmlsoap.org/ws/2004/09/policy");
				base.AppliesTo.WriteTo(AddressingVersion.WSAddressing10, writer);
				writer.WriteEndElement();
			}
			if (this.RequestedProofToken != null)
			{
				writer.WriteStartElement("RequestedProofToken", "http://schemas.xmlsoap.org/ws/2005/02/trust");
				wssecurityTokenSerializer.WriteToken(writer, this.RequestedProofToken);
				writer.WriteEndElement();
			}
			if (this.IssuerEntropy != null && this.ComputeKey)
			{
				writer.WriteStartElement("RequestedProofToken", "http://schemas.xmlsoap.org/ws/2005/02/trust");
				writer.WriteStartElement("ComputedKey", "http://schemas.xmlsoap.org/ws/2005/02/trust");
				writer.WriteValue("http://schemas.xmlsoap.org/ws/2005/02/trust/CK/PSHA1");
				writer.WriteEndElement();
				writer.WriteEndElement();
				if (this.IssuerEntropy != null)
				{
					writer.WriteStartElement("Entropy", "http://schemas.xmlsoap.org/ws/2005/02/trust");
					wssecurityTokenSerializer.WriteToken(writer, this.IssuerEntropy);
					writer.WriteEndElement();
				}
			}
			writer.WriteEndElement();
		}

		private SecurityToken m_requestedSecurityToken;

		private SecurityToken m_requestedProofToken;

		private SecurityToken m_issuerEntropy;

		private SecurityKeyIdentifierClause m_requestedAttachedReference;

		private SecurityKeyIdentifierClause m_requestedUnattachedReference;

		private bool m_computeKey;
	}
}
