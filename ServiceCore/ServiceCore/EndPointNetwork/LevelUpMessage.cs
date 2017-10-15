using System;
using System.Collections.Generic;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class LevelUpMessage : IMessage
	{
		public int Level { get; set; }

		public IDictionary<string, int> StatIncreased { get; set; }

		public LevelUpMessage(int Level, List<KeyValuePair<string, int>> increased)
		{
			this.Level = Level;
			this.StatIncreased = new Dictionary<string, int>();
			foreach (KeyValuePair<string, int> keyValuePair in increased)
			{
				this.StatIncreased.Add(keyValuePair.Key, keyValuePair.Value);
			}
		}

		public override string ToString()
		{
			return string.Format("LevelUpMessage [ Level = {0} ]", this.Level);
		}
	}
}
