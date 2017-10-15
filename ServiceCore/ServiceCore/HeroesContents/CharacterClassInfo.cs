using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.CharacterClassInfo")]
	public class CharacterClassInfo
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

		[Column(Name = "[Level]", Storage = "_Level", DbType = "Int NOT NULL")]
		public int Level
		{
			get
			{
				return this._Level;
			}
			set
			{
				if (this._Level != value)
				{
					this._Level = value;
				}
			}
		}

		[Column(Storage = "_BaseSTR", DbType = "Int NOT NULL")]
		public int BaseSTR
		{
			get
			{
				return this._BaseSTR;
			}
			set
			{
				if (this._BaseSTR != value)
				{
					this._BaseSTR = value;
				}
			}
		}

		[Column(Storage = "_BaseDEX", DbType = "Int NOT NULL")]
		public int BaseDEX
		{
			get
			{
				return this._BaseDEX;
			}
			set
			{
				if (this._BaseDEX != value)
				{
					this._BaseDEX = value;
				}
			}
		}

		[Column(Storage = "_BaseINT", DbType = "Int NOT NULL")]
		public int BaseINT
		{
			get
			{
				return this._BaseINT;
			}
			set
			{
				if (this._BaseINT != value)
				{
					this._BaseINT = value;
				}
			}
		}

		[Column(Storage = "_BaseWILL", DbType = "Int NOT NULL")]
		public int BaseWILL
		{
			get
			{
				return this._BaseWILL;
			}
			set
			{
				if (this._BaseWILL != value)
				{
					this._BaseWILL = value;
				}
			}
		}

		[Column(Storage = "_BaseHP", DbType = "Int NOT NULL")]
		public int BaseHP
		{
			get
			{
				return this._BaseHP;
			}
			set
			{
				if (this._BaseHP != value)
				{
					this._BaseHP = value;
				}
			}
		}

		[Column(Storage = "_BaseSTAMINA", DbType = "Int NOT NULL")]
		public int BaseSTAMINA
		{
			get
			{
				return this._BaseSTAMINA;
			}
			set
			{
				if (this._BaseSTAMINA != value)
				{
					this._BaseSTAMINA = value;
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

		private int _Identifier;

		private int _Level;

		private int _BaseSTR;

		private int _BaseDEX;

		private int _BaseINT;

		private int _BaseWILL;

		private int _BaseHP;

		private int _BaseSTAMINA;

		private string _Feature;
	}
}
