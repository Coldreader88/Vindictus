using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.AbilityClassInfo")]
	public class AbilityClassInfo
	{
		[Column(Storage = "_ID", DbType = "Int NOT NULL")]
		public int ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				if (this._ID != value)
				{
					this._ID = value;
				}
			}
		}

		[Column(Storage = "_AbilityClass", DbType = "NVarChar(100) NOT NULL", CanBeNull = false)]
		public string AbilityClass
		{
			get
			{
				return this._AbilityClass;
			}
			set
			{
				if (this._AbilityClass != value)
				{
					this._AbilityClass = value;
				}
			}
		}

		[Column(Storage = "_Event", DbType = "Int")]
		public int? Event
		{
			get
			{
				return this._Event;
			}
			set
			{
				if (this._Event != value)
				{
					this._Event = value;
				}
			}
		}

		[Column(Storage = "_Arg1", DbType = "NVarChar(50)")]
		public string Arg1
		{
			get
			{
				return this._Arg1;
			}
			set
			{
				if (this._Arg1 != value)
				{
					this._Arg1 = value;
				}
			}
		}

		[Column(Storage = "_Arg2", DbType = "NVarChar(50)")]
		public string Arg2
		{
			get
			{
				return this._Arg2;
			}
			set
			{
				if (this._Arg2 != value)
				{
					this._Arg2 = value;
				}
			}
		}

		[Column(Storage = "_Arg3", DbType = "Int NOT NULL")]
		public int Arg3
		{
			get
			{
				return this._Arg3;
			}
			set
			{
				if (this._Arg3 != value)
				{
					this._Arg3 = value;
				}
			}
		}

		[Column(Storage = "_TargetQuestGroupID", DbType = "VarChar(32)")]
		public string TargetQuestGroupID
		{
			get
			{
				return this._TargetQuestGroupID;
			}
			set
			{
				if (this._TargetQuestGroupID != value)
				{
					this._TargetQuestGroupID = value;
				}
			}
		}

		private int _ID;

		private string _AbilityClass;

		private int? _Event;

		private string _Arg1;

		private string _Arg2;

		private int _Arg3;

		private string _TargetQuestGroupID;
	}
}
