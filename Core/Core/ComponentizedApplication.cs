using System;
using System.IO;
using System.Reflection;
using System.Threading;

namespace Devcat.Core
{
	public sealed class ComponentizedApplication
	{
		public event EventHandler<ComponentizedApplication, EventArgs<object>> ReceiveMessage;

		public event EventHandler<ComponentizedApplication, EventArgs> Finalized;

		public event EventHandler<ComponentizedApplication, EventArgs<Exception>> ExceptionOccur;

		public int ID
		{
			get
			{
				return this.id;
			}
		}

		public string Name
		{
			get
			{
				return this.name;
			}
		}

		private void Initialize(AppDomain appDomain, MethodInfo executor, object parameter)
		{
			this.appDomain = appDomain;
			this.id = appDomain.Id;
			this.name = appDomain.FriendlyName;
			this.executorType = executor.ReflectedType;
			this.communicator = new ComponentizedAppCommunicator(this, parameter);
            this.thread = new Thread(() => this.ExecuteInitializer(new CrossAppDomainDelegate(new ComponentizedApplicationExecutor(executor, this.communicator).Execute)));
            this.thread.IsBackground = true;
		}

		public ComponentizedApplication(string appDomainName, ComponentizedAppEntryDelegate executor, object parameter) : this(appDomainName, executor.Method, parameter)
		{
		}

		public ComponentizedApplication(string appDomainName, MethodInfo executor, object parameter)
		{
			if (!executor.IsStatic)
			{
				throw new ArgumentException("Initializer method must be a static.", "executor");
			}
			if (executor.GetParameters().Length != 1 || typeof(ComponentizedAppCommunicator).IsAssignableFrom(executor.GetParameters()[0].ParameterType))
			{
				throw new ArgumentException("Initializer method must have 1 parameter type of ComponentizedAppCommunicator", "executor");
			}
			this.Initialize(AppDomain.CreateDomain(appDomainName), executor, parameter);
		}

		public ComponentizedApplication(string appDomainName, string assemblyFileName, string typeName, string methodName, object parameter)
		{
			assemblyFileName = Path.GetFullPath(assemblyFileName);
			Assembly assembly = null;
			try
			{
				assembly = Assembly.LoadFile(assemblyFileName);
			}
			catch
			{
			}
			if (assembly == null)
			{
				throw new ArgumentException(string.Format("Can't find assembly file {0}.", assemblyFileName));
			}
			this.executorType = assembly.GetType(typeName, false, true);
			if (this.executorType == null)
			{
				throw new ArgumentException(string.Format("Can't find type {1} from assembly {0}. Check your namespace and public attribute.", assemblyFileName, typeName));
			}
			MethodInfo method = this.executorType.GetMethod(methodName, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[]
			{
				typeof(ComponentizedAppCommunicator)
			}, null);
			if (method == null)
			{
				throw new ArgumentException(string.Format("Can't find method {2} from type {1} in assembly {0}. Check static attribute, no return type and must have ComponentizedAppCommunicator parameter.", assemblyFileName, typeName, methodName));
			}
			AppDomainSetup appDomainSetup = new AppDomainSetup();
			appDomainSetup.ApplicationName = assemblyFileName;
			appDomainSetup.PrivateBinPath = Path.GetDirectoryName(assemblyFileName);
			AppDomain appDomain = AppDomain.CreateDomain(appDomainName, AppDomain.CurrentDomain.Evidence, appDomainSetup);
			this.Initialize(appDomain, method, parameter);
		}

		public void Start()
		{
			this.thread.Start();
		}

		public void Join()
		{
			this.thread.Join();
		}

		public void Join(int milliseconds)
		{
			this.thread.Join(milliseconds);
		}

		public void Terminate()
		{
			this.thread.Abort();
		}

		public void Kill()
		{
			AppDomain.Unload(this.appDomain);
		}

		public void Command(ComponentizedAppCommandDelegate executor, object parameter)
		{
			this.Command(executor.Method, parameter);
		}

		public void Command(MethodInfo executor, object parameter)
		{
			if (!executor.IsStatic)
			{
				throw new ArgumentException("Command method must be a static.");
			}
			if (executor.GetParameters().Length != 1)
			{
				throw new ArgumentException("Command method must have 1 parametersigniture of 'void ()(object)'");
			}
			this.appDomain.DoCallBack(new CrossAppDomainDelegate(new ComponentizedApplication.ComponentizedApplicationExecutor(executor, parameter).Execute));
		}

		public void Command(string methodName, object parameter)
		{
			MethodInfo method = this.executorType.GetMethod(methodName, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			if (method == null)
			{
				throw new ArgumentException(string.Format("Can't find method {2} from type {1} in assembly {0}. Check static attribute, no return type and must have one parameter.", this.executorType.Assembly.FullName, this.executorType.FullName, methodName));
			}
			this.Command(method, parameter);
		}

		internal void ReportMessage(object message)
		{
			if (this.ReceiveMessage != null)
			{
				this.ReceiveMessage(this, new EventArgs<object>(message));
			}
		}

		private void ExecuteInitializer(CrossAppDomainDelegate executor)
		{
			try
			{
				this.appDomain.DoCallBack(executor);
			}
			catch (ThreadAbortException)
			{
				Thread.ResetAbort();
			}
			catch (Exception value)
			{
				if (this.ExceptionOccur != null)
				{
					this.ExceptionOccur(this, new EventArgs<Exception>(value));
				}
			}
			finally
			{
				try
				{
					AppDomain.Unload(this.appDomain);
				}
				catch
				{
				}
				if (this.Finalized != null)
				{
					this.Finalized(this, EventArgs.Empty);
				}
			}
		}

		private AppDomain appDomain;

		private int id;

		private string name;

		private Type executorType;

		private ComponentizedAppCommunicator communicator;

		private Thread thread;

		[Serializable]
		private class ComponentizedApplicationExecutor
		{
			public ComponentizedApplicationExecutor(MethodInfo executor, object parameter)
			{
				this.executor = executor;
				this.parameter = parameter;
			}

			public void Execute()
			{
				this.executor.Invoke(null, new object[]
				{
					this.parameter
				});
			}

			private MethodInfo executor;

			private object parameter;
		}
	}
}
