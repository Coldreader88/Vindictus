using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.CharacterServiceOperations
{
	[Serializable]
	public sealed class QueryCIDByName : Operation
	{
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		public string ItemClass
		{
			get
			{
				return this.itemClass;
			}
		}

		public long CID
		{
			get
			{
				return this.cid;
			}
			set
			{
				this.cid = value;
			}
		}

		public int UID
		{
			get
			{
				return this.uid;
			}
			set
			{
				this.uid = value;
			}
		}

		public int Level
		{
			get
			{
				return this.level;
			}
			set
			{
				this.level = value;
			}
		}

		public int BaseCharacter
		{
			get
			{
				return this.baseCharacter;
			}
			set
			{
				this.baseCharacter = value;
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

		public bool IsEquipIImpossible
		{
			get
			{
				return this.isEquipIImpossible;
			}
			set
			{
				this.isEquipIImpossible = value;
			}
		}

		public QueryCIDByName(string name)
		{
			this.name = name;
			this.itemClass = "";
		}

		public QueryCIDByName(string name, string itemClass)
		{
			this.name = name;
			this.itemClass = itemClass;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new QueryCIDByName.Request(this);
		}

		private string name;

		private string itemClass;

		[NonSerialized]
		private long cid;

		[NonSerialized]
		private int uid;

		[NonSerialized]
		private int level;

		[NonSerialized]
		private int baseCharacter;

		[NonSerialized]
		private int characterSN;

		[NonSerialized]
		private bool isEquipIImpossible;

		private class Request : OperationProcessor<QueryCIDByName>
		{
			public Request(QueryCIDByName op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				if (base.Feedback is long)
				{
					base.Operation.CID = (long)base.Feedback;
					yield return null;
					base.Operation.UID = (int)base.Feedback;
					yield return null;
					base.Operation.Level = (int)base.Feedback;
					yield return null;
					base.Operation.BaseCharacter = (int)base.Feedback;
					yield return null;
					base.Operation.CharacterSN = (int)base.Feedback;
					yield return null;
					base.Operation.IsEquipIImpossible = (bool)base.Feedback;
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
