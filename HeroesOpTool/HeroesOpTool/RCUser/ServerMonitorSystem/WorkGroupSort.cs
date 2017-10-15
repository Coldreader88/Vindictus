using System;
using System.Windows.Forms;

namespace HeroesOpTool.RCUser.ServerMonitorSystem
{
	internal class WorkGroupSort
	{
		public SortType SortType { get; set; }

		public WorkGroup Current { get; set; }

		public ListView ShowListView { get; set; }
	}
}
