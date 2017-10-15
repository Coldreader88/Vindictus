using System;

namespace ServiceCore.EndPointNetwork.CharacterList
{
	[Serializable]
	public sealed class DeleteCharacterCancelMessage : IMessage
	{
		public string Name { get; set; }

		public int CharacterSN { get; set; }

		public override string ToString()
		{
			return string.Format("DeleteCharacterCancelMessage[ Name = {0} SN = {1} ]", this.Name, this.CharacterSN);
		}
	}
}
