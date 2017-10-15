using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class RemoveServerStatusEffectMessage : IMessage
	{
		public string Type { get; set; }

		public override string ToString()
		{
			return string.Format("RemoveServerStatusEffectMessage[ type = {0} ]", this.Type);
		}
	}
}
