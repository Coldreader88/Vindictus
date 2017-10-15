using System;
using System.Text.RegularExpressions;

namespace ServiceCore.PlayerServiceOperations
{
	[Serializable]
	public sealed class StoryDropData
	{
		public int StoryDropID { get; set; }

		public string ObjectType { get; set; }

		public string ObjectRegexString { get; set; }

		public string ItemClass { get; set; }

		public int Amount { get; set; }

		public int CurrentAmount { get; set; }

		public int Probability { get; set; }

		public int TryCount { get; set; }

		public int MaxTryCount { get; set; }

		public StoryDropData(int storyDropID, string objectType, string regexString, string itemClass, int currentAmount, int amount, int probability, int tryCount, int maxTryCount)
		{
			this.StoryDropID = storyDropID;
			this.ObjectType = objectType;
			this.ObjectRegexString = regexString;
			this.ItemClass = itemClass;
			this.CurrentAmount = currentAmount;
			this.Amount = amount;
			this.Probability = probability;
			this.TryCount = tryCount;
			this.MaxTryCount = maxTryCount;
		}

		[NonSerialized]
		public Regex ObjectRegex;
	}
}
