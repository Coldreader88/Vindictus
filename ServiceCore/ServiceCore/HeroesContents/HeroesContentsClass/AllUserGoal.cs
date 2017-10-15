using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Utility;

namespace ServiceCore.HeroesContents.HeroesContentsClass
{
	public class AllUserGoal
	{
		public void Initialize()
		{
			AllUserGoal.AllUserGoalDicByGoal.Clear();
			AllUserGoal.AllUserGoalDicBySlotID.Clear();
			AllUserGoal.AllUserGoalDicByID.Clear();
			AllUserGoal.RefreshEventSlot.Clear();
			List<AllUserGoalInfo> list = (from x in HeroesContentsLoader.GetTable<AllUserGoalInfo>()
			where ServiceCore.FeatureMatrix.IsEnable(x.Feature) && ServiceCore.FeatureMatrix.IsMatchServerCode(x.ServerCode)
			select x).ToList<AllUserGoalInfo>();
			foreach (AllUserGoalInfo allUserGoalInfo in list)
			{
				Regex key = new Regex(allUserGoalInfo.Goal, RegexOptions.IgnoreCase | RegexOptions.Compiled);
				if (!AllUserGoal.AllUserGoalDicByGoal.ContainsKey(key))
				{
					AllUserGoal.AllUserGoalDicByGoal.Add(key, new List<AllUserGoalData>());
				}
				AllUserGoalData allUserGoalData = new AllUserGoalData(allUserGoalInfo);
				if (AllUserGoal.AllUserGoalDicByGoal.ContainsKey(key))
				{
					AllUserGoal.AllUserGoalDicByGoal[key].Add(allUserGoalData);
					AllUserGoal.AllUserGoalDicByID.Add(allUserGoalData.GoalID, allUserGoalData);
				}
				if (!AllUserGoal.AllUserGoalDicBySlotID.ContainsKey(allUserGoalInfo.SlotID))
				{
					AllUserGoal.AllUserGoalDicBySlotID.Add(allUserGoalInfo.SlotID, new List<AllUserGoalData>());
					AllUserGoal.AllUserGoalDicBySlotID[allUserGoalInfo.SlotID].Add(allUserGoalData);
				}
				else
				{
					AllUserGoal.AllUserGoalDicBySlotID[allUserGoalInfo.SlotID].Add(allUserGoalData);
				}
				if (!AllUserGoal.RefreshEventSlot.ContainsKey(allUserGoalInfo.StartTime))
				{
					AllUserGoal.RefreshEventSlot.Add(allUserGoalInfo.StartTime, new List<AllUserGoalData>());
					AllUserGoal.RefreshEventSlot[allUserGoalInfo.StartTime].Add(allUserGoalData);
				}
				else
				{
					AllUserGoal.RefreshEventSlot[allUserGoalInfo.StartTime].Add(allUserGoalData);
				}
			}
		}

		public void SetEndEventByID(int goalID, bool isEnd)
		{
			if (AllUserGoal.AllUserGoalDicByID.ContainsKey(goalID))
			{
				AllUserGoal.AllUserGoalDicByID[goalID].IsEndEvent = isEnd;
			}
		}

		public int GetCurrentIDByGoal(string goal)
		{
			foreach (KeyValuePair<Regex, List<AllUserGoalData>> keyValuePair in AllUserGoal.AllUserGoalDicByGoal)
			{
				if (keyValuePair.Key.IsMatch(goal))
				{
					foreach (AllUserGoalData allUserGoalData in keyValuePair.Value)
					{
						if (!allUserGoalData.IsEndEvent && allUserGoalData.StartTime <= DateTime.UtcNow && DateTime.UtcNow < allUserGoalData.EndTime)
						{
							return allUserGoalData.GoalID;
						}
					}
				}
			}
			return -1;
		}

		public int GetGoalCountByID(int goalID)
		{
			if (AllUserGoal.AllUserGoalDicByID.ContainsKey(goalID))
			{
				return AllUserGoal.AllUserGoalDicByID[goalID].GoalCount;
			}
			Log<AllUserGoal>.Logger.FatalFormat("GetGoalCountByID :: AllUserGoalDicByID is not Contains GoalID : {0}", goalID);
			return -1;
		}

