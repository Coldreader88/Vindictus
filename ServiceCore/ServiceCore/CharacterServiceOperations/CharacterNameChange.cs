using System;
using System.Collections.Generic;
using ServiceCore.EndPointNetwork;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.CharacterServiceOperations
{
	[Serializable]
	public sealed class CharacterNameChange : Operation
	{
		public string NexonID { get; set; }

		public long CID { get; set; }

		public string RequestName { get; set; }

		public bool IsTrans { get; set; }

		public long ItemID { get; set; }

		public CharacterNameChange(string nexonID, long cid, string requestName, bool isTrans, long itemID)
		{
			this.NexonID = nexonID;
			this.CID = cid;
			this.RequestName = requestName;
			this.IsTrans = isTrans;
			this.ItemID = itemID;
		}

		public CharacterNameChangeResult CharacterNameChangeResult
		{
			get
			{
				return this.characterNameChangeResult;
			}
			set
			{
				this.characterNameChangeResult = value;
			}
		}

		public int CharacterSN
		{
			get
			{
				return this.characterSN;
			}
			set
			{
				this.characterSN = value;
			}
		}

		public string OldName
		{
			get
			{
				return this.oldName;
			}
			set
			{
				this.oldName = value;
			}
		}

		public override OperationProcessor RequestProcessor()
		{
			return new CharacterNameChange.Request(this);
		}

		[NonSerialized]
		public CharacterNameChangeResult characterNameChangeResult;

		[NonSerialized]
		public int characterSN;

		[NonSerialized]
		public string oldName;

		private class Request : OperationProcessor<CharacterNameChange>
		{
			public Request(CharacterNameChange op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				if (base.Feedback is CharacterNameChangeResult)
				{
					base.Result = true;
					base.Operation.CharacterNameChangeResult = (CharacterNameChangeResult)base.Feedback;
					yield return null;
					base.Operation.CharacterSN = (int)base.Feedback;
					yield return null;
					base.Operation.OldName = (string)base.Feedback;
				}
				else
				{
					base.Result = false;
					base.Operation.CharacterNameChangeResult = CharacterNameChangeResult.Unknown;
				}
				yield break;
			}
		}
	}
}
