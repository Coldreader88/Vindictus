using System;
using ServiceCore.CharacterServiceOperations;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.CashShopServiceOperation
{
	[Serializable]
	public sealed class QueryBeautyShopInfo : Operation
	{
		public BaseCharacter CharacterType { get; set; }

		public QueryBeautyShopInfo(BaseCharacter characterType)
		{
			this.CharacterType = characterType;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
