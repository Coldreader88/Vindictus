using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.MarbleNodeInfo")]
	public class MarbleNodeInfo
	{
		[Column(Storage = "_MarbleID", DbType = "Int NOT NULL")]
		public int MarbleID
		{
			get
			{
				return this._MarbleID;
			}
			set
			{
				if (this._MarbleID != value)
				{
					this._MarbleID = value;
				}
			}
		}

		[Column(Storage = "_NodeIndex", DbType = "Int NOT NULL")]
		public int NodeIndex
		{
			get
			{
				return this._NodeIndex;
			}
			set
			{
				if (this._NodeIndex != value)
				{
					this._NodeIndex = value;
				}
			}
		}

		[Column(Storage = "_NodeGrade", DbType = "Int NOT NULL")]
		public int NodeGrade
		{
			get
			{
				return this._NodeGrade;
			}
			set
			{
				if (this._NodeGrade != value)
				{
					this._NodeGrade = value;
				}
			}
		}

		[Column(Storage = "_Action", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
		public string Action
		{
			get
			{
				return this._Action;
			}
			set
			{
				if (this._Action != value)
				{
					this._Action = value;
				}
			}
		}

		[Column(Storage = "_Command", DbType = "NVarChar(256)")]
		public string Command
		{
			get
			{
				return this._Command;
			}
			set
			{
				if (this._Command != value)
				{
					this._Command = value;
				}
			}
		}

		[Column(Name = "[Desc]", Storage = "_Desc", DbType = "NVarChar(256)")]
		public string Desc
		{
			get
			{
				return this._Desc;
			}
			set
			{
				if (this._Desc != value)
				{
					this._Desc = value;
				}
			}
		}

		[Column(Storage = "_Probability", DbType = "Decimal(18,0) NOT NULL")]
		public decimal Probability
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

		private int _MarbleID;

		private int _NodeIndex;

		private int _NodeGrade;

		private string _Action;

		private string _Command;

		private string _Desc;

		private decimal _Probability;
	}
}
