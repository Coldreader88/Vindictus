using System;

namespace ServiceCore.CharacterServiceOperations
{
	[Serializable]
	public sealed class PvpResultInfo
	{
		public string PvpType { get; set; }

		public int Win { get; set; }

		public int Draw { get; set; }

		public int Lose { get; set; }

		public PvpResultInfo(string PvpType, int Win, int Draw, int Lose)
		{
			this.PvpType = PvpType;
			this.Win = Win;
			this.Draw = Draw;
			this.Lose = Lose;
		}

		public override string ToString()
		{
			return string.Format("PvpResult({0} {1} {2} {3})", new object[]
			{
				this.PvpType,
				this.Win,
				this.Draw,
				this.Lose
			});
		}
	}
}
