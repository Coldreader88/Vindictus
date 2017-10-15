using System;
using System.Collections.Generic;
using ServiceCore.CharacterServiceOperations;
using ServiceCore.EndPointNetwork;
using ServiceCore.ItemServiceOperations;
using ServiceCore.MicroPlayServiceOperations;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.PlayerServiceOperations
{
	[Serializable]
	public class QueryPlayerInfo : Operation
	{
		public long CID { get; set; }

		public string QuestID { get; set; }

		public float SubWeaponMultiplier { get; set; }

		public float QuickSlotMultiplier { get; set; }

		public QueryPlayerInfo(long cid, string questID, float subWeaponMultiplier, float quickSlotMultiplier)
		{
			this.CID = cid;
			this.QuestID = questID;
			this.SubWeaponMultiplier = subWeaponMultiplier;
			this.QuickSlotMultiplier = quickSlotMultiplier;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new QueryPlayerInfo.Request(this);
		}

		[NonSerialized]
		public IDictionary<int, ConsumablesInfo> Consumables;

		[NonSerialized]
		public ICollection<StorageInfo> StorageInfo;

		[NonSerialized]
		public ICollection<SlotInfo> InventoryInfo;

		[NonSerialized]
		public IDictionary<int, long> EquipmentInfo;

		[NonSerialized]
		public QuickSlotInfo QuickSlotInfo;

		[NonSerialized]
		public ICollection<int> UnequippableParts;

		[NonSerialized]
		public ICollection<int> TitleGoalIDs;

		[NonSerialized]
		public SetBonusInfo SetBonusInfo;

		[NonSerialized]
		public Dictionary<int, string> EquippedItemInfo;

		[NonSerialized]
		public IDictionary<string, int> StatusEffectDict;

		[NonSerialized]
		public IList<int> AbilityList;

		[NonSerialized]
		public List<StoryDropData> StoryDropData;

		[NonSerialized]
		public int MaxLevel;

		[NonSerialized]
		public List<AchieveGoalInfo> AchievedGoalInfoList;

		private class Request : OperationProcessor<QueryPlayerInfo>
		{
			public Request(QueryPlayerInfo op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				base.Operation.Consumables = (base.Feedback as IDictionary<int, ConsumablesInfo>);
				if (base.Operation.Consumables == null)
				{
					base.Result = false;
				}
				else
				{
					yield return null;
					InventoryInfo info = base.Feedback as InventoryInfo;
					base.Operation.StorageInfo = info.StorageInfo;
					base.Operation.InventoryInfo = info.InventorySlotInfo;
					base.Operation.EquipmentInfo = info.EquipmentInfo;
					base.Operation.QuickSlotInfo = info.QuickSlotInfo;
					base.Operation.UnequippableParts = info.UnequippableParts;
					yield return null;
					base.Operation.TitleGoalIDs = (base.Feedback as IList<int>);
					yield return null;
					base.Operation.SetBonusInfo = (base.Feedback as SetBonusInfo);
					yield return null;
					base.Operation.EquippedItemInfo = (base.Feedback as Dictionary<int, string>);
					yield return null;
					base.Operation.StatusEffectDict = (base.Feedback as IDictionary<string, int>);
					yield return null;
					base.Operation.AbilityList = (base.Feedback as IList<int>);
					yield return null;
					base.Operation.StoryDropData = (base.Feedback as List<StoryDropData>);
					yield return null;
					base.Operation.MaxLevel = (int)base.Feedback;
					yield return null;
					base.Operation.AchievedGoalInfoList = (base.Feedback as List<AchieveGoalInfo>);
				}
				yield break;
			}
		}
	}
}
