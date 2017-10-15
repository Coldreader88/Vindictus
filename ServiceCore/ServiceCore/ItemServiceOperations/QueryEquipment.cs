using System;
using System.Collections.Generic;
using ServiceCore.CharacterServiceOperations;
using UnifiedNetwork.Cooperation;
using Utility;

namespace ServiceCore.ItemServiceOperations
{
	[Serializable]
	public sealed class QueryEquipment : Operation
	{
		public bool RequestEquip { get; set; }

		public bool RequestStat { get; set; }

		public bool RequestAdditionalInfo { get; set; }

		public IDictionary<int, long> EquipmentInfo
		{
			get
			{
				return this.equipmentInfo;
			}
		}

		public SetBonusInfo SetBonusInfo
		{
			get
			{
				return this.setBonusInfo;
			}
		}

		public IList<EquippedItemInfo> EquippedItemInfo
		{
			get
			{
				return this.equippedItemInfo;
			}
		}

		public IDictionary<string, int> StatusEffectDict
		{
			get
			{
				return this.statusEffectDict;
			}
		}

		public IList<int> AbilityInfo
		{
			get
			{
				return this.abilityInfo;
			}
		}

		public int VIPCodeNumber
		{
			get
			{
				return this.vipCode;
			}
		}

		public DateTime VIPTimeLine
		{
			get
			{
				return this.vipTimeLine;
			}
		}

		public Dictionary<int, string> GetEquippedItemClassDict()
		{
			Dictionary<int, string> dictionary = new Dictionary<int, string>();
			foreach (EquippedItemInfo equippedItemInfo in this.EquippedItemInfo)
			{
				if (equippedItemInfo.ItemClass != null && !(equippedItemInfo.ItemClass == ""))
				{
					if (dictionary.ContainsKey(equippedItemInfo.PartNum))
					{
						Log<QueryEquipment>.Logger.ErrorFormat("키가 중복됩니다. [{0} {1} <= {2}]", equippedItemInfo.PartNum, dictionary[equippedItemInfo.PartNum], equippedItemInfo.ItemClass);
					}
					else
					{
						dictionary.Add(equippedItemInfo.PartNum, equippedItemInfo.ItemClass);
					}
				}
			}
			return dictionary;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new QueryEquipment.Request(this);
		}

		public static QueryEquipment MakeResult(IDictionary<int, long> equipmentInfo, SetBonusInfo setBonusInfo, IList<EquippedItemInfo> equippedItemInfo, IDictionary<string, int> statusEffectDict, IList<int> abilityInfo, int vipCode, DateTime vipTimeLine)
		{
			return new QueryEquipment
			{
				equipmentInfo = equipmentInfo,
				setBonusInfo = setBonusInfo,
				equippedItemInfo = equippedItemInfo,
				statusEffectDict = statusEffectDict,
				abilityInfo = abilityInfo,
				vipCode = vipCode,
				vipTimeLine = vipTimeLine
			};
		}

		[NonSerialized]
		private IDictionary<int, long> equipmentInfo;

		[NonSerialized]
		private SetBonusInfo setBonusInfo;

		[NonSerialized]
		private IList<EquippedItemInfo> equippedItemInfo;

		[NonSerialized]
		private IDictionary<string, int> statusEffectDict;

		[NonSerialized]
		private IList<int> abilityInfo;

		[NonSerialized]
		private int vipCode;

		[NonSerialized]
		private DateTime vipTimeLine;

		private class Request : OperationProcessor<QueryEquipment>
		{
			public Request(QueryEquipment op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				if (base.Operation.RequestEquip)
				{
					yield return null;
					base.Operation.equipmentInfo = (base.Feedback as IDictionary<int, long>);
					if (base.Operation.equipmentInfo == null)
					{
						base.Result = false;
					}
				}
				if (base.Operation.RequestStat)
				{
					yield return null;
					base.Operation.setBonusInfo = (base.Feedback as SetBonusInfo);
					yield return null;
					base.Operation.equippedItemInfo = (base.Feedback as IList<EquippedItemInfo>);
					yield return null;
					base.Operation.statusEffectDict = (base.Feedback as IDictionary<string, int>);
					yield return null;
					base.Operation.abilityInfo = (base.Feedback as IList<int>);
				}
				if (base.Operation.RequestAdditionalInfo)
				{
					yield return null;
					base.Operation.vipCode = (int)base.Feedback;
					yield return null;
					base.Operation.vipTimeLine = (DateTime)base.Feedback;
				}
				yield break;
			}
		}
	}
}
