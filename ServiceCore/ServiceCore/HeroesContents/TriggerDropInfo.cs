using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.TriggerDropInfo")]
	public class TriggerDropInfo
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

		[Column(Name = "[Trigger]", Storage = "_Trigger", DbType = "NVarChar(128) NOT NULL", CanBeNull = false)]
		public string Trigger
		{
			get
			{
				return this._Trigger;
			}
			set
			{
				if (this._Trigger != value)
				{
					this._Trigger = value;
				}
			}
		}

		[Column(Storage = "_ItemClass", DbType = "NVarChar(128) NOT NULL", CanBeNull = false)]
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

		[Column(Storage = "_Probability", DbType = "Int NOT NULL")]
		public int Probability
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

		private string _Trigger;

		private string _ItemClass;

		private int _Probability;

		private string _Feature;
	}
}
