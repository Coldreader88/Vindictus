using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.RandomItemRecipeClass")]
	public class RandomItemRecipeClass
	{
		[Column(Storage = "_RandomItemID", DbType = "Int NOT NULL")]
		public int RandomItemID
		{
			get
			{
				return this._RandomItemID;
			}
			set
			{
				if (this._RandomItemID != value)
				{
					this._RandomItemID = value;
				}
			}
		}

		[Column(Storage = "_ClassName", DbType = "NVarChar(128) NOT NULL", CanBeNull = false)]
		public string ClassName
		{
			get
			{
				return this._ClassName;
			}
			set
			{
				if (this._ClassName != value)
				{
					this._ClassName = value;
				}
			}
		}

		[Column(Storage = "_ExpectedAppearanceCount", DbType = "Int")]
		public int? ExpectedAppearanceCount
		{
			get
			{
				return this._ExpectedAppearanceCount;
			}
			set
			{
				if (this._ExpectedAppearanceCount != value)
				{
					this._ExpectedAppearanceCount = value;
				}
			}
		}

		[Column(Storage = "_Probability", DbType = "Float NOT NULL")]
		public double Probability
		{
			get
			{
				return this._Probability;
			}
			set
			{
				if (this._Probability != value)
				{
					this._Probability = value;
				}
			}
		}

		[Column(Storage = "_ItemNumber", DbType = "Int NOT NULL")]
		public int ItemNumber
		{
			get
			{
				return this._ItemNumber;
			}
			set
			{
				if (this._ItemNumber != value)
				{
					this._ItemNumber = value;
				}
			}
		}

		[Column(Storage = "_AdditionalRule", DbType = "VarChar(128)")]
		public string AdditionalRule
		{
			get
			{
				return this._AdditionalRule;
			}
			set
			{
				if (this._AdditionalRule != value)
				{
					this._AdditionalRule = value;
				}
			}
		}

		private int _RandomItemID;

		private string _ClassName;

		private int? _ExpectedAppearanceCount;

		private double _Probability;

		private int _ItemNumber;

		private string _AdditionalRule;
	}
}
