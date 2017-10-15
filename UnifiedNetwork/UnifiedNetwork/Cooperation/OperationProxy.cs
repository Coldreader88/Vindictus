using System;
using System.Diagnostics;
using System.Threading;
using Devcat.Core.Threading;
using UnifiedNetwork.EntityGraph;
using Utility;

namespace UnifiedNetwork.Cooperation
{
	public class OperationProxy : IDisposable
	{
		public static OperationProxy CurrentProxy
		{
			get
			{
				return OperationProxy.currentProxy;
			}
		}

		public static bool IsTimeReportEnabled
		{
			get
			{
				return OperationProxy.timeReportEnabled;
			}
		}

		public static void EnableTimeReport()
		{
			OperationProxy.timeReportEnabled = true;
		}

		public static void DisableTimeReport()
		{
			OperationProxy.timeReportEnabled = false;
		}

		public double TimerResult
		{
			get
			{
				return this.timerresult;
			}
		}

		public string RecentOperationName
		{
			get
			{
				return OperationTimeReportElement.GenerateProcessorName(this.recentprocessor, this.recentoperation, this.isRequesting);
			}
		}

		public bool IsRequesting
		{
			get
			{
				return this.isRequesting;
			}
		}

		public event Action<object> Sending;

		public event Action<OperationProxy> ProcessorDisposed;

		public event Action<OperationProxy, OperationProcessor> ProcessorAttached;

		public OperationProcessor Processor
		{
			get
			{
				return this.processor;
			}
		}

		public bool IsProcessing
		{
			get
			{
				return this.processor != null;
			}
		}

		public OperationProxy(Action<object> sendHandler, Func<Type, Func<Operation, OperationProcessor>> processorBuilder)
		{
			this.thread = Thread.CurrentThread.ManagedThreadId;
			if (sendHandler == null)
			{
				Log<OperationProxy>.Logger.ErrorFormat("sendhandler is null : operationproxy will not be functional", new object[0]);
			}
			else
			{
				this.Sending += sendHandler;
			}
			this.ProcessorBuilder = processorBuilder;
		}

		internal void DetachProcessor()
		{
			if (OperationProxy.timeReportEnabled)
			{
				this.timer.Stop();
				this.timerresult = (double)(this.timer.ElapsedTicks * 1000L) / (double)Stopwatch.Frequency;
				this.recentprocessor = this.processor;
			}
			if (this.thread != Thread.CurrentThread.ManagedThreadId)
			{
				Log<OperationProxy>.Logger.FatalFormat("Thread Unsafe code DetachProcessor : [{0} != {1} ({2})]", this.thread, Thread.CurrentThread.ManagedThreadId, this.Processor);
			}
			if (this.processor != null)
			{
				OperationProcessor operationProcessor = this.processor;
				this.processor = null;
				if (this.timeOutSchedule != 0L)
				{
					Scheduler.Cancel(this.timeOutSchedule);
					this.timeOutSchedule = 0L;
				}
				operationProcessor.Dispose();
				if (this.ProcessorDisposed != null)
				{
					try
					{
						this.ProcessorDisposed(this);
					}
					catch (Exception ex)
					{
						Log<OperationProxy>.Logger.Error("Error in ProcessorDisposed event", ex);
					}
				}
			}
		}

		private void OnSynchronizableFinished(ISynchronizable sync)
		{
			sync.OnFinished -= this.OnSynchronizableFinished;
			JobProcessor.Current.Enqueue(Job.Create<ISynchronizable>(new Action<ISynchronizable>(this.ProcessMessage), sync));
		}

		private bool AttachProcessor(OperationProcessor newProcessor)
		{
			if (this.thread != Thread.CurrentThread.ManagedThreadId)
			{
				Log<OperationProxy>.Logger.FatalFormat("Thread Unsafe code AttachProcessor : [{0} != {1} ({2})]", this.thread, Thread.CurrentThread.ManagedThreadId, this.Processor);
			}
			if (this.processor != null)
			{
				Log<OperationProxy>.Logger.ErrorFormat("duplicate processor : [{0} on {1}]", newProcessor, this.processor);
				this.DetachProcessor();
				return false;
			}
			this.processor = newProcessor;
			if (this.processor == null)
			{
				return true;
			}
			Log<OperationProxy>.Logger.DebugFormat("Attach {0}({1})", newProcessor.GetType().Name, newProcessor.Operation.GetType().Name);
			JobProcessor loop = JobProcessor.Current;
			this.processor.OnSend += delegate(object msg)
			{
				if (msg is ISynchronizable)
				{
					ISynchronizable synchronizable = (ISynchronizable)msg;
					synchronizable.OnFinished += this.OnSynchronizableFinished;
					synchronizable.OnSync();
					return;
				}
				if (this.Sending != null)
				{
					this.Sending(msg);
				}
			};
			if (OperationProxy.timeReportEnabled)
			{
				this.timer = Stopwatch.StartNew();
			}
			if (this.processor.Operation.TimeOut != 0)
			{
				this.timeOutSchedule = Scheduler.Schedule(loop, Job.Create<OperationProcessor>(new Action<OperationProcessor>(this.OnTimeOut), this.processor), this.processor.Operation.TimeOut);
			}
			if (this.ProcessorAttached != null)
			{
				try
				{
					this.ProcessorAttached(this, this.processor);
				}
				catch (Exception ex)
				{
					Log<OperationProxy>.Logger.Error("ProcessorAttached", ex);
					this.DetachProcessor();
					return false;
				}
			}
			this.processor.Start();
			return true;
		}

		private void CheckFinish()
		{
			if (this.processor != null && this.processor.Finished)
			{
				this.DetachProcessor();
			}
		}

