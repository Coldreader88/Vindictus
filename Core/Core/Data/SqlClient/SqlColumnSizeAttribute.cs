using System;

namespace Devcat.Core.Data.SqlClient
{
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
	public class SqlColumnSizeAttribute : Attribute
	{
		public int Size
		{
			get
			{
				return this.size;
			}
			set
			{
				this.size = value;
			}
		}

		public SqlColumnSizeAttribute(int size)
		{
			this.size = size;
		}

		private int size;
	}
}
