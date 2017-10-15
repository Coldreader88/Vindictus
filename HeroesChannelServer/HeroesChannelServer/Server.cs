using System;
using System.Collections.Generic;
using Devcat.Core;
using Devcat.Core.Net;
using Devcat.Core.Net.Message;
using Devcat.Core.Threading;
using ServiceCore.EndPointNetwork;
using Utility;

namespace HeroesChannelServer
{
	public class Server
	{
		public event EventHandler<EventArgs<Client>> ClientIdentified;

		public string Address
		{
			get
			{
				return this.server.LocalEndPoint.Address.ToString();
			}
		}

		public int Port
		{
			get
			{
				return this.server.LocalEndPoint.Port;
			}
		}

		public JobProcessor Thread
		{
			get
			{
				return this.thread;
			}
		}

		public Server(JobProcessor thread, int port)
		{
			this.thread = thread;
			this.server = new TcpServer2();
			this.server.ClientAccept += this.server_ClientAccept;
			this.server.Start(thread, ServerBindType.PublicPrivate, port);
			this.mf.Register<Client>(Messages.TypeConverters, "ProcessMessage");
		}

		private void server_ClientAccept(object sender, AcceptEventArgs e)
		{
			e.PacketAnalyzer = new MessageAnalyzer();
			e.JobProcessor = this.thread;
			e.Client.Tag = new Client(this, e.Client, this.mf);
			this.thread.Enqueue(Job.Create(delegate
			{
				object typeConverter = this.mf.GetTypeConverter();
				e.Client.Transmit(SerializeWriter.ToBinary(typeConverter));
			}));
			Log<Server>.Logger.Info("server_ClientAccepted!");
		}

		public int KeyGen(long id)
		{
			int num = this.random.Next();
			this.keyDic[id] = num;
			return num;
		}

		public bool VerifyKey(long id, int key)
		{
			int num;
			if (this.keyDic.TryGetValue(id, out num))
			{
				this.keyDic.Remove(id);
				if (key != num)
				{
					Log<Client>.Logger.WarnFormat("invalid key : [{0} != {1}]", key, num);
				}
				return key == num;
			}
			return false;
		}

		public int RemoveKey(long id)
		{
			int result;
			if (this.keyDic.TryGetValue(id, out result))
			{
				this.keyDic.Remove(id);
				return result;
			}
			return -1;
		}

		internal void IdentifyClient(Client client)
		{
			if (client != null && this.ClientIdentified != null)
			{
				this.ClientIdentified(this, new EventArgs<Client>(client));
			}
		}

		private ITcpServer server;

		private JobProcessor thread;

		private MessageHandlerFactory mf = new MessageHandlerFactory();

		private Dictionary<long, int> keyDic = new Dictionary<long, int>();

		private Random random = new Random();
	}
}
