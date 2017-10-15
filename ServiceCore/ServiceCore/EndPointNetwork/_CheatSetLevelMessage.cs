using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class _CheatSetLevelMessage : IMessage
	{
		public int Level { get; set; }

		public int ExpPercent { get; set; }

		public _CheatSetLevelMessage(int Level, int ExpPercent)
		{
			this.Level = Level;
			this.ExpPercent = ExpPercent;
		}

		public string ToStringForQA()
		{
			return string.Format("set level {0} {1}", this.Level, this.ExpPercent);
		}
	}
}
