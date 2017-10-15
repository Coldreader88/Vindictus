using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class CostumeUpdateMessage : IMessage
	{
		public CostumeInfo CostumeInfo
		{
			get
			{
				return this.info;
			}
		}

		public CostumeUpdateMessage(CostumeInfo init)
		{
			this.info = init;
		}

		public override string ToString()
		{
			return string.Format("CostumeUpdateMessage[ info = {0} ]", this.info);
		}

		private CostumeInfo info;
	}
}
