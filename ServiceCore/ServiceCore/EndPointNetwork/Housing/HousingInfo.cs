using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceCore.EndPointNetwork.Housing
{
	[Serializable]
	public sealed class HousingInfo
	{
		public long HousingID { get; set; }

		public string HousingName { get; set; }

		public string Password { get; set; }

		public int MemberCount { get; set; }

		public int MaxHousingMemberCount { get; set; }

		public ICollection<HousingPartyMemberInfo> Members { get; set; }

		public PartyInfoState State { get; set; }

		public LinkedListNode<HousingInfo> HousingListNode
		{
			get
			{
				return this.housingListNode;
			}
			set
			{
				this.housingListNode = value;
			}
		}

		public override string ToString()
		{
			new StringBuilder("housingInfo");
			return string.Format("{0}[ housingId = {1} member x {2} {3}]", new object[]
			{
				this.HousingName,
				this.HousingID,
				this.MemberCount,
				this.State
			});
		}

		[NonSerialized]
		private LinkedListNode<HousingInfo> housingListNode;
	}
}
