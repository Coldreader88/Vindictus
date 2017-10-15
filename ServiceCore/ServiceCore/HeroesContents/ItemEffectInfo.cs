using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.ItemEffectInfo")]
	public class ItemEffectInfo
	{
		[Column(Storage = "_Identifier", DbType = "Int NOT NULL")]
		public int Identifier
		{
			get
			{
				return this._Identifier;
			}
			set
			{
				if (this._Identifier != value)
				{
					this._Identifier = value;
				}
			}
		}

		[Column(Storage = "_Target", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
		public string Target
		{
			get
			{
				return this._Target;
			}
			set
			{
				if (this._Target != value)
				{
					this._Target = value;
				}
			}
		}

		[Column(Storage = "_Amount", DbType = "Int NOT NULL")]
		public int Amount
		{
			get
			{
				return this._Amount;
			}
			set
			{
				if (this._Amount != value)
				{
					this._Amount = value;
				}
			}
		}

		[Column(Storage = "_ItemID", DbType = "Int")]
		public int? ItemID
		{
			get
			{
				return this._ItemID;
			}
			set
			{
				if (this._ItemID != value)
				{
					this._ItemID = value;
				}
			}
		}

		private int _Identifier;

		private string _Target;

		private int _Amount;

		private int? _ItemID;
	}
}
