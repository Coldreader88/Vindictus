using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class CharacterNameChangeMessage : IMessage
	{
		public int Result { get; set; }

		public long CID { get; set; }

		public string Name { get; set; }

		public bool IsTrans { get; set; }

		public CharacterNameChangeMessage(CharacterNameChangeResult result, long cid, string name, bool isTrans)
		{
			this.Result = (int)result;
			this.CID = cid;
			this.Name = name;
			this.IsTrans = isTrans;
		}

		public override string ToString()
		{
			return string.Format("CharacterNameChangeMessage [ Result = {0}, CID = {1}, Name = {2}, IsTrans = {3} ]", new object[]
			{
				this.Result,
				this.CID,
				this.Name,
				this.IsTrans
			});
		}
	}
}
