using System;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.GroupPermissions")]
	public class GroupPermissions
	{
		[Column(Storage = "_GroupID", DbType = "NVarChar(32) NOT NULL", CanBeNull = false)]
		public string GroupID
		{
			get
			{
				return this._GroupID;
			}
			set
			{
				if (this._GroupID != value)
				{
					this._GroupID = value;
				}
			}
		}

		[Column(Storage = "_Permissions", DbType = "VarBinary(8) NOT NULL", CanBeNull = false, UpdateCheck = UpdateCheck.Never)]
		public Binary Permissions
		{
			get
			{
				return this._Permissions;
			}
			set
			{
				if (this._Permissions != value)
				{
					this._Permissions = value;
				}
			}
		}

		[Column(Storage = "_IsGranted", DbType = "Bit NOT NULL")]
		public bool IsGranted
		{
			get
			{
				return this._IsGranted;
			}
			set
			{
				if (this._IsGranted != value)
				{
					this._IsGranted = value;
				}
			}
		}

		private string _GroupID;

		private Binary _Permissions;

		private bool _IsGranted;
	}
}
