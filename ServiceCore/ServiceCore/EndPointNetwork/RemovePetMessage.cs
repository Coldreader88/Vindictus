using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class RemovePetMessage : IMessage
	{
		public long PetID { get; set; }

		public override string ToString()
		{
			return string.Format(" [ RemovePetMessage ] PetID : {0} ", this.PetID);
		}
	}
}
