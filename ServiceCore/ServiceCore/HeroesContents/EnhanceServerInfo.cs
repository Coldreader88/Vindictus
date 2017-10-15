using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.EnhanceServerInfo")]
	public class EnhanceServerInfo
	{
		[Column(Storage = "_EnhanceType", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
		public string EnhanceType
		{
			get
			{
				return this._EnhanceType;
			}
			set
			{
				if (this._EnhanceType != value)
				{
					this._EnhanceType = value;
				}
			}
		}

		[Column(Storage = "_EnhanceLevel", DbType = "Int NOT NULL")]
		public int EnhanceLevel
		{
			get
			{
				return this._EnhanceLevel;
			}
			set
			{
				if (this._EnhanceLevel != value)
				{
					this._EnhanceLevel = value;
				}
			}
		}

		[Column(Storage = "_SuccessRatio", DbType = "Real NOT NULL")]
		public float SuccessRatio
		{
			get
			{
				return this._SuccessRatio;
			}
			set
			{
				if (this._SuccessRatio != value)
				{
					this._SuccessRatio = value;
				}
			}
		}

		[Column(Storage = "_Feature", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
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

		private string _EnhanceType;

		private int _EnhanceLevel;

		private float _SuccessRatio;

		private string _Feature;
	}
}
