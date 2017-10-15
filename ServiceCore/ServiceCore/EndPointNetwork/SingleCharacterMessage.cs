using System;
using System.Collections.Generic;
using System.Text;
using ServiceCore.CharacterServiceOperations;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class SingleCharacterMessage : IMessage
	{
		public SingleCharacterMessage(CharacterSummary character, int loginPartyState)
		{
			this.Characters = new List<CharacterSummary>();
			this.Characters.Add(character);
			this.LoginPartyState = new List<int>();
			this.LoginPartyState.Add(loginPartyState);
		}

		public ICollection<CharacterSummary> Characters { get; private set; }

		public ICollection<int> LoginPartyState { get; private set; }

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder("SingleCharacterMessage{ characters = {");
			int num = 0;
			foreach (CharacterSummary value in this.Characters)
			{
				stringBuilder.Append((num++ == 0) ? " " : ", ");
				stringBuilder.Append(value);
			}
			stringBuilder.Append(" } }");
			return stringBuilder.ToString();
		}
	}
}
