using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class CreateCharacterMessage : IMessage
	{
		public string Name { get; set; }

		public CharacterTemplate Template { get; set; }

		public bool UsePreset { get; set; }

		public bool IsPremium { get; set; }

		public override string ToString()
		{
			return string.Format("CreateCharacterMessage[ {0} ]", this.Name);
		}
	}
}
