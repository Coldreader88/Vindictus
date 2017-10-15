using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class TeacherRequestResult : IMessage
	{
		public bool Accepted { get; set; }

		public string AcceptedUserName { get; set; }

		public override string ToString()
		{
			return string.Format("{0}({1}, {2})", base.ToString(), this.Accepted, this.AcceptedUserName);
		}
	}
}
