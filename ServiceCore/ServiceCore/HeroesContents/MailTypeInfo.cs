using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.MailTypeInfo")]
	public class MailTypeInfo
	{
		[Column(Storage = "_MailType", DbType = "Int NOT NULL")]
		public int MailType
		{
			get
			{
				return this._MailType;
			}
			set
			{
				if (this._MailType != value)
				{
					this._MailType = value;
				}
			}
		}

		[Column(Storage = "_Description", DbType = "NVarChar(100) NOT NULL", CanBeNull = false)]
		public string Description
		{
			get
			{
				return this._Description;
			}
			set
			{
				if (this._Description != value)
				{
					this._Description = value;
				}
			}
		}

		[Column(Storage = "_Sender", DbType = "NVarChar(20)")]
		public string Sender
		{
			get
			{
				return this._Sender;
			}
			set
			{
				if (this._Sender != value)
				{
					this._Sender = value;
				}
			}
		}

		[Column(Storage = "_System", DbType = "Bit NOT NULL")]
		public bool System
		{
			get
			{
				return this._System;
			}
			set
			{
				if (this._System != value)
				{
					this._System = value;
				}
			}
		}

		[Column(Storage = "_ItemAttached", DbType = "Bit NOT NULL")]
		public bool ItemAttached
		{
			get
			{
				return this._ItemAttached;
			}
			set
			{
				if (this._ItemAttached != value)
				{
					this._ItemAttached = value;
				}
			}
		}

		[Column(Storage = "_Charged", DbType = "Bit NOT NULL")]
		public bool Charged
		{
			get
			{
				return this._Charged;
			}
			set
			{
				if (this._Charged != value)
				{
					this._Charged = value;
				}
			}
		}

		[Column(Storage = "_Replyable", DbType = "Bit NOT NULL")]
		public bool Replyable
		{
			get
			{
				return this._Replyable;
			}
			set
			{
				if (this._Replyable != value)
				{
					this._Replyable = value;
				}
			}
		}

		[Column(Storage = "_Returnable", DbType = "Bit NOT NULL")]
		public bool Returnable
		{
			get
			{
				return this._Returnable;
			}
			set
			{
				if (this._Returnable != value)
				{
					this._Returnable = value;
				}
			}
		}

		[Column(Storage = "_Expire_in", DbType = "Int NOT NULL")]
		public int Expire_in
		{
			get
			{
				return this._Expire_in;
			}
			set
			{
				if (this._Expire_in != value)
				{
					this._Expire_in = value;
				}
			}
		}

		[Column(Storage = "_Arrive_in", DbType = "Int NOT NULL")]
		public int Arrive_in
		{
			get
			{
				return this._Arrive_in;
			}
			set
			{
				if (this._Arrive_in != value)
				{
					this._Arrive_in = value;
				}
			}
		}

		[Column(Storage = "_Fee", DbType = "Int NOT NULL")]
		public int Fee
		{
			get
			{
				return this._Fee;
			}
			set
			{
				if (this._Fee != value)
				{
					this._Fee = value;
				}
			}
		}

		[Column(Storage = "_FeeForItem", DbType = "Int NOT NULL")]
		public int FeeForItem
		{
			get
			{
				return this._FeeForItem;
			}
			set
			{
				if (this._FeeForItem != value)
				{
					this._FeeForItem = value;
				}
			}
		}

		[Column(Storage = "_Fee_Express", DbType = "Int NOT NULL")]
		public int Fee_Express
		{
			get
			{
				return this._Fee_Express;
			}
			set
			{
				if (this._Fee_Express != value)
				{
					this._Fee_Express = value;
				}
			}
		}

		[Column(Storage = "_FeeForItem_Express", DbType = "Int NOT NULL")]
		public int FeeForItem_Express
		{
			get
			{
				return this._FeeForItem_Express;
			}
			set
			{
				if (this._FeeForItem_Express != value)
				{
					this._FeeForItem_Express = value;
				}
			}
		}

		private int _MailType;

		private string _Description;

		private string _Sender;

		private bool _System;

		private bool _ItemAttached;

		private bool _Charged;

		private bool _Replyable;

		private bool _Returnable;

		private int _Expire_in;

		private int _Arrive_in;

		private int _Fee;

		private int _FeeForItem;

		private int _Fee_Express;

		private int _FeeForItem_Express;
	}
}
