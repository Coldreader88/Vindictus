using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Threading;

namespace Devcat.Core.Collections.Concurrent
{
	[DebuggerDisplay("Count = {Count}, Type = {m_collection}")]
	[ComVisible(false)]
	[DebuggerTypeProxy(typeof(SystemThreadingCollections_BlockingCollectionDebugView<>))]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	public class BlockingCollection<T> : IEnumerable<T>, ICollection, IEnumerable, IDisposable
	{
		public BlockingCollection() : this(new ConcurrentQueue<T>())
		{
		}

		public BlockingCollection(IProducerConsumerCollection<T> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			this.Initialize(collection, -1, collection.Count);
		}

		public BlockingCollection(int boundedCapacity) : this(new ConcurrentQueue<T>(), boundedCapacity)
		{
		}

		public BlockingCollection(IProducerConsumerCollection<T> collection, int boundedCapacity)
		{
			if (boundedCapacity < 1)
			{
				throw new ArgumentOutOfRangeException("boundedCapacity", boundedCapacity, "BlockingCollection_ctor_BoundedCapacityRange");
			}
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			int count = collection.Count;
			if (count > boundedCapacity)
			{
				throw new ArgumentException("BlockingCollection_ctor_CountMoreThanCapacity");
			}
			this.Initialize(collection, boundedCapacity, count);
		}

		public void Add(T item)
		{
			this.TryAddWithNoTimeValidation(item, -1, default(CancellationToken));
		}

		public void Add(T item, CancellationToken cancellationToken)
		{
			this.TryAddWithNoTimeValidation(item, -1, cancellationToken);
		}

		public static int AddToAny(BlockingCollection<T>[] collections, T item)
		{
			return BlockingCollection<T>.TryAddToAny(collections, item, -1, default(CancellationToken));
		}

		public static int AddToAny(BlockingCollection<T>[] collections, T item, CancellationToken cancellationToken)
		{
			return BlockingCollection<T>.TryAddToAny(collections, item, -1, cancellationToken);
		}

		private void CancelWaitingConsumers()
		{
			this.m_ConsumersCancellationTokenSource.Cancel();
		}

		private void CancelWaitingProducers()
		{
			this.m_ProducersCancellationTokenSource.Cancel();
		}

		private void CheckDisposed()
		{
			if (this.m_isDisposed)
			{
				throw new ObjectDisposedException("BlockingCollection", "BlockingCollection_Disposed");
			}
		}

        public void CompleteAdding()
        {
            this.CheckDisposed();
            if (this.IsAddingCompleted)
            {
                return;
            }
            SpinWait spinWait = new SpinWait();
            while (true)
            {
                int mCurrentAdders = this.m_currentAdders;
                if ((mCurrentAdders & -2147483648) != 0)
                {
                    spinWait.Reset();
                    while (this.m_currentAdders != -2147483648)
                    {
                        spinWait.SpinOnce();
                    }
                    return;
                }
                if (Interlocked.CompareExchange(ref this.m_currentAdders, mCurrentAdders | -2147483648, mCurrentAdders) == mCurrentAdders)
                {
                    break;
                }
                spinWait.SpinOnce();
            }
            spinWait.Reset();
            while (this.m_currentAdders != -2147483648)
            {
                spinWait.SpinOnce();
            }
            if (this.Count == 0)
            {
                this.CancelWaitingConsumers();
            }
            this.CancelWaitingProducers();
        }

		public void CopyTo(T[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (!this.m_isDisposed)
			{
				if (this.m_freeNodes != null)
				{
					this.m_freeNodes.Dispose();
				}
				this.m_occupiedNodes.Dispose();
				this.m_isDisposed = true;
			}
		}

		public IEnumerable<T> GetConsumingEnumerable()
		{
			return this.GetConsumingEnumerable(CancellationToken.None);
		}

		public IEnumerable<T> GetConsumingEnumerable(CancellationToken cancellationToken)
		{
			CancellationTokenSource combinedTokenSource = null;
			combinedTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, this.m_ConsumersCancellationTokenSource.Token);
			while (!this.IsCompleted)
			{
				T iteratorVariable;
				if (this.TryTakeWithNoTimeValidation(out iteratorVariable, -1, cancellationToken, combinedTokenSource))
				{
					yield return iteratorVariable;
				}
			}
			yield break;
		}

		private static List<WaitHandle> GetHandles(BlockingCollection<T>[] collections, BlockingCollection<T>.OperationMode operationMode, CancellationToken externalCancellationToken, bool excludeCompleted, out CancellationToken[] cancellationTokens)
		{
			List<WaitHandle> list = new List<WaitHandle>(collections.Length);
			List<CancellationToken> list2 = new List<CancellationToken>(collections.Length + 1)
			{
				externalCancellationToken
			};
			if (operationMode == BlockingCollection<T>.OperationMode.Add)
			{
				for (int i = 0; i < collections.Length; i++)
				{
					if (collections[i].m_freeNodes != null)
					{
						list.Add(collections[i].m_freeNodes.AvailableWaitHandle);
						list2.Add(collections[i].m_ProducersCancellationTokenSource.Token);
					}
				}
			}
			else
			{
				for (int j = 0; j < collections.Length; j++)
				{
					if (!excludeCompleted || !collections[j].IsCompleted)
					{
						list.Add(collections[j].m_occupiedNodes.AvailableWaitHandle);
						list2.Add(collections[j].m_ConsumersCancellationTokenSource.Token);
					}
				}
			}
			cancellationTokens = list2.ToArray();
			return list;
		}

		private void Initialize(IProducerConsumerCollection<T> collection, int boundedCapacity, int collectionCount)
		{
			this.m_collection = collection;
			this.m_boundedCapacity = boundedCapacity;
			this.m_isDisposed = false;
			this.m_ConsumersCancellationTokenSource = new CancellationTokenSource();
			this.m_ProducersCancellationTokenSource = new CancellationTokenSource();
			if (boundedCapacity == -1)
			{
				this.m_freeNodes = null;
			}
			else
			{
				this.m_freeNodes = new SemaphoreSlim(boundedCapacity - collectionCount);
			}
			this.m_occupiedNodes = new SemaphoreSlim(collectionCount);
		}

		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			this.CheckDisposed();
			return this.m_collection.GetEnumerator();
		}

