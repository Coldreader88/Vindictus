using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading;
using Devcat.Core;
using Devcat.Core.ExceptionHandler;
using Devcat.Core.Net;
using Devcat.Core.Threading;
using log4net;
using UnifiedNetwork.Cooperation;
using UnifiedNetwork.EntityGraph;
using UnifiedNetwork.FunctionSelector;
using UnifiedNetwork.PipedNetwork;
using UnifiedNetwork.Properties;
using UnifiedNetwork.ReportService.Operations;
using UnifiedNetwork.ReportService.Processors;
using Utility;

namespace UnifiedNetwork.OperationService
{
	public abstract class Service : IBootSequence, IDisposable, IEntityGraphNode, IFunctionSelector, IQueryPerformance
	{
		public bool BootResult
		{
			get
			{
				return this.bootStep == 0 || this.IsBooting || this.bootResult;
			}
			private set
			{
				this.bootResult = value;
			}
		}

		public bool IsBooting
		{
			get
			{
				return this.bootStep != 0 && this.bootStep > this.current;
			}
		}

		public void SetBootEvent()
		{
			Assembly assembly = Assembly.LoadFrom("Executer.exe");
			if (assembly != null)
			{
				Type type = assembly.GetType("Executer.Program");
				if (type != null)
				{
					MethodInfo methodInfo = type.GetMethod("SetLaunched");
					if (methodInfo != null)
					{
						this.OnBootFail(delegate
						{
							Environment.FailFast("Boot fail");
						});
						this.OnBootSuccess(delegate
						{
							Console.WriteLine("launched");
							methodInfo.Invoke(null, null);
						});
						if (!this.BootResult)
						{
							Environment.FailFast("Boot fail");
						}
					}
				}
			}
		}

		public void OnBootFail(Action action)
		{
			this.onBootFail = action;
		}

		public void OnBootSuccess(Action action)
		{
			this.onBootSuccess = action;
		}

		public void BootFail()
		{
			this.BootResult = false;
			if (this.onBootFail != null)
			{
				this.onBootFail();
			}
		}

		public void AddBootStep()
		{
			this.bootStep++;
		}

		public void BootStep()
		{
			int num = Interlocked.Increment(ref this.current);
			if (num >= this.bootStep)
			{
				this.BootResult = true;
				if (this.onBootSuccess != null)
				{
					this.onBootSuccess();
				}
			}
		}

		public bool HasSmallestServiceID()
		{
			List<int> list = new List<int>();
			list.AddRange(this.LookUp.FindIndex(this.Category));
			list.Sort();
			int num = list.IndexOf(this.ID);
			if (num < 0)
			{
				Log<Service>.Logger.FatalFormat("{1}: [{0}] is not in the Lookup Service List", this.ID, this.Category);
				return false;
			}
			return num == 0;
		}

		protected internal Dictionary<Type, Func<Operation, OperationProcessor>> ProcessorBuilder { get; private set; }

		protected internal Acceptor Acceptor
		{
			get
			{
				return this.acceptor;
			}
		}

		public IPEndPoint EndPointAddress
		{
			get
			{
				return this.Acceptor.EndPointAddress;
			}
		}

		public JobProcessor Thread { get; private set; }

		private List<JobProcessor> ThreadPublished { get; set; }

		public int ID
		{
			get
			{
				return this._id;
			}
			set
			{
				this._id = value;
				if (this.OnSetupID != null)
				{
					this.OnSetupID(this._id);
				}
			}
		}

		public int ServiceOrder { get; internal set; }

		public int LocalServiceOrder { get; internal set; }

		public bool IsStarted { get; private set; }

		public byte Suffix
		{
			get
			{
				return this.suffix;
			}
			internal set
			{
				this.suffix = value;
				if (this.SuffixChanged != null)
				{
					this.SuffixChanged(this, EventArgs.Empty);
				}
			}
		}

		public LookUp LookUp { get; private set; }

		public string Category
		{
			get
			{
				return base.GetType().FullName;
			}
		}

		protected internal IEnumerable<Peer> Peers
		{
			get
			{
				if (this.disposed)
				{
					return new List<Peer>().AsEnumerable<Peer>();
				}
				return this.acceptedPeers.Union(this.peers.Values);
			}
		}

