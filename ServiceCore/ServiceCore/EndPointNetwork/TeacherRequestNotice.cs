using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class TeacherRequestNotice : IMessage
	{
		public string CharacterName { get; set; }

		public bool IsNotice { get; set; }

		public override string ToString()
		{
			return string.Format("{0}({1}, {2})", base.ToString(), this.CharacterName, this.IsNotice);
		}
	}
}
