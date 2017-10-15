using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.CharacterServiceOperations
{
	[Serializable]
	public sealed class QueryBaseHairInfo : Operation
	{
		public int CharacterClass
		{
			get
			{
				return this.characterClass;
			}
			set
			{
				this.characterClass = value;
			}
		}

		public int CostumeID
		{
			get
			{
				return this.costumeID;
			}
			set
			{
				this.costumeID = value;
			}
		}

		public int Color1
		{
			get
			{
				return this.color1;
			}
			set
			{
				this.color1 = value;
			}
		}

		public int Color2
		{
			get
			{
				return this.color2;
			}
			set
			{
				this.color2 = value;
			}
		}

		public int Color3
		{
			get
			{
				return this.color3;
			}
			set
			{
				this.color3 = value;
			}
		}

		public override OperationProcessor RequestProcessor()
		{
			return new QueryBaseHairInfo.Request(this);
		}

		[NonSerialized]
		private int characterClass;

		[NonSerialized]
		private int costumeID;

		[NonSerialized]
		private int color1;

		[NonSerialized]
		private int color2;

		[NonSerialized]
		private int color3;

		private class Request : OperationProcessor<QueryBaseHairInfo>
		{
			public Request(QueryBaseHairInfo op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				base.Operation.CharacterClass = (int)base.Feedback;
				yield return null;
				base.Operation.CostumeID = (int)base.Feedback;
				yield return null;
				base.Operation.Color1 = (int)base.Feedback;
				yield return null;
				base.Operation.Color2 = (int)base.Feedback;
				yield return null;
				base.Operation.Color3 = (int)base.Feedback;
				yield break;
			}
		}
	}
}
