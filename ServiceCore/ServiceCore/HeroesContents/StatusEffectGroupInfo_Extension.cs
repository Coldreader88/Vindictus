using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.StatusEffectGroupInfo_Extension")]
	public class StatusEffectGroupInfo_Extension
	{
		[Column(Storage = "_EffectID", DbType = "Text", UpdateCheck = UpdateCheck.Never)]
		public string EffectID
		{
			get
			{
				return this._EffectID;
			}
			set
			{
				if (this._EffectID != value)
				{
					this._EffectID = value;
				}
			}
		}

		[Column(Storage = "_ServerEffect", DbType = "Text", UpdateCheck = UpdateCheck.Never)]
		public string ServerEffect
		{
			get
			{
				return this._ServerEffect;
			}
			set
			{
				if (this._ServerEffect != value)
				{
					this._ServerEffect = value;
				}
			}
		}

		[Column(Storage = "_Feature", DbType = "NVarChar(50)")]
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

		private string _EffectID;

		private string _ServerEffect;

		private string _Feature;
	}
}
