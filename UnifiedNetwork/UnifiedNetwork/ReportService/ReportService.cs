using System;
using System.Collections.Generic;
using System.Net;
using Devcat.Core.Net;
using Devcat.Core.Net.Message;
using Devcat.Core.Threading;
using UnifiedNetwork.Entity.Operations;
using UnifiedNetwork.LocationService;
using UnifiedNetwork.OperationService;
using UnifiedNetwork.Properties;
using UnifiedNetwork.ReportService.Operations;
using Utility;

namespace UnifiedNetwork.ReportService
{
	public class ReportService : Service
	{
		public MessageHandlerFactory MF
		{
			get
			{
				return this.mf;
			}
		}

		public override void Initialize(JobProcessor thread)
		{
			Log<ReportService>.Logger.Error("Report Service Failure : Cannot find listen port");
		}

		public void Initialize(JobProcessor thread, int listenport)
		{
			base.ID = this.ReportServiceID;
			base.Initialize(thread, null);
			base.RegisterMessage(ReportServiceOperationMessages.TypeConverters);
			base.RegisterMessage(Messages.TypeConverters);
			this.acceptor.ClientAccept += this.server_ClientAccept;
			this.acceptor.Start(thread, listenport);
			Log<ReportService>.Logger.Debug("Starts ReportService");
			this.mf.Register<ReportAdminClient>(ReportServiceOperationMessages.TypeConverters, "ProcessMessage");
		}

		private void server_ClientAccept(object sender, AcceptEventArgs e)
		{
			e.PacketAnalyzer = new MessageAnalyzer();
			e.Client.Disconnected += this.Client_Disconnected;
			e.Client.Tag = this.clientTag++;
			this.controllers.Add((int)e.Client.Tag, new ReportAdminClient(this, e.Client));
			Log<ReportService>.Logger.DebugFormat("Admin Client Connected. (id = {0})", (int)e.Client.Tag);
		}

		private void Client_Disconnected(object sender, EventArgs e)
		{
			int num = (int)(sender as TcpClient).Tag;
			Log<ReportService>.Logger.DebugFormat("Admin Client Disconnected. (id = {0})", num);
			ReportAdminClient reportAdminClient;
			if (this.controllers.TryGetValue(num, out reportAdminClient))
			{
				this.controllers.Remove(num);
			}
		}

		public static void StartService(string ip, string portstr)
		{
			int port = int.Parse(portstr);
			IPAddress[] hostAddresses = Dns.GetHostAddresses(ip);
			if (hostAddresses.Length == 0)
			{
				Log<Service>.Logger.ErrorFormat("cannot find host [{0}]", ip);
				return;
			}
			IPAddress address = hostAddresses[new Random().Next(hostAddresses.Length - 1)];
			IPEndPoint arg = new IPEndPoint(address, port);
			ReportService reportService = new ReportService();
			JobProcessor jobProcessor = new JobProcessor();
			reportService.Initialize(jobProcessor, (int)Settings.Default.ReportServiceListenPort);
			UnifiedNetwork.LocationService.LookUp @object = new UnifiedNetwork.LocationService.LookUp(reportService);
			jobProcessor.Start();
			jobProcessor.Enqueue(Job.Create<IPEndPoint>(new Action<IPEndPoint>(@object.StartService), arg));
		}

		private readonly int ReportServiceID = 65536;

		private TcpServer acceptor = new TcpServer();

		private Dictionary<int, ReportAdminClient> controllers = new Dictionary<int, ReportAdminClient>();

		private int clientTag = 1;

		private MessageHandlerFactory mf = new MessageHandlerFactory();
	}
}
