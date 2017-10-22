using System;
using System.Collections.Generic;
using System.Reflection;
using Devcat.Core.Net.Message;
using Devcat.Core.Threading;
using ServiceCore;
using ServiceCore.Configuration;
using ServiceCore.TradeServiceOperations;
using TradeService.Properties;
using UnifiedNetwork.Entity;
using Utility;

namespace TradeService
{
	internal class TradeService : Service
	{
		public TradeItemAvgPrice TradeItemAvgPrice { get; set; }

		public override void Initialize(JobProcessor thread)
		{
			IEnumerable<Type> types = OperationTypes.Types((Type type) => type.Namespace != null && type.Namespace == typeof(TradeServiceOperations).Namespace);
			ConnectionStringLoader.LoadFromServiceCore(Settings.Default);
			base.Initialize(thread, types.GetConverter());
			base.RegisterMessage(OperationMessages.TypeConverters);
			base.RegisterAllProcessors(Assembly.GetExecutingAssembly());
			this.TradeItemAvgPrice = new TradeItemAvgPrice(this);
		}

		public override int CompareAndSwapServiceID(long id, string category, int beforeID)
		{
			ushort num = (ushort)id;
			if (category != base.Category || ((int)num != base.ID && !base.LookUp.FindIndex(category).Contains((int)num)))
			{
				Log<TradeService>.Logger.InfoFormat("Invalid entity location : [{0} {1} {2}]", id, num, base.ID);
				return -1;
			}
			return (int)num;
		}

		internal static TradeService Instance { get; private set; }

		public static Service StartService(string ip, string portstr)
		{
			TradeService.Instance = new TradeService();
			ServiceInvoker.StartService(ip, portstr, TradeService.Instance);
			return TradeService.Instance;
		}
	}
}
