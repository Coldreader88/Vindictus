using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class RemovePetSkillMessage : IMessage
	{
		public long PetID { get; set; }

		public int PetSkillID { get; set; }
	}
}
