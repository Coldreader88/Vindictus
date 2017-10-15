using System;
using ServiceCore.CharacterServiceOperations;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class CharacterCommonInfoMessage : IMessage
	{
		public long QueryID
		{
			set
			{
				this.queryID = value;
			}
		}

		public CharacterSummary Info
		{
			get
			{
				return this.info;
			}
		}

		public CharacterCommonInfoMessage(CharacterSummary info)
		{
			this.info = info;
		}

		public override string ToString()
		{
			return string.Format("CharacterCommonInfoMessage [ info = {0} ]", this.info);
		}

		private long queryID;

		private CharacterSummary info;
	}
}
