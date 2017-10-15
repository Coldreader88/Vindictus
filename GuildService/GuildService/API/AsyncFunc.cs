using System;
using Devcat.Core;
using Devcat.Core.Threading;
using Utility;

namespace GuildService.API
{
	public class AsyncFunc
	{
		public static void Call<T>(Func<T> func, Action<T> onComplete, T exceptionCode)
		{
			JobProcessor mainThread = JobProcessor.Current;
			JobProcessor jobThread = new JobProcessor();
			jobThread.ExceptionOccur += delegate(object s, EventArgs<Exception> e)
			{
				Log<GuildService>.Logger.Fatal("Exception occurred in AsyncFunc Thread", e.Value);
			};
			jobThread.Start();
			jobThread.Enqueue(Job.Create(delegate
			{
				T funcResult;
				try
				{
					funcResult = func();
				}
				catch (Exception ex)
				{
					Log<GuildService>.Logger.Error("Exception occurred in Async function call", ex);
					funcResult = exceptionCode;
				}
				mainThread.Enqueue(Job.Create(delegate
				{
					if (onComplete != null)
					{
						onComplete(funcResult);
					}
					jobThread.Stop(false);
				}));
			}));
		}

		public static void Call(Action func, Action<bool> onComplete)
		{
			JobProcessor mainThread = JobProcessor.Current;
			JobProcessor jobThread = new JobProcessor();
			jobThread.ExceptionOccur += delegate(object s, EventArgs<Exception> e)
			{
				Log<GuildService>.Logger.Fatal("Exception occurred in AsyncFunc Thread", e.Value);
			};
			jobThread.Start();
			jobThread.Enqueue(Job.Create(delegate
			{
				bool result;
				try
				{
					func();
					result = true;
				}
				catch (Exception ex)
				{
					Log<GuildService>.Logger.Error("Exception occurred in Async function call", ex);
					result = false;
				}
				mainThread.Enqueue(Job.Create(delegate
				{
					if (onComplete != null)
					{
						onComplete(result);
					}
					jobThread.Stop(false);
				}));
			}));
		}
	}
}
