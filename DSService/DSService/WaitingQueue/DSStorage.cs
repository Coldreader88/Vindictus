using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using ServiceCore;
using ServiceCore.DSServiceOperations;
using Utility;

namespace DSService.WaitingQueue
{
	public class DSStorage
	{
		public string GiantRaidProcessFormula { get; set; }

		public string NormalRaidProcessFormula { get; set; }

		public string PvpProcessFormula { get; set; }

		public int? GiantRaidMachineCount { get; set; }

		public int? NormalRaidMachineCount { get; set; }

		public int? PvpMachineCount { get; set; }

		public int? DevMachineCount { get; set; }

		public int? IsolateNormalRaidMachineCount { get; set; }

		public List<DSType> DSTypeForLeftOvers { get; set; }

		public Dictionary<DSType, int> DSMachineCount { get; set; }

		public Dictionary<int, DSInfo> PvpDSMap { get; set; }

		public List<string> IsolateNormalQuest { get; set; }

		public Dictionary<int, DSInfo> DSMap { get; set; }

		public Dictionary<int, DSShip> DSShipMap { get; set; }

		public DSStorage()
		{
			this.GiantRaidProcessFormula = FeatureMatrix.GetString("DSGiantRaidProcessCount");
			this.NormalRaidProcessFormula = FeatureMatrix.GetString("DSNormalRaidProcessCount");
			this.PvpProcessFormula = FeatureMatrix.GetString("DSPvpProcessCount");
			this.GiantRaidMachineCount = new int?(ServiceCoreSettings.Default.DSGiantRaidMachineCount);
			this.NormalRaidMachineCount = new int?(ServiceCoreSettings.Default.DSNormalRaidMachineCount);
			this.PvpMachineCount = new int?(ServiceCoreSettings.Default.DSPvpMachineCount);
			this.DevMachineCount = new int?(ServiceCoreSettings.Default.DSDevMachineCount);
			this.IsolateNormalRaidMachineCount = new int?(ServiceCoreSettings.Default.DSIsolateNormalRaidMachineCount);
			this.DSTypeForLeftOvers = new List<DSType>();
			if (this.GiantRaidMachineCount == -1)
			{
				this.DSTypeForLeftOvers.Add(DSType.GiantRaid);
			}
			if (this.NormalRaidMachineCount == -1)
			{
				this.DSTypeForLeftOvers.Add(DSType.NormalRaid);
			}
			if (this.PvpMachineCount == -1)
			{
				this.DSTypeForLeftOvers.Add(DSType.Pvp);
			}
			if (this.DevMachineCount == -1)
			{
				this.DSTypeForLeftOvers.Add(DSType.Dev_All);
			}
			this.IsolateNormalQuest = new List<string>();
			string[] array = ServiceCoreSettings.Default.DSIsolateNormalQuest.Split(new char[]
			{
				',',
				';'
			});
			foreach (string input in array)
			{
				string item = Regex.Replace(input, "\\s", "");
				this.IsolateNormalQuest.Add(item);
			}
			this.DSMachineCount = new Dictionary<DSType, int>();
			this.PvpDSMap = new Dictionary<int, DSInfo>();
			this.DSMap = new Dictionary<int, DSInfo>();
			this.DSShipMap = new Dictionary<int, DSShip>();
		}

		public DSInfo GetWaitingDS(DSType dsType)
		{
			Random random = new Random();
			DSInfo[] array = (from x in this.DSMap.Values
			where this.DSShipMap.TryGetValue(x.DSID) == null && x.DSType == dsType
			select x).ToArray<DSInfo>();
			if (array.Length < 1)
			{
				return null;
			}
			return array[random.Next(0, array.Length)];
		}

		public override string ToString()
		{
			Dictionary<DSShipState, int> dictionary = new Dictionary<DSShipState, int>();
			foreach (DSInfo dsinfo in this.DSMap.Values)
			{
				DSShip dsship = this.DSShipMap.TryGetValue(dsinfo.DSID);
				if (dsship == null)
				{
					dictionary.AddOrIncrease(DSShipState.Initial, 1);
				}
				else
				{
					dictionary.AddOrIncrease(dsship.ShipState, 1);
				}
			}
			int num = (from x in this.DSMap.Values
			where x.DSType == DSType.GiantRaid
			select x).Count<DSInfo>();
			int num2 = (from x in this.DSMap.Values
			where x.DSType == DSType.Pvp
			select x).Count<DSInfo>();
			StringBuilder stringBuilder = new StringBuilder("========ds storage======\n");
			stringBuilder.AppendFormat("Total : {0}, Giant Raid {1} Pvp {2}\n", this.DSMap.Count, num, num2);
			foreach (KeyValuePair<DSShipState, int> keyValuePair in dictionary)
			{
				stringBuilder.AppendFormat("{0} : {1}", keyValuePair.Key, keyValuePair.Value);
			}
			return stringBuilder.ToString();
		}

		private int RegisterDSEntity(int serviceID, int start, int count, DSType dsType)
		{
			foreach (int num in Enumerable.Range(start, count))
			{
				DSInfo value = new DSInfo(num, serviceID, dsType);
				if (dsType != DSType.Pvp)
				{
					this.DSMap.Add(num, value);
				}
				else
				{
					this.PvpDSMap.Add(num, value);
				}
			}
			Log<DSWaitingSystem>.Logger.WarnFormat("Register DSInfo : {0} ~ {1}", start, start + count - 1);
			return start + count;
		}

