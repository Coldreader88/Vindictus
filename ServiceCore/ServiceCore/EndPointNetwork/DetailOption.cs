using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class DetailOption : IMessage
	{
		public string Key { get; set; }

		public int Value { get; set; }

		public byte SearchType { get; set; }

		public DetailOption(string key, int value, byte type = 0)
		{
			this.Key = key;
			this.Value = value;
			this.SearchType = type;
		}
	}
}
