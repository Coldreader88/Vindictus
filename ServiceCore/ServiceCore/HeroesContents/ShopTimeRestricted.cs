using System;

namespace ServiceCore.HeroesContents
{
	public abstract class ShopTimeRestricted
	{
		public bool HasRestrictTimeInfo()
		{
			return this.GetResetTime() != null && this.GetPeriodDay() > 0 && this.GetRestrictCount() > 0;
		}

		public bool IsRestricted()
		{
			return this.GetRestrictCount() > 0;
		}

		public abstract short GetOrder();

		public abstract int GetRestrictCount();

		public abstract DateTime? GetResetTime();

		public abstract int GetPeriodDay();

		public abstract short FirstUniqueKey();

		public abstract short SecondUniqueKey();
	}
}
