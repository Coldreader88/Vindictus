using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class CreateCharacterFailMessage : IMessage
	{
		public int ErrorCode { get; set; }

		public CreateCharacterFailMessage(CreateCharacterResult errorcode)
		{
			this.ErrorCode = (int)errorcode;
		}

		public override string ToString()
		{
			return string.Format("CreateCharacterFailMessage[ ErrorCode = {0}]", (CreateCharacterResult)this.ErrorCode);
		}
	}
}
