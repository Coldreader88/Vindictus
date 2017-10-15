using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.CharacterServiceOperations
{
	[Serializable]
	public sealed class SkillEnhanceSession : Operation
	{
		public List<long> AdditionalItemIDs { get; set; }

		public SkillEnhanceSession(List<long> additionalItemIDs)
		{
			this.AdditionalItemIDs = additionalItemIDs;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
