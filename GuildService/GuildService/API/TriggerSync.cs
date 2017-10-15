using System;
using Devcat.Core.Threading;
using UnifiedNetwork.Cooperation;

namespace GuildService.API
{
	public class TriggerSync : ISynchronizable
	{
		private JobProcessor MainThread { get; set; }

		private bool IsFinished { get; set; }

		public TriggerSync(int milisec)
		{
			this.MainThread = JobProcessor.Current;
			this.IsFinished = false;
			Scheduler.Schedule(this.MainThread, Job.Create<bool>(new Action<bool>(this.Trigger), false), milisec);
		}

		public void Trigger()
		{
			this.Trigger(true);
		}

		public void Trigger(bool result)
		{
			if (this.IsFinished)
			{
				return;
			}
			this.MainThread.Enqueue(Job.Create(delegate
			{
				if (this.IsFinished)
				{
					return;
				}
				this.IsFinished = true;
				this.Result = result;
				if (this.OnFinished != null)
				{
					this.OnFinished(this);
				}
			}));
		}

		public event Action<ISynchronizable> OnFinished;

		public bool Result { get; private set; }

		public void OnSync()
		{
		}
	}
}
