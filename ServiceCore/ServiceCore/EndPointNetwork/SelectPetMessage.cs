using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class SelectPetMessage : IMessage
	{
		public long PetID { get; set; }

		public override string ToString()
		{
			return string.Format(" [ SelectPetMessage ] PetID : {0} ", this.PetID);
		}
	}
}
