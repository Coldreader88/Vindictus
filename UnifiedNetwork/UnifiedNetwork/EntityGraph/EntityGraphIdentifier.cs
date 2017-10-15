using System;

namespace UnifiedNetwork.EntityGraph
{
	[Serializable]
	public sealed class EntityGraphIdentifier
	{
		public long entityID { get; set; }

		public int serviceID { get; set; }

		public string category { get; set; }

		public string states { get; set; }

		public EntityGraphIdentifier()
		{
			this.entityID = 0L;
			this.serviceID = 1048576;
			this.category = "#";
			this.states = "#";
		}

		public bool isNull
		{
			get
			{
				return this.category == "#" && this.serviceID == 1048576;
			}
		}

		public bool isNumeric
		{
			get
			{
				return this.category == "#";
			}
		}

		public bool isCategoric
		{
			get
			{
				return this.serviceID == 1048576;
			}
		}

		public override string ToString()
		{
			return string.Format("[{0},{1}/{2}]", this.serviceID, this.category, this.entityID);
		}
	}
}
