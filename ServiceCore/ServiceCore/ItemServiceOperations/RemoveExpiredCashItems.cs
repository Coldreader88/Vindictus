﻿using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.ItemServiceOperations
{
	[Serializable]
	public sealed class RemoveExpiredCashItems : Operation
	{
		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
