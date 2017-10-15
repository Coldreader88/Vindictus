using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class CheckCharacterNameMessage : IMessage
	{
		public string Name { get; set; }

		public bool IsNameChange { get; set; }

		public override string ToString()
		{
			return string.Format("CheckCharacterNameMessage [ Name = {0}, IsNameChange = {1} ]", this.Name, this.IsNameChange);
		}
	}
}
