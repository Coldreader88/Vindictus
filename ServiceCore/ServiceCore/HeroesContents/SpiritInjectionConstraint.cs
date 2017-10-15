using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.SpiritInjectionConstraint")]
	public class SpiritInjectionConstraint
	{
		[Column(Storage = "_ItemClass", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
		public string ItemClass
		{
			get
			{
				return this._ItemClass;
			}
			set
			{
				if (this._ItemClass != value)
				{
					this._ItemClass = value;
				}
			}
		}

		[Column(Storage = "_ItemConstraint", DbType = "NVarChar(1024)")]
		public string ItemConstraint
		{
			get
			{
				return this._ItemConstraint;
			}
			set
			{
				if (this._ItemConstraint != value)
				{
					this._ItemConstraint = value;
				}
			}
		}

		private string _ItemClass;

		private string _ItemConstraint;
	}
}