		public Service()
		{
			this.IsStarted = false;
			this.peers = new Dictionary<IPEndPoint, Peer>();
			this.acceptedPeers = new List<Peer>();
			this.LookUp = new LookUp();
			this.ProcessorBuilder = new Dictionary<Type, Func<Operation, OperationProcessor>>();
			this.ThreadPublished = new List<JobProcessor>();
			this.LookUp.ServiceAdded += this.OnServiceAdded;
			this.LookUp.ServiceRemoved += this.OnServiceRemoved;
			AppDomain.CurrentDomain.DomainUnload += this.DomainUnloaded;
			this.InitEnvironmentVariable();
			this.SetBootEvent();
		}

		protected internal void DomainUnloaded(object sender, EventArgs e)
		{
			this.Dispose();
		}

		protected virtual void OnServiceRemoved(string category, int serviceID)
		{
		}

		protected virtual void OnServiceAdded(string category, int serviceID)
		{
		}

		protected internal void RegisterMessage(IDictionary<Type, int> typeConverters)
		{
			this.acceptor.RegisterPipeMessageGroup(typeConverters);
		}

		protected internal void RegisterProcessor(Type operationType, Func<Operation, OperationProcessor> builder)
		{
			if (this.ProcessorBuilder.ContainsKey(operationType))
			{
				Log<Service>.Logger.ErrorFormat("Duplicated Processor for Operation {0} : ignore...", operationType.FullName);
				return;
			}
			this.ProcessorBuilder.Add(operationType, builder);
		}

		public void InitEnvironmentVariable()
		{
			Process currentProcess = Process.GetCurrentProcess();
			DateTime utcNow = DateTime.UtcNow;
			Environment.SetEnvironmentVariable("logfile.name", string.Concat(new object[]
			{
				utcNow.Year.ToString(),
				utcNow.Month.ToString(),
				utcNow.Day.ToString(),
				"_",
				this.Category,
				"_",
				currentProcess.StartInfo.Arguments,
				"_",
				currentProcess.Id,
				"_",
				utcNow.Millisecond
			}));
		}

		public void Initialize(JobProcessor thread, IDictionary<Type, int> typeConverters)
		{
			this.Initialize<TcpServer>(thread, typeConverters);
		}

		public void Initialize<T>(JobProcessor thread, IDictionary<Type, int> typeConverters) where T : ITcpServer, new()
		{
			GlobalContext.Properties["Category"] = this.Category;
			this.Thread = thread;
			thread.Enqueue(Job.Create(delegate
			{
				System.Threading.Thread.CurrentThread.Name = this.Category;
			}));
			VEH.Install();
			this.Thread.ExceptionFilter += this.Thread_ExceptionFilter;
			this.Thread.ExceptionOccur += this.Thread_ExceptionOccur;
			this.acceptor = new Acceptor((default(T) == null) ? Activator.CreateInstance<T>() : default(T), thread);
			this.acceptor.OnAccepted += this.acceptor_OnAccepted;
			if (typeConverters != null)
			{
				this.acceptor.RegisterPipeMessageGroup(typeConverters);
			}
			this.acceptor.RegisterPipeMessageGroup(UnifiedNetwork.Cooperation.Message.TypeConverters);
			if (this.Initializing != null)
			{
				this.Initializing(this, EventArgs.Empty);
			}
			if (Settings.Default.PrintPerformanceInfo)
			{
				new PerformanceTest(this).RegisterPerformanceTestJob();
			}
			this.RegisterMessage(Operations.TypeConverters);
			this.RegisterProcessor(typeof(RequestLookUpInfo), (Operation x) => new RequsetLookUpInfoProcessor(this, x as RequestLookUpInfo));
			this.RegisterProcessor(typeof(RequestUnderingList), (Operation x) => new RequsetUnderingListProcessor(this, x as RequestUnderingList));
			this.RegisterProcessor(typeof(RequestOperationTimeReport), (Operation x) => new RequestOperationTimeReportProcessor(this, x as RequestOperationTimeReport));
			this.RegisterProcessor(typeof(EnableOperationTimeReport), (Operation x) => new EnableOperationTimeReportProcessor(this, x as EnableOperationTimeReport));
			this.RegisterProcessor(typeof(RequestShutDownEntity), (Operation x) => new RequestShutDownEntityProcessor(this, x as RequestShutDownEntity));
		}

