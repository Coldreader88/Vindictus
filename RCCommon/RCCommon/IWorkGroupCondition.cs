using System;

namespace RemoteControlSystem
{
	public interface IWorkGroupCondition : IComparable
	{
		string ToString(string clientName, string processName);
	}
}
