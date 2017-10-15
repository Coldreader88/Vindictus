using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.SynthesisInfo")]
	public class SynthesisInfo
	{
		[Column(Storage = "_ItemLevel", DbType = "Int NOT NULL")]
		public int ItemLevel
		{
			get
			{
				return this._ItemLevel;
			}
			set
			{
				if (this._ItemLevel != value)
				{
					this._ItemLevel = value;
				}
			}
		}

		[Column(Storage = "_MaterialItemClass", DbType = "NVarChar(128)")]
		public string MaterialItemClass
		{
			get
			{
				return this._MaterialItemClass;
			}
			set
			{
				if (this._MaterialItemClass != value)
				{
					this._MaterialItemClass = value;
				}
			}
		}

		[Column(Storage = "_MaterialItemCount", DbType = "Int NOT NULL")]
		public int MaterialItemCount
		{
			get
			{
				return this._MaterialItemCount;
			}
			set
			{
				if (this._MaterialItemCount != value)
				{
					this._MaterialItemCount = value;
				}
			}
		}

		[Column(Storage = "_SuccessRatio", DbType = "Int NOT NULL")]
		public int SuccessRatio
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

		private int _ItemLevel;

		private string _MaterialItemClass;

		private int _MaterialItemCount;

		private int _SuccessRatio;
	}
}
