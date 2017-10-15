using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class HeroesString
	{
		public string Format { get; set; }

		public ICollection<string> Parameters { get; set; }

		public HeroesString(string format)
		{
			this.Format = format;
			this.Parameters = new string[0];
		}

		public HeroesString(string format, params string[] parameters)
		{
			this.Format = format;
			this.Parameters = parameters;
		}

		public HeroesString(string format, params object[] parameters)
		{
			this.Format = format;
			this.Parameters = (from obj in parameters
			select obj.ToString()).ToArray<string>();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(this.Format);
			bool flag = true;
			stringBuilder.Append("(");
			foreach (string value in this.Parameters)
			{
				if (flag)
				{
					flag = false;
				}
				else
				{
					stringBuilder.Append(", ");
				}
				stringBuilder.Append(value);
			}
			stringBuilder.Append(")");
			return stringBuilder.ToString();
		}
	}
}
