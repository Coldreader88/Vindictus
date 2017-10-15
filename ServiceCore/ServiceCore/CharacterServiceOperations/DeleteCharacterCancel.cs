using System;
using System.Collections.Generic;
using ServiceCore.EndPointNetwork.CharacterList;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.CharacterServiceOperations
{
	[Serializable]
	public sealed class DeleteCharacterCancel : Operation
	{
		public int NexonSN { get; set; }

		public int CharacterSN { get; set; }

		public string Name { get; set; }

		public DeleteCharacterCancel(int nexonSN, int characterSN, string name)
		{
			this.NexonSN = nexonSN;
			this.CharacterSN = characterSN;
			this.Name = name;
		}

		public DeleteCharacterCancelResult DeleteCharacterCancelResult
		{
			get
			{
				return this.deleteCharacterCancelResult;
			}
			set
			{
				this.deleteCharacterCancelResult = value;
			}
		}

		public override OperationProcessor RequestProcessor()
		{
			return new DeleteCharacterCancel.Request(this);
		}

		[NonSerialized]
		public DeleteCharacterCancelResult deleteCharacterCancelResult;

		private class Request : OperationProcessor<DeleteCharacterCancel>
		{
			public Request(DeleteCharacterCancel op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				if (base.Feedback is DeleteCharacterCancelResult)
				{
					base.Result = true;
					base.Operation.DeleteCharacterCancelResult = (DeleteCharacterCancelResult)base.Feedback;
				}
				else
				{
					base.Result = false;
					base.Operation.DeleteCharacterCancelResult = DeleteCharacterCancelResult.Fail_Unknown;
				}
				yield break;
			}
		}
	}
}
