using System;
using System.IdentityModel.Claims;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.ServiceModel.Description;

namespace Nexon.Enterprise.ServiceFacade.Security
{
	public class SamlClientCredentials : ClientCredentials
	{
		public SamlClientCredentials()
		{
			base.SupportInteractive = false;
		}

		protected SamlClientCredentials(SamlClientCredentials other) : base(other)
		{
			this.assertion = other.assertion;
			this.claims = other.claims;
			this.proofToken = other.proofToken;
		}

		public SamlAssertion Assertion
		{
			get
			{
				return this.assertion;
			}
			set
			{
				this.assertion = value;
			}
		}

		public SecurityToken ProofToken
		{
			get
			{
				return this.proofToken;
			}
			set
			{
				this.proofToken = value;
			}
		}

		public ClaimSet Claims
		{
			get
			{
				return this.claims;
			}
			set
			{
				this.claims = value;
			}
		}

		protected override ClientCredentials CloneCore()
		{
			return new SamlClientCredentials(this);
		}

		public override SecurityTokenManager CreateSecurityTokenManager()
		{
			return new SamlSecurityTokenManager(this);
		}

		private ClaimSet claims;

		private SamlAssertion assertion;

		private SecurityToken proofToken;
	}
}
