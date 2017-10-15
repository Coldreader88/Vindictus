using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.CommonOperations
{
	[Serializable]
	public sealed class UpdateFeatureMatrix : Operation
	{
		public UpdateFeatureMatrix(Dictionary<string, string> features)
		{
			this.Features = features;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new NullProcessor(this);
		}

		public Dictionary<string, string> Features = new Dictionary<string, string>();
	}
}
