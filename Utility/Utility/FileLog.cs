using System;
using System.IO;

namespace Utility
{
	public class FileLog
	{
		public static void Log(string filename, string log)
		{
			try
			{
				using (FileStream fileStream = new FileStream(filename, FileMode.Append, FileAccess.Write))
				{
					using (StreamWriter streamWriter = new StreamWriter(fileStream))
					{
						streamWriter.Write("[{0}]", DateTime.Now);
						streamWriter.WriteLine(log);
					}
				}
			}
			catch (Exception ex)
			{
				Log<FileLog>.Logger.Error("Error in Log ", ex);
			}
		}
	}
}
