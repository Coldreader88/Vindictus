using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.CharacterServiceOperations
{
	[Serializable]
	public sealed class UpdateWhisperFilter : Operation
	{
		public UpdateWhisperFilter.UpdateType OperationType { get; set; }

		public long CID { get; set; }

		public int Level { get; set; }

		public IDictionary<string, int> WhisperFilter
		{
			get
			{
				return this.filter;
			}
		}

		public override OperationProcessor RequestProcessor()
		{
			return new UpdateWhisperFilter.Request(this);
		}

		[NonSerialized]
		private IDictionary<string, int> filter;

		public enum UpdateType
		{
			Add,
			Update,
			Remove
		}

		private class Request : OperationProcessor<UpdateWhisperFilter>
		{
			public Request(UpdateWhisperFilter op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				if (base.Feedback is IDictionary<string, int>)
				{
					base.Result = true;
					base.Operation.filter = (base.Feedback as IDictionary<string, int>);
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
