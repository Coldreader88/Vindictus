using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace UnifiedNetwork.ProfileService
{
	public class Profiler
	{
		public Profiler()
		{
			this.watch = new Stopwatch();
			this.profileCache = new List<LogProfile>();
			try
			{
				string connection = "Data Source=caesar\\SQLEXPRESS;Initial Catalog=heroesLog;Integrated Security=True";
				this.profileDataContext = new ProfileDataContext(connection);
			}
			catch (Exception ex)
			{
				throw ex;
			}
			this.isWorking = false;
		}

		public void Start(string profileTarget)
		{
			if (this.isWorking)
			{
				return;
			}
			this.isWorking = true;
			this.watch.Start();
			this.operation = profileTarget;
		}

		public void Abort()
		{
			this.watch.Stop();
			this.watch.Reset();
			this.isWorking = false;
		}

		public void Stop()
		{
			this.watch.Stop();
			LogProfile item = new LogProfile
			{
				TimeStamp = DateTime.Now,
				ElapsedMillisecond = this.watch.ElapsedMilliseconds,
				ElapsedTicks = this.watch.ElapsedTicks,
				EntitySize = 0,
				Operation = this.operation
			};
			this.profileCache.Add(item);
			this.watch.Reset();
			this.isWorking = false;
		}

		public bool WriteToDB()
		{
			foreach (LogProfile profile in this.profileCache)
			{
				this.profileDataContext.AddProfileRecord(profile);
			}
			this.profileDataContext.SubmitChanges();
			this.profileCache.Clear();
			return true;
		}

		private Stopwatch watch;

		private string operation;

		private List<LogProfile> profileCache;

		private ProfileDataContext profileDataContext;

		private bool isWorking;
	}
}
