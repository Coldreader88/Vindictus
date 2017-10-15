using System;
using System.Collections.Generic;

namespace Devcat.Core.Collections
{
	internal class DelayedWriteMap<TKey, TValue> : SortedDictionary<TKey, TValue>
	{
		public DelayedWriteMap()
		{
			this.reservedPairs = new WriteFreeQueue2<DelayedWriteMap<TKey, TValue>.ReservedPair>();
		}

		public DelayedWriteMap(IComparer<TKey> comparer) : base(comparer)
		{
			this.reservedPairs = new WriteFreeQueue2<DelayedWriteMap<TKey, TValue>.ReservedPair>();
		}

		public DelayedWriteMap(IDictionary<TKey, TValue> dictionary) : base(dictionary)
		{
			this.reservedPairs = new WriteFreeQueue2<DelayedWriteMap<TKey, TValue>.ReservedPair>();
		}

		public DelayedWriteMap(IDictionary<TKey, TValue> dictionary, IComparer<TKey> comparer) : base(dictionary, comparer)
		{
			this.reservedPairs = new WriteFreeQueue2<DelayedWriteMap<TKey, TValue>.ReservedPair>();
		}

		public new int Count
		{
			get
			{
				this.ProcessReserved();
				return base.Count;
			}
		}

		public new SortedDictionary<TKey, TValue>.KeyCollection Keys
		{
			get
			{
				this.ProcessReserved();
				return base.Keys;
			}
		}

		public new SortedDictionary<TKey, TValue>.ValueCollection Values
		{
			get
			{
				this.ProcessReserved();
				return base.Values;
			}
		}

		public new TValue this[TKey key]
		{
			get
			{
				this.ProcessReserved();
				return base[key];
			}
			set
			{
				this.reservedPairs.Enqueue(new DelayedWriteMap<TKey, TValue>.ReservedPair(key, value, 2));
			}
		}

		public new void Add(TKey key, TValue value)
		{
			this.reservedPairs.Enqueue(new DelayedWriteMap<TKey, TValue>.ReservedPair(key, value, 0));
		}

		public new void Clear()
		{
			this.reservedPairs.Enqueue(new DelayedWriteMap<TKey, TValue>.ReservedPair(default(TKey), default(TValue), 3));
		}

		public new bool ContainsKey(TKey key)
		{
			this.ProcessReserved();
			return base.ContainsKey(key);
		}

		public new bool ContainsValue(TValue value)
		{
			this.ProcessReserved();
			return base.ContainsValue(value);
		}

		public new void CopyTo(KeyValuePair<TKey, TValue>[] array, int index)
		{
			this.ProcessReserved();
			base.CopyTo(array, index);
		}

		public new SortedDictionary<TKey, TValue>.Enumerator GetEnumerator()
		{
			this.ProcessReserved();
			return base.GetEnumerator();
		}

		public new void Remove(TKey key)
		{
			this.reservedPairs.Enqueue(new DelayedWriteMap<TKey, TValue>.ReservedPair(key, default(TValue), 1));
		}

		public new bool TryGetValue(TKey key, out TValue value)
		{
			this.ProcessReserved();
			return base.TryGetValue(key, out value);
		}

		private void ProcessReserved()
		{
			DelayedWriteMap<TKey, TValue>.ReservedPair reservedPair;
			while (this.reservedPairs.TryDequeue(out reservedPair))
			{
				switch (reservedPair.Mode)
				{
				case 0:
					base.Add(reservedPair.Key, reservedPair.Value);
					break;
				case 1:
					base.Remove(reservedPair.Key);
					break;
				case 2:
					base[reservedPair.Key] = reservedPair.Value;
					break;
				case 3:
					base.Clear();
					break;
				}
			}
		}

		private const byte ReservedAdd = 0;

		private const byte ReservedRemove = 1;

		private const byte ReservedReplace = 2;

		private const byte ReservedClear = 3;

		private WriteFreeQueue2<DelayedWriteMap<TKey, TValue>.ReservedPair> reservedPairs;

		private struct ReservedPair
		{
			public ReservedPair(TKey key, TValue value, byte mode)
			{
				this.Key = key;
				this.Value = value;
				this.Mode = mode;
			}

			public TKey Key;

			public TValue Value;

			public byte Mode;
		}
	}
}
