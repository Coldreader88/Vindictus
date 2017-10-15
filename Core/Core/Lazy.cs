using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Threading;

namespace Devcat.Core
{
	[DebuggerTypeProxy(typeof(System_LazyDebugView<>))]
	[ComVisible(false)]
	[DebuggerDisplay("ThreadSafetyMode={Mode}, IsValueCreated={IsValueCreated}, IsValueFaulted={IsValueFaulted}, Value={ValueForDebugDisplay}")]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	[Serializable]
	public class Lazy<T>
	{
		public Lazy() : this(LazyThreadSafetyMode.ExecutionAndPublication)
		{
		}

		public Lazy(bool isThreadSafe) : this(isThreadSafe ? LazyThreadSafetyMode.ExecutionAndPublication : LazyThreadSafetyMode.None)
		{
		}

		public Lazy(Func<T> valueFactory) : this(valueFactory, LazyThreadSafetyMode.ExecutionAndPublication)
		{
		}

		public Lazy(LazyThreadSafetyMode mode)
		{
			this.m_threadSafeObj = Lazy<T>.GetObjectFromMode(mode);
		}

		public Lazy(Func<T> valueFactory, bool isThreadSafe) : this(valueFactory, isThreadSafe ? LazyThreadSafetyMode.ExecutionAndPublication : LazyThreadSafetyMode.None)
		{
		}

		public Lazy(Func<T> valueFactory, LazyThreadSafetyMode mode)
		{
			if (valueFactory == null)
			{
				throw new ArgumentNullException("valueFactory");
			}
			this.m_threadSafeObj = Lazy<T>.GetObjectFromMode(mode);
			this.m_valueFactory = valueFactory;
		}

		private Lazy<T>.Boxed CreateValue()
		{
			Lazy<T>.Boxed result = null;
			LazyThreadSafetyMode mode = this.Mode;
			if (this.m_valueFactory != null)
			{
				try
				{
					if (mode != LazyThreadSafetyMode.PublicationOnly && this.m_valueFactory == Lazy<T>.PUBLICATION_ONLY_OR_ALREADY_INITIALIZED)
					{
						throw new InvalidOperationException(Environment2.GetResourceString("Lazy_Value_RecursiveCallsToValue"));
					}
					Func<T> valueFactory = this.m_valueFactory;
					if (mode != LazyThreadSafetyMode.PublicationOnly)
					{
						this.m_valueFactory = Lazy<T>.PUBLICATION_ONLY_OR_ALREADY_INITIALIZED;
					}
					return new Lazy<T>.Boxed(valueFactory());
				}
				catch (Exception ex)
				{
					if (mode != LazyThreadSafetyMode.PublicationOnly)
					{
						this.m_boxed = new Lazy<T>.LazyInternalExceptionHolder(ex);
					}
					throw;
				}
			}
			try
			{
				result = new Lazy<T>.Boxed((T)((object)Activator.CreateInstance(typeof(T))));
			}
			catch (MissingMethodException)
			{
				Exception ex2 = new MissingMemberException(Environment2.GetResourceString("Lazy_CreateValue_NoParameterlessCtorForT"));
				if (mode != LazyThreadSafetyMode.PublicationOnly)
				{
					this.m_boxed = new Lazy<T>.LazyInternalExceptionHolder(ex2);
				}
				throw ex2;
			}
			return result;
		}

		private static object GetObjectFromMode(LazyThreadSafetyMode mode)
		{
			if (mode == LazyThreadSafetyMode.ExecutionAndPublication)
			{
				return new object();
			}
			if (mode == LazyThreadSafetyMode.PublicationOnly)
			{
				return Lazy<T>.PUBLICATION_ONLY_OR_ALREADY_INITIALIZED;
			}
			if (mode != LazyThreadSafetyMode.None)
			{
				throw new ArgumentOutOfRangeException("mode", Environment2.GetResourceString("Lazy_ctor_ModeInvalid"));
			}
			return null;
		}

		private T LazyInitValue()
		{
			Lazy<T>.Boxed boxed = null;
			switch (this.Mode)
			{
			case LazyThreadSafetyMode.None:
				boxed = this.CreateValue();
				this.m_boxed = boxed;
				break;
			case LazyThreadSafetyMode.PublicationOnly:
				boxed = this.CreateValue();
				if (Interlocked.CompareExchange(ref this.m_boxed, boxed, null) != null)
				{
					boxed = (Lazy<T>.Boxed)this.m_boxed;
				}
				break;
			default:
				lock (this.m_threadSafeObj)
				{
					if (this.m_boxed == null)
					{
						boxed = this.CreateValue();
						this.m_boxed = boxed;
					}
					else
					{
						boxed = (this.m_boxed as Lazy<T>.Boxed);
						if (boxed == null)
						{
							Lazy<T>.LazyInternalExceptionHolder lazyInternalExceptionHolder = this.m_boxed as Lazy<T>.LazyInternalExceptionHolder;
							throw lazyInternalExceptionHolder.m_exception;
						}
					}
				}
				break;
			}
			return boxed.m_value;
		}

		[OnSerializing]
		private void OnSerializing(StreamingContext context)
		{
			T value = this.Value;
		}

		public override string ToString()
		{
			if (!this.IsValueCreated)
			{
				return Environment2.GetResourceString("Lazy_ToString_ValueNotCreated");
			}
			T value = this.Value;
			return value.ToString();
		}

		public bool IsValueCreated
		{
			get
			{
				return this.m_boxed != null && this.m_boxed is Lazy<T>.Boxed;
			}
		}

		internal bool IsValueFaulted
		{
			get
			{
				return this.m_boxed is Lazy<T>.LazyInternalExceptionHolder;
			}
		}

		internal LazyThreadSafetyMode Mode
		{
			get
			{
				if (this.m_threadSafeObj == null)
				{
					return LazyThreadSafetyMode.None;
				}
				if (this.m_threadSafeObj == (object)Lazy<T>.PUBLICATION_ONLY_OR_ALREADY_INITIALIZED)
				{
					return LazyThreadSafetyMode.PublicationOnly;
				}
				return LazyThreadSafetyMode.ExecutionAndPublication;
			}
		}

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public T Value
		{
			get
			{
				if (this.m_boxed == null)
				{
					return this.LazyInitValue();
				}
				Lazy<T>.Boxed boxed = this.m_boxed as Lazy<T>.Boxed;
				if (boxed != null)
				{
					return boxed.m_value;
				}
				Lazy<T>.LazyInternalExceptionHolder lazyInternalExceptionHolder = this.m_boxed as Lazy<T>.LazyInternalExceptionHolder;
				throw lazyInternalExceptionHolder.m_exception;
			}
		}

		internal T ValueForDebugDisplay
		{
			get
			{
				if (!this.IsValueCreated)
				{
					return default(T);
				}
				return ((Lazy<T>.Boxed)this.m_boxed).m_value;
			}
		}

		private volatile object m_boxed;

		[NonSerialized]
		private readonly object m_threadSafeObj;

		[NonSerialized]
		private Func<T> m_valueFactory;

		private static Func<T> PUBLICATION_ONLY_OR_ALREADY_INITIALIZED = () => default(T);

		[Serializable]
		private class Boxed
		{
			internal Boxed(T value)
			{
				this.m_value = value;
			}

			internal T m_value;
		}

		private class LazyInternalExceptionHolder
		{
			internal LazyInternalExceptionHolder(Exception ex)
			{
				this.m_exception = ex;
			}

			internal Exception m_exception;
		}
	}
}
