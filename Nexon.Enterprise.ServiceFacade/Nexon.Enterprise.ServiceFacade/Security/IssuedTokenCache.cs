using System;
using System.IdentityModel.Tokens;
using System.ServiceModel;

namespace Nexon.Enterprise.ServiceFacade.Security
{
	public abstract class IssuedTokenCache
	{
		public abstract void AddToken(GenericXmlSecurityToken token, EndpointAddress target, EndpointAddress issuer);

		public abstract bool TryGetToken(EndpointAddress target, EndpointAddress issuer, out GenericXmlSecurityToken cachedToken);
	}
}
