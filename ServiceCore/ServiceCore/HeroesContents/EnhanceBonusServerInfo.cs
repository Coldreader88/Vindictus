using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.EnhanceBonusServerInfo")]
	public class EnhanceBonusServerInfo
	{
		[Column(Storage = "_ItemClass", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
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

		[Column(Storage = "_Bonus", DbType = "Float NOT NULL")]
		public double Bonus
		{
			get
			{
				return this._Bonus;
			}
			set
			{
				if (this._Bonus != value)
				{
					this._Bonus = value;
				}
			}
		}

		[Column(Storage = "_EnhanceMin", DbType = "SmallInt NOT NULL")]
		public short EnhanceMin
		{
			get
			{
				return this._EnhanceMin;
			}
			set
			{
				if (this._EnhanceMin != value)
				{
					this._EnhanceMin = value;
				}
			}
		}

		[Column(Storage = "_EnhanceMax", DbType = "SmallInt NOT NULL")]
		public short EnhanceMax
		{
			get
			{
				return this._EnhanceMax;
			}
			set
			{
				if (this._EnhanceMax != value)
				{
					this._EnhanceMax = value;
				}
			}
		}

		private string _ItemClass;

		private double _Bonus;

		private short _EnhanceMin;

		private short _EnhanceMax;
	}
}
