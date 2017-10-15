using System;
using System.Collections.Generic;
using System.Linq;
using ServiceCore;
using ServiceCore.HeroesContents;

namespace MMOChannelService
{
	public class MMOChannelContents
	{
		internal static List<BurnJackpot> BurnJackpot { get; set; }

		static MMOChannelContents()
		{
			MMOChannelContents.Load();
			HeroesContentsLoader.DB3Changed += MMOChannelContents.Load;
		}

		public static void Initialize()
		{
		}

		public static void Load()
		{
			MMOChannelContents.BurnJackpot = (from x in HeroesContentsLoader.GetTable<BurnJackpot>()
			where ServiceCore.FeatureMatrix.IsEnable(x.Feature)
			select x).ToList<BurnJackpot>();
		}

		public static BurnJackpot GetBurnJackpot()
		{
			foreach (BurnJackpot burnJackpot in MMOChannelContents.BurnJackpot)
			{
				if (ServiceCore.FeatureMatrix.IsEnable(burnJackpot.Feature))
				{
					return burnJackpot;
				}
			}
			return null;
		}
	}
}
