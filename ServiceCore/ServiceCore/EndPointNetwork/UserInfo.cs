using System;
using ServiceCore.Configuration;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class UserInfo
	{
		public int NexonSN
		{
			get
			{
				return this.nexonSN;
			}
			set
			{
				this.nexonSN = value;
			}
		}

		public string CharacterName { get; set; }

		public int RegionCode
		{
			get
			{
				return this.regionCode;
			}
			set
			{
				this.regionCode = value;
			}
		}

		public int Limited
		{
			get
			{
				return this.limited;
			}
			set
			{
				this.limited = value;
			}
		}

		public int CharacterSN
		{
			get
			{
				return this.characterSN;
			}
			set
			{
				this.characterSN = value;
			}
		}

		public long CID
		{
			get
			{
				return this.cid;
			}
			set
			{
				this.cid = value;
			}
		}

		public byte Age
		{
			get
			{
				return this.age;
			}
			set
			{
				this.age = value;
			}
		}

		public byte Character { get; set; }

		public int Level { get; set; }

		public byte Ready { get; set; }

		public int CafeLevel { get; set; }

		public int CafeType { get; set; }

		public bool IsShutdownEnabled { get; set; }

		public bool HasCafeBonus { get; set; }

		public bool Validate(CafeKinds value)
		{
			return (this.CafeType != 1 && (value & CafeKinds.Private) != (CafeKinds)0) || (this.CafeType == 1 && (value & CafeKinds.Cafe) != (CafeKinds)0);
		}

		public string FacebookToken { get; set; }

		public int UserCareType { get; set; }

		public int UserCareNextState { get; set; }

		public long UserCareScheduleID { get; set; }

		public bool IsNeedUserCareMeetingState { get; set; }

		public byte MapStateInfo { get; set; }

		public UserInfo()
		{
			this.UserCareType = -1;
			this.UserCareNextState = -1;
		}

		public override string ToString()
		{
			return string.Format("UserInfo( ch = {0} name = {1} level = {2} ready = {3} )", new object[]
			{
				this.Character,
				this.CharacterName,
				this.Level,
				this.Ready
			});
		}

		private int nexonSN;

		private int regionCode;

		private int limited;

		private int characterSN;

		private long cid;

		private byte age;
	}
}
