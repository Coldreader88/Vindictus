using System;
using System.Collections.Generic;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class DyeAmpleRequestMessage : IMessage
	{
		public int X { get; set; }

		public int Y { get; set; }

		public bool IsAvatar { get; set; }

		public List<int> ColorValue { get; set; }

		public override string ToString()
		{
			return string.Format("DyeAmpleRequestMessage [ x = {0} y = {1} ]", this.X, this.Y);
		}
	}
}
