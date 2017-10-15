using System;

namespace ServiceCore.EndPointNetwork.Housing
{
	[Serializable]
	public sealed class HousingOptionInfo
	{
		public string HousingName { get; set; }

		public int MaxMemberCount { get; set; }

		public bool IsSecret { get; set; }

		public string Password { get; set; }

		public void Merge(HousingOptionInfo rhs)
		{
			this.HousingName = rhs.HousingName;
			if (rhs.MaxMemberCount > 0)
			{
				this.MaxMemberCount = rhs.MaxMemberCount;
			}
			this.IsSecret = rhs.IsSecret;
			this.Password = rhs.Password;
		}
	}
}
