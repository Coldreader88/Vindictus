using System;
using System.Collections.Generic;
using System.IdentityModel.Claims;
using System.IdentityModel.Policy;
using System.Linq;

namespace Nexon.Enterprise.ServiceFacade.Policy
{
	public class AuthorizationPolicy : IAuthorizationPolicy, IAuthorizationComponent
	{
		public bool Evaluate(EvaluationContext evaluationContext, ref object state)
		{
			AuthorizationPolicy.CustomAuthorizationState customAuthorizationState = null;
			if (state == null)
			{
				customAuthorizationState = new AuthorizationPolicy.CustomAuthorizationState();
			}
			else
			{
				customAuthorizationState = (state as AuthorizationPolicy.CustomAuthorizationState);
			}
			if (!customAuthorizationState.ClaimsAdded)
			{
				IList<Claim> list = new List<Claim>();
				Func<Claim, IEnumerable<string>> selector = (Claim ienum) => AuthorizationPolicy.GetAllowedOperationList(ienum.Resource.ToString());
				IEnumerable<Claim> source = evaluationContext.ClaimSets.SelectMany((ClaimSet s) => s.FindClaims(ClaimTypes.Name, Rights.PossessProperty));
				foreach (string resource in source.SelectMany(selector))
				{
					list.Add(new Claim("http://bam.nexon.com/claims/allowed/operation", resource, Rights.PossessProperty));
				}
				evaluationContext.AddClaimSet(this, new DefaultClaimSet(this.Issuer, list));
				customAuthorizationState.ClaimsAdded = true;
				return true;
			}
			return true;
		}

		public ClaimSet Issuer
		{
			get
			{
				return ClaimSet.System;
			}
		}

		public string Id
		{
			get
			{
				if (string.IsNullOrEmpty(this.id))
				{
					this.id = Guid.NewGuid().ToString();
				}
				return this.id;
			}
		}

		private static IEnumerable<string> GetAllowedOperationList(string username)
		{
			IList<string> list = new List<string>();
			if (username.Equals("localhost", StringComparison.InvariantCultureIgnoreCase))
			{
				list.Add("http://bam.nexon.com/ITransferService/EchoStream");
				list.Add("http://bam.nexon.com/ITransferService/DownloadStream");
				list.Add("http://bam.nexon.com/ITransferService/UploadStream");
				list.Add("http://bam.nexon.com/IEventCallback/OnMessageEvent");
				list.Add("http://bam.nexon.com/IEventRegistration/Register");
				list.Add("http://bam.nexon.com/IEventRegistration/UnRegister");
			}
			return list;
		}

		private string id;

		private class CustomAuthorizationState
		{
			public bool ClaimsAdded { get; set; }
		}
	}
}
