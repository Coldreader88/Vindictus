using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class PetOperationMessage : IMessage
	{
		public PetOperationType OperationCode { get; set; }

		public long PetID { get; set; }

		public string Arg { get; set; }

		public int Value1 { get; set; }

		public int Value2 { get; set; }

		public int Value3 { get; set; }

		public override string ToString()
		{
			return string.Format("PetOperationMessage[ {0} ]", this.OperationCode.ToString());
		}
	}
}
