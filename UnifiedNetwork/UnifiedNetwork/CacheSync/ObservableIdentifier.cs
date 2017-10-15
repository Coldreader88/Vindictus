using System;

namespace UnifiedNetwork.CacheSync
{
	[Serializable]
	public struct ObservableIdentifier
	{
		public long ID { get; set; }

		public string Category { get; set; }

		public ObservableIdentifier(long id, string category)
		{
			this = default(ObservableIdentifier);
			this.ID = id;
			this.Category = category;
		}

		public override int GetHashCode()
		{
			return this.ID.GetHashCode() & this.Category.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			if (base.GetType() != obj.GetType())
			{
				return false;
			}
			ObservableIdentifier observableIdentifier = (ObservableIdentifier)obj;
			return this.ID == observableIdentifier.ID && this.Category == observableIdentifier.Category;
		}

		public override string ToString()
		{
			return string.Format("({0},\"{1}\"", this.ID, this.Category);
		}
	}
}
