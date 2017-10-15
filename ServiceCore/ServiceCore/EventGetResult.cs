using System;
using System.Data.Linq.Mapping;

namespace ServiceCore
{
	public class EventGetResult
	{
		[Column(Storage = "_Name", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				if (this._Name != value)
				{
					this._Name = value;
				}
			}
		}

		[Column(Storage = "_Feature", DbType = "NVarChar(256)")]
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

		[Column(Storage = "_StartScript", DbType = "NVarChar(MAX)")]
		public string StartScript
		{
			get
			{
				return this._StartScript;
			}
			set
			{
				if (this._StartScript != value)
				{
					this._StartScript = value;
				}
			}
		}

		[Column(Storage = "_EndScript", DbType = "NVarChar(MAX)")]
		public string EndScript
		{
			get
			{
				return this._EndScript;
			}
			set
			{
				if (this._EndScript != value)
				{
					this._EndScript = value;
				}
			}
		}

		[Column(Storage = "_StartTime", DbType = "DateTime2")]
		public DateTime? StartTime
		{
			get
			{
				return this._StartTime;
			}
			set
			{
				if (this._StartTime != value)
				{
					this._StartTime = value;
				}
			}
		}

		[Column(Storage = "_EndTime", DbType = "DateTime2")]
		public DateTime? EndTime
		{
			get
			{
				return this._EndTime;
			}
			set
			{
				if (this._EndTime != value)
				{
					this._EndTime = value;
				}
			}
		}

		[Column(Storage = "_PeriodBegin", DbType = "Time")]
		public TimeSpan? PeriodBegin
		{
			get
			{
				return this._PeriodBegin;
			}
			set
			{
				if (this._PeriodBegin != value)
				{
					this._PeriodBegin = value;
				}
			}
		}

		[Column(Storage = "_PeriodEnd", DbType = "Time")]
		public TimeSpan? PeriodEnd
		{
			get
			{
				return this._PeriodEnd;
			}
			set
			{
				if (this._PeriodEnd != value)
				{
					this._PeriodEnd = value;
				}
			}
		}

		[Column(Storage = "_StartMessage", DbType = "NVarChar(256)")]
		public string StartMessage
		{
			get
			{
				return this._StartMessage;
			}
			set
			{
				if (this._StartMessage != value)
				{
					this._StartMessage = value;
				}
			}
		}

		[Column(Storage = "_NotifyMessage", DbType = "NVarChar(256)")]
		public string NotifyMessage
		{
			get
			{
				return this._NotifyMessage;
			}
			set
			{
				if (this._NotifyMessage != value)
				{
					this._NotifyMessage = value;
				}
			}
		}

		[Column(Storage = "_EndMessage", DbType = "NVarChar(256)")]
		public string EndMessage
		{
			get
			{
				return this._EndMessage;
			}
			set
			{
				if (this._EndMessage != value)
				{
					this._EndMessage = value;
				}
			}
		}

		[Column(Storage = "_NotifyInterval", DbType = "Int")]
		public int? NotifyInterval
		{
			get
			{
				return this._NotifyInterval;
			}
			set
			{
				if (this._NotifyInterval != value)
				{
					this._NotifyInterval = value;
				}
			}
		}

		[Column(Storage = "_StartCount", DbType = "Int")]
		public int? StartCount
		{
			get
			{
				return this._StartCount;
			}
			set
			{
				if (this._StartCount != value)
				{
					this._StartCount = value;
				}
			}
		}

		[Column(Storage = "_CurrentCount", DbType = "Int NOT NULL")]
		public int CurrentCount
		{
			get
			{
				return this._CurrentCount;
			}
			set
			{
				if (this._CurrentCount != value)
				{
					this._CurrentCount = value;
				}
			}
		}

		private string _Name;

		private string _Feature;

		private string _StartScript;

		private string _EndScript;

		private DateTime? _StartTime;

		private DateTime? _EndTime;

		private TimeSpan? _PeriodBegin;

		private TimeSpan? _PeriodEnd;

		private string _StartMessage;

		private string _NotifyMessage;

		private string _EndMessage;

		private int? _NotifyInterval;

		private int? _StartCount;

		private int _CurrentCount;
	}
}
