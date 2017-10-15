using System;
using System.Collections.Generic;
using ServiceCore.EndPointNetwork.Captcha;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.CharacterServiceOperations
{
	[Serializable]
	public sealed class CaptchaRefresh : Operation
	{
		public CaptchaRefreshResult ErrorCode
		{
			get
			{
				return this.error;
			}
			set
			{
				this.error = value;
			}
		}

		public override OperationProcessor RequestProcessor()
		{
			return new CaptchaRefresh.Request(this);
		}

		[NonSerialized]
		private CaptchaRefreshResult error;

		private class Request : OperationProcessor<CaptchaRefresh>
		{
			public Request(CaptchaRefresh op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				if (base.Feedback is int)
				{
					base.Result = true;
					base.Operation.ErrorCode = (CaptchaRefreshResult)base.Feedback;
				}
				else
				{
					base.Result = false;
				}
				yield break;
			}
		}
	}
}
