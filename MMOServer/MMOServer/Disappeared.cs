using System;

namespace MMOServer
{
	public class Disappeared : IMessage
	{
		public long ID { get; private set; }

		internal Disappeared(long id)
		{
			this.ID = id;
		}

		public override string ToString()
		{
			return string.Format("Disappeared {0}", this.ID);
		}
	}
}
