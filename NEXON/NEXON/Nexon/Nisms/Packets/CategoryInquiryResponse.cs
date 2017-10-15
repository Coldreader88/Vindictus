using System;
using System.Collections.Generic;
using System.Linq;

namespace Nexon.Nisms.Packets
{
	internal class CategoryInquiryResponse
	{
		public Result Result { get; private set; }

		public ICollection<Category> CategoryArray { get; private set; }

		internal CategoryInquiryResponse(ref Packet packet)
		{
			this.Result = (Result)packet.ReadInt32();
			int num = packet.ReadInt32();
			this.CategoryArray = new List<Category>(num);
			foreach (int num2 in Enumerable.Range(0, num))
			{
				Category item = new Category(ref packet);
				this.CategoryArray.Add(item);
			}
		}
	}
}
