using System;
using System.Collections.Generic;
using ServiceCore.ItemServiceOperations;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class RandomItemRewardListMessage : IMessage
	{
		public ICollection<ColoredEquipment> ItemClasses { get; set; }

		public ICollection<int> ItemQuantities { get; set; }

		public bool IsUserCare { get; set; }

		public string KeyItemClass { get; set; }

		public bool DisableShowPopUp { get; set; }

		public RandomItemRewardListMessage(ICollection<ColoredEquipment> itemClasses, ICollection<int> itemQuantities)
		{
			this.ItemClasses = itemClasses;
			this.ItemQuantities = itemQuantities;
			this.IsUserCare = false;
			this.KeyItemClass = "";
			this.DisableShowPopUp = false;
		}

		public RandomItemRewardListMessage(ICollection<ColoredEquipment> itemClasses, ICollection<int> itemQuantities, bool IsUserCare, string keyItemClass)
		{
			this.ItemClasses = itemClasses;
			this.ItemQuantities = itemQuantities;
			this.IsUserCare = IsUserCare;
			this.KeyItemClass = keyItemClass;
			this.DisableShowPopUp = false;
		}

		public RandomItemRewardListMessage(bool disableShowPopUp, string keyItemClass)
		{
			this.DisableShowPopUp = disableShowPopUp;
			this.KeyItemClass = keyItemClass;
		}
	}
}
