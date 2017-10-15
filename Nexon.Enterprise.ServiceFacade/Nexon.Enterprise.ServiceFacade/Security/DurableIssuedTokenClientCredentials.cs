using System;
using System.IdentityModel.Selectors;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Security.Tokens;

namespace Nexon.Enterprise.ServiceFacade.Security
{
	public class DurableIssuedTokenClientCredentials : ClientCredentials
	{
		public DurableIssuedTokenClientCredentials()
		{
		}

		private DurableIssuedTokenClientCredentials(DurableIssuedTokenClientCredentials other) : base(other)
		{
			this.cache = other.cache;
		}

		public IssuedTokenCache IssuedTokenCache
		{
			get
			{
				return this.cache;
			}
			set
			{
				this.cache = value;
			}
		}

		protected override ClientCredentials CloneCore()
		{
			return new DurableIssuedTokenClientCredentials(this);
		}

		public override SecurityTokenManager CreateSecurityTokenManager()
		{
			return new DurableIssuedTokenClientCredentials.DurableIssuedTokenClientCredentialsTokenManager((DurableIssuedTokenClientCredentials)base.Clone());
		}

		private IssuedTokenCache cache;

		private class DurableIssuedTokenClientCredentialsTokenManager : ClientCredentialsSecurityTokenManager
		{
			public DurableIssuedTokenClientCredentialsTokenManager(DurableIssuedTokenClientCredentials creds) : base(creds)
			{
				this.cache = creds.IssuedTokenCache;
			}

			public override SecurityTokenProvider CreateSecurityTokenProvider(SecurityTokenRequirement tokenRequirement)
			{
				if (base.IsIssuedSecurityTokenRequirement(tokenRequirement))
				{
					return new DurableIssuedSecurityTokenProvider((IssuedSecurityTokenProvider)base.CreateSecurityTokenProvider(tokenRequirement), this.cache);
				}
				return base.CreateSecurityTokenProvider(tokenRequirement);
			}

			private IssuedTokenCache cache;
		}
	}
}
