using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class NpcTalkEntity
	{
		public string Command { get; set; }

		public string Arg1 { get; set; }

		public string Arg2 { get; set; }

		public NpcTalkEntity(string command)
		{
			this.Command = command;
			this.Arg1 = "";
			this.Arg2 = "";
		}

		public NpcTalkEntity(string command, string arg1)
		{
			this.Command = command;
			this.Arg1 = (arg1 ?? "");
			this.Arg2 = "";
		}

		public NpcTalkEntity(string command, string arg1, string arg2)
		{
			this.Command = command;
			this.Arg1 = (arg1 ?? "");
			this.Arg2 = (arg2 ?? "");
		}

		public override string ToString()
		{
			if (this.Arg2 == "")
			{
				return string.Format("{0}({1})", this.Command, this.Arg1, this.Arg2);
			}
			return string.Format("{0}({1},{2})", this.Command, this.Arg1, this.Arg2);
		}
	}
}
