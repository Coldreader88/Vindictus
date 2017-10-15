using System;

namespace RemoteControlSystem
{
	public class ServerGroupCondition : IWorkGroupCondition, IComparable
	{
		public string ClientName { get; private set; }

		public ServerGroupCondition()
		{
		}

		public ServerGroupCondition(string clientName)
		{
			if (clientName == null)
			{
				throw new ArgumentNullException("ClientName", "ClientName cannot be null!");
			}
			this.ClientName = clientName;
		}

		public override string ToString()
		{
			return this.ToString(this.ClientName, "");
		}

		public string ToString(string clientName, string processName)
		{
			return string.Format("{0}", clientName);
		}

		public static implicit operator string(ServerGroupCondition target)
		{
			return target.ClientName;
		}

		public static implicit operator ServerGroupCondition(string target)
		{
			if (target == null || target == string.Empty)
			{
				return null;
			}
			return new ServerGroupCondition(target);
		}

		public int CompareTo(object obj)
		{
			return this.ClientName.CompareTo(((ServerGroupCondition)obj).ClientName);
		}
	}
}
