using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.EnchantStatInfo")]
	public class EnchantStatInfo
	{
		[Column(Storage = "_EnchantClass", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
		public string EnchantClass
		{
			get
			{
				return this._EnchantClass;
			}
			set
			{
				if (this._EnchantClass != value)
				{
					this._EnchantClass = value;
				}
			}
		}

		[Column(Name = "[Order]", Storage = "_Order", DbType = "Int NOT NULL")]
		public int Order
		{
			get
			{
				return this._Order;
			}
			set
			{
				if (this._Order != value)
				{
					this._Order = value;
				}
			}
		}

		[Column(Storage = "_Condition", DbType = "NVarChar(1024)")]
		public string Condition
		{
			get
			{
				return this._Condition;
			}
			set
			{
				if (this._Condition != value)
				{
					this._Condition = value;
				}
			}
		}

		[Column(Storage = "_ConditionDesc", DbType = "NVarChar(256) NOT NULL", CanBeNull = false)]
		public string ConditionDesc
		{
			get
			{
				return this._ConditionDesc;
			}
			set
			{
				if (this._ConditionDesc != value)
				{
					this._ConditionDesc = value;
				}
			}
		}

		[Column(Storage = "_Stat", DbType = "NVarChar(1024) NOT NULL", CanBeNull = false)]
		public string Stat
		{
			get
			{
				return this._Stat;
			}
			set
			{
				if (this._Stat != value)
				{
					this._Stat = value;
				}
			}
		}

		[Column(Storage = "_StatDesc", DbType = "NVarChar(256) NOT NULL", CanBeNull = false)]
		public string StatDesc
		{
			get
			{
				return this._StatDesc;
			}
			set
			{
				if (this._StatDesc != value)
				{
					this._StatDesc = value;
				}
			}
		}

		private string _EnchantClass;

		private int _Order;

		private string _Condition;

		private string _ConditionDesc;

		private string _Stat;

		private string _StatDesc;
	}
}
