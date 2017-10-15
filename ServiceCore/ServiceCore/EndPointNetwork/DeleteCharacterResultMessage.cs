using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class DeleteCharacterResultMessage : IMessage
	{
		public int CharacterSN { get; set; }

		public DeleteCharacterResult Result { get; set; }

		public int RemainTimeSec { get; set; }

		public DeleteCharacterResultMessage(int characterSN, DeleteCharacterResult result)
		{
			this.CharacterSN = characterSN;
			this.Result = result;
			this.RemainTimeSec = 0;
		}

		public DeleteCharacterResultMessage(int characterSN, DeleteCharacterResult result, int remainTimeSec)
		{
			this.CharacterSN = characterSN;
			this.Result = result;
			this.RemainTimeSec = remainTimeSec;
		}

		public override string ToString()
		{
			return string.Format("DeleteCharacterResultMessage[ {0} RemainTimeSec = {1}]", this.Result, this.RemainTimeSec);
		}
	}
}
