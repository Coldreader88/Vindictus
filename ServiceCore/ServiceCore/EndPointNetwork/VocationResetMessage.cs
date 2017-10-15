using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class VocationResetMessage : IMessage
	{
		public int VocationClass { get; set; }

		public override string ToString()
		{
			return string.Format("VocationResetMessage [ {0} ]", this.VocationClass);
		}
	}
}
