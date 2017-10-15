using System;

namespace MMOServer
{
	internal struct Key
	{
		public Key(long id, string category)
		{
			this.id = id;
			this.category = category;
		}

		public override bool Equals(object obj)
		{
			if (base.GetType() != obj.GetType())
			{
				return false;
			}
			Key key = (Key)obj;
			return key.id == this.id && key.category == this.category;
		}

		public override int GetHashCode()
		{
			return this.id.GetHashCode() ^ this.category.GetHashCode();
		}

		public long id;

		public string category;
	}
}
