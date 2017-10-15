using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Threading;

namespace Devcat.Core.Collections.Concurrent
{
	[ComVisible(true)]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	public static class Partitioner
	{
		public static OrderablePartitioner<TSource> Create<TSource>(IEnumerable<TSource> source)
		{
			return Partitioner.Create<TSource>(source, -1);
		}

		public static OrderablePartitioner<TSource> Create<TSource>(TSource[] array, bool loadBalance)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (loadBalance)
			{
				return new Partitioner.DynamicPartitionerForArray<TSource>(array);
			}
			return new Partitioner.StaticIndexRangePartitionerForArray<TSource>(array);
		}

		internal static OrderablePartitioner<TSource> Create<TSource>(IEnumerable<TSource> source, int maxChunkSize)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new Partitioner.DynamicPartitionerForIEnumerable<TSource>(source, maxChunkSize);
		}

		public static OrderablePartitioner<TSource> Create<TSource>(IList<TSource> list, bool loadBalance)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			if (loadBalance)
			{
				return new Partitioner.DynamicPartitionerForIList<TSource>(list);
			}
			return new Partitioner.StaticIndexRangePartitionerForIList<TSource>(list);
		}

		private static int GetDefaultChunkSize<TSource>()
		{
			if (!typeof(TSource).IsValueType)
			{
				return 512 / IntPtr.Size;
			}
			if (typeof(TSource).StructLayoutAttribute.Value == LayoutKind.Explicit)
			{
				return System.Math.Max(1, 512 / Marshal.SizeOf(typeof(TSource)));
			}
			return 128;
		}

		private const int DEFAULT_BYTES_PER_CHUNK = 512;

		private abstract class DynamicPartitionEnumerator_Abstract<TSource, TSourceReader> : IEnumerator<KeyValuePair<long, TSource>>, IDisposable, IEnumerator
		{
			protected DynamicPartitionEnumerator_Abstract(TSourceReader sharedReader, Partitioner.Shared<long> sharedIndex) : this(sharedReader, sharedIndex, -1)
			{
			}

			protected DynamicPartitionEnumerator_Abstract(TSourceReader sharedReader, Partitioner.Shared<long> sharedIndex, int maxChunkSize)
			{
				this.m_sharedReader = sharedReader;
				this.m_sharedIndex = sharedIndex;
				if (maxChunkSize == -1)
				{
					this.m_maxChunkSize = Partitioner.DynamicPartitionEnumerator_Abstract<TSource, TSourceReader>.s_defaultMaxChunkSize;
					return;
				}
				this.m_maxChunkSize = maxChunkSize;
			}

			public abstract void Dispose();

			protected abstract bool GrabNextChunk(int requestedChunkSize);

			public bool MoveNext()
			{
				if (this.m_localOffset == null)
				{
					this.m_localOffset = new Partitioner.Shared<int>(-1);
					this.m_currentChunkSize = new Partitioner.Shared<int>(0);
					this.m_doublingCountdown = 3;
				}
				if (this.m_localOffset.Value < this.m_currentChunkSize.Value - 1)
				{
					this.m_localOffset.Value++;
					return true;
				}
				int requestedChunkSize;
				if (this.m_currentChunkSize.Value == 0)
				{
					requestedChunkSize = 1;
				}
				else if (this.m_doublingCountdown > 0)
				{
					requestedChunkSize = this.m_currentChunkSize.Value;
				}
				else
				{
					requestedChunkSize = System.Math.Min(this.m_currentChunkSize.Value * 2, this.m_maxChunkSize);
					this.m_doublingCountdown = 3;
				}
				this.m_doublingCountdown--;
				if (this.GrabNextChunk(requestedChunkSize))
				{
					this.m_localOffset.Value = 0;
					return true;
				}
				return false;
			}

			public void Reset()
			{
				throw new NotSupportedException();
			}

			public abstract KeyValuePair<long, TSource> Current { get; }

			protected abstract bool HasNoElementsLeft { get; set; }

			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			private const int CHUNK_DOUBLING_RATE = 3;

			protected Partitioner.Shared<int> m_currentChunkSize;

			private int m_doublingCountdown;

			protected Partitioner.Shared<int> m_localOffset;

			protected readonly int m_maxChunkSize;

			protected readonly Partitioner.Shared<long> m_sharedIndex;

			protected readonly TSourceReader m_sharedReader;

			protected static int s_defaultMaxChunkSize = Partitioner.GetDefaultChunkSize<TSource>();
		}

		private abstract class DynamicPartitionEnumeratorForIndexRange_Abstract<TSource, TSourceReader> : Partitioner.DynamicPartitionEnumerator_Abstract<TSource, TSourceReader>
		{
			protected DynamicPartitionEnumeratorForIndexRange_Abstract(TSourceReader sharedReader, Partitioner.Shared<long> sharedIndex) : base(sharedReader, sharedIndex)
			{
			}

			public override void Dispose()
			{
			}

			protected override bool GrabNextChunk(int requestedChunkSize)
			{
				while (!this.HasNoElementsLeft)
				{
					long value = this.m_sharedIndex.Value;
					if (this.HasNoElementsLeft)
					{
						return false;
					}
					long num = System.Math.Min((long)(this.SourceCount - 1), value + (long)requestedChunkSize);
					if (Interlocked.CompareExchange(ref this.m_sharedIndex.Value, num, value) == value)
					{
						this.m_currentChunkSize.Value = (int)(num - value);
						this.m_localOffset.Value = -1;
						this.m_startIndex = (int)(value + 1L);
						return true;
					}
				}
				return false;
			}

			protected override bool HasNoElementsLeft
			{
				get
				{
					return this.m_sharedIndex.Value >= (long)(this.SourceCount - 1);
				}
				set
				{
				}
			}

			protected abstract int SourceCount { get; }

			protected int m_startIndex;
		}

		private abstract class DynamicPartitionerForIndexRange_Abstract<TSource, TCollection> : OrderablePartitioner<TSource>
		{
			protected DynamicPartitionerForIndexRange_Abstract(TCollection data) : base(true, false, true)
			{
				this.m_data = data;
			}

			public override IEnumerable<KeyValuePair<long, TSource>> GetOrderableDynamicPartitions()
			{
				return this.GetOrderableDynamicPartitions_Factory(this.m_data);
			}

			protected abstract IEnumerable<KeyValuePair<long, TSource>> GetOrderableDynamicPartitions_Factory(TCollection data);

			public override IList<IEnumerator<KeyValuePair<long, TSource>>> GetOrderablePartitions(int partitionCount)
			{
				if (partitionCount <= 0)
				{
					throw new ArgumentOutOfRangeException("partitionCount");
				}
				IEnumerator<KeyValuePair<long, TSource>>[] array = new IEnumerator<KeyValuePair<long, TSource>>[partitionCount];
				IEnumerable<KeyValuePair<long, TSource>> orderableDynamicPartitions_Factory = this.GetOrderableDynamicPartitions_Factory(this.m_data);
				for (int i = 0; i < partitionCount; i++)
				{
					array[i] = orderableDynamicPartitions_Factory.GetEnumerator();
				}
				return array;
			}

			public override bool SupportsDynamicPartitions
			{
				get
				{
					return true;
				}
			}

			private TCollection m_data;
		}

		private class DynamicPartitionerForArray<TSource> : Partitioner.DynamicPartitionerForIndexRange_Abstract<TSource, TSource[]>
		{
			internal DynamicPartitionerForArray(TSource[] source) : base(source)
			{
			}

			protected override IEnumerable<KeyValuePair<long, TSource>> GetOrderableDynamicPartitions_Factory(TSource[] m_data)
			{
				return new Partitioner.DynamicPartitionerForArray<TSource>.InternalPartitionEnumerable(m_data);
			}

			private class InternalPartitionEnumerable : IEnumerable<KeyValuePair<long, TSource>>, IEnumerable
			{
				internal InternalPartitionEnumerable(TSource[] sharedReader)
				{
					this.m_sharedReader = sharedReader;
					this.m_sharedIndex = new Partitioner.Shared<long>(-1L);
				}

				public IEnumerator<KeyValuePair<long, TSource>> GetEnumerator()
				{
					return new Partitioner.DynamicPartitionerForArray<TSource>.InternalPartitionEnumerator(this.m_sharedReader, this.m_sharedIndex);
				}

				IEnumerator IEnumerable.GetEnumerator()
				{
					return this.GetEnumerator();
				}

				private Partitioner.Shared<long> m_sharedIndex;

				private readonly TSource[] m_sharedReader;
			}

			private class InternalPartitionEnumerator : Partitioner.DynamicPartitionEnumeratorForIndexRange_Abstract<TSource, TSource[]>
			{
				internal InternalPartitionEnumerator(TSource[] sharedReader, Partitioner.Shared<long> sharedIndex) : base(sharedReader, sharedIndex)
				{
				}

				public override KeyValuePair<long, TSource> Current
				{
					get
					{
						if (this.m_currentChunkSize == null)
						{
							throw new InvalidOperationException(Environment2.GetResourceString("PartitionerStatic_CurrentCalledBeforeMoveNext"));
						}
						return new KeyValuePair<long, TSource>((long)(this.m_startIndex + this.m_localOffset.Value), this.m_sharedReader[this.m_startIndex + this.m_localOffset.Value]);
					}
				}

				protected override int SourceCount
				{
					get
					{
						return this.m_sharedReader.Length;
					}
				}
			}
		}

		private class DynamicPartitionerForIEnumerable<TSource> : OrderablePartitioner<TSource>
		{
			internal DynamicPartitionerForIEnumerable(IEnumerable<TSource> source, int maxChunkSize) : base(true, false, true)
			{
				this.m_source = source;
				this.m_maxChunkSize = maxChunkSize;
			}

			public override IEnumerable<KeyValuePair<long, TSource>> GetOrderableDynamicPartitions()
			{
				return new Partitioner.DynamicPartitionerForIEnumerable<TSource>.InternalPartitionEnumerable(this.m_source.GetEnumerator(), this.m_maxChunkSize);
			}

			public override IList<IEnumerator<KeyValuePair<long, TSource>>> GetOrderablePartitions(int partitionCount)
			{
				if (partitionCount <= 0)
				{
					throw new ArgumentOutOfRangeException("partitionCount");
				}
				IEnumerator<KeyValuePair<long, TSource>>[] array = new IEnumerator<KeyValuePair<long, TSource>>[partitionCount];
				IEnumerable<KeyValuePair<long, TSource>> enumerable = new Partitioner.DynamicPartitionerForIEnumerable<TSource>.InternalPartitionEnumerable(this.m_source.GetEnumerator(), this.m_maxChunkSize);
				for (int i = 0; i < partitionCount; i++)
				{
					array[i] = enumerable.GetEnumerator();
				}
				return array;
			}

			public override bool SupportsDynamicPartitions
			{
				get
				{
					return true;
				}
			}

			private int m_maxChunkSize;

			private IEnumerable<TSource> m_source;

			private class InternalPartitionEnumerable : IEnumerable<KeyValuePair<long, TSource>>, IEnumerable, IDisposable
			{
				internal InternalPartitionEnumerable(IEnumerator<TSource> sharedReader, int maxChunkSize)
				{
					this.m_sharedReader = sharedReader;
					this.m_sharedIndex = new Partitioner.Shared<long>(-1L);
					this.m_hasNoElementsLeft = new Partitioner.Shared<bool>(false);
					this.m_sharedLock = new object();
					this.m_activePartitionCount = new Partitioner.Shared<int>(0);
					this.m_maxChunkSize = maxChunkSize;
				}

				public void Dispose()
				{
					if (!this.m_disposed)
					{
						this.m_disposed = true;
						this.m_sharedReader.Dispose();
					}
				}

				public IEnumerator<KeyValuePair<long, TSource>> GetEnumerator()
				{
					if (this.m_disposed)
					{
						throw new ObjectDisposedException(Environment2.GetResourceString("PartitionerStatic_CanNotCallGetEnumeratorAfterSourceHasBeenDisposed"));
					}
					return new Partitioner.DynamicPartitionerForIEnumerable<TSource>.InternalPartitionEnumerator(this.m_sharedReader, this.m_sharedIndex, this.m_hasNoElementsLeft, this.m_sharedLock, this.m_activePartitionCount, this, this.m_maxChunkSize);
				}

				IEnumerator IEnumerable.GetEnumerator()
				{
					return this.GetEnumerator();
				}

				private Partitioner.Shared<int> m_activePartitionCount;

				private bool m_disposed;

				private Partitioner.Shared<bool> m_hasNoElementsLeft;

				private readonly int m_maxChunkSize;

				private Partitioner.Shared<long> m_sharedIndex;

				private object m_sharedLock;

				private readonly IEnumerator<TSource> m_sharedReader;
			}

			private class InternalPartitionEnumerator : Partitioner.DynamicPartitionEnumerator_Abstract<TSource, IEnumerator<TSource>>
			{
				internal InternalPartitionEnumerator(IEnumerator<TSource> sharedReader, Partitioner.Shared<long> sharedIndex, Partitioner.Shared<bool> hasNoElementsLeft, object sharedLock, Partitioner.Shared<int> activePartitionCount, Partitioner.DynamicPartitionerForIEnumerable<TSource>.InternalPartitionEnumerable enumerable, int maxChunkSize) : base(sharedReader, sharedIndex, maxChunkSize)
				{
					this.m_hasNoElementsLeft = hasNoElementsLeft;
					this.m_sharedLock = sharedLock;
					this.m_enumerable = enumerable;
					this.m_activePartitionCount = activePartitionCount;
					Interlocked.Increment(ref this.m_activePartitionCount.Value);
				}

				public override void Dispose()
				{
					if (Interlocked.Decrement(ref this.m_activePartitionCount.Value) == 0)
					{
						this.m_enumerable.Dispose();
					}
				}

				protected override bool GrabNextChunk(int requestedChunkSize)
				{
					if (this.HasNoElementsLeft)
					{
						return false;
					}
					bool result;
					lock (this.m_sharedLock)
					{
						if (this.HasNoElementsLeft)
						{
							result = false;
						}
						else
						{
							try
							{
								int i;
								for (i = 0; i < requestedChunkSize; i++)
								{
									if (!this.m_sharedReader.MoveNext())
									{
										this.HasNoElementsLeft = true;
										break;
									}
									if (this.m_localList == null)
									{
										this.m_localList = new KeyValuePair<long, TSource>[this.m_maxChunkSize];
									}
									this.m_sharedIndex.Value += 1L;
									this.m_localList[i] = new KeyValuePair<long, TSource>(this.m_sharedIndex.Value, this.m_sharedReader.Current);
								}
								if (i > 0)
								{
									this.m_currentChunkSize.Value = i;
									return true;
								}
								result = false;
							}
							catch
							{
								this.HasNoElementsLeft = true;
								throw;
							}
						}
					}
					return result;
				}

				public override KeyValuePair<long, TSource> Current
				{
					get
					{
						if (this.m_currentChunkSize == null)
						{
							throw new InvalidOperationException(Environment2.GetResourceString("PartitionerStatic_CurrentCalledBeforeMoveNext"));
						}
						return this.m_localList[this.m_localOffset.Value];
					}
				}

				protected override bool HasNoElementsLeft
				{
					get
					{
						return this.m_hasNoElementsLeft.Value;
					}
					set
					{
						this.m_hasNoElementsLeft.Value = true;
					}
				}

				private readonly Partitioner.Shared<int> m_activePartitionCount;

				private Partitioner.DynamicPartitionerForIEnumerable<TSource>.InternalPartitionEnumerable m_enumerable;

				private readonly Partitioner.Shared<bool> m_hasNoElementsLeft;

				private KeyValuePair<long, TSource>[] m_localList;

				private readonly object m_sharedLock;
			}
		}

		private class DynamicPartitionerForIList<TSource> : Partitioner.DynamicPartitionerForIndexRange_Abstract<TSource, IList<TSource>>
		{
			internal DynamicPartitionerForIList(IList<TSource> source) : base(source)
			{
			}

			protected override IEnumerable<KeyValuePair<long, TSource>> GetOrderableDynamicPartitions_Factory(IList<TSource> m_data)
			{
				return new Partitioner.DynamicPartitionerForIList<TSource>.InternalPartitionEnumerable(m_data);
			}

			private class InternalPartitionEnumerable : IEnumerable<KeyValuePair<long, TSource>>, IEnumerable
			{
				internal InternalPartitionEnumerable(IList<TSource> sharedReader)
				{
					this.m_sharedReader = sharedReader;
					this.m_sharedIndex = new Partitioner.Shared<long>(-1L);
				}

				public IEnumerator<KeyValuePair<long, TSource>> GetEnumerator()
				{
					return new Partitioner.DynamicPartitionerForIList<TSource>.InternalPartitionEnumerator(this.m_sharedReader, this.m_sharedIndex);
				}

				IEnumerator IEnumerable.GetEnumerator()
				{
					return this.GetEnumerator();
				}

				private Partitioner.Shared<long> m_sharedIndex;

				private readonly IList<TSource> m_sharedReader;
			}

			private class InternalPartitionEnumerator : Partitioner.DynamicPartitionEnumeratorForIndexRange_Abstract<TSource, IList<TSource>>
			{
				internal InternalPartitionEnumerator(IList<TSource> sharedReader, Partitioner.Shared<long> sharedIndex) : base(sharedReader, sharedIndex)
				{
				}

				public override KeyValuePair<long, TSource> Current
				{
					get
					{
						if (this.m_currentChunkSize == null)
						{
							throw new InvalidOperationException(Environment2.GetResourceString("PartitionerStatic_CurrentCalledBeforeMoveNext"));
						}
						return new KeyValuePair<long, TSource>((long)(this.m_startIndex + this.m_localOffset.Value), this.m_sharedReader[this.m_startIndex + this.m_localOffset.Value]);
					}
				}

				protected override int SourceCount
				{
					get
					{
						return this.m_sharedReader.Count;
					}
				}
			}
		}

		private class Shared<TSource>
		{
			internal Shared(TSource value)
			{
				this.Value = value;
			}

			internal TSource Value;
		}

		private abstract class StaticIndexRangePartition<TSource> : IEnumerator<KeyValuePair<long, TSource>>, IDisposable, IEnumerator
		{
			protected StaticIndexRangePartition(int startIndex, int endIndex)
			{
				this.m_startIndex = startIndex;
				this.m_endIndex = endIndex;
				this.m_offset = startIndex - 1;
			}

			public void Dispose()
			{
			}

			public bool MoveNext()
			{
				if (this.m_offset < this.m_endIndex)
				{
					this.m_offset++;
					return true;
				}
				this.m_offset = this.m_endIndex + 1;
				return false;
			}

			public void Reset()
			{
				throw new NotSupportedException();
			}

			public abstract KeyValuePair<long, TSource> Current { get; }

			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			protected readonly int m_endIndex;

			protected volatile int m_offset;

			protected readonly int m_startIndex;
		}

		private abstract class StaticIndexRangePartitioner<TSource, TCollection> : OrderablePartitioner<TSource>
		{
			protected StaticIndexRangePartitioner() : base(true, true, true)
			{
			}

			protected abstract IEnumerator<KeyValuePair<long, TSource>> CreatePartition(int startIndex, int endIndex);

			public override IList<IEnumerator<KeyValuePair<long, TSource>>> GetOrderablePartitions(int partitionCount)
			{
				if (partitionCount <= 0)
				{
					throw new ArgumentOutOfRangeException("partitionCount");
				}
				int num2;
				int num = System.Math.DivRem(this.SourceCount, partitionCount, out num2);
				IEnumerator<KeyValuePair<long, TSource>>[] array = new IEnumerator<KeyValuePair<long, TSource>>[partitionCount];
				int num3 = -1;
				for (int i = 0; i < partitionCount; i++)
				{
					int num4 = num3 + 1;
					if (i < num2)
					{
						num3 = num4 + num;
					}
					else
					{
						num3 = num4 + num - 1;
					}
					array[i] = this.CreatePartition(num4, num3);
				}
				return array;
			}

			protected abstract int SourceCount { get; }
		}

		private class StaticIndexRangePartitionerForArray<TSource> : Partitioner.StaticIndexRangePartitioner<TSource, TSource[]>
		{
			internal StaticIndexRangePartitionerForArray(TSource[] array)
			{
				this.m_array = array;
			}

			protected override IEnumerator<KeyValuePair<long, TSource>> CreatePartition(int startIndex, int endIndex)
			{
				return new Partitioner.StaticIndexRangePartitionForArray<TSource>(this.m_array, startIndex, endIndex);
			}

			protected override int SourceCount
			{
				get
				{
					return this.m_array.Length;
				}
			}

			private TSource[] m_array;
		}

		private class StaticIndexRangePartitionerForIList<TSource> : Partitioner.StaticIndexRangePartitioner<TSource, IList<TSource>>
		{
			internal StaticIndexRangePartitionerForIList(IList<TSource> list)
			{
				this.m_list = list;
			}

			protected override IEnumerator<KeyValuePair<long, TSource>> CreatePartition(int startIndex, int endIndex)
			{
				return new Partitioner.StaticIndexRangePartitionForIList<TSource>(this.m_list, startIndex, endIndex);
			}

			protected override int SourceCount
			{
				get
				{
					return this.m_list.Count;
				}
			}

			private IList<TSource> m_list;
		}

		private class StaticIndexRangePartitionForArray<TSource> : Partitioner.StaticIndexRangePartition<TSource>
		{
			internal StaticIndexRangePartitionForArray(TSource[] array, int startIndex, int endIndex) : base(startIndex, endIndex)
			{
				this.m_array = array;
			}

			public override KeyValuePair<long, TSource> Current
			{
				get
				{
					if (this.m_offset < this.m_startIndex)
					{
						throw new InvalidOperationException(Environment2.GetResourceString("PartitionerStatic_CurrentCalledBeforeMoveNext"));
					}
					return new KeyValuePair<long, TSource>((long)this.m_offset, this.m_array[this.m_offset]);
				}
			}

			private volatile TSource[] m_array;
		}

		private class StaticIndexRangePartitionForIList<TSource> : Partitioner.StaticIndexRangePartition<TSource>
		{
			internal StaticIndexRangePartitionForIList(IList<TSource> list, int startIndex, int endIndex) : base(startIndex, endIndex)
			{
				this.m_list = list;
			}

			public override KeyValuePair<long, TSource> Current
			{
				get
				{
					if (this.m_offset < this.m_startIndex)
					{
						throw new InvalidOperationException(Environment2.GetResourceString("PartitionerStatic_CurrentCalledBeforeMoveNext"));
					}
					return new KeyValuePair<long, TSource>((long)this.m_offset, this.m_list[this.m_offset]);
				}
			}

			private volatile IList<TSource> m_list;
		}
	}
}
