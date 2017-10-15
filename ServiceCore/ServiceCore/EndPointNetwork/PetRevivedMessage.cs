using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class PetRevivedMessage : IMessage
	{
		public int CasterTag { get; set; }

		public int ReviverTag { get; set; }

		public long PetID { get; set; }

		public string Method { get; set; }

		public override string ToString()
		{
			return string.Format("PetRevivedMessage[ caster = {0} reviver = {1} PetID = {2} method = {3} ]", new object[]
			{
				this.CasterTag,
				this.ReviverTag,
				this.PetID,
				this.Method
			});
		}
	}
}
