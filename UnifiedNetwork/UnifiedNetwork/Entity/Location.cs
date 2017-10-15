using System;

namespace UnifiedNetwork.Entity
{
	public struct Location
	{
		public long ID { get; set; }

		public string Category { get; set; }

		public Location(long id, string category)
		{
			this = default(Location);
			this.ID = id;
			this.Category = category;
		}

		public override bool Equals(object obj)
		{
			return obj != null && base.GetType() == obj.GetType() && this.ID == ((Location)obj).ID && this.Category == ((Location)obj).Category;
		}

		public override int GetHashCode()
		{
			return this.ID.GetHashCode() ^ this.Category.GetHashCode();
		}
	}
}
