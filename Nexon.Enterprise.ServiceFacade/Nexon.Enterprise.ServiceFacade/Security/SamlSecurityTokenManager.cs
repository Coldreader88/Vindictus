using System;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Security;
using System.ServiceModel.Security.Tokens;

namespace Nexon.Enterprise.ServiceFacade.Security
{
	public class SamlSecurityTokenManager : ClientCredentialsSecurityTokenManager
	{
		public SamlSecurityTokenManager(SamlClientCredentials samlClientCredentials) : base(samlClientCredentials)
		{
			this.samlClientCredentials = samlClientCredentials;
		}

		public override SecurityTokenProvider CreateSecurityTokenProvider(SecurityTokenRequirement tokenRequirement)
		{
			if (tokenRequirement.TokenType == SecurityTokenTypes.Saml || tokenRequirement.TokenType == "http://docs.oasis-open.org/wss/oasis-wss-saml-token-profile-1.1#SAMLV1.1")
			{
				SamlAssertion samlAssertion = this.samlClientCredentials.Assertion;
				SecurityToken securityToken = this.samlClientCredentials.ProofToken;
				if (samlAssertion == null || securityToken == null)
				{
					SecurityBindingElement securityBindingElement = null;
					SecurityAlgorithmSuite algoSuite = null;
					if (tokenRequirement.TryGetProperty<SecurityBindingElement>("http://schemas.microsoft.com/ws/2006/05/servicemodel/securitytokenrequirement/SecurityBindingElement", out securityBindingElement))
					{
						algoSuite = securityBindingElement.DefaultAlgorithmSuite;
					}
					if (tokenRequirement.KeyType == SecurityKeyType.SymmetricKey)
					{
						securityToken = SamlUtilities.CreateSymmetricProofToken(tokenRequirement.KeySize);
						samlAssertion = SamlUtilities.CreateSymmetricKeyBasedAssertion(this.samlClientCredentials.Claims, new X509SecurityToken(this.samlClientCredentials.ClientCertificate.Certificate), new X509SecurityToken(this.samlClientCredentials.ServiceCertificate.DefaultCertificate), (BinarySecretSecurityToken)securityToken, algoSuite);
					}
					else
					{
						securityToken = SamlUtilities.CreateAsymmetricProofToken();
						samlAssertion = SamlUtilities.CreateAsymmetricKeyBasedAssertion(this.samlClientCredentials.Claims, securityToken, algoSuite);
					}
				}
				return new SamlSecurityTokenProvider(samlAssertion, securityToken);
			}
			return base.CreateSecurityTokenProvider(tokenRequirement);
		}

		private SamlClientCredentials samlClientCredentials;
	}
}