		public AllUserGoalData GetGoalDataByID(int goalID)
		{
			if (AllUserGoal.AllUserGoalDicByID.ContainsKey(goalID))
			{
				return AllUserGoal.AllUserGoalDicByID[goalID];
			}
			Log<AllUserGoal>.Logger.FatalFormat("GetGoalDataByID :: AllUserGoalDicByID is not Contains GoalID : {0}", goalID);
			return null;
		}

        public AllUserGoalData GetCurrentGoalDataBySlotID(int slotID)
        {
            if (AllUserGoal.AllUserGoalDicBySlotID.ContainsKey(slotID))
            {
                using (List<AllUserGoalData>.Enumerator enumerator = AllUserGoal.AllUserGoalDicBySlotID[slotID].GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        AllUserGoalData current = enumerator.Current;
                        if (current.StartTime <= DateTime.UtcNow && DateTime.UtcNow < current.EndTime)
                        {
                            return current;
                        }
                    }
                    return null;
                }
            }
            Log<AllUserGoal>.Logger.WarnFormat("GetCurrentGoalDataBySlotID :: AllUserGoalDicBySlotID is not Contains SlotID : {0}", slotID);
            return null;
        }

        public AllUserGoalData GetNextGoalDataBySlotID(int slotID)
        {
            if (AllUserGoal.AllUserGoalDicBySlotID.ContainsKey(slotID))
            {
                using (IEnumerator<AllUserGoalData> enumerator = (from x in AllUserGoal.AllUserGoalDicBySlotID[slotID]
                                                                  orderby x.StartTime
                                                                  select x).GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        AllUserGoalData current = enumerator.Current;
                        if (DateTime.UtcNow < current.StartTime)
                        {
                            return current;
                        }
                    }
                    return null;
                }
            }
            Log<AllUserGoal>.Logger.WarnFormat("GetNextGoalDataBySlotID :: AllUserGoalDicBySlotID is not Contains SlotID : {0}", slotID);
            return null;
        }

        public List<AllUserGoalData> GetTimeData(DateTime startTime)
		{
			if (AllUserGoal.RefreshEventSlot.ContainsKey(startTime))
			{
				return AllUserGoal.RefreshEventSlot[startTime];
			}
			return null;
		}

		public DateTime GetNextTimeRefreshTime()
		{
			foreach (KeyValuePair<DateTime, List<AllUserGoalData>> keyValuePair in from x in AllUserGoal.RefreshEventSlot
			orderby x.Key
			select x)
			{
				if (DateTime.UtcNow < keyValuePair.Key)
				{
					return keyValuePair.Key;
				}
			}
			return DateTime.MinValue;
		}

		public int GetGoalTypeByID(int goalID)
		{
			if (AllUserGoal.AllUserGoalDicByID.ContainsKey(goalID))
			{
				return AllUserGoal.AllUserGoalDicByID[goalID].GoalType;
			}
			Log<AllUserGoal>.Logger.FatalFormat("GetGoalTypeByID :: AllUserGoalDicByID is not Contains GoalID : {0}", goalID);
			return 0;
		}

		public bool IsGroupGoal(int goalType)
		{
			return goalType == 1;
		}

		public static Dictionary<Regex, List<AllUserGoalData>> AllUserGoalDicByGoal = new Dictionary<Regex, List<AllUserGoalData>>();

		public static Dictionary<int, List<AllUserGoalData>> AllUserGoalDicBySlotID = new Dictionary<int, List<AllUserGoalData>>();

		public static Dictionary<DateTime, List<AllUserGoalData>> RefreshEventSlot = new Dictionary<DateTime, List<AllUserGoalData>>();

		public static Dictionary<int, AllUserGoalData> AllUserGoalDicByID = new Dictionary<int, AllUserGoalData>();

		public enum AllUserGoalType
		{
			ENUM_GOALTYPE_UNKWON,
			ENUM_GOALTYPE_KILL,
			ENUM_GOALTYPE_GETITEM,
			ENUM_GOALTYPE_CLEAR
		}
	}
}