		void ICollection.CopyTo(Array array, int index)
		{
			this.CheckDisposed();
			T[] array2 = this.m_collection.ToArray();
			try
			{
				Array.Copy(array2, 0, array, index, array2.Length);
			}
			catch (ArgumentNullException)
			{
				throw new ArgumentNullException("array");
			}
			catch (ArgumentOutOfRangeException)
			{
				throw new ArgumentOutOfRangeException("index", index, "BlockingCollection_CopyTo_NonNegative");
			}
			catch (ArgumentException)
			{
				throw new ArgumentException("BlockingCollection_CopyTo_TooManyElems", "index");
			}
			catch (RankException)
			{
				throw new ArgumentException("BlockingCollection_CopyTo_MultiDim", "array");
			}
			catch (InvalidCastException)
			{
				throw new ArgumentException("BlockingCollection_CopyTo_IncorrectType", "array");
			}
			catch (ArrayTypeMismatchException)
			{
				throw new ArgumentException("BlockingCollection_CopyTo_IncorrectType", "array");
			}
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<T>)this).GetEnumerator();
		}

		public T Take()
		{
			T result;
			if (!this.TryTake(out result, -1, CancellationToken.None))
			{
				throw new InvalidOperationException("BlockingCollection_CantTakeWhenDone");
			}
			return result;
		}

		public T Take(CancellationToken cancellationToken)
		{
			T result;
			if (!this.TryTake(out result, -1, cancellationToken))
			{
				throw new InvalidOperationException("BlockingCollection_CantTakeWhenDone");
			}
			return result;
		}

		public static int TakeFromAny(BlockingCollection<T>[] collections, out T item)
		{
			return BlockingCollection<T>.TryTakeFromAny(collections, out item, -1);
		}

		public static int TakeFromAny(BlockingCollection<T>[] collections, out T item, CancellationToken cancellationToken)
		{
			return BlockingCollection<T>.TryTakeFromAny(collections, out item, -1, cancellationToken);
		}

		public T[] ToArray()
		{
			this.CheckDisposed();
			return this.m_collection.ToArray();
		}

		public bool TryAdd(T item)
		{
			return this.TryAddWithNoTimeValidation(item, 0, default(CancellationToken));
		}

		public bool TryAdd(T item, int millisecondsTimeout)
		{
			BlockingCollection<T>.ValidateMillisecondsTimeout(millisecondsTimeout);
			return this.TryAddWithNoTimeValidation(item, millisecondsTimeout, default(CancellationToken));
		}

		public bool TryAdd(T item, TimeSpan timeout)
		{
			BlockingCollection<T>.ValidateTimeout(timeout);
			return this.TryAddWithNoTimeValidation(item, (int)timeout.TotalMilliseconds, default(CancellationToken));
		}

		public bool TryAdd(T item, int millisecondsTimeout, CancellationToken cancellationToken)
		{
			BlockingCollection<T>.ValidateMillisecondsTimeout(millisecondsTimeout);
			return this.TryAddWithNoTimeValidation(item, millisecondsTimeout, cancellationToken);
		}

		private static int TryAddTakeAny(BlockingCollection<T>[] collections, ref T item, int millisecondsTimeout, BlockingCollection<T>.OperationMode operationMode, CancellationToken externalCancellationToken)
		{
			BlockingCollection<T>[] array = BlockingCollection<T>.ValidateCollectionsArray(collections, operationMode);
			int num = millisecondsTimeout;
			long startTimeTicks = 0L;
			if (millisecondsTimeout != -1)
			{
				startTimeTicks = DateTime.UtcNow.Ticks;
			}
			if (operationMode == BlockingCollection<T>.OperationMode.Add)
			{
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i].m_freeNodes == null)
					{
						array[i].TryAdd(item);
						return i;
					}
				}
			}
			CancellationToken[] tokens;
			List<WaitHandle> handles = BlockingCollection<T>.GetHandles(array, operationMode, externalCancellationToken, false, out tokens);
			while (millisecondsTimeout == -1 || num >= 0)
			{
				int num2 = -1;
				CancellationTokenSource cancellationTokenSource = null;
				try
				{
					num2 = BlockingCollection<T>.WaitHandle_WaitAny(handles, 0, externalCancellationToken, externalCancellationToken);
					if (num2 == 258)
					{
						cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(tokens);
						num2 = BlockingCollection<T>.WaitHandle_WaitAny(handles, num, cancellationTokenSource.Token, externalCancellationToken);
					}
				}
				catch (OperationCanceledException)
				{
					if (externalCancellationToken.IsCancellationRequested)
					{
						throw;
					}
					if (operationMode != BlockingCollection<T>.OperationMode.Take || millisecondsTimeout == -1)
					{
						throw new ArgumentException("BlockingCollection_CantTakeAnyWhenDone", "collections");
					}
					handles = BlockingCollection<T>.GetHandles(array, operationMode, externalCancellationToken, true, out tokens);
					num = BlockingCollection<T>.UpdateTimeOut(startTimeTicks, millisecondsTimeout);
					if (handles.Count != 0 && num != 0)
					{
						continue;
					}
					return -1;
				}
				finally
				{
					if (cancellationTokenSource != null)
					{
						cancellationTokenSource.Dispose();
					}
				}
				if (num2 == 258)
				{
					return -1;
				}
				if (operationMode == BlockingCollection<T>.OperationMode.Add)
				{
					if (array[num2].TryAdd(item))
					{
						return num2;
					}
				}
				else if (operationMode == BlockingCollection<T>.OperationMode.Take && array[num2].TryTake(out item))
				{
					return num2;
				}
				if (millisecondsTimeout <= 0)
				{
					continue;
				}
				num = BlockingCollection<T>.UpdateTimeOut(startTimeTicks, millisecondsTimeout);
				if (num <= 0)
				{
					return -1;
				}
			}
			return -1;
		}

		public static int TryAddToAny(BlockingCollection<T>[] collections, T item)
		{
			return BlockingCollection<T>.TryAddToAny(collections, item, 0, default(CancellationToken));
		}

		public static int TryAddToAny(BlockingCollection<T>[] collections, T item, int millisecondsTimeout)
		{
			BlockingCollection<T>.ValidateMillisecondsTimeout(millisecondsTimeout);
			return BlockingCollection<T>.TryAddTakeAny(collections, ref item, millisecondsTimeout, BlockingCollection<T>.OperationMode.Add, default(CancellationToken));
		}

		public static int TryAddToAny(BlockingCollection<T>[] collections, T item, TimeSpan timeout)
		{
			BlockingCollection<T>.ValidateTimeout(timeout);
			return BlockingCollection<T>.TryAddTakeAny(collections, ref item, (int)timeout.TotalMilliseconds, BlockingCollection<T>.OperationMode.Add, default(CancellationToken));
		}

		public static int TryAddToAny(BlockingCollection<T>[] collections, T item, int millisecondsTimeout, CancellationToken cancellationToken)
		{
			BlockingCollection<T>.ValidateMillisecondsTimeout(millisecondsTimeout);
			return BlockingCollection<T>.TryAddTakeAny(collections, ref item, millisecondsTimeout, BlockingCollection<T>.OperationMode.Add, cancellationToken);
		}

        private bool TryAddWithNoTimeValidation(T item, int millisecondsTimeout, CancellationToken cancellationToken)
        {
            bool flag;
            CheckDisposed();
            if (cancellationToken.IsCancellationRequested)
            {
                throw new OperationCanceledException2("Common_OperationCanceled", cancellationToken);
            }
            if (IsAddingCompleted)
            {
                throw new InvalidOperationException("BlockingCollection_Completed");
            }
            bool flag1 = true;
            if (m_freeNodes != null)
            {
                CancellationTokenSource cancellationTokenSource = null;
                {
                    try
                    {
                        flag1 = m_freeNodes.Wait(0);
                        if (!flag1 && millisecondsTimeout != 0)
                        {
                            cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, m_ProducersCancellationTokenSource.Token);
                            flag1 = m_freeNodes.Wait(millisecondsTimeout, cancellationTokenSource.Token);
                        }
                    }
                    catch (OperationCanceledException operationCanceledException)
                    {
                        if (!cancellationToken.IsCancellationRequested)
                        {
                            throw new InvalidOperationException("BlockingCollection_Add_ConcurrentCompleteAdd", operationCanceledException);
                        }
                        throw new OperationCanceledException2("Common_OperationCanceled", cancellationToken);
                    }
                }
            }
            if (!flag1)
            {
                return flag1;
            }
            SpinWait spinWait = new SpinWait();
            while (true)
            {
                int mCurrentAdders = m_currentAdders;
                if ((mCurrentAdders & -2147483648) != 0)
                {
                    spinWait.Reset();
                    while (m_currentAdders != -2147483648)
                    {
                        spinWait.SpinOnce();
                    }
                    throw new InvalidOperationException("BlockingCollection_Completed");
                }
                if (Interlocked.CompareExchange(ref m_currentAdders, mCurrentAdders + 1, mCurrentAdders) == mCurrentAdders)
                {
                    break;
                }
                spinWait.SpinOnce();
            }
            try
            {
                bool flag2 = false;
                try
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    flag2 = m_collection.TryAdd(item);
                }
                catch
                {
                    if (m_freeNodes != null)
                    {
                        m_freeNodes.Release();
                    }
                    throw;
                }
                if (!flag2)
                {
                    throw new InvalidOperationException("BlockingCollection_Add_Failed");
                }
                m_occupiedNodes.Release();
                flag = flag1;
            }
            finally
            {
                Interlocked.Decrement(ref m_currentAdders);
            }
            return flag;
        }

        public bool TryTake(out T item)
		{
			return this.TryTake(out item, 0, CancellationToken.None);
		}

		public bool TryTake(out T item, int millisecondsTimeout)
		{
			BlockingCollection<T>.ValidateMillisecondsTimeout(millisecondsTimeout);
			return this.TryTakeWithNoTimeValidation(out item, millisecondsTimeout, CancellationToken.None, null);
		}

		public bool TryTake(out T item, TimeSpan timeout)
		{
			BlockingCollection<T>.ValidateTimeout(timeout);
			return this.TryTakeWithNoTimeValidation(out item, (int)timeout.TotalMilliseconds, CancellationToken.None, null);
		}

		public bool TryTake(out T item, int millisecondsTimeout, CancellationToken cancellationToken)
		{
			BlockingCollection<T>.ValidateMillisecondsTimeout(millisecondsTimeout);
			return this.TryTakeWithNoTimeValidation(out item, millisecondsTimeout, cancellationToken, null);
		}

		public static int TryTakeFromAny(BlockingCollection<T>[] collections, out T item)
		{
			return BlockingCollection<T>.TryTakeFromAny(collections, out item, 0);
		}

		public static int TryTakeFromAny(BlockingCollection<T>[] collections, out T item, int millisecondsTimeout)
		{
			BlockingCollection<T>.ValidateMillisecondsTimeout(millisecondsTimeout);
			T t = default(T);
			int result = BlockingCollection<T>.TryAddTakeAny(collections, ref t, millisecondsTimeout, BlockingCollection<T>.OperationMode.Take, default(CancellationToken));
			item = t;
			return result;
		}

		public static int TryTakeFromAny(BlockingCollection<T>[] collections, out T item, TimeSpan timeout)
		{
			BlockingCollection<T>.ValidateTimeout(timeout);
			T t = default(T);
			int result = BlockingCollection<T>.TryAddTakeAny(collections, ref t, (int)timeout.TotalMilliseconds, BlockingCollection<T>.OperationMode.Take, default(CancellationToken));
			item = t;
			return result;
		}

		public static int TryTakeFromAny(BlockingCollection<T>[] collections, out T item, int millisecondsTimeout, CancellationToken cancellationToken)
		{
			BlockingCollection<T>.ValidateMillisecondsTimeout(millisecondsTimeout);
			T t = default(T);
			int result = BlockingCollection<T>.TryAddTakeAny(collections, ref t, millisecondsTimeout, BlockingCollection<T>.OperationMode.Take, cancellationToken);
			item = t;
			return result;
		}

		private bool TryTakeWithNoTimeValidation(out T item, int millisecondsTimeout, CancellationToken cancellationToken, CancellationTokenSource combinedTokenSource)
		{
			this.CheckDisposed();
			item = default(T);
			if (cancellationToken.IsCancellationRequested)
			{
				throw new OperationCanceledException2("Common_OperationCanceled", cancellationToken);
			}
			if (this.IsCompleted)
			{
				return false;
			}
			bool flag = false;
			CancellationTokenSource cancellationTokenSource = combinedTokenSource;
			try
			{
				flag = this.m_occupiedNodes.Wait(0);
				if (!flag && millisecondsTimeout != 0)
				{
					if (combinedTokenSource == null)
					{
						cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, this.m_ConsumersCancellationTokenSource.Token);
					}
					flag = this.m_occupiedNodes.Wait(millisecondsTimeout, cancellationTokenSource.Token);
				}
			}
			catch (OperationCanceledException)
			{
				if (cancellationToken.IsCancellationRequested)
				{
					throw new OperationCanceledException2("Common_OperationCanceled", cancellationToken);
				}
				return false;
			}
			finally
			{
				if (cancellationTokenSource != null && combinedTokenSource == null)
				{
					cancellationTokenSource.Dispose();
				}
			}
			if (flag)
			{
				bool flag2 = false;
				bool flag3 = true;
				try
				{
					cancellationToken.ThrowIfCancellationRequested();
					flag2 = this.m_collection.TryTake(out item);
					flag3 = false;
					if (!flag2)
					{
						throw new InvalidOperationException("BlockingCollection_Take_CollectionModified");
					}
				}
				finally
				{
					if (flag2)
					{
						if (this.m_freeNodes != null)
						{
							this.m_freeNodes.Release();
						}
					}
					else if (flag3)
					{
						this.m_occupiedNodes.Release();
					}
					if (this.IsCompleted)
					{
						this.CancelWaitingConsumers();
					}
				}
			}
			return flag;
		}

		private static int UpdateTimeOut(long startTimeTicks, int originalWaitMillisecondsTimeout)
		{
			if (originalWaitMillisecondsTimeout == 0)
			{
				return 0;
			}
			long num = (DateTime.UtcNow.Ticks - startTimeTicks) / 10000L;
			if (num > 2147483647L)
			{
				return 0;
			}
			int num2 = originalWaitMillisecondsTimeout - (int)num;
			if (num2 <= 0)
			{
				return 0;
			}
			return num2;
		}

		private static BlockingCollection<T>[] ValidateCollectionsArray(BlockingCollection<T>[] collections, BlockingCollection<T>.OperationMode operationMode)
		{
			if (collections == null)
			{
				throw new ArgumentNullException("collections");
			}
			if (collections.Length < 1)
			{
				throw new ArgumentException("BlockingCollection_ValidateCollectionsArray_ZeroSize", "collections");
			}
			if ((Thread.CurrentThread.GetApartmentState() == ApartmentState.MTA && collections.Length > 63) || (Thread.CurrentThread.GetApartmentState() == ApartmentState.STA && collections.Length > 62))
			{
				throw new ArgumentOutOfRangeException("collections", "BlockingCollection_ValidateCollectionsArray_LargeSize");
			}
			BlockingCollection<T>[] array = new BlockingCollection<T>[collections.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = collections[i];
				if (array[i] == null)
				{
					throw new ArgumentException("BlockingCollection_ValidateCollectionsArray_NullElems", "collections");
				}
				if (array[i].m_isDisposed)
				{
					throw new ObjectDisposedException("collections", "BlockingCollection_ValidateCollectionsArray_DispElems");
				}
				if (operationMode == BlockingCollection<T>.OperationMode.Add && array[i].IsAddingCompleted)
				{
					throw new ArgumentException("BlockingCollection_CantTakeAnyWhenDone", "collections");
				}
			}
			return array;
		}

		private static void ValidateMillisecondsTimeout(int millisecondsTimeout)
		{
			if (millisecondsTimeout < 0 && millisecondsTimeout != -1)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeout", millisecondsTimeout, string.Format(CultureInfo.InvariantCulture, "BlockingCollection_TimeoutInvalid", new object[]
				{
					int.MaxValue
				}));
			}
		}

		private static void ValidateTimeout(TimeSpan timeout)
		{
			long num = (long)timeout.TotalMilliseconds;
			if ((num < 0L || num > 2147483647L) && num != -1L)
			{
				throw new ArgumentOutOfRangeException("timeout", timeout, string.Format(CultureInfo.InvariantCulture, "BlockingCollection_TimeoutInvalid", new object[]
				{
					int.MaxValue
				}));
			}
		}

		private static int WaitHandle_WaitAny(List<WaitHandle> handles, int millisecondsTimeout, CancellationToken combinedToken, CancellationToken externalToken)
		{
			WaitHandle[] array = new WaitHandle[handles.Count + 1];
			for (int i = 0; i < handles.Count; i++)
			{
				array[i] = handles[i];
			}
			array[handles.Count] = combinedToken.WaitHandle;
			int result = WaitHandle.WaitAny(array, millisecondsTimeout, false);
			if (!combinedToken.IsCancellationRequested)
			{
				return result;
			}
			if (externalToken.IsCancellationRequested)
			{
				throw new OperationCanceledException2("Common_OperationCanceled", externalToken);
			}
			throw new OperationCanceledException2("Common_OperationCanceled");
		}

		public int BoundedCapacity
		{
			get
			{
				this.CheckDisposed();
				return this.m_boundedCapacity;
			}
		}

		public int Count
		{
			get
			{
				this.CheckDisposed();
				return this.m_occupiedNodes.CurrentCount;
			}
		}

		public bool IsAddingCompleted
		{
			get
			{
				this.CheckDisposed();
				return this.m_currentAdders == int.MinValue;
			}
		}

		public bool IsCompleted
		{
			get
			{
				this.CheckDisposed();
				return this.IsAddingCompleted && this.m_occupiedNodes.CurrentCount == 0;
			}
		}

		bool ICollection.IsSynchronized
		{
			get
			{
				this.CheckDisposed();
				return false;
			}
		}

		object ICollection.SyncRoot
		{
			get
			{
				throw new NotSupportedException("ConcurrentCollection_SyncRoot_NotSupported");
			}
		}

		private const int COMPLETE_ADDING_ON_MASK = -2147483648;

		private const int NON_BOUNDED = -1;

		private int m_boundedCapacity;

		private IProducerConsumerCollection<T> m_collection;

		private CancellationTokenSource m_ConsumersCancellationTokenSource;

		private volatile int m_currentAdders;

		private SemaphoreSlim m_freeNodes;

		private volatile bool m_isDisposed;

		private SemaphoreSlim m_occupiedNodes;

		private CancellationTokenSource m_ProducersCancellationTokenSource;

		private enum OperationMode
		{
			Add,
			Take
		}
	}
}
