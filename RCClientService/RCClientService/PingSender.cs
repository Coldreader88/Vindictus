using System;
using System.Threading;
using RemoteControlSystem.ServerMessage;

namespace RemoteControlSystem.Client
{
	public class PingSender
	{
		public int SleepTime
		{
			get
			{
				return this._sleepTime / 60 / 1000;
			}
		}

		public PingSender(int period)
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
				Base.ClientControlManager.Send<PingMessage>(new PingMessage());
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
			this._runThread = null;
		}

		private int _sleepTime;

		private bool _run;

		private ManualResetEvent _sleeper;

		private Thread _runThread;
	}
}
