using System;
using System.Windows.Forms;

namespace ExecutionSupporter
{
	internal static class ExecutionSupporterProgram
	{
		[STAThread]
		private static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new ExecutionSupporterForm());
			Environment.Exit(0);
		}
	}
}
