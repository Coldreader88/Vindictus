using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class SelectCharacterMessage : IMessage
	{
		public int Index
		{
			get
			{
				return this.index;
			}
		}

		public SelectCharacterMessage(int index)
		{
			this.index = index;
		}

		public override string ToString()
		{
			return string.Format("SelectCharacterMessage[ serialNumber = {0} ]", this.index);
		}

		private int index;
	}
}
