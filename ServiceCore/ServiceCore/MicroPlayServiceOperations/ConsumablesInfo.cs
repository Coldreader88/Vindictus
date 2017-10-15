using System;
using System.Collections.Generic;

namespace ServiceCore.MicroPlayServiceOperations
{
	[Serializable]
	public sealed class ConsumablesInfo
	{
		public string ItemClass
		{
			get
			{
				return this.itemClass;
			}
			set
			{
				this.itemClass = value;
			}
		}

		public int BringNum
		{
			get
			{
				return this.bringNum;
			}
			set
			{
				this.bringNum = value;
			}
		}

		public int Usednum
		{
			get
			{
				return this.usedNum;
			}
			set
			{
				this.usedNum = value;
			}
		}

		public int DraftNum
		{
			get
			{
				return this.draftNum;
			}
			set
			{
				this.draftNum = value;
			}
		}

		public List<ConsumablesInfo> InnerConsumables
		{
			get
			{
				return this.innerConsumables;
			}
		}

		public bool Used(int num)
		{
			if (this.bringNum < num)
			{
				this.usedNum = this.bringNum;
				return false;
			}
			if (this.bringNum - num < this.usedNum)
			{
				this.usedNum = this.bringNum;
				return false;
			}
			this.usedNum += num;
			return true;
		}

		public bool IsInfiniteUse()
		{
			return this.draftNum == 0;
		}

		public bool Refill(int targetCount)
		{
			this.usedNum = 0;
			this.BringNum = targetCount;
			return true;
		}

		public ConsumablesInfo(string iclass, int bring, int draft)
		{
			this.itemClass = iclass;
			this.bringNum = bring;
			this.usedNum = 0;
			this.draftNum = draft;
		}

		private string itemClass;

		private int bringNum;

		private int usedNum;

		private int draftNum;

		private List<ConsumablesInfo> innerConsumables = new List<ConsumablesInfo>();
	}
}
