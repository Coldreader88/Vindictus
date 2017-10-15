using System;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.IO;
using System.ServiceModel.Security;
using System.Xml;

namespace Nexon.Enterprise.ServiceFacade.Security
{
	public class SamlSecurityTokenProvider : SecurityTokenProvider
	{
		public SamlSecurityTokenProvider(SamlAssertion assertion, SecurityToken proofToken)
		{
			this.assertion = assertion;
			this.proofToken = proofToken;
		}

		protected override SecurityToken GetTokenCore(TimeSpan timeout)
		{
			SamlSecurityToken samlSecurityToken = new SamlSecurityToken(this.assertion);
			WSSecurityTokenSerializer wssecurityTokenSerializer = new WSSecurityTokenSerializer();
			MemoryStream memoryStream = new MemoryStream(65535);
			XmlWriter writer = XmlWriter.Create(memoryStream);
			wssecurityTokenSerializer.WriteToken(writer, samlSecurityToken);
			memoryStream.Seek(0L, SeekOrigin.Begin);
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(memoryStream);
			SamlAssertionKeyIdentifierClause samlAssertionKeyIdentifierClause = samlSecurityToken.CreateKeyIdentifierClause<SamlAssertionKeyIdentifierClause>();
			return new GenericXmlSecurityToken(xmlDocument.DocumentElement, this.proofToken, this.assertion.Conditions.NotBefore, this.assertion.Conditions.NotOnOrAfter, samlAssertionKeyIdentifierClause, samlAssertionKeyIdentifierClause, null);
		}

		private SamlAssertion assertion;

		private SecurityToken proofToken;
	}
}
