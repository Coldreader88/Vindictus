using System;
using System.Collections.Generic;
using ServiceCore.EndPointNetwork;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.CharacterServiceOperations
{
	[Serializable]
	public sealed class CheckCharacterName : Operation
	{
		public string NexonID { get; set; }

		public string Name { get; set; }

		public CreateCharacterResult ErrorCode
		{
			get
			{
				return this.errorCode;
			}
			set
			{
				this.errorCode = value;
			}
		}

		public CheckCharacterName(string nexonID, string name)
		{
			this.NexonID = nexonID;
			this.Name = name;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new CheckCharacterName.Request(this);
		}

		[NonSerialized]
		private CreateCharacterResult errorCode;

		private class Request : OperationProcessor<CheckCharacterName>
		{
			public Request(CheckCharacterName op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				base.Finished = true;
				if (base.Feedback is OkMessage)
				{
					base.Result = true;
				}
				else
				{
					base.Result = false;
					base.Operation.ErrorCode = (CreateCharacterResult)base.Feedback;
				}
				yield break;
			}
		}
	}
}
