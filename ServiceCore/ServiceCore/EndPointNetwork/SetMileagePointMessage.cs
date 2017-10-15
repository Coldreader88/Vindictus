using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class SetMileagePointMessage : IMessage
	{
		public SetMileagePointMessage(int mileagePoint)
		{
			this.MileagePoint = mileagePoint;
		}

		public override string ToString()
		{
			return string.Format("SetMileagePointMessage[ ]", new object[0]);
		}

		private int MileagePoint;
	}
}
