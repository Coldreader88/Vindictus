using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class SpiritInjectionItemResultMessage : IMessage
	{
		public SpiritInjectionItemResult Result { get; private set; }

		public string StatName { get; private set; }

		public int Value { get; private set; }

		public SpiritInjectionItemResultMessage(SpiritInjectionItemResult result, string statName, int value)
		{
			this.Result = result;
			this.StatName = statName;
			this.Value = value;
		}

		public override string ToString()
		{
			return string.Format("SpiritInjectionItemResultMessage[ Result = {0}, StatName = {1}, Value = {2} ]", this.Result, this.StatName, this.Value);
		}
	}
}
