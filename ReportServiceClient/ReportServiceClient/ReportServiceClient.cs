using System;
using System.Windows.Forms;

namespace ReportServiceClient
{
	internal static class ReportServiceClient
	{
		[STAThread]
		private static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new ReportServiceClientForm());
			Environment.Exit(0);
		}
	}
}
