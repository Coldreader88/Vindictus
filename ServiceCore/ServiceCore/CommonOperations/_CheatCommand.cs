using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.CommonOperations
{
	[Serializable]
	public sealed class _CheatCommand : Operation
	{
		public string Command { get; set; }

		public string Arg { get; set; }

		public long FrontendID { get; set; }

		public _CheatCommand(string command, long frontendID)
		{
			this.FrontendID = frontendID;
			this.Command = command.Split(new char[]
			{
				' '
			})[0];
			if (command == this.Command)
			{
				this.Arg = "";
				return;
			}
			this.Arg = command.Substring(this.Command.Length).Trim();
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}

		private int ToInt(string str, int defaultValue)
		{
			if (str == null)
			{
				return defaultValue;
			}
			int result;
			if (int.TryParse(str, out result))
			{
				return result;
			}
			return defaultValue;
		}

		private long ToLong(string str, long defaultValue)
		{
			if (str == null)
			{
				return defaultValue;
			}
			long result;
			if (long.TryParse(str, out result))
			{
				return result;
			}
			return defaultValue;
		}

		public int GetArgCount()
		{
			this.Arg.Trim().Split(new char[]
			{
				' '
			});
			return this.Arg.Length;
		}

		public string GetArgString(int i, string defaultValue)
		{
			string[] array = this.Arg.Split(new char[]
			{
				' '
			});
			if (0 > i || i >= array.Length)
			{
				return defaultValue;
			}
			if (array[i] == "(null)")
			{
				return defaultValue;
			}
			return array[i].Trim().Trim(new char[]
			{
				'"'
			});
		}

		public List<int> GetArgInts()
		{
			List<int> list = new List<int>();
			string[] array = this.Arg.Split(new char[]
			{
				' '
			});
			for (int i = 1; i < array.Length; i++)
			{
				list.Add(this.ToInt(array[i].Trim(), 0));
			}
			return list;
		}

		public string GetArgStringNotEmpty(int i, string defaultValue)
		{
			string[] array = this.Arg.Split(new char[]
			{
				' '
			});
			if (0 <= i && i < array.Length && array[i].Trim() != "")
			{
				return array[i].Trim();
			}
			return defaultValue;
		}

		public int GetArgInt(int i, int defaultValue)
		{
			return this.ToInt(this.GetArgString(i, null), defaultValue);
		}

		public long GetArgInt64(int i, long defaultValue)
		{
			return this.ToLong(this.GetArgString(i, null), defaultValue);
		}

		public string GetArgLong(int i, string defaultValue)
		{
			string[] array = this.Arg.Split(new char[]
			{
				' '
			});
			if (0 <= i && i < array.Length)
			{
				return array[i];
			}
			return "";
		}
	}
}
