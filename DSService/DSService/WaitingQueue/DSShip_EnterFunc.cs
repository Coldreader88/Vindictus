using System;
using System.Collections.Generic;
using System.Linq;

namespace DSService.WaitingQueue
{
	public static class DSShip_EnterFunc
	{
		public static Func<DSShip, Dictionary<long, DSPlayer>, bool> GetEnterFunc(string questID)
		{
			if (questID != null)
			{
				if (questID == "quest_dragon_ancient_elculous" || questID == "quest_dragon_ancient_siglint" || questID == "quest_dragon_ancient_bugeikloth")
				{
					return new Func<DSShip, Dictionary<long, DSPlayer>, bool>(DSShip_EnterFunc.High16Low8);
				}
				if (questID == "quest_giant_spider_all" || questID == "quest_bear_giant")
				{
					return new Func<DSShip, Dictionary<long, DSPlayer>, bool>(DSShip_EnterFunc.High16Low24);
				}
			}
			return new Func<DSShip, Dictionary<long, DSPlayer>, bool>(DSShip_EnterFunc.NoConstraint);
		}

		public static bool NoConstraint(DSShip ship, Dictionary<long, DSPlayer> players)
		{
			return true;
		}

		public static bool High16Low8(DSShip ship, Dictionary<long, DSPlayer> players)
		{
			int num = 0;
			int num2 = 0;
			foreach (DSPlayer dsplayer in ship.Players.Values.Concat(players.Values))
			{
				if (dsplayer.Level >= 60)
				{
					num++;
				}
				else
				{
					num2++;
				}
				if (num > 16)
				{
					return false;
				}
				if (num2 > 8)
				{
					return false;
				}
			}
			return true;
		}

		public static bool High16Low24(DSShip ship, Dictionary<long, DSPlayer> players)
		{
			int num = 0;
			int num2 = 0;
			foreach (DSPlayer dsplayer in ship.Players.Values.Concat(players.Values))
			{
				if (dsplayer.Level >= 60)
				{
					num++;
				}
				else
				{
					num2++;
				}
				if (num > 16)
				{
					return false;
				}
				if (num2 > 24)
				{
					return false;
				}
			}
			return true;
		}
	}
}
