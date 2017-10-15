using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class CheckCharacterNameResultMessage : IMessage
	{
		public bool Valid { get; set; }

		public string Name { get; set; }

		public string ErrorMsg { get; set; }

		public CheckCharacterNameResultMessage(bool valid, string name)
		{
			this.Valid = valid;
			this.Name = name;
			this.ErrorMsg = null;
		}

		public CheckCharacterNameResultMessage(bool valid, string name, string errorMsg)
		{
			this.Valid = valid;
			this.Name = name;
			this.ErrorMsg = errorMsg;
		}

		public override string ToString()
		{
			return string.Format("CheckCharacterNameResultMessage [ Valid = {0}, Name = {1}, ErrorMsg = {2} ]", this.Valid, this.Name, this.ErrorMsg);
		}
	}
}