		private void Thread_ExceptionFilter(object sender, EventArgs<Exception> e)
		{
			WER.Report(this.Category, e.Value, new object[0]);
		}

		private void Thread_ExceptionOccur(object sender, EventArgs<Exception> e)
		{
			Log<Service>.Error(CallerInfo.Get("z:\\066B\\global2\\HeroesCode\\server\\UnifiedNetwork\\OperationService\\Service.cs", 220), "Unhandled exception", e.Value);
		}

		public void Start(int port)
		{
			this.acceptor.Start(port);
			this.IsStarted = true;
		}

		protected virtual void acceptor_OnAccepted(Peer peer)
		{
			this.acceptedPeers.Add(peer);
			peer.Disconnected += delegate(Peer x)
			{
				Log<Service>.Logger.FatalFormat("disconnected accepted peer {0}", x);
				if (!this.disposed)
				{
					this.acceptedPeers.Remove(x);
				}
			};
			peer.PipeOpen += this.peer_OnPipeOpen;
		}

		protected virtual void peer_OnPipeOpen(Peer peer, Pipe pipe)
		{
			this.MakeProxy(peer, pipe);
		}

		protected event EventHandler Initializing;

		public abstract void Initialize(JobProcessor thread);

		internal event EventHandler SuffixChanged;

		public event Action<int> OnSetupID;

		public event Action<Peer> OnConnected;

		public event Action<Peer, Pipe, OperationProxy> OnOpenProxy;

