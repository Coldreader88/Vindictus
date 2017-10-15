using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace Utility
{
	public static class CallerInfo
	{
		public static string Get([CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = -1)
		{
			string fileName = Path.GetFileName(Path.GetDirectoryName(filePath));
			return string.Format("{0}\\{1}:{2}", fileName, Path.GetFileName(filePath), lineNumber);
		}
	}
}
