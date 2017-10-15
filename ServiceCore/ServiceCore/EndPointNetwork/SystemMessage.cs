using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class SystemMessage : IMessage
	{
		public byte Category { get; set; }

		public HeroesString Message { get; set; }

		public SystemMessage(SystemMessageCategory category, HeroesString msg)
		{
			this.Message = msg;
			this.Category = (byte)category;
		}

		public SystemMessage(SystemMessageCategory category, string msg)
		{
			this.Message = new HeroesString(msg);
			this.Category = (byte)category;
		}

		public SystemMessage(SystemMessageCategory category, string msg, params string[] parameters)
		{
			this.Message = new HeroesString(msg, parameters);
			this.Category = (byte)category;
		}

		public SystemMessage(SystemMessageCategory category, string msg, params object[] parameters)
		{
			this.Message = new HeroesString(msg, parameters);
			this.Category = (byte)category;
		}

		public override string ToString()
		{
			return string.Format("SystemMessage[ Category = {0} message = {1} ]", this.Category, this.Message);
		}
	}
}
