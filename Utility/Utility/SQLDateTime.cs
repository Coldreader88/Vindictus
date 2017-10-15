using System;
using System.Data.SqlTypes;

namespace Utility
{
	public static class SQLDateTime
	{
		public static readonly DateTime MinValue = (DateTime)SqlDateTime.MinValue;
	}
}
