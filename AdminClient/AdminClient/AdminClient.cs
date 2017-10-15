using System;
using System.Windows.Forms;

namespace AdminClient
{
	internal static class AdminClient
	{
		[STAThread]
		private static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new AdminClientForm());
			Environment.Exit(0);
		}
	}
}
