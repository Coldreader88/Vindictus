using System;
using System.Collections.Generic;
using System.Linq;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.CharacterServiceOperations
{
	[Serializable]
	public sealed class QueryCharacterInfos : Operation
	{
		public IDictionary<long, int> Characters
		{
			get
			{
				return this.characters;
			}
		}

		public int MaxLevel
		{
			get
			{
				if (this.characters.Count == 0)
				{
					return 0;
				}
				return this.characters.Values.Max();
			}
		}

		public override OperationProcessor RequestProcessor()
		{
			return new QueryCharacterInfos.Request(this);
		}

		[NonSerialized]
		private IDictionary<long, int> characters;

		private class Request : OperationProcessor<QueryCharacterInfos>
		{
			public Request(QueryCharacterInfos op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				if (base.Feedback is IDictionary<long, int>)
				{
					base.Operation.characters = (base.Feedback as IDictionary<long, int>);
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
