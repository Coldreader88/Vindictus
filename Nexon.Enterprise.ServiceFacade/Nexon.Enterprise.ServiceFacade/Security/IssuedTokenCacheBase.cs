using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.ServiceModel;

namespace Nexon.Enterprise.ServiceFacade.Security
{
	public abstract class IssuedTokenCacheBase : IssuedTokenCache
	{
		protected IssuedTokenCacheBase()
		{
			this.cache = new Dictionary<IssuedTokenCacheBase.Key, GenericXmlSecurityToken>();
			this.thisLock = new object();
		}

		protected Dictionary<IssuedTokenCacheBase.Key, GenericXmlSecurityToken> Cache
		{
			get
			{
				return this.cache;
			}
		}

		protected abstract void OnTokenAdded(IssuedTokenCacheBase.Key key, GenericXmlSecurityToken token);

		protected abstract void OnTokenRemoved(IssuedTokenCacheBase.Key key);

		public override void AddToken(GenericXmlSecurityToken token, EndpointAddress target, EndpointAddress issuer)
		{
			if (token == null)
			{
				throw new ArgumentNullException("token");
			}
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			lock (this.thisLock)
			{
				IssuedTokenCacheBase.Key key = new IssuedTokenCacheBase.Key(target.Uri, (issuer == null) ? null : issuer.Uri);
				this.cache.Add(key, token);
				this.OnTokenAdded(key, token);
			}
		}

		public override bool TryGetToken(EndpointAddress target, EndpointAddress issuer, out GenericXmlSecurityToken cachedToken)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			cachedToken = null;
			lock (this.thisLock)
			{
				IssuedTokenCacheBase.Key key = new IssuedTokenCacheBase.Key(target.Uri, (issuer == null) ? null : issuer.Uri);
				GenericXmlSecurityToken genericXmlSecurityToken;
				if (this.cache.TryGetValue(key, out genericXmlSecurityToken))
				{
					if (genericXmlSecurityToken.ValidTo < DateTime.UtcNow + TimeSpan.FromMinutes(1.0))
					{
						this.cache.Remove(key);
						this.OnTokenRemoved(key);
					}
					else
					{
						cachedToken = genericXmlSecurityToken;
					}
				}
			}
			return cachedToken != null;
		}

		private Dictionary<IssuedTokenCacheBase.Key, GenericXmlSecurityToken> cache;

		private object thisLock;

		protected class Key
		{
			public Key(Uri target, Uri issuer)
			{
				this.target = target;
				this.issuer = issuer;
			}

			public Uri Target
			{
				get
				{
					return this.target;
				}
			}

			public Uri Issuer
			{
				get
				{
					return this.issuer;
				}
			}

			public override bool Equals(object obj)
			{
				IssuedTokenCacheBase.Key key = obj as IssuedTokenCacheBase.Key;
				if (key == null)
				{
					return false;
				}
				bool flag = (this.target == null && key.target == null) || this.target.Equals(key.target);
				if (flag)
				{
					flag = ((this.issuer == null && key.issuer == null) || this.issuer.Equals(key.issuer));
				}
				return flag;
			}

			public override int GetHashCode()
			{
				int num = (this.target == null) ? 0 : this.target.GetHashCode();
				int num2 = (this.issuer == null) ? 0 : this.issuer.GetHashCode();
				return num ^ num2;
			}

			private Uri target;

			private Uri issuer;
		}
	}
}
