using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ServiceCore.HeroesContents.HeroesContentsClass
{
	public class TodayMission
	{
		static TodayMission()
		{
			TodayMission.Init();
		}

		public static void Init()
		{
			List<TodayMissionGoalInfo> list = HeroesContentsLoader.GetTable<TodayMissionGoalInfo>().ToList<TodayMissionGoalInfo>();
			foreach (TodayMissionGoalInfo todayMissionGoalInfo in list)
			{
				Regex key = new Regex(todayMissionGoalInfo.MissionGoal, RegexOptions.IgnoreCase | RegexOptions.Compiled);
				if (!TodayMission.TodayMissionGoalInfo.ContainsKey(key))
				{
					TodayMission.TodayMissionGoalInfo.Add(key, new List<TodayMissionGoalInfo>());
				}
				TodayMission.TodayMissionGoalInfo[key].Add(todayMissionGoalInfo);
			}
		}

		public List<TodayMissionGoalInfo> GetMissionGoalInfoList(string cmd)
		{
			List<TodayMissionGoalInfo> list = new List<TodayMissionGoalInfo>();
			foreach (KeyValuePair<Regex, List<TodayMissionGoalInfo>> keyValuePair in TodayMission.TodayMissionGoalInfo)
			{
				if (keyValuePair.Key.IsMatch(cmd))
				{
					foreach (TodayMissionGoalInfo item in keyValuePair.Value)
					{
						list.Add(item);
					}
				}
			}
			return list;
		}

		public static Dictionary<Regex, List<TodayMissionGoalInfo>> TodayMissionGoalInfo = new Dictionary<Regex, List<TodayMissionGoalInfo>>();
	}
}
