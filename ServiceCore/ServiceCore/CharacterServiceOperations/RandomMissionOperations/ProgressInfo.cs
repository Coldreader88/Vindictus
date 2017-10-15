using System;

namespace ServiceCore.CharacterServiceOperations.RandomMissionOperations
{
	[Serializable]
	public class ProgressInfo
	{
		public int ID { get; set; }

		public string Title { get; set; }

		public int Current { get; set; }

		public int Max { get; set; }

		public override string ToString()
		{
			string text = "";
			object obj = text;
			text = string.Concat(new object[]
			{
				obj,
				"ID : ",
				this.ID,
				"\n"
			});
			text = text + "Title : " + this.Title + "\n";
			object obj2 = text;
			text = string.Concat(new object[]
			{
				obj2,
				"Current : ",
				this.Current,
				"\n"
			});
			object obj3 = text;
			return string.Concat(new object[]
			{
				obj3,
				"Max : ",
				this.Max,
				"\n"
			});
		}
	}
}
