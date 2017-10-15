using System;
using System.Collections.Generic;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class SyncFeatureMatrixMessage : IMessage
	{
		public Dictionary<string, string> FeatureDic { get; set; }

		public SyncFeatureMatrixMessage(Dictionary<string, string> featureDic)
		{
			this.FeatureDic = featureDic;
		}

		public override string ToString()
		{
			return string.Format("SyncFeatureMatrixMessage [ FeatureDic.Count = {0} ]", this.FeatureDic.Count);
		}
	}
}
