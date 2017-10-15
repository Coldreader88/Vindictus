using System;

namespace ServiceCore.EndPointNetwork.CharacterList
{
	[Serializable]
	public sealed class DeleteCharacterCancelResultMessage : IMessage
	{
		public int CharacterSN { get; set; }

		public DeleteCharacterCancelResult Result { get; set; }

		public DeleteCharacterCancelResultMessage(int characterSN, DeleteCharacterCancelResult result)
		{
			this.CharacterSN = characterSN;
			this.Result = result;
		}

		public override string ToString()
		{
			return string.Format("DeleteCharacterCancelResultMessage[ {0} ]", this.Result);
		}
	}
}