		private DSType GetNextDSType(bool giantRaidMachine)
		{
			if (0 < this.GiantRaidMachineCount && this.DSMachineCount.TryGetValue(DSType.GiantRaid) < this.GiantRaidMachineCount && giantRaidMachine)
			{
				this.DSMachineCount.AddOrIncrease(DSType.GiantRaid, 1);
				return DSType.GiantRaid;
			}
			if (0 < this.PvpMachineCount && this.DSMachineCount.TryGetValue(DSType.Pvp) < this.PvpMachineCount)
			{
				this.DSMachineCount.AddOrIncrease(DSType.Pvp, 1);
				return DSType.Pvp;
			}
			if (0 < this.IsolateNormalRaidMachineCount && this.DSMachineCount.TryGetValue(DSType.IsolateNormalRaid) < this.IsolateNormalRaidMachineCount)
			{
				this.DSMachineCount.AddOrIncrease(DSType.IsolateNormalRaid, 1);
				return DSType.IsolateNormalRaid;
			}
			if (0 < this.NormalRaidMachineCount && this.DSMachineCount.TryGetValue(DSType.NormalRaid) < this.NormalRaidMachineCount)
			{
				this.DSMachineCount.AddOrIncrease(DSType.NormalRaid, 1);
				return DSType.NormalRaid;
			}
			if (0 < this.DevMachineCount && this.DSMachineCount.TryGetValue(DSType.Dev_All) < this.DevMachineCount)
			{
				this.DSMachineCount.AddOrIncrease(DSType.Dev_All, 1);
				return DSType.Dev_All;
			}
			int num = this.DSMachineCount.Values.Sum();
			DSType dstype = this.DSTypeForLeftOvers[num % this.DSTypeForLeftOvers.Count];
			this.DSMachineCount.AddOrIncrease(dstype, 1);
			return dstype;
		}

		private int ToInt(string str, int defaultValue)
		{
			int result;
			if (int.TryParse(str.Trim(), out result))
			{
				return result;
			}
			return defaultValue;
		}

		private int GetProcessCount(string formula, int coreCount)
		{
			if (formula == "" || formula == null)
			{
				return 0;
			}
			if (formula.Contains('C'))
			{
				int num = formula.IndexOf('C');
				string str = formula.Substring(0, num);
				string str2 = (formula.Length > num) ? formula.Substring(num + 1) : "";
				return this.ToInt(str, 1) * coreCount + this.ToInt(str2, 0);
			}
			return this.ToInt(formula, 1);
		}

		public DSType RegisterDSService(int serviceID, int coreCount, bool giantRaidMachine, out int idStart, out int processCount)
		{
			idStart = this.DSMap.Count + this.PvpDSMap.Count;
			DSType nextDSType = this.GetNextDSType(giantRaidMachine);
			switch (nextDSType)
			{
			case DSType.GiantRaid:
				processCount = this.GetProcessCount(this.GiantRaidProcessFormula, coreCount);
				this.RegisterDSEntity(serviceID, idStart, processCount, DSType.GiantRaid);
				return nextDSType;
			case DSType.NormalRaid:
				processCount = this.GetProcessCount(this.NormalRaidProcessFormula, coreCount);
				this.RegisterDSEntity(serviceID, idStart, processCount, DSType.NormalRaid);
				return nextDSType;
			case DSType.Pvp:
				processCount = this.GetProcessCount(this.PvpProcessFormula, coreCount);
				this.RegisterDSEntity(serviceID, idStart, processCount, DSType.Pvp);
				DSService.Instance.UpdatePvpDS();
				return nextDSType;
			case DSType.IsolateNormalRaid:
				processCount = this.GetProcessCount(this.NormalRaidProcessFormula, coreCount);
				this.RegisterDSEntity(serviceID, idStart, processCount, DSType.IsolateNormalRaid);
				return nextDSType;
			}
			int num = idStart;
			processCount = this.GetProcessCount(this.PvpProcessFormula, coreCount);
			num = this.RegisterDSEntity(serviceID, num, processCount, DSType.Pvp);
			processCount = this.GetProcessCount(this.GiantRaidProcessFormula, coreCount);
			num = this.RegisterDSEntity(serviceID, num, processCount, DSType.GiantRaid);
			processCount = this.GetProcessCount(this.NormalRaidProcessFormula, coreCount);
			num = this.RegisterDSEntity(serviceID, num, processCount, DSType.NormalRaid);
			processCount = this.GetProcessCount(this.NormalRaidProcessFormula, coreCount);
			num = this.RegisterDSEntity(serviceID, num, processCount, DSType.IsolateNormalRaid);
			processCount = num - idStart;
			DSService.Instance.UpdatePvpDS();
			return nextDSType;
		}

		public bool CheckIsolateNormalQuest(string questID)
		{
			return this.IsolateNormalQuest != null && this.IsolateNormalRaidMachineCount > 0 && this.IsolateNormalQuest.Contains(questID);
		}
	}
}
