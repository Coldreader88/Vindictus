using System;

namespace ServiceCore
{
	public class CmdTokenizer
	{
		public CmdTokenizer(string text)
		{
			this.original = text;
			this.remained = text;
		}

		public string GetNext()
		{
			string result;
			if (this.GetNext(out result))
			{
				return result;
			}
			return "";
		}

		public bool GetNext(out string token)
		{
			token = "";
			this.remained.Trim();
			if (this.remained.Length <= 0)
			{
				return false;
			}
			if (this.remained[0] == '"')
			{
				int num = this.remained.IndexOf('"', 1);
				if (num != -1)
				{
					token = this.remained.Substring(1, num - 1).Trim();
					if (this.remained.Length > num + 1)
					{
						this.remained = this.remained.Substring(num + 1).Trim();
					}
					else
					{
						this.remained = "";
					}
				}
				else
				{
					token = this.remained.Substring(1);
					this.remained = "";
				}
				return token.Length > 0;
			}
			int num2 = this.remained.IndexOf(' ');
			if (num2 != -1)
			{
				token = this.remained.Substring(0, num2);
				this.remained = this.remained.Substring(num2).Trim();
			}
			else
			{
				token = this.remained;
				this.remained = "";
			}
			return token.Length > 0;
		}

		private string original;

		private string remained;
	}
}
