using System;

namespace Devcat.Core.Threading
{
	public static class Job
	{
		public static IJob Create(Action func)
		{
			return new Job.Caller(func);
		}

		public static IJob Create<T1>(Action<T1> func, T1 arg1)
		{
			return new Job.Caller<T1>(func, arg1);
		}

		public static IJob Create<T1, T2>(Action<T1, T2> func, T1 arg1, T2 arg2)
		{
			return new Job.Caller<T1, T2>(func, arg1, arg2);
		}

		public static IJob Create<T1, T2, T3>(Action<T1, T2, T3> func, T1 arg1, T2 arg2, T3 arg3)
		{
			return new Job.Caller<T1, T2, T3>(func, arg1, arg2, arg3);
		}

		public static IJob Create<T1, T2, T3, T4>(Action<T1, T2, T3, T4> func, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
		{
			return new Job.Caller<T1, T2, T3, T4>(func, arg1, arg2, arg3, arg4);
		}

		public static IJob Create<T1, T2, T3, T4, T5>(Action<T1, T2, T3, T4, T5> func, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
		{
			return new Job.Caller<T1, T2, T3, T4, T5>(func, arg1, arg2, arg3, arg4, arg5);
		}

		private abstract class BaseJob : IJob
		{
			public long EnqueueTick { get; set; }

			public long StartTick { get; set; }

			public long EndTick { get; set; }

			public abstract void Do();
		}

		private class Caller : Job.BaseJob
		{
			public Caller(Action func)
			{
				this.func = func;
			}

			public override void Do()
			{
				this.func();
			}

			private Action func;
		}

		private class Caller<T1> : Job.BaseJob
		{
			public Caller(Action<T1> func, T1 arg1)
			{
				this.func = func;
				this.arg1 = arg1;
			}

			public override void Do()
			{
				this.func(this.arg1);
			}

			private Action<T1> func;

			private T1 arg1;
		}

		private class Caller<T1, T2> : Job.BaseJob
		{
			public Caller(Action<T1, T2> func, T1 arg1, T2 arg2)
			{
				this.func = func;
				this.arg1 = arg1;
				this.arg2 = arg2;
			}

			public override void Do()
			{
				this.func(this.arg1, this.arg2);
			}

			private Action<T1, T2> func;

			private T1 arg1;

			private T2 arg2;
		}

		private class Caller<T1, T2, T3> : Job.BaseJob
		{
			public Caller(Action<T1, T2, T3> func, T1 arg1, T2 arg2, T3 arg3)
			{
				this.func = func;
				this.arg1 = arg1;
				this.arg2 = arg2;
				this.arg3 = arg3;
			}

			public override void Do()
			{
				this.func(this.arg1, this.arg2, this.arg3);
			}

			private Action<T1, T2, T3> func;

			private T1 arg1;

			private T2 arg2;

			private T3 arg3;
		}

		private class Caller<T1, T2, T3, T4> : Job.BaseJob
		{
			public Caller(Action<T1, T2, T3, T4> func, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
			{
				this.func = func;
				this.arg1 = arg1;
				this.arg2 = arg2;
				this.arg3 = arg3;
				this.arg4 = arg4;
			}

			public override void Do()
			{
				this.func(this.arg1, this.arg2, this.arg3, this.arg4);
			}

			private Action<T1, T2, T3, T4> func;

			private T1 arg1;

			private T2 arg2;

			private T3 arg3;

			private T4 arg4;
		}

		private class Caller<T1, T2, T3, T4, T5> : Job.BaseJob
		{
			public Caller(Action<T1, T2, T3, T4, T5> func, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
			{
				this.func = func;
				this.arg1 = arg1;
				this.arg2 = arg2;
				this.arg3 = arg3;
				this.arg4 = arg4;
				this.arg5 = arg5;
			}

			public override void Do()
			{
				this.func(this.arg1, this.arg2, this.arg3, this.arg4, this.arg5);
			}

			private Action<T1, T2, T3, T4, T5> func;

			private T1 arg1;

			private T2 arg2;

			private T3 arg3;

			private T4 arg4;

			private T5 arg5;
		}
	}
}
