using System;
using System.Collections.Generic;
using ServiceCore.CharacterServiceOperations;
using ServiceCore.EndPointNetwork;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.ItemServiceOperations
{
	[Serializable]
	public sealed class CustomizeCharacter : Operation
	{
		public int NexonSN { get; set; }

		public CharacterTemplate Templete { get; set; }

		public Dictionary<string, int> DefaultItem { get; set; }

		public List<int> UnlockSlot { get; set; }

		public ICollection<EquippedItemInfo> BaseCostume
		{
			get
			{
				return this.baseCostume;
			}
			set
			{
				this.baseCostume = value;
			}
		}

		public ICollection<EquippedItemInfo> ItemCostume
		{
			get
			{
				return this.itemCostume;
			}
			set
			{
				this.itemCostume = value;
			}
		}

		public int SkinSSSColor
		{
			get
			{
				return this.skinSSSColor;
			}
			set
			{
				this.skinSSSColor = value;
			}
		}

		private CustomizeCharacter()
		{
		}

		public CustomizeCharacter(int nexonSN, CharacterTemplate templete, Dictionary<string, int> defaultItem, List<int> unlockSlot)
		{
			this.NexonSN = nexonSN;
			this.Templete = templete;
			this.DefaultItem = defaultItem;
			this.UnlockSlot = unlockSlot;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new CustomizeCharacter.Request(this);
		}

		public static CustomizeCharacter MakeResult(ICollection<EquippedItemInfo> baseCostume, ICollection<EquippedItemInfo> itemCostume, int sss)
		{
			return new CustomizeCharacter
			{
				baseCostume = baseCostume,
				itemCostume = itemCostume,
				skinSSSColor = sss
			};
		}

		[NonSerialized]
		private ICollection<EquippedItemInfo> baseCostume;

		[NonSerialized]
		private ICollection<EquippedItemInfo> itemCostume;

		[NonSerialized]
		private int skinSSSColor;

		private class Request : OperationProcessor<CustomizeCharacter>
		{
			public Request(CustomizeCharacter op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				base.Operation.BaseCostume = (base.Feedback as IList<EquippedItemInfo>);
				if (base.Operation.BaseCostume != null)
				{
					yield return null;
					base.Operation.ItemCostume = (base.Feedback as ICollection<EquippedItemInfo>);
					yield return null;
					base.Operation.SkinSSSColor = (int)base.Feedback;
				}
				else
				{
					base.Result = false;
				}
				yield break;
			}
		}
	}
}
