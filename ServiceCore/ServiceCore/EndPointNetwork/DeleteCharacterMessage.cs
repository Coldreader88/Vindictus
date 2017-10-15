using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class DeleteCharacterMessage : IMessage
	{
		public string Name { get; set; }

		public int CharacterSN { get; set; }

		public override string ToString()
		{
			return string.Format("DeleteCharacterMessage[ Name = {0} SN = {1} ]", this.Name, this.CharacterSN);
		}
	}
}
