using System;

namespace Nexon.Nisms
{
	[Serializable]
	public class Category
	{
		public int CategoryNo { get; private set; }

		public string CategoryName { get; private set; }

		public int ParentCategoryNo { get; private set; }

		public int DisplayNo { get; private set; }

		internal Category(ref Packet packet)
		{
			this.CategoryNo = packet.ReadInt32();
			this.CategoryName = packet.ReadString();
			this.ParentCategoryNo = packet.ReadInt32();
			this.DisplayNo = packet.ReadInt32();
		}
	}
}
