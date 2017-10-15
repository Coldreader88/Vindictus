using System;

namespace ServiceCore.MicroPlayServiceOperations
{
	[Serializable]
	public sealed class ErgInfo
	{
		public int EID
		{
			get
			{
				return this.ergID;
			}
			set
			{
				this.ergID = value;
			}
		}

		public string Class
		{
			get
			{
				return this.ergClass;
			}
		}

		public string ErgType
		{
			get
			{
				return this.ergType;
			}
		}

		public int Amount
		{
			get
			{
				return this.amount;
			}
		}

		public int Winner
		{
			get
			{
				return this.winner;
			}
			set
			{
				this.winner = value;
			}
		}

		public ErgInfo(string ergClass, string ergType, int amount) : this(0, ergClass, ergType, amount)
		{
		}

		public ErgInfo(int _eid, string _ergClass, string _ergType, int _amount)
		{
			this.ergID = _eid;
			if (_ergClass.ToLower() == "instant")
			{
				this.ergClass = "INSTANT";
			}
			else if (_ergClass.ToLower() == "server")
			{
				this.ergClass = "SERVER";
			}
			else if (_ergClass.ToLower() == "gold")
			{
				this.ergClass = "GOLD";
			}
			else if (_ergClass.ToLower() == "hugeluckygold")
			{
				this.ergClass = "HUGELUCKYGOLD";
			}
			else
			{
				if (!(_ergClass.ToLower() == "holyprop"))
				{
					throw new Exception(string.Format("잘못된 에르그 class 입니다. : {0}", this.ergClass));
				}
				this.ergClass = "HOLYPROP";
			}
			this.ergType = _ergType;
			this.amount = _amount;
			this.winner = -1;
		}

		public override string ToString()
		{
			if (this.amount > 1)
			{
				return string.Format("{0} {1} {2}", this.ErgType, this.Amount, this.Class);
			}
			return this.ergType;
		}

		private int ergID;

		private string ergClass;

		private string ergType;

		private int amount;

		private int winner = -1;
	}
}
