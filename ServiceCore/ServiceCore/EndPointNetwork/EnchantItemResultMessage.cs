using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class EnchantItemResultMessage : IMessage
	{
		public int CurrentValue { get; set; }

		public int GoalValue { get; set; }

		public int Result { get; set; }

		public string RolledDice { get; set; }

		public int CurrentSuccessRatio { get; set; }

		public EnchantItemResultMessage(EnchantItemResult result, int currentValue, int goalValue, string rolledDice, float successRatio)
		{
			this.Result = (int)result;
			this.CurrentValue = currentValue;
			this.GoalValue = goalValue;
			this.RolledDice = rolledDice;
			this.CurrentSuccessRatio = (int)(successRatio * 100f);
		}

		public override string ToString()
		{
			return string.Format("EnchantItemResultMessage[ Result = {0} CurrentValue = {1} GoalValue = {2} RolledDice = {3} ]", new object[]
			{
				(EnchantItemResult)this.Result,
				this.CurrentValue,
				this.GoalValue,
				this.RolledDice
			});
		}
	}
}