		public void RequestOperation(int serviceID, Operation op)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(this.ToString(), op.ToString());
			}
			this.LookUp.FindLocation(serviceID, delegate(IPEndPoint location)
			{
				this.RequestOperation(location, op);
			});
		}

		public void RequestOperation(string target, Operation op)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(this.ToString(), op.ToString());
			}
			this.LookUp.FindLocation(target, delegate(IPEndPoint location)
			{
				if (location == null)
				{
					Log<Service>.Logger.WarnFormat("Cannot find service category {0} for {1}", target, op);
					op.IssueFailEvent();
					return;
				}
				this.RequestOperation(location, op);
			});
		}

		protected void RequestOperation(IPEndPoint location, Operation op)
		{
			if (location == null)
			{
				JobProcessor.Current.Enqueue(Job.Create(new Action(op.IssueFailEvent)));
				return;
			}
			JobProcessor currentProcessor = JobProcessor.Current;
			this.ConnectToIP(location, delegate(Peer peer)
			{
				if (peer == null)
				{
					currentProcessor.Enqueue(Job.Create(new Action(op.IssueFailEvent)));
					return;
				}
				this.RequestOperation(peer, op);
			});
		}

		protected virtual TcpClient CreateTcpClient()
		{
			return new TcpClient();
		}

		internal void ConnectToIP(IPEndPoint location, Action<Peer> callback)
		{
			if (this.disposed)
			{
				callback(null);
				return;
			}
			Peer peer;
			if (!this.peers.TryGetValue(location, out peer))
			{
				peer = Peer.CreateClient(this.acceptor, this.CreateTcpClient());
				this.peers[location] = peer;
				peer.PipeOpen += this.peer_OnPipeOpen;
				peer.Connected += delegate(Peer x)
				{
					if (this.OnConnected != null)
					{
						this.OnConnected(x);
					}
					Log<Service>.Logger.DebugFormat("connected to {0}", location);
					callback(x);
				};
				peer.Disconnected += delegate(Peer x)
				{
					Log<Service>.Logger.FatalFormat("disconnected peer {0}", x);
					this.RemovePeer(location, x);
				};
				peer.ConnectionFailed += delegate(Peer x)
				{
					Log<Service>.Logger.ErrorFormat("connection failed : {0}", location);
					callback(null);
					this.RemovePeer(location, x);
				};
				peer.Connect(location);
				return;
			}
			if (peer.IsConnected)
			{
				callback(peer);
				return;
			}
			peer.Connected += delegate(Peer x)
			{
				callback(peer);
			};
		}

		private void RemovePeer(IPEndPoint location, Peer peer)
		{
			Peer peer2;
			if (this.peers != null && this.peers.TryGetValue(peer.RemoteEndPoint, out peer2) && peer2 == peer)
			{
				this.peers.Remove(location);
			}
		}

		internal OperationProxy MakeProxy(Peer peer, Pipe pipe)
		{
			OperationProxy proxy = new OperationProxy(delegate(object msg)
			{
				if (!pipe.IsClosed)
				{
					pipe.SendObject(msg);
					return;
				}
				if (Log<Service>.Logger.IsInfoEnabled)
				{
					Log<Service>.Logger.InfoFormat("try send to closed pipe : {0}", msg);
				}
			}, new Func<Type, Func<Operation, OperationProcessor>>(this.FindProcessor));
			pipe.MessageReceived += delegate(object msg)
			{
				proxy.ProcessMessage(msg);
			};
			pipe.Closed += delegate(Pipe p)
			{
				proxy.Dispose();
			};
			proxy.ProcessorDisposed += delegate(OperationProxy _)
			{
				proxy.Dispose();
				pipe.Close();
			};
			proxy.ProcessorAttached += delegate(OperationProxy _, OperationProcessor processor)
			{
				pipe.Tag = processor.ToString();
			};
			if (this.OnOpenProxy != null)
			{
				this.OnOpenProxy(peer, pipe, proxy);
			}
			return proxy;
		}

		internal void RequestOperation(Peer peer, Operation op)
		{
			Pipe pipe = peer.InitPipe();
			if (pipe == null)
			{
				Log<Service>.Logger.DebugFormat("wait for connect : {0}", op);
				peer.Connected += delegate(Peer x)
				{
					this.RequestOperation(x, op);
				};
				return;
			}
			this.MakeProxy(peer, pipe).RequestOperation(op);
		}

		public JobProcessor AcquireNewThread()
		{
			JobProcessor jobProcessor = new JobProcessor();
			this.ThreadPublished.Add(jobProcessor);
			return jobProcessor;
		}

		protected Func<Operation, OperationProcessor> FindProcessor(Type type)
		{
			return this.ProcessorBuilder.TryGetValue(type);
		}

		public void RegisterAllProcessors(Assembly assembly)
		{
			foreach (Type type in assembly.GetTypes())
			{
				Type baseType = type.BaseType;
				while (baseType != null)
				{
					if (baseType == typeof(OperationProcessor))
					{
						bool flag = false;
						foreach (ConstructorInfo info in type.GetConstructors())
						{
							if (this.RegisterProcessorByConstructor(type, info))
							{
								flag = true;
								break;
							}
						}
						if (!flag)
						{
							Log<Service>.Logger.ErrorFormat("{0} no registered", type.FullName);
							break;
						}
						break;
					}
					else
					{
						baseType = baseType.BaseType;
					}
				}
			}
		}

		private bool RegisterProcessorByConstructor(Type type, ConstructorInfo info)
		{
			ParameterInfo[] args = info.GetParameters();
			if (args.Length <= 0)
			{
				return false;
			}
			Type type2 = null;
			for (int i = 0; i < args.Length; i++)
			{
				if (!typeof(Service).IsAssignableFrom(args[i].ParameterType) && !typeof(IFunctionSelector).IsAssignableFrom(args[i].ParameterType))
				{
					if (!typeof(Operation).IsAssignableFrom(args[i].ParameterType))
					{
						return false;
					}
					type2 = args[i].ParameterType;
				}
			}
			if (type2 == null)
			{
				return false;
			}
			Log<Service>.Logger.DebugFormat("Register processor {0} => {1}", type2.FullName, type.FullName);
			this.RegisterProcessor(type2, delegate(Operation op)
			{
				object[] array = new object[args.Length];
				for (int j = 0; j < array.Length; j++)
				{
					if (typeof(Service).IsAssignableFrom(args[j].ParameterType))
					{
						array[j] = this;
					}
					else if (typeof(IFunctionSelector).IsAssignableFrom(args[j].ParameterType))
					{
						array[j] = this;
					}
					else if (typeof(Operation).IsAssignableFrom(args[j].ParameterType))
					{
						array[j] = op;
					}
				}
				return info.Invoke(array) as OperationProcessor;
			});
			return true;
		}

        ~Service()
        {
            try
            {
                this.Dispose(false);
            }
            catch (Exception exception)
            {
                Log<Service>.Logger.Error("exception on dispose", exception);
            }
        }

        public event EventHandler Disposed;

		private void Dispose(bool disposing)
		{
			if (!disposing)
			{
				Log<Service>.Logger.ErrorFormat("{0} service not disposed", this.Category);
			}
			if (this.disposed)
			{
				Log<Service>.Logger.ErrorFormat("{0} service already disposed!", this.Category);
				return;
			}
			this.disposed = true;
			Log<Service>.Logger.ErrorFormat("{0} service disposing...", this.Category);
			if (this.acceptor != null)
			{
				this.acceptor.Stop();
				this.acceptor = null;
			}
			if (this.peers != null)
			{
				foreach (Peer peer in this.peers.Values.ToArray<Peer>())
				{
					peer.Disconnect();
				}
				this.peers.Clear();
				this.peers = null;
			}
			if (this.acceptedPeers != null)
			{
				foreach (Peer peer2 in this.acceptedPeers.ToArray())
				{
					peer2.Disconnect();
				}
				this.acceptedPeers.Clear();
				this.acceptedPeers = null;
			}
			if (this.Thread != null)
			{
				this.Thread.Stop();
				this.Thread = null;
			}
			if (this.ThreadPublished != null)
			{
				foreach (JobProcessor jobProcessor in this.ThreadPublished)
				{
					jobProcessor.Stop();
				}
			}
			if (this.Disposed != null)
			{
				this.Disposed(this, EventArgs.Empty);
			}
		}

		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		public virtual EntityGraphIdentifier[] ReportConnectedNodeList()
		{
			Log<Service>.Logger.Debug("Nothing under here.");
			return null;
		}

		public virtual EntityGraphIdentifier[] ReportConnectedNodeList(EntityGraphIdentifier target)
		{
			Log<Service>.Logger.Debug("Nothing under here.");
			return null;
		}

		public virtual EntityGraphIdentifier[] ReportConnectedNodeList(long includedEID)
		{
			Log<Service>.Logger.Debug("Nothing under here.");
			return null;
		}

		public virtual IEntityGraphNode GetNode(EntityGraphIdentifier target)
		{
			return EntityGraphNode.NullNode;
		}

		public Dictionary<int, KeyValuePair<string, IPEndPoint>> ReportExtendedLookUpInfo()
		{
			return this.LookUp.ReportExtendedLookUpInfo();
		}

		public virtual string ReportOperationTimeReport(EntityGraphIdentifier p, EntityGraphIdentifier q)
		{
			Log<Service>.Logger.Debug("Nothing under here.");
			return "";
		}

		public virtual void ClearOperationTimeReport()
		{
		}

		public virtual void ShutDownEntity(EntityGraphIdentifier p)
		{
		}

		public virtual int ReportUnderingCounts()
		{
			return 0;
		}

		public bool RegisterFunction<FuncT>(FuncT function)
		{
			Type typeFromHandle = typeof(FuncT);
			if (this.functions.ContainsKey(typeFromHandle))
			{
				return false;
			}
			this.functions[typeFromHandle] = function;
			return true;
		}

		public FuncT GetFunction<FuncT>()
		{
			Type typeFromHandle = typeof(FuncT);
			object obj;
			if (this.functions.TryGetValue(typeFromHandle, out obj))
			{
				return (FuncT)((object)obj);
			}
			return default(FuncT);
		}

		public virtual long GetEntityCount()
		{
			return -1L;
		}

		public virtual long GetQueueLength()
		{
			if (this.Thread != null)
			{
				return (long)(this.Thread.JobCount + 1);
			}
			return -1L;
		}

		private bool bootResult;

		protected Action onBootFail;

		protected Action onBootSuccess;

		private int bootStep;

		private int current;

		private Acceptor acceptor;

		private Dictionary<IPEndPoint, Peer> peers;

		private List<Peer> acceptedPeers;

		private int _id;

		private byte suffix;

		private bool disposed;

		private Dictionary<Type, object> functions = new Dictionary<Type, object>();
	}
}
