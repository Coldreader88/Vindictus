using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.VariableStatInfo")]
	public class VariableStatInfo
	{
		[Column(Storage = "_ItemClass", DbType = "NVarChar(128) NOT NULL", CanBeNull = false)]
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

		[Column(Storage = "_VariableStat", DbType = "NVarChar(256) NOT NULL", CanBeNull = false)]
		public string VariableStat
		{
			get
			{
				return this._VariableStat;
			}
			set
			{
				if (this._VariableStat != value)
				{
					this._VariableStat = value;
				}
			}
		}

		[Column(Storage = "_Feature", DbType = "NVarChar(128)")]
		public string Feature
		{
			get
			{
				return this._Feature;
			}
			set
			{
				if (this._Feature != value)
				{
					this._Feature = value;
				}
			}
		}

		private string _ItemClass;

		private string _VariableStat;

		private string _Feature;
	}
}
