using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class CIDByNameMessage : IMessage
	{
		public CIDByNameMessage(long cid, string name, int result, bool isEqualAccount, int level, int characterClass)
		{
			this.CID = cid;
			this.Name = name;
			this.Result = result;
			this.IsEqualAccount = isEqualAccount;
			this.Level = level;
			this.CharacterClass = characterClass;
		}

		public long CID { get; set; }

		public string Name { get; set; }

		public int Result { get; set; }

		public bool IsEqualAccount { get; set; }

		public int Level { get; set; }

		public int CharacterClass { get; set; }

		public override string ToString()
		{
			return string.Format("CIDByNameMessage[{0}, Name{1}]", this.CID, this.Name);
		}

		public enum CIDByNameMessageResult
		{
			CIDByNameResult_Begin,
			Success,
			UnknownCharacter,
			EquipIImpossible,
			CIDByNameResult_End
		}
	}
}
