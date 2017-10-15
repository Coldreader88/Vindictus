using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.DSServiceOperations
{
	[Serializable]
	public sealed class QueryDSInfo : Operation
	{
		public string IP
		{
			get
			{
				return this.iP;
			}
			set
			{
				this.iP = value;
			}
		}

		public int DSProcessCount
		{
			get
			{
				return this.dSProcessCount;
			}
			set
			{
				this.dSProcessCount = value;
			}
		}

		public int WaitingPartyCount
		{
			get
			{
				return this.waitingPartyCount;
			}
			set
			{
				this.waitingPartyCount = value;
			}
		}

		public int ShipCount
		{
			get
			{
				return this.shipCount;
			}
			set
			{
				this.shipCount = value;
			}
		}

		public List<Dictionary<string, string>> ProcessInfo
		{
			get
			{
				return this.processInfo;
			}
			set
			{
				this.processInfo = value;
			}
		}

		public Dictionary<string, Dictionary<string, int>> WaitingQueueInfo
		{
			get
			{
				return this.queueInfo;
			}
			set
			{
				this.queueInfo = value;
			}
		}

		public override int TimeOut
		{
			get
			{
				return this.timeOut;
			}
			set
			{
				this.timeOut = value;
			}
		}

		public override OperationProcessor RequestProcessor()
		{
			return new QueryDSInfo.Request(this);
		}

		public override string ToString()
		{
			string text = string.Concat(new object[]
			{
				"\nIP                 : ",
				this.IP,
				"\nDSProcessCount     : ",
				this.DSProcessCount,
				"\nWaitingPartyCount  : ",
				this.WaitingPartyCount,
				"\nShipCount          : ",
				this.ShipCount,
				"\n"
			});
			if (this.ProcessInfo == null)
			{
				return text;
			}
			foreach (Dictionary<string, string> dictionary in this.ProcessInfo)
			{
				text += "________________________________________________\n";
				foreach (KeyValuePair<string, string> keyValuePair in dictionary)
				{
					string text2 = text;
					text = string.Concat(new string[]
					{
						text2,
						keyValuePair.Key,
						"\t\t\t\t\t",
						keyValuePair.Value,
						"\n"
					});
				}
				text += "________________________________________________\n";
			}
			return text;
		}

		[NonSerialized]
		private string iP;

		[NonSerialized]
		private int dSProcessCount;

		[NonSerialized]
		private int waitingPartyCount;

		[NonSerialized]
		private int shipCount;

		[NonSerialized]
		private List<Dictionary<string, string>> processInfo;

		[NonSerialized]
		private Dictionary<string, Dictionary<string, int>> queueInfo;

		[NonSerialized]
		private int timeOut;

		private class Request : OperationProcessor<QueryDSInfo>
		{
			public Request(QueryDSInfo op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				base.Operation.IP = (string)base.Feedback;
				yield return null;
				int queueCount = (int)base.Feedback;
				yield return null;
				base.Operation.WaitingQueueInfo = new Dictionary<string, Dictionary<string, int>>();
				for (;;)
				{
					int num;
					queueCount = (num = queueCount) - 1;
					if (num <= 0)
					{
						break;
					}
					string queueCategory = (string)base.Feedback;
					yield return null;
					int dicCount = (int)base.Feedback;
					yield return null;
					Dictionary<string, int> countDic = new Dictionary<string, int>();
					for (;;)
					{
						int num2;
						dicCount = (num2 = dicCount) - 1;
						if (num2 <= 0)
						{
							break;
						}
						string userState = (string)base.Feedback;
						yield return null;
						int userCount = (int)base.Feedback;
						yield return null;
						countDic.Add(userState, userCount);
					}
					base.Operation.WaitingQueueInfo.Add(queueCategory, countDic);
				}
				int processCount = (int)base.Feedback;
				string key = "";
				string val = "";
				base.Operation.ProcessInfo = new List<Dictionary<string, string>>();
				for (;;)
				{
					int num3;
					processCount = (num3 = processCount) - 1;
					if (num3 <= 0)
					{
						break;
					}
					yield return null;
					int propertyCount = (int)base.Feedback;
					Dictionary<string, string> proDic = new Dictionary<string, string>();
					for (;;)
					{
						int num4;
						propertyCount = (num4 = propertyCount) - 1;
						if (num4 <= 0)
						{
							break;
						}
						yield return null;
						key = (string)base.Feedback;
						yield return null;
						val = (string)base.Feedback;
						proDic.Add(key, val);
					}
					base.Operation.ProcessInfo.Add(proDic);
				}
				yield break;
			}
		}
	}
}
