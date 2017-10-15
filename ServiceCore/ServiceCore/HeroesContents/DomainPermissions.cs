using System;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.DomainPermissions")]
	public class DomainPermissions
	{
		[Column(Storage = "_DomainID", DbType = "NVarChar(32) NOT NULL", CanBeNull = false)]
		public string DomainID
		{
			get
			{
				return this._DomainID;
			}
			set
			{
				if (this._DomainID != value)
				{
					this._DomainID = value;
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

		private string _DomainID;

		private Binary _Permissions;

		private bool _IsGranted;
	}
}
