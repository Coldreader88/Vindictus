using System;
using ServiceCore;
using Utility;

namespace DSService
{
	public class DSLog
	{
		private static DSLogDataContext Context { get; set; }

		public static void AddLog(DSEntity ds, string action, string arg)
		{
			if (FeatureMatrix.IsEnable("DisableLogging"))
			{
				return;
			}
			if (DSLog.Context == null)
			{
				DSLog.Context = new DSLogDataContext();
			}
			try
			{
				DSLog.Context.AddDSLog(new int?(DSService.Instance.ID), new int?(ds.DSID), ds.QuestID ?? "", new long?(ds.PartyID), new long?(ds.MicroPlayID), new int?(-1), action ?? "", arg ?? "");
			}
			catch (Exception ex)
			{
				Log<DSLog>.Logger.Error("error while logging", ex);
				DSLog.Context = null;
			}
		}

		public static void AddLog(int dsid, string questID, long partyID, int state, string action, string arg)
		{
			if (FeatureMatrix.IsEnable("DisableLogging"))
			{
				return;
			}
			if (DSLog.Context == null)
			{
				DSLog.Context = new DSLogDataContext();
			}
			try
			{
				DSLog.Context.AddDSLog(new int?(DSService.Instance.ID), new int?(dsid), questID ?? "", new long?(partyID), new long?(-1L), new int?(state), action ?? "", arg ?? "");
			}
			catch (Exception ex)
			{
				Log<DSLog>.Logger.Error("error while logging", ex);
				DSLog.Context = null;
			}
		}
	}
}
