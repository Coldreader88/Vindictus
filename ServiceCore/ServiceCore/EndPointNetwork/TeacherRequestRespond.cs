using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class TeacherRequestRespond : IMessage
	{
		public bool Accepted { get; set; }

		public override string ToString()
		{
			return string.Format("{0}({1})", base.ToString(), this.Accepted);
		}
	}
}
