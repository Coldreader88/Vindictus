using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class EffectTelepathyMessage : IMessage
	{
		public bool IsEffectFail { get; set; }

		public int Level { get; set; }

		public string EffectName { get; set; }
	}
}
