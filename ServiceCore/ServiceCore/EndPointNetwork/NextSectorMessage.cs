using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class NextSectorMessage : IMessage
	{
		public string OnSectorStart { get; set; }

		public NextSectorMessage(string onSectorStart)
		{
			this.OnSectorStart = onSectorStart;
		}

		public override string ToString()
		{
			return string.Format("NextSectorMessage [ {0} ]", this.OnSectorStart);
		}
	}
}
