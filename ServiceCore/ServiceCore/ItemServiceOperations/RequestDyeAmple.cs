using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.ItemServiceOperations
{
	[Serializable]
	public sealed class RequestDyeAmple : Operation
	{
		public int X { get; set; }

		public int Y { get; set; }

		public bool IsAvatar { get; set; }

		public List<int> ColorValue { get; set; }

		public RequestDyeAmple(int x, int y, bool isAvatar, List<int> colorValue)
		{
			this.X = x;
			this.Y = y;
			this.IsAvatar = isAvatar;
			this.ColorValue = colorValue;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