		private void OnTimeOut(OperationProcessor op)
		{
			if (this.processor != null && op == this.processor && !this.processor.Finished)
			{
				this.timeOutSchedule = 0L;
				this.ProcessMessage(new FailMessage(string.Format("[OnTimeOut] {0}", op))
				{
					Reason = FailMessage.ReasonCode.TimeOut
				});
				Log<OperationProxy>.Logger.ErrorFormat("Operation Time Out : [{0} on {1}]", op.Operation, op);
			}
		}

		public void ProcessMessage(object msg)
		{
			if (this.thread != Thread.CurrentThread.ManagedThreadId)
			{
				Log<OperationProxy>.Logger.FatalFormat("Thread Unsafe code ProcessMessage : [{0} != {1} ({2})]", this.thread, Thread.CurrentThread.ManagedThreadId, this.Processor);
			}
			if (this.disposed)
			{
				if (msg is FunctionSync)
				{
					FunctionSync functionSync = msg as FunctionSync;
					Log<OperationProxy>.Logger.ErrorFormat("Try to access disposed object in ProcessMessage [(processing {0})]", functionSync.Name);
				}
				else
				{
					Log<OperationProxy>.Logger.ErrorFormat("Try to access disposed object in ProcessMessage [(processing {0})]", msg.GetType().ToString());
				}
				throw new ObjectDisposedException("OperationProxy");
			}
			try
			{
				OperationProxy.currentProxy = this;
				if (msg is Operation)
				{
					OperationProcessor operationProcessor = this.MakeProcessor(msg as Operation);
					if (OperationPerformance.GatherPerformance)
					{
						operationProcessor.GatherPerformance = true;
					}
					if ((operationProcessor == null || !this.AttachProcessor(operationProcessor)) && this.Sending != null)
					{
						this.Sending(new FailMessage(string.Format("[ProcessMessage] !AttachProcessor [0]", operationProcessor))
						{
							Reason = FailMessage.ReasonCode.NotSupportedOperation
						});
					}
				}
				else
				{
					this.processor.ProcessFeedback(msg);
				}
				this.CheckFinish();
			}
			catch (Exception ex)
			{
				Log<OperationProxy>.Logger.ErrorFormat("Error in ProcessMessage [(processing {0}) {1}]", msg.GetType().ToString(), ex.ToString());
				this.CheckFinish();
			}
			finally
			{
				OperationProxy.currentProxy = null;
			}
		}

		public void RequestOperation(Operation op)
		{
			if (this.thread != Thread.CurrentThread.ManagedThreadId)
			{
				Log<OperationProxy>.Logger.FatalFormat("Thread Unsafe code RequestOperation : [{0} != {1} ({2})]", this.thread, Thread.CurrentThread.ManagedThreadId, this.Processor);
			}
			if (this.disposed)
			{
				throw new ObjectDisposedException("OperationProxy");
			}
			try
			{
				OperationProxy.currentProxy = this;
				if (this.processor == null)
				{
					this.isRequesting = true;
					if (OperationProxy.IsTimeReportEnabled)
					{
						this.recentoperation = op;
					}
					this.Sending(op);
					this.AttachProcessor(op.RequestProcessor());
					this.CheckFinish();
				}
				else
				{
					Log<OperationProxy>.Logger.ErrorFormat("Duplicated Request Processor on Proxy : [{0} on {1}]", op, this.processor);
				}
			}
			catch (Exception ex)
			{
				Log<OperationProxy>.Logger.Error("Exception occured", ex);
				try
				{
					if (this.processor != null)
					{
						this.DetachProcessor();
					}
					else if (this.ProcessorDisposed != null)
					{
						this.ProcessorDisposed(this);
					}
				}
				catch (Exception ex2)
				{
					Log<OperationProxy>.Logger.Fatal("어쩌라고 ㄱ-;", ex2);
				}
			}
			finally
			{
				OperationProxy.currentProxy = null;
			}
		}

		private OperationProcessor MakeProcessor(Operation operation)
		{
			Func<Operation, OperationProcessor> func = this.ProcessorBuilder(operation.GetType());
			if (func != null)
			{
				try
				{
					return func(operation);
				}
				catch (Exception ex)
				{
					Log<OperationProxy>.Logger.ErrorFormat("Error while make processor of {0} : {1}", operation, ex.Message);
					return null;
				}
			}
			return null;
		}

        ~OperationProxy()
        {
            try
            {
                this.Dispose(false);
            }
            catch (Exception exception)
            {
                Log<OperationProxy>.Logger.Error("exception on dispose", exception);
            }
        }

        public void Dispose(bool disposing)
		{
			if (!disposing)
			{
				FileLog.Log("ThreadUnsafe.log", string.Format("{0}[{1}]", this, this.Processor));
				Log<OperationProxy>.Logger.FatalFormat("not disposed operationproxy : {0}", this.processor);
			}
			if (this.disposed)
			{
				return;
			}
			this.disposed = true;
			this.DetachProcessor();
		}

		public void Dispose()
		{
			if (this.thread != Thread.CurrentThread.ManagedThreadId)
			{
				Log<OperationProxy>.Logger.FatalFormat("Thread Unsafe code Dispose : [{0} != {1} ({2})]", this.thread, Thread.CurrentThread.ManagedThreadId, this.Processor);
			}
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		[ThreadStatic]
		private static OperationProxy currentProxy;

		[ThreadStatic]
		private static bool timeReportEnabled = false;

		private Stopwatch timer;

		private double timerresult;

		private Operation recentoperation;

		private OperationProcessor recentprocessor;

		private bool isRequesting;

		private OperationProcessor processor;

		private Func<Type, Func<Operation, OperationProcessor>> ProcessorBuilder;

		private int thread;

		private long timeOutSchedule;

		private bool disposed;
	}
}
