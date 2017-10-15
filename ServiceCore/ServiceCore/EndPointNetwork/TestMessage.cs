using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class TestMessage : IMessage
	{
		public string Name { get; set; }

		public int ID { get; set; }

		public TestMessage()
		{
			this.Name = "";
		}

		public TestMessage(string n, int i)
		{
			this.Name = n;
			this.ID = i;
		}

		public override string ToString()
		{
			return string.Format("TestMessage[ name = {0} id = {1} ]", this.Name, this.ID);
		}
	}
}
