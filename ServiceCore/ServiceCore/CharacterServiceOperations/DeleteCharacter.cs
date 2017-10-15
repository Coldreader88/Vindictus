using System;
using System.Collections.Generic;
using ServiceCore.EndPointNetwork;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.CharacterServiceOperations
{
	[Serializable]
	public sealed class DeleteCharacter : Operation
	{
		public int NexonSN { get; set; }

		public int CharacterSN { get; set; }

		public string Name { get; set; }

		public DeleteCharacter(int nexonSN, int characterSN, string name)
		{
			this.NexonSN = nexonSN;
			this.CharacterSN = characterSN;
			this.Name = name;
		}

		public DeleteCharacterResult DeleteCharacterResult
		{
			get
			{
				return this.deleteCharacterResult;
			}
			set
			{
				this.deleteCharacterResult = value;
			}
		}

		public int WaitingTimeLeft
		{
			get
			{
				return this.waitingTimeLeft;
			}
			set
			{
				this.waitingTimeLeft = value;
			}
		}

		public override OperationProcessor RequestProcessor()
		{
			return new DeleteCharacter.Request(this);
		}

		[NonSerialized]
		public DeleteCharacterResult deleteCharacterResult;

		[NonSerialized]
		public int waitingTimeLeft;

		private class Request : OperationProcessor<DeleteCharacter>
		{
			public Request(DeleteCharacter op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				if (base.Feedback is DeleteCharacterResult)
				{
					base.Result = true;
					base.Operation.DeleteCharacterResult = (DeleteCharacterResult)base.Feedback;
					yield return null;
					base.Operation.WaitingTimeLeft = (int)base.Feedback;
				}
				else
				{
					base.Result = false;
					base.Operation.DeleteCharacterResult = DeleteCharacterResult.Fail_Unknown;
					base.Operation.WaitingTimeLeft = 0;
				}
				yield break;
			}
		}
	}
}
