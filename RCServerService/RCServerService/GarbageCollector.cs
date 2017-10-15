using System;
using System.Threading;

namespace RemoteControlSystem.Server
{
	public class GarbageCollector
	{
		public int SleepTime
		{
			get
			{
				return this._sleepTime / 60 / 1000;
			}
		}

		public GarbageCollector(int period)
		{
			if (period > 1440 || period < 1)
			{
				throw new ArgumentException("It must be range from 1~1440");
			}
			this._sleepTime = period * 60 * 1000;
			this._run = false;
		}

		public void Start()
		{
			if (this._run)
			{
				return;
			}
			this._run = true;
			this._sleeper = new ManualResetEvent(false);
			this._runThread = new Thread(new ThreadStart(this.Run));
			this._runThread.Start();
		}

		private void Run()
		{
			while (!this._sleeper.WaitOne(this._sleepTime, false))
			{
				GC.Collect();
			}
		}

		public void Stop()
		{
			if (!this._run)
			{
				return;
			}
			this._run = false;
			this._sleeper.Set();
			if (!this._runThread.Join(1000))
			{
				this._runThread.Abort();
			}
		}

		private int _sleepTime;

		private bool _run;

		private ManualResetEvent _sleeper;

		private Thread _runThread;
	}
}
