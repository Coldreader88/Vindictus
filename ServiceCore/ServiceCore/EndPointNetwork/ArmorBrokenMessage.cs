using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class ArmorBrokenMessage : IMessage
	{
		public int Owner
		{
			get
			{
				return this.owner;
			}
		}

		public int Part
		{
			get
			{
				return this.part;
			}
		}

		public ArmorBrokenMessage(int owner, int part)
		{
			this.owner = owner;
			this.part = part;
		}

		public override string ToString()
		{
			return string.Format("ArmorBrokenMessage[ owner = {0} part = {1} ]", this.owner, this.part);
		}

		private int owner;

		private int part;
	}
}
