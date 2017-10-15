using System;
using System.IdentityModel.Tokens;

namespace Nexon.Enterprise.ServiceFacade.Security
{
	public class InMemoryIssuedTokenCache : IssuedTokenCacheBase
	{
		protected override void OnTokenAdded(IssuedTokenCacheBase.Key key, GenericXmlSecurityToken token)
		{
		}

		protected override void OnTokenRemoved(IssuedTokenCacheBase.Key key)
		{
		}
	}
}
