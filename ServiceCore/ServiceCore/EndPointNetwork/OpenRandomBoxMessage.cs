using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class OpenRandomBoxMessage : IMessage
	{
		public int GroupID { get; set; }

		public string RandomBoxName { get; set; }

		public override string ToString()
		{
			return string.Format("OpenRandomBoxMessage[ GroupID = {0} RandomBoxName = {1} ]", this.GroupID, this.RandomBoxName);
		}
	}
}
