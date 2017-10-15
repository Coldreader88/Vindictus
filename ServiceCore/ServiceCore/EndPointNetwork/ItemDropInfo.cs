using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class ItemDropInfo
	{
		public bool Lucky
		{
			get
			{
				return this.lucky;
			}
		}

		public string ItemClass
		{
			get
			{
				return this.itemClass;
			}
		}

		public int Count
		{
			get
			{
				return this.count;
			}
		}

		public bool IsStoryDrop
		{
			get
			{
				return this.isStoryDrop;
			}
		}

		public string EntityName
		{
			get
			{
				return this.entityName;
			}
		}

		public bool IsDropped
		{
			get
			{
				return this.isDropped;
			}
			set
			{
				this.isDropped = value;
			}
		}

		public string LogString
		{
			get
			{
				if (this.count == 1)
				{
					return this.itemClass;
				}
				return string.Format("{0}/{1}", this.itemClass, this.count);
			}
		}

		public ItemDropInfo(bool lucky, string itemClassString, bool isStoryDrop, string ename)
		{
			this.lucky = lucky;
			if (itemClassString.Contains("/"))
			{
				char[] separator = new char[]
				{
					'/'
				};
				string[] array = itemClassString.Split(separator);
				if (array.Length == 2)
				{
					this.itemClass = array[0];
					this.count = int.Parse(array[1]);
				}
				else if (array.Length == 3)
				{
					this.itemClass = array[0];
					this.count = new Random().Next(int.Parse(array[1]), int.Parse(array[2]));
				}
			}
			else
			{
				this.itemClass = itemClassString;
				this.count = 1;
			}
			if (!FeatureMatrix.IsEnable("MultipleItemDrop"))
			{
				this.count = 1;
			}
			this.isStoryDrop = isStoryDrop;
			this.entityName = ename;
			this.isDropped = false;
		}

		private bool lucky;

		private string itemClass;

		private string entityName;

		private bool isStoryDrop;

		private int count;

		[NonSerialized]
		private bool isDropped;
	}
}
