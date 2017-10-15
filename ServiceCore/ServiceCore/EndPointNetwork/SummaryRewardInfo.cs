using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class SummaryRewardInfo
	{
		public string Desc { get; set; }

		public int Arg1 { get; set; }

		public int Arg2 { get; set; }

		public int Value { get; set; }

		public SummaryRewardInfo(string desc, int arg1, int arg2, int value)
		{
			this.Desc = desc;
			this.Arg1 = arg1;
			this.Arg2 = arg2;
			this.Value = value;
		}
	}
}
