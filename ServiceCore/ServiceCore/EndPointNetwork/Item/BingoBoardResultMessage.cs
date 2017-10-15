using System;
using System.Collections.Generic;

namespace ServiceCore.EndPointNetwork.Item
{
	[Serializable]
	public sealed class BingoBoardResultMessage : IMessage
	{
		public BingoBoardResultMessage(BingoBoardResultMessage.Bingo_Result result, List<int> bingoboardnumbers)
		{
			this.Result = (int)result;
			this.BingoBoardNumbers = bingoboardnumbers;
			if (this.BingoBoardNumbers == null)
			{
				this.BingoBoardNumbers = new List<int>();
			}
		}

		public override string ToString()
		{
			string str = "";
			if (this.BingoBoardNumbers != null)
			{
				for (int i = 0; i < this.BingoBoardNumbers.Count; i++)
				{
					str = str + this.BingoBoardNumbers[i].ToString() + ",";
				}
			}
			return str + "[ Result : " + this.Result.ToString() + "]";
		}

		public List<int> BingoBoardNumbers;

		public int Result;

		public enum Bingo_Result
		{
			Result_Bingo_InitError,
			Result_Bingo_Expired,
			Result_Bingo_FeatureOff,
			Result_Bingo_Ok,
			Result_Bingo_Completed,
			Result_Bingo_RewardProcess,
			Result_Bingo_Loading
		}
	}
}
