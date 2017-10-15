using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class PickErgMessage : IMessage
	{
		public int ParentProp
		{
			get
			{
				return this.prop;
			}
		}

		public PickErgMessage(int prop)
		{
			this.prop = prop;
		}

		public override string ToString()
		{
			return string.Format("PickErgMessage[ prop = {0} ]", this.prop);
		}

		private int prop;
	}
}
