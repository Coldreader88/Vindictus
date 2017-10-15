using System;
using System.Collections.Generic;
using UnifiedNetwork.PerfMon;
using Utility;

namespace UnifiedNetwork.Cooperation
{
	public abstract class OperationProcessor : IDisposable
	{
		public OperationProbe Performance
		{
			get
			{
				return this.performance;
			}
		}

		public bool Result { get; protected internal set; }

		public bool Finished { get; protected internal set; }

		public bool GatherPerformance { get; set; }

		public Operation Operation { get; private set; }

		public event Action<object> OnSend;

		public abstract IEnumerable<object> Run();

		protected object Feedback { get; private set; }

		public event Action PreStep;

		public OperationProcessor(Operation op)
		{
			this.Operation = op;
			this.Result = true;
		}

		public object Start()
		{
			if (this.state != null || this.Operation == null)
			{
				throw new InvalidOperationException(string.Format("잘못된 OperationProcessor를 시작하려고 했습니다. state[{0}] Target[{1}]", this.state, this.Operation));
			}
			this.state = this.Run().GetEnumerator();
			this.ProcessFeedback(this.Operation);
			return this.Operation;
		}

		private bool Step()
		{
			if (this.state == null || this.Operation == null)
			{
				throw new InvalidOperationException(string.Format("잘못된 OperationProcessor를 계속하려고 했습니다. state[{0}] Target[{1}]", this.state, this.Operation));
			}
			StepProbe probe = default(StepProbe);
			probe = default(StepProbe);
			probe.StartTime = DateTime.Now;
			bool flag;
			try
			{
				flag = this.state.MoveNext();
			}
			catch (Exception)
			{
				throw;
			}
			finally
			{
				probe.EndTime = DateTime.Now;
				this.performance.AddStep(probe);
				if (this.pcInstance != null)
				{
					this.pcInstance.Increment("Step count");
					this.pcInstance.IncrementBy("Step time", probe.SpendMilliseconds);
				}
			}
			if (!flag)
			{
				this.Feedback = null;
				this.Finished = true;
				return false;
			}
			this.Feedback = null;
			if (this.state.Current != null)
			{
				this.OnSend(this.state.Current);
			}
			return true;
		}

		public bool ProcessFeedback(object feedback)
		{
			if (this.state == null || this.Operation == null)
			{
				throw new InvalidOperationException(string.Format("잘못된 OperationProcessor를 계속하려고 했습니다. state[{0}] Target[{1}]", this.state, this.Operation));
			}
			this.Feedback = feedback;
			bool result;
			try
			{
				bool flag = true;
				if (this.Feedback is FailMessage)
				{
					Log<OperationProcessor>.Logger.WarnFormat("{0}({1}) failed : {2}", this, this.Operation, (this.Feedback as FailMessage).Reason);
					this.Finished = true;
					this.Result = false;
				}
				else
				{
					try
					{
						if (this.PreStep != null)
						{
							this.PreStep();
						}
						flag = this.Step();
					}
					catch (Exception ex)
					{
						Log<OperationProcessor>.Logger.Error(string.Format("[{0}] Exception occured in step", this.Operation), ex);
						this.Finished = true;
						this.Result = false;
						try
						{
							this.OnSend(new FailMessage(string.Format("[ProcessFeedback] Exception 1. [{0}]", this.Operation))
							{
								Reason = FailMessage.ReasonCode.ExceptionOccured
							});
						}
						catch (Exception ex2)
						{
							Log<OperationProcessor>.Logger.Error(string.Format("[{0}] Exception occured in send fail message", this.Operation), ex2);
						}
					}
				}
				if (this.Finished || !flag)
				{
					this.Finished = true;
					if (!(this.Feedback is FailMessage))
					{
						try
						{
							while (this.Step())
							{
							}
						}
						catch (Exception ex3)
						{
							Log<OperationProcessor>.Logger.Error(string.Format("[{0}] Exception occured in cleaning", this.Operation), ex3);
							this.Result = false;
						}
					}
					try
					{
						if (this.Result)
						{
							this.Operation.IssueCompleteEvent();
						}
						else
						{
							this.Operation.IssueFailEvent();
						}
					}
					catch (Exception ex4)
					{
						Log<OperationProcessor>.Logger.Error(string.Format("[{0}] Exception occured in ended {1} event", this.Operation, this.Result), ex4);
						this.Result = false;
					}
					finally
					{
						OperationPerformance.AddProbe(this.Operation.GetType(), this.performance);
						if (this.pcInstance != null)
						{
							this.pcInstance.Increment("Operation count");
							this.pcInstance.IncrementBy("Operation time", this.performance.SpendMilliseconds);
							if (this.Result)
							{
								this.pcInstance.Increment("Operation success");
							}
						}
					}
					if (this.state != null)
					{
						IEnumerator<object> enumerator = this.state;
						this.state = null;
						enumerator.Dispose();
					}
					this.Operation = null;
					result = false;
				}
				else
				{
					result = true;
				}
			}
			catch (Exception ex5)
			{
				Log<OperationProcessor>.Logger.Error(string.Format("[{0}] Exception occured", this.Operation), ex5);
				this.Finished = true;
				this.Operation.IssueFailEvent();
				if (this.state != null)
				{
					IEnumerator<object> enumerator2 = this.state;
					this.state = null;
					enumerator2.Dispose();
				}
				this.Operation = null;
				this.OnSend(new FailMessage(string.Format("[ProcessFeedback] Exception 2. [{0}]", this.Operation))
				{
					Reason = FailMessage.ReasonCode.ExceptionOccured
				});
				result = false;
			}
			return result;
		}

        ~OperationProcessor()
        {
            try
            {
                this.Dispose(false);
            }
            catch (Exception exception)
            {
                Log<OperationProcessor>.Logger.Error("exception on dispose", exception);
            }
        }

        public void Dispose(bool disposing)
		{
			if (!disposing)
			{
				FileLog.Log("ThreadUnsafe.log", string.Format("{0}", this));
				Log<OperationProcessor>.Logger.ErrorFormat("not disposed OperationProcessor : [{0}]", this);
			}
			if (this.disposed)
			{
				return;
			}
			this.disposed = true;
			IEnumerator<object> enumerator = this.state;
			Operation operation = this.Operation;
			this.state = null;
			this.Operation = null;
			try
			{
				if (enumerator != null)
				{
					enumerator.Dispose();
				}
				if (operation != null)
				{
					operation.IssueFailEvent();
				}
			}
			catch (Exception ex)
			{
				Log<OperationProcessor>.Logger.Error("exception occured on dispose", ex);
			}
		}

		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		public override string ToString()
		{
			return string.Format("{0}[{1}]", base.GetType().Name, this.Operation);
		}

		private IEnumerator<object> state;

		private OperationProbe performance = default(OperationProbe);

		private PCWrapper.CounterInstance pcInstance;

		private bool disposed;
	}
}
