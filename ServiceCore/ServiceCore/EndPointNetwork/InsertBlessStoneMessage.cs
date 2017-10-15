using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class InsertBlessStoneMessage : IMessage
	{
		public BlessStoneType StoneType { get; set; }

		public bool IsInsert { get; set; }

		public int RemainFatigue { get; set; }

		public override string ToString()
		{
			return string.Format("InsertBlessStoneMessage[ {0} x {1} / {2}]", this.StoneType.ToString(), this.IsInsert ? 1 : -1, this.RemainFatigue);
		}
	}
}
