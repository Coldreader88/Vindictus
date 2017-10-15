using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class DyeAmpleUsedMessage : IMessage
	{
		public string ColorTable { get; set; }

		public int Seed1 { get; set; }

		public int Seed2 { get; set; }

		public int Seed3 { get; set; }

		public int Seed4 { get; set; }

		public DyeAmpleUsedMessage(string colorTable, int[] seed)
		{
			this.ColorTable = colorTable;
			if (seed.Length == 4)
			{
				this.Seed1 = seed[0];
				this.Seed2 = seed[1];
				this.Seed3 = seed[2];
				this.Seed4 = seed[3];
			}
		}

		public override string ToString()
		{
			return string.Format("DyeAmpleUsedMessage[ colorTable = {0} ]", this.ColorTable);
		}
	}
}
