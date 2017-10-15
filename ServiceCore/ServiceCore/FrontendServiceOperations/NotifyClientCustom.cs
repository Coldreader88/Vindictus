using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.FrontendServiceOperations
{
	[Serializable]
	public sealed class NotifyClientCustom : Operation
	{
		public int DialogType { get; set; }

		public List<string> Arg { get; set; }

		public bool IsTownOnly { get; set; }

		public int RandomItemAnnounceLevel { get; set; }

		public override OperationProcessor RequestProcessor()
		{
			return new NullProcessor(this);
		}
	}
}
