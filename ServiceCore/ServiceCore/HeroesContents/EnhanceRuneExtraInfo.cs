using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.EnhanceRuneExtraInfo")]
	public class EnhanceRuneExtraInfo
	{
		[Column(Storage = "_ReleationNum", DbType = "Int NOT NULL")]
		public int ReleationNum
		{
			get
			{
				return this._ReleationNum;
			}
			set
			{
				if (this._ReleationNum != value)
				{
					this._ReleationNum = value;
				}
			}
		}

		[Column(Storage = "_SuccessRatio", DbType = "Float NOT NULL")]
		public double SuccessRatio
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

		[Column(Storage = "_IsUseItemSuccessRatio", DbType = "Bit NOT NULL")]
		public bool IsUseItemSuccessRatio
		{
			get
			{
				return this._IsUseItemSuccessRatio;
			}
			set
			{
				if (this._IsUseItemSuccessRatio != value)
				{
					this._IsUseItemSuccessRatio = value;
				}
			}
		}

		[Column(Storage = "_IsDestoryOnSuccess", DbType = "Bit NOT NULL")]
		public bool IsDestoryOnSuccess
		{
			get
			{
				return this._IsDestoryOnSuccess;
			}
			set
			{
				if (this._IsDestoryOnSuccess != value)
				{
					this._IsDestoryOnSuccess = value;
				}
			}
		}

		[Column(Storage = "_IsResetRankOnFail", DbType = "Bit NOT NULL")]
		public bool IsResetRankOnFail
		{
			get
			{
				return this._IsResetRankOnFail;
			}
			set
			{
				if (this._IsResetRankOnFail != value)
				{
					this._IsResetRankOnFail = value;
				}
			}
		}

		[Column(Storage = "_ForceRankDownOnFail", DbType = "Int NOT NULL")]
		public int ForceRankDownOnFail
		{
			get
			{
				return this._ForceRankDownOnFail;
			}
			set
			{
				if (this._ForceRankDownOnFail != value)
				{
					this._ForceRankDownOnFail = value;
				}
			}
		}

		private int _ReleationNum;

		private double _SuccessRatio;

		private bool _IsUseItemSuccessRatio;

		private bool _IsDestoryOnSuccess;

		private bool _IsResetRankOnFail;

		private int _ForceRankDownOnFail;
	}
}
