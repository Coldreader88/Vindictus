using System;

namespace MMOServer
{
	internal struct PairKey<V1, V2>
	{
		public PairKey(V1 v1, V2 v2)
		{
			this.v1 = v1;
			this.v2 = v2;
		}

		public override bool Equals(object obj)
		{
			if (base.GetType() != obj.GetType())
			{
				return false;
			}
			PairKey<V1, V2> pairKey = (PairKey<V1, V2>)obj;
			return pairKey.v1.Equals(this.v1) && pairKey.v2.Equals(this.v2);
		}

		public override int GetHashCode()
		{
			return this.v1.GetHashCode() ^ this.v2.GetHashCode();
		}

		public V1 v1;

		public V2 v2;
	}
}
