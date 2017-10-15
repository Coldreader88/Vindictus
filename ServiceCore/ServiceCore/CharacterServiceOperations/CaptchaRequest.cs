using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.CharacterServiceOperations
{
	[Serializable]
	public sealed class CaptchaRequest : Operation
	{
		public bool ShouldWaitForResponse
		{
			get
			{
				return this.waitForResponse;
			}
			set
			{
				this.waitForResponse = value;
			}
		}

		public override OperationProcessor RequestProcessor()
		{
			return new CaptchaRequest.Request(this);
		}

		[NonSerialized]
		private bool waitForResponse;

		private class Request : OperationProcessor<CaptchaRequest>
		{
			public Request(CaptchaRequest op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				if (base.Feedback is OkMessage)
				{
					base.Result = true;
					yield return null;
					base.Operation.ShouldWaitForResponse = (bool)base.Feedback;
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
