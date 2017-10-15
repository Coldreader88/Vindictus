using System;

namespace ServiceCore.EndPointNetwork.GuildService
{
	[Serializable]
	public sealed class GuildOperationInfo
	{
		public string Command { get; set; }

		public string Target { get; set; }

		public int Value { get; set; }

		public override string ToString()
		{
			return string.Format("{0} {1} {2}", this.Command, this.Target, this.Value);
		}
	}
}
