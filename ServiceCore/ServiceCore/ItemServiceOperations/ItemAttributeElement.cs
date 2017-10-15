using System;

namespace ServiceCore.ItemServiceOperations
{
	[Serializable]
	public sealed class ItemAttributeElement : ICloneable
	{
		public string AttributeName { get; set; }

		public string Value { get; set; }

		public int Arg { get; set; }

		public int Arg2 { get; set; }

		public object Clone()
		{
			return new ItemAttributeElement(this.AttributeName, this.Value, this.Arg, this.Arg2);
		}

		public ItemAttributeElement(string attributeName, string value, int arg, int arg2)
		{
			this.AttributeName = attributeName;
			this.Value = value;
			this.Arg = arg;
			this.Arg2 = arg2;
		}
	}
}
