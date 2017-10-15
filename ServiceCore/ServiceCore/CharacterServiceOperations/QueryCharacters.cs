using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.CharacterServiceOperations
{
	[Serializable]
	public sealed class QueryCharacters : Operation
	{
		public int NexonSN { get; set; }

		public bool IsCheat { get; set; }

		public QueryCharacters(int nexonSN, bool isCheat)
		{
			this.NexonSN = nexonSN;
			this.IsCheat = isCheat;
		}

		public long LastCID
		{
			get
			{
				return this.lastCID;
			}
		}

		public IList<CharacterSummary> QueryResult
		{
			get
			{
				return this.queryResult;
			}
		}

		public int MaxFreeCharacters
		{
			get
			{
				return this.maxFreeCharacters;
			}
		}

		public int MaxPurchasedCharacters
		{
			get
			{
				return this.maxPurchasedCharacters;
			}
		}

		public int MaxPremiumCharacters
		{
			get
			{
				return this.maxPremiumCharacters;
			}
		}

		public bool ProloguePlayed
		{
			get
			{
				return this.prologuePlayed;
			}
		}

		public int PresetUsedCharacterCount
		{
			get
			{
				return this.presetUsedCharacterCount;
			}
		}

		public IList<TransferElement> TransferResult
		{
			get
			{
				return this.transferResult;
			}
		}

		public override OperationProcessor RequestProcessor()
		{
			return new QueryCharacters.Request(this);
		}

		[NonSerialized]
		private long lastCID;

		[NonSerialized]
		private IList<CharacterSummary> queryResult;

		[NonSerialized]
		private int maxFreeCharacters;

		[NonSerialized]
		private int maxPurchasedCharacters;

		[NonSerialized]
		private int maxPremiumCharacters;

		[NonSerialized]
		private bool prologuePlayed;

		[NonSerialized]
		private int presetUsedCharacterCount;

		[NonSerialized]
		private IList<TransferElement> transferResult;

		private class Request : OperationProcessor<QueryCharacters>
		{
			public Request(QueryCharacters op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				if (base.Feedback is long)
				{
					base.Operation.lastCID = (long)base.Feedback;
					yield return null;
					base.Operation.queryResult = (base.Feedback as IList<CharacterSummary>);
					yield return null;
					base.Operation.maxFreeCharacters = (int)base.Feedback;
					yield return null;
					base.Operation.maxPurchasedCharacters = (int)base.Feedback;
					yield return null;
					base.Operation.maxPremiumCharacters = (int)base.Feedback;
					yield return null;
					base.Operation.prologuePlayed = (bool)base.Feedback;
					yield return null;
					base.Operation.presetUsedCharacterCount = (int)base.Feedback;
					yield return null;
					base.Operation.transferResult = (base.Feedback as IList<TransferElement>);
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
