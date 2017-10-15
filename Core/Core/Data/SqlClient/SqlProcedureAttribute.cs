using System;

namespace Devcat.Core.Data.SqlClient
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false, Inherited = false)]
	public class SqlProcedureAttribute : Attribute
	{
		public string ProcedureName
		{
			get
			{
				return this.procedureName;
			}
			set
			{
				this.procedureName = value;
			}
		}

		public Type LinkType
		{
			get
			{
				return this.linkType;
			}
			set
			{
				this.linkType = value;
			}
		}

		public SqlProcedureAttribute(string procedureName, Type linkType)
		{
			this.procedureName = procedureName;
			this.linkType = linkType;
		}

		private string procedureName;

		private Type linkType;
	}
}
