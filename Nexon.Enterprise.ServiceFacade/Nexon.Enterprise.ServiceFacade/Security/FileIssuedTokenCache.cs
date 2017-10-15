using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IdentityModel.Policy;
using System.IdentityModel.Tokens;
using System.IO;
using System.ServiceModel.Security;
using System.ServiceModel.Security.Tokens;
using System.Text;
using System.Xml;

namespace Nexon.Enterprise.ServiceFacade.Security
{
	public class FileIssuedTokenCache : IssuedTokenCacheBase
	{
		public FileIssuedTokenCache(string fileName)
		{
			this.fileName = fileName;
			if (File.Exists(fileName))
			{
				using (FileStream fileStream = File.Open(fileName, FileMode.OpenOrCreate, FileAccess.Read))
				{
					fileStream.Seek(0L, SeekOrigin.Begin);
					FileIssuedTokenCache.PopulateCache(base.Cache, fileStream);
				}
			}
		}

		protected override void OnTokenAdded(IssuedTokenCacheBase.Key key, GenericXmlSecurityToken token)
		{
			using (FileStream fileStream = File.Open(this.fileName, FileMode.Append, FileAccess.Write))
			{
				fileStream.Seek(0L, SeekOrigin.End);
				FileIssuedTokenCache.SerializeCachedToken(fileStream, key.Target, key.Issuer, token);
			}
		}

		protected override void OnTokenRemoved(IssuedTokenCacheBase.Key key)
		{
			using (FileStream fileStream = File.Open(this.fileName, FileMode.Append, FileAccess.Write))
			{
				fileStream.Seek(0L, SeekOrigin.Begin);
				foreach (IssuedTokenCacheBase.Key key2 in base.Cache.Keys)
				{
					GenericXmlSecurityToken token = base.Cache[key2];
					FileIssuedTokenCache.SerializeCachedToken(fileStream, key.Target, key.Issuer, token);
				}
			}
		}

		private static void SerializeCachedToken(Stream stream, Uri target, Uri issuer, GenericXmlSecurityToken token)
		{
			XmlTextWriter xmlTextWriter = new XmlTextWriter(stream, Encoding.UTF8);
			xmlTextWriter.WriteStartElement("Entry");
			xmlTextWriter.WriteElementString("Target", target.AbsoluteUri);
			xmlTextWriter.WriteElementString("Issuer", (issuer == null) ? "" : issuer.AbsoluteUri);
			xmlTextWriter.WriteStartElement("Token");
			xmlTextWriter.WriteStartElement("XML");
			token.TokenXml.WriteTo(xmlTextWriter);
			xmlTextWriter.WriteEndElement();
			SymmetricSecurityKey symmetricSecurityKey = (SymmetricSecurityKey)token.SecurityKeys[0];
			xmlTextWriter.WriteElementString("Key", Convert.ToBase64String(symmetricSecurityKey.GetSymmetricKey()));
			xmlTextWriter.WriteElementString("Id", token.Id);
			xmlTextWriter.WriteElementString("ValidFrom", Convert.ToString(token.ValidFrom));
			xmlTextWriter.WriteElementString("ValidTo", Convert.ToString(token.ValidTo));
			WSSecurityTokenSerializer wssecurityTokenSerializer = new WSSecurityTokenSerializer();
			xmlTextWriter.WriteStartElement("InternalTokenReference");
			wssecurityTokenSerializer.WriteKeyIdentifierClause(xmlTextWriter, token.InternalTokenReference);
			xmlTextWriter.WriteEndElement();
			xmlTextWriter.WriteStartElement("ExternalTokenReference");
			wssecurityTokenSerializer.WriteKeyIdentifierClause(xmlTextWriter, token.ExternalTokenReference);
			xmlTextWriter.WriteEndElement();
			xmlTextWriter.WriteEndElement();
			xmlTextWriter.WriteEndElement();
			xmlTextWriter.Flush();
			stream.Flush();
		}

		private static void PopulateCache(Dictionary<IssuedTokenCacheBase.Key, GenericXmlSecurityToken> cache, Stream stream)
		{
			XmlTextReader xmlTextReader = new XmlTextReader(stream);
			while (xmlTextReader.IsStartElement("Entry"))
			{
				xmlTextReader.ReadStartElement();
				Uri target = new Uri(xmlTextReader.ReadElementString("Target"));
				string text = xmlTextReader.ReadElementString("Issuer");
				Uri issuer = string.IsNullOrEmpty(text) ? null : new Uri(text);
				xmlTextReader.ReadStartElement("Token");
				xmlTextReader.ReadStartElement("XML");
				XmlDocument xmlDocument = new XmlDocument();
				XmlElement tokenXml = xmlDocument.ReadNode(xmlTextReader) as XmlElement;
				xmlTextReader.ReadEndElement();
				byte[] key = Convert.FromBase64String(xmlTextReader.ReadElementString("Key"));
				xmlTextReader.ReadElementString("Id");
				DateTime effectiveTime = Convert.ToDateTime(xmlTextReader.ReadElementString("ValidFrom"));
				DateTime expirationTime = Convert.ToDateTime(xmlTextReader.ReadElementString("ValidTo"));
				WSSecurityTokenSerializer wssecurityTokenSerializer = new WSSecurityTokenSerializer();
				xmlTextReader.ReadStartElement("InternalTokenReference");
				SecurityKeyIdentifierClause internalTokenReference = wssecurityTokenSerializer.ReadKeyIdentifierClause(xmlTextReader);
				xmlTextReader.ReadEndElement();
				xmlTextReader.ReadStartElement("ExternalTokenReference");
				SecurityKeyIdentifierClause externalTokenReference = wssecurityTokenSerializer.ReadKeyIdentifierClause(xmlTextReader);
				xmlTextReader.ReadEndElement();
				xmlTextReader.ReadEndElement();
				xmlTextReader.ReadEndElement();
				List<IAuthorizationPolicy> list = new List<IAuthorizationPolicy>();
				GenericXmlSecurityToken value = new GenericXmlSecurityToken(tokenXml, new BinarySecretSecurityToken(key), effectiveTime, expirationTime, internalTokenReference, externalTokenReference, new ReadOnlyCollection<IAuthorizationPolicy>(list));
				cache.Add(new IssuedTokenCacheBase.Key(target, issuer), value);
			}
		}

		private string fileName;
	}
}
