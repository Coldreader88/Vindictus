using System;
using System.IdentityModel.Claims;
using System.Linq;
using System.ServiceModel;

namespace Nexon.Enterprise.ServiceFacade
{
	public class AuthorizationManager : ServiceAuthorizationManager
	{
		protected override bool CheckAccessCore(OperationContext operationContext)
		{
			bool result;
			try
			{
				string action = operationContext.RequestContext.RequestMessage.Headers.Action;
				Console.WriteLine("action: {0}", action);
				Claim claim = (from m in operationContext.ServiceSecurityContext.AuthorizationContext.ClaimSets.SelectMany((ClaimSet s) => s.FindClaims("http://bam.nexon.com/claims/allowed/operation", Rights.PossessProperty))
				where m.Resource.ToString().Equals(action, StringComparison.InvariantCultureIgnoreCase)
				select m).SingleOrDefault<Claim>();
				if (claim == null)
				{
					result = false;
				}
				else
				{
					Console.WriteLine("resource:{0}", claim.Resource.ToString());
					result = true;
				}
			}
			catch (Exception value)
			{
				Console.WriteLine(value);
				result = false;
			}
			return result;
		}
	}
}
