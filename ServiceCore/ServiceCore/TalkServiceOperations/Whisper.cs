using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.TalkServiceOperations
{
	[Serializable]
	public class Whisper : Operation
	{
		public string From { get; private set; }

		public string To { get; private set; }

		public string Message { get; private set; }

		public Whisper.WhisperResult FailReason { get; set; }

		public Whisper(string from, string to, string message)
		{
			this.From = from;
			this.To = to;
			this.Message = message;
			this.FailReason = Whisper.WhisperResult.LogicalFail;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new Whisper.Request(this);
		}

		public enum WhisperResult : byte
		{
			Success,
			LogicalFail,
			OffLine,
			NoCharacter,
			Wait
		}

		private class Request : OperationProcessor<Whisper>
		{
			public Request(Whisper op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				base.Operation.FailReason = (Whisper.WhisperResult)base.Feedback;
				if (base.Operation.FailReason == Whisper.WhisperResult.Success)
				{
					base.Result = true;
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
