using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class SetFeverPointMessage : IMessage
	{
		public SetFeverPointMessage(int feverPoint)
		{
			this.FeverPoint = feverPoint;
		}

		public override string ToString()
		{
			return string.Format("SetFeverPointMessage[ ]", new object[0]);
		}

		private int FeverPoint;
	}
}
