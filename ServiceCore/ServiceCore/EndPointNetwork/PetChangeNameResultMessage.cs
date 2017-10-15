using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class PetChangeNameResultMessage : IMessage
	{
		public bool IsSuccess { get; set; }

		public string PetName { get; set; }

		public string ResultType { get; set; }

		public PetChangeNameResultMessage(bool isSuccess, string petName, string resultType)
		{
			this.IsSuccess = isSuccess;
			this.PetName = petName;
			this.ResultType = resultType;
		}
	}
}
