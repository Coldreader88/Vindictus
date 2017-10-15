using System;
using System.Windows.Forms;
using RemoteControlSystem;

namespace HeroesOpTool.RCUser.ServerMonitorSystem
{
	internal class WorkGroup<T> : WorkGroup where T : IWorkGroupCondition, new()
	{
		public WorkGroup(TreeView view) : base(view)
		{
		}

		public override string GetConditionKey(RCClient client, RCProcess process)
		{
			return this.dummy.ToString(client.Name, process.Name);
		}

		private T dummy = (default(T) == null) ? Activator.CreateInstance<T>() : default(T);
	}
}
