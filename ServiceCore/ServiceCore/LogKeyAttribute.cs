using System;

namespace ServiceCore
{
	public class LogKeyAttribute : Attribute
	{
		public string Table { get; set; }

		public string Coloumn { get; set; }

		public LogKeyAttribute(string table, string coloumn)
		{
			this.Table = table;
			this.Coloumn = coloumn;
		}
	}
}
