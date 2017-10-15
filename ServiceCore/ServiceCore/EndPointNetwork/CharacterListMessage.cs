using System;
using System.Collections.Generic;
using System.Text;
using ServiceCore.CharacterServiceOperations;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class CharacterListMessage : IMessage
	{
		public CharacterListMessage(ICollection<CharacterSummary> characters, int maxFreeCharacterCount, int maxPurchasedCharacterCount, int maxPremiumCharactersCount, bool prologuePlayed, int presetUsedCount, ICollection<int> loginPartyState)
		{
			this.Characters = characters;
			this.MaxFreeCharacterCount = maxFreeCharacterCount;
			this.MaxPurchasedCharacterCount = maxPurchasedCharacterCount;
			this.MaxPremiumCharacters = maxPremiumCharactersCount;
			this.ProloguePlayed = (prologuePlayed ? (byte) 1 : (byte) 0);
			this.PresetUsedCharacterCount = presetUsedCount;
			this.LoginPartyState = loginPartyState;
		}

		public ICollection<CharacterSummary> Characters { get; private set; }

		public int MaxFreeCharacterCount { get; private set; }

		public int MaxPurchasedCharacterCount { get; private set; }

		public int MaxPremiumCharacters { get; private set; }

		public byte ProloguePlayed { get; private set; }

		public int PresetUsedCharacterCount { get; private set; }

		public ICollection<int> LoginPartyState { get; private set; }

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder("CharacterListMessage{ characters = {");
			int num = 0;
			foreach (CharacterSummary value in this.Characters)
			{
				stringBuilder.Append((num++ == 0) ? " " : ", ");
				stringBuilder.Append(value);
			}
			stringBuilder.Append(" } }");
			return stringBuilder.ToString();
		}

		public enum LoginPartyStateType
		{
			None,
			Normal
		}
	}
}
