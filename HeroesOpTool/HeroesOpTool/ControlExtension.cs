using System;
using System.Windows.Forms;

namespace HeroesOpTool
{
	internal static class ControlExtension
	{
		public static void UIThread(this Control conrtol, MethodInvoker code)
		{
			if (conrtol.InvokeRequired)
			{
				conrtol.Invoke(code);
				return;
			}
			code();
		}
	}
}
