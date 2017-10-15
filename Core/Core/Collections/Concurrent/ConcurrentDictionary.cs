using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Threading;
using Devcat.Core.Collections.Generic;
using Devcat.Core.Threading;

namespace Devcat.Core.Collections.Concurrent
{
    [DebuggerTypeProxy(typeof(Mscorlib_DictionaryDebugView<,>)), DebuggerDisplay("Count = {Count}"), ComVisible(false)]
    [HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
    [Serializable]
    public class ConcurrentDictionary<TKey, TValue> : IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IDictionary, ICollection, IEnumerable
    {
        public ConcurrentDictionary() : this(ConcurrentDictionary<TKey, TValue>.DefaultConcurrencyLevel, 31)
        {
        }

        public ConcurrentDictionary(IEnumerable<KeyValuePair<TKey, TValue>> collection) : this(collection, EqualityComparer<TKey>.Default)
        {
        }

        public ConcurrentDictionary(IEqualityComparer<TKey> comparer) : this(ConcurrentDictionary<TKey, TValue>.DefaultConcurrencyLevel, 31, comparer)
        {
        }

        public ConcurrentDictionary(IEnumerable<KeyValuePair<TKey, TValue>> collection, IEqualityComparer<TKey> comparer) : this(ConcurrentDictionary<TKey, TValue>.DefaultConcurrencyLevel, collection, comparer)
        {
        }

        public ConcurrentDictionary(int concurrencyLevel, int capacity) : this(concurrencyLevel, capacity, EqualityComparer<TKey>.Default)
        {
        }

        public ConcurrentDictionary(int concurrencyLevel, IEnumerable<KeyValuePair<TKey, TValue>> collection, IEqualityComparer<TKey> comparer) : this(concurrencyLevel, 31, comparer)
        {
            if (collection == null)
            {
                throw new ArgumentNullException("collection");
            }
            if (comparer == null)
            {
                throw new ArgumentNullException("comparer");
            }
            InitializeFromCollection(collection);
        }

        public ConcurrentDictionary(int concurrencyLevel, int capacity, IEqualityComparer<TKey> comparer)
        {
            if (concurrencyLevel < 1)
            {
                throw new ArgumentOutOfRangeException("concurrencyLevel", GetResource("ConcurrentDictionary_ConcurrencyLevelMustBePositive"));
            }
            if (capacity < 0)
            {
                throw new ArgumentOutOfRangeException("capacity", GetResource("ConcurrentDictionary_CapacityMustNotBeNegative"));
            }
            if (comparer == null)
            {
                throw new ArgumentNullException("comparer");
            }
            if (capacity < concurrencyLevel)
            {
                capacity = concurrencyLevel;
            }
            m_locks = new object[concurrencyLevel];
            for (int i = 0; i < m_locks.Length; i++)
            {
                m_locks[i] = new object();
            }
            m_countPerLock = new int[m_locks.Length];
            m_buckets = new ConcurrentDictionary<TKey, TValue>.Node[capacity];
            m_comparer = comparer;
        }

        private void AcquireAllLocks(ref int locksAcquired)
        {
            AcquireLocks(0, m_locks.Length, ref locksAcquired);
        }

        private void AcquireLocks(int fromInclusive, int toExclusive, ref int locksAcquired)
        {
            for (int i = fromInclusive; i < toExclusive; i++)
            {
                bool flag = false;
                try
                {
                    Monitor2.Enter(m_locks[i], ref flag);
                }
                finally
                {
                    if (flag)
                    {
                        locksAcquired++;
                    }
                }
            }
        }

        public TValue AddOrUpdate(TKey key, TValue addValue, Func<TKey, TValue, TValue> updateValueFactory)
        {
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }
            if (updateValueFactory == null)
            {
                throw new ArgumentNullException("updateValueFactory");
            }
            TValue taken;
            while (true)
            {
                TValue i;
                if (!TryGetValue(key, out i))
                {
                    TValue result;
                    if (TryAddInternal(key, addValue, false, true, out result))
                    {
                        return result;
                    }
                }
                else
                {
                    taken = updateValueFactory(key, i);
                    if (TryUpdate(key, taken, i))
                    {
                        break;
                    }
                }
            }
            return taken;
        }

        public TValue AddOrUpdate(TKey key, Func<TKey, TValue> addValueFactory, Func<TKey, TValue, TValue> updateValueFactory)
        {
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }
            if (addValueFactory == null)
            {
                throw new ArgumentNullException("addValueFactory");
            }
            if (updateValueFactory == null)
            {
                throw new ArgumentNullException("updateValueFactory");
            }
            TValue local3;
            while (true)
            {
                TValue tValue;
                if (!TryGetValue(key, out tValue))
                {
                    local3 = addValueFactory(key);
                    TValue newValue;
                    if (TryAddInternal(key, local3, false, true, out newValue))
                    {
                        return newValue;
                    }
                }
                else
                {
                    local3 = updateValueFactory(key, tValue);
                    if (TryUpdate(key, local3, tValue))
                    {
                        break;
                    }
                }
            }
            return local3;
        }

        [Conditional("DEBUG")]
        private void Assert(bool condition)
        {
        }

        public void Clear()
        {
            int toExclusive = 0;
            try
            {
                AcquireAllLocks(ref toExclusive);
                m_buckets = new ConcurrentDictionary<TKey, TValue>.Node[31];
                Array.Clear(m_countPerLock, 0, m_countPerLock.Length);
            }
            finally
            {
                ReleaseLocks(0, toExclusive);
            }
        }

        public bool ContainsKey(TKey key)
        {
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }
            TValue locksAcquired;
            return TryGetValue(key, out locksAcquired);
        }

        private void CopyToEntries(DictionaryEntry[] array, int index)
        {
            ConcurrentDictionary<TKey, TValue>.Node[] buckets = m_buckets;
            for (int local = 0; local < buckets.Length; local++)
            {
                for (ConcurrentDictionary<TKey, TValue>.Node node = buckets[local]; node != null; node = node.m_next)
                {
                    array[index] = new DictionaryEntry(node.m_key, node.m_value);
                    index++;
                }
            }
        }

        private void CopyToObjects(object[] array, int index)
        {
            ConcurrentDictionary<TKey, TValue>.Node[] buckets = m_buckets;
            for (int i = 0; i < buckets.Length; i++)
            {
                for (ConcurrentDictionary<TKey, TValue>.Node node = buckets[i]; node != null; node = node.m_next)
                {
                    array[index] = new KeyValuePair<TKey, TValue>(node.m_key, node.m_value);
                    index++;
                }
            }
        }

        private void CopyToPairs(KeyValuePair<TKey, TValue>[] array, int index)
        {
            ConcurrentDictionary<TKey, TValue>.Node[] buckets = m_buckets;
            for (int i = 0; i < buckets.Length; i++)
            {
                for (ConcurrentDictionary<TKey, TValue>.Node node = buckets[i]; node != null; node = node.m_next)
                {
                    array[index] = new KeyValuePair<TKey, TValue>(node.m_key, node.m_value);
                    index++;
                }
            }
        }

        private void GetBucketAndLockNo(int hashcode, out int bucketNo, out int lockNo, int bucketCount)
        {
            bucketNo = (hashcode & 2147483647) % bucketCount;
            lockNo = bucketNo % m_locks.Length;
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            try
            {
                ConcurrentDictionary<TKey, TValue>.Node[] buckets = m_buckets;
                for (int i = 0; i < buckets.Length; i++)
                {
                    ConcurrentDictionary<TKey, TValue>.Node node = buckets[i];
                    Thread.MemoryBarrier();
                    for (ConcurrentDictionary<TKey, TValue>.Node node2 = node; node2 != null; node2 = node2.m_next)
                    {
                        yield return new KeyValuePair<TKey, TValue>(node2.m_key, node2.m_value);
                    }
                }
            }
            finally
            {
            }
            yield break;
        }

        private ReadOnlyCollection<TKey> GetKeys()
        {
            int toExclusive = 0;
            ReadOnlyCollection<TKey> result;
            try
            {
                AcquireAllLocks(ref toExclusive);
                List<TKey> list = new List<TKey>();
                for (int i = 0; i < m_buckets.Length; i++)
                {
                    for (ConcurrentDictionary<TKey, TValue>.Node node = m_buckets[i]; node != null; node = node.m_next)
                    {
                        list.Add(node.m_key);
                    }
                }
                result = new ReadOnlyCollection<TKey>(list);
            }
            finally
            {
                ReleaseLocks(0, toExclusive);
            }
            return result;
        }

        public TValue GetOrAdd(TKey key, TValue value)
        {
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }
            TValue locksAcquired;
            TryAddInternal(key, value, false, true, out locksAcquired);
            return locksAcquired;
        }

        public TValue GetOrAdd(TKey key, Func<TKey, TValue> valueFactory)
        {
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }
            if (valueFactory == null)
            {
                throw new ArgumentNullException("valueFactory");
            }
            TValue result;
            if (!TryGetValue(key, out result))
            {
                TryAddInternal(key, valueFactory(key), false, true, out result);
            }
            return result;
        }

        private string GetResource(string key)
        {
            return Environment2.GetResourceString(key);
        }

        private ReadOnlyCollection<TValue> GetValues()
        {
            int toExclusive = 0;
            ReadOnlyCollection<TValue> result;
            try
            {
                AcquireAllLocks(ref toExclusive);
                List<TValue> list = new List<TValue>();
                for (int i = 0; i < m_buckets.Length; i++)
                {
                    for (ConcurrentDictionary<TKey, TValue>.Node node = m_buckets[i]; node != null; node = node.m_next)
                    {
                        list.Add(node.m_value);
                    }
                }
                result = new ReadOnlyCollection<TValue>(list);
            }
            finally
            {
                ReleaseLocks(0, toExclusive);
            }
            return result;
        }

        private void GrowTable(ConcurrentDictionary<TKey, TValue>.Node[] buckets)
        {
            int locksAcquired = 0;
            try
            {
                AcquireLocks(0, 1, ref locksAcquired);
                if (buckets == m_buckets)
                {
                    int list;
                    try
                    {
                        list = buckets.Length * 2 + 1;
                        while (list % 3 == 0 || list % 5 == 0 || list % 7 == 0)
                        {
                            list += 2;
                        }
                    }
                    catch (OverflowException)
                    {
                        return;
                    }
                    ConcurrentDictionary<TKey, TValue>.Node[] i = new ConcurrentDictionary<TKey, TValue>.Node[list];
                    int[] node = new int[m_locks.Length];
                    AcquireLocks(1, m_locks.Length, ref locksAcquired);
                    for (int j = 0; j < buckets.Length; j++)
                    {
                        ConcurrentDictionary<TKey, TValue>.Node next;
                        for (ConcurrentDictionary<TKey, TValue>.Node onlys = buckets[j]; onlys != null; onlys = next)
                        {
                            next = onlys.m_next;
                            int num;
                            int num2;
                            GetBucketAndLockNo(onlys.m_hashcode, out num, out num2, i.Length);
                            i[num] = new ConcurrentDictionary<TKey, TValue>.Node(onlys.m_key, onlys.m_value, onlys.m_hashcode, i[num]);
                            node[num2]++;
                        }
                    }
                    m_buckets = i;
                    m_countPerLock = node;
                }
            }
            finally
            {
                ReleaseLocks(0, locksAcquired);
            }
        }

        private void InitializeFromCollection(IEnumerable<KeyValuePair<TKey, TValue>> collection)
        {
            foreach (KeyValuePair<TKey, TValue> current in collection)
            {
                if (current.Key == null)
                {
                    throw new ArgumentNullException("key");
                }
                TValue num2;
                if (!TryAddInternal(current.Key, current.Value, false, false, out num2))
                {
                    throw new ArgumentException(GetResource("ConcurrentDictionary_SourceContainsDuplicateKeys"));
                }
            }
        }

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            KeyValuePair<TKey, TValue>[] serializationArray = m_serializationArray;
            m_buckets = new ConcurrentDictionary<TKey, TValue>.Node[m_serializationCapacity];
            m_countPerLock = new int[m_serializationConcurrencyLevel];
            m_locks = new object[m_serializationConcurrencyLevel];
            for (int pair = 0; pair < m_locks.Length; pair++)
            {
                m_locks[pair] = new object();
            }
            InitializeFromCollection(serializationArray);
            m_serializationArray = null;
        }

        [OnSerializing]
        private void OnSerializing(StreamingContext context)
        {
            m_serializationArray = ToArray();
            m_serializationConcurrencyLevel = m_locks.Length;
            m_serializationCapacity = m_buckets.Length;
        }

        private void ReleaseLocks(int fromInclusive, int toExclusive)
        {
            for (int i = fromInclusive; i < toExclusive; i++)
            {
                Monitor.Exit(m_locks[i]);
            }
        }

        void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> keyValuePair)
        {
            ((IDictionary<TKey, TValue>)this).Add(keyValuePair.Key, keyValuePair.Value);
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> keyValuePair)
        {
            TValue x;
            return TryGetValue(keyValuePair.Key, out x) && EqualityComparer<TValue>.Default.Equals(x, keyValuePair.Value);
        }

        void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int index)
        {
            if (array == null)
            {
                throw new ArgumentNullException("array");
            }
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException("index", GetResource("ConcurrentDictionary_IndexIsNegative"));
            }
            int local = 0;
            try
            {
                AcquireAllLocks(ref local);
                int num = 0;
                for (int i = 0; i < m_locks.Length; i++)
                {
                    num += m_countPerLock[i];
                }
                if (array.Length - num < index || num < 0)
                {
                    throw new ArgumentException(GetResource("ConcurrentDictionary_ArrayNotLargeEnough"));
                }
                CopyToPairs(array, index);
            }
            finally
            {
                ReleaseLocks(0, local);
            }
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> keyValuePair)
        {
            if (keyValuePair.Key == null)
            {
                throw new ArgumentNullException(GetResource("ConcurrentDictionary_ItemKeyIsNull"));
            }
            TValue tValue;
            return TryRemoveInternal(keyValuePair.Key, out tValue, true, keyValuePair.Value);
        }

        void IDictionary<TKey, TValue>.Add(TKey key, TValue value)
        {
            if (!TryAdd(key, value))
            {
                throw new ArgumentException(GetResource("ConcurrentDictionary_KeyAlreadyExisted"));
            }
        }

        bool IDictionary<TKey, TValue>.Remove(TKey key)
        {
            TValue tValue;
            return TryRemove(key, out tValue);
        }

        void ICollection.CopyTo(Array array, int index)
        {
            if (array == null)
            {
                throw new ArgumentNullException("array");
            }
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException("index", GetResource("ConcurrentDictionary_IndexIsNegative"));
            }
            int local = 0;
            try
            {
                AcquireAllLocks(ref local);
                int num = 0;
                for (int i = 0; i < m_locks.Length; i++)
                {
                    num += m_countPerLock[i];
                }
                if (array.Length - num < index || num < 0)
                {
                    throw new ArgumentException(GetResource("ConcurrentDictionary_ArrayNotLargeEnough"));
                }
                KeyValuePair<TKey, TValue>[] array2 = array as KeyValuePair<TKey, TValue>[];
                if (array2 != null)
                {
                    CopyToPairs(array2, index);
                }
                else
                {
                    DictionaryEntry[] array3 = array as DictionaryEntry[];
                    if (array3 != null)
                    {
                        CopyToEntries(array3, index);
                    }
                    else
                    {
                        object[] array4 = array as object[];
                        if (array4 == null)
                        {
                            throw new ArgumentException(GetResource("ConcurrentDictionary_ArrayIncorrectType"), "array");
                        }
                        CopyToObjects(array4, index);
                    }
                }
            }
            finally
            {
                ReleaseLocks(0, local);
            }
        }

        void IDictionary.Add(object key, object value)
        {
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }
            if (!(key is TKey))
            {
                throw new ArgumentException(GetResource("ConcurrentDictionary_TypeOfKeyIncorrect"));
            }
            TValue value2;
            try
            {
                value2 = (TValue)((object)value);
            }
            catch (InvalidCastException)
            {
                throw new ArgumentException(GetResource("ConcurrentDictionary_TypeOfValueIncorrect"));
            }
            ((IDictionary<TKey, TValue>)this).Add((TKey)((object)key), value2);
        }

        bool IDictionary.Contains(object key)
        {
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }
            return key is TKey && ContainsKey((TKey)((object)key));
        }

        IDictionaryEnumerator IDictionary.GetEnumerator()
        {
            return new ConcurrentDictionary<TKey, TValue>.DictionaryEnumerator(this);
        }

        void IDictionary.Remove(object key)
        {
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }
            if (key is TKey)
            {
                TValue tValue;
                TryRemove((TKey)((object)key), out tValue);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public KeyValuePair<TKey, TValue>[] ToArray()
        {
            int toExclusive = 0;
            KeyValuePair<TKey, TValue>[] result;
            try
            {
                AcquireAllLocks(ref toExclusive);
                int num = 0;
                for (int i = 0; i < m_locks.Length; i++)
                {
                    num += m_countPerLock[i];
                }
                KeyValuePair<TKey, TValue>[] array = new KeyValuePair<TKey, TValue>[num];
                CopyToPairs(array, 0);
                result = array;
            }
            finally
            {
                ReleaseLocks(0, toExclusive);
            }
            return result;
        }

        public bool TryAdd(TKey key, TValue value)
        {
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }
            TValue locksAcquired;
            return TryAddInternal(key, value, false, true, out locksAcquired);
        }

        private bool TryAddInternal(TKey key, TValue value, bool updateIfExists, bool acquireLock, out TValue resultingValue)
        {
            int hashCode = m_comparer.GetHashCode(key);
            ConcurrentDictionary<TKey, TValue>.Node[] local;
            bool flag;
            while (true)
            {
                local = m_buckets;
                int num;
                int num2;
                GetBucketAndLockNo(hashCode, out num, out num2, local.Length);
                flag = false;
                bool flag2 = false;
                try
                {
                    if (acquireLock)
                    {
                        Monitor2.Enter(m_locks[num2], ref flag2);
                    }
                    if (local != m_buckets)
                    {
                        continue;
                    }
                    ConcurrentDictionary<TKey, TValue>.Node node = null;
                    for (ConcurrentDictionary<TKey, TValue>.Node node2 = local[num]; node2 != null; node2 = node2.m_next)
                    {
                        if (m_comparer.Equals(node2.m_key, key))
                        {
                            if (updateIfExists)
                            {
                                ConcurrentDictionary<TKey, TValue>.Node node3 = new ConcurrentDictionary<TKey, TValue>.Node(node2.m_key, value, hashCode, node2.m_next);
                                if (node == null)
                                {
                                    local[num] = node3;
                                }
                                else
                                {
                                    node.m_next = node3;
                                }
                                resultingValue = value;
                            }
                            else
                            {
                                resultingValue = node2.m_value;
                            }
                            return false;
                        }
                        node = node2;
                    }
                    local[num] = new ConcurrentDictionary<TKey, TValue>.Node(key, value, hashCode, local[num]);
                    m_countPerLock[num2]++;
                    if (m_countPerLock[num2] > local.Length / m_locks.Length)
                    {
                        flag = true;
                    }
                }
                finally
                {
                    if (flag2)
                    {
                        Monitor.Exit(m_locks[num2]);
                    }
                }
                break;
            }
            if (flag)
            {
                GrowTable(local);
            }
            resultingValue = value;
            return true;
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }
            ConcurrentDictionary<TKey, TValue>.Node[] hashCode = m_buckets;
            int nodeArray;
            int num2;
            GetBucketAndLockNo(m_comparer.GetHashCode(key), out nodeArray, out num2, hashCode.Length);
            ConcurrentDictionary<TKey, TValue>.Node num3 = hashCode[nodeArray];
            Thread.MemoryBarrier();
            while (num3 != null)
            {
                if (m_comparer.Equals(num3.m_key, key))
                {
                    value = num3.m_value;
                    return true;
                }
                num3 = num3.m_next;
            }
            value = default(TValue);
            return false;
        }

        public bool TryRemove(TKey key, out TValue value)
        {
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }
            return TryRemoveInternal(key, out value, false, default(TValue));
        }

        private bool TryRemoveInternal(TKey key, out TValue value, bool matchValue, TValue oldValue)
        {
            while (true)
            {
                ConcurrentDictionary<TKey, TValue>.Node[] buckets = m_buckets;
                int num;
                int num2;
                GetBucketAndLockNo(m_comparer.GetHashCode(key), out num, out num2, buckets.Length);
                lock (m_locks[num2])
                {
                    if (buckets != m_buckets)
                    {
                        continue;
                    }
                    ConcurrentDictionary<TKey, TValue>.Node node = null;
                    ConcurrentDictionary<TKey, TValue>.Node node2 = m_buckets[num];
                    while (node2 != null)
                    {
                        if (m_comparer.Equals(node2.m_key, key))
                        {
                            bool result;
                            if (matchValue && !EqualityComparer<TValue>.Default.Equals(oldValue, node2.m_value))
                            {
                                value = default(TValue);
                                result = false;
                                return result;
                            }
                            if (node == null)
                            {
                                m_buckets[num] = node2.m_next;
                            }
                            else
                            {
                                node.m_next = node2.m_next;
                            }
                            value = node2.m_value;
                            m_countPerLock[num2]--;
                            result = true;
                            return result;
                        }
                        else
                        {
                            node = node2;
                            node2 = node2.m_next;
                        }
                    }
                }
                break;
            }
            value = default(TValue);
            return false;
        }

        public bool TryUpdate(TKey key, TValue newValue, TValue comparisonValue)
        {
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }
            int nodeArray = m_comparer.GetHashCode(key);
            IEqualityComparer<TValue> num = EqualityComparer<TValue>.Default;
            bool result;
            while (true)
            {
                ConcurrentDictionary<TKey, TValue>.Node[] num2 = m_buckets;
                int num3;
                int num4;
                GetBucketAndLockNo(nodeArray, out num3, out num4, num2.Length);
                lock (m_locks[num4])
                {
                    if (num2 != m_buckets)
                    {
                        continue;
                    }
                    ConcurrentDictionary<TKey, TValue>.Node node2 = null;
                    ConcurrentDictionary<TKey, TValue>.Node node3 = num2[num3];
                    while (node3 != null)
                    {
                        if (m_comparer.Equals(node3.m_key, key))
                        {
                            if (!num.Equals(node3.m_value, comparisonValue))
                            {
                                result = false;
                                return result;
                            }
                            ConcurrentDictionary<TKey, TValue>.Node node4 = new ConcurrentDictionary<TKey, TValue>.Node(node3.m_key, newValue, nodeArray, node3.m_next);
                            if (node2 == null)
                            {
                                num2[num3] = node4;
                            }
                            else
                            {
                                node2.m_next = node4;
                            }
                            result = true;
                            return result;
                        }
                        else
                        {
                            node2 = node3;
                            node3 = node3.m_next;
                        }
                    }
                    result = false;
                }
                break;
            }
            return result;
        }

        public int Count
        {
            get
            {
                int num = 0;
                int hashCode = 0;
                try
                {
                    AcquireAllLocks(ref hashCode);
                    for (int comparer = 0; comparer < m_countPerLock.Length; comparer++)
                    {
                        num += m_countPerLock[comparer];
                    }
                }
                finally
                {
                    ReleaseLocks(0, hashCode);
                }
                return num;
            }
        }

        private static int DefaultConcurrencyLevel
        {
            get
            {
                return 4 * Environment.ProcessorCount;
            }
        }

        public bool IsEmpty
        {
            get
            {
                int toExclusive = 0;
                try
                {
                    AcquireAllLocks(ref toExclusive);
                    for (int i = 0; i < m_countPerLock.Length; i++)
                    {
                        if (m_countPerLock[i] != 0)
                        {
                            return false;
                        }
                    }
                }
                finally
                {
                    ReleaseLocks(0, toExclusive);
                }
                return true;
            }
        }

        public TValue this[TKey key]
        {
            get
            {
                TValue locksAcquired;
                if (!TryGetValue(key, out locksAcquired))
                {
                    throw new KeyNotFoundException();
                }
                return locksAcquired;
            }
            set
            {
                if (key == null)
                {
                    throw new ArgumentNullException("key");
                }
                TValue local;
                TryAddInternal(key, value, true, true, out local);
            }
        }

        public ICollection<TKey> Keys
        {
            get
            {
                return GetKeys();
            }
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly
        {
            get
            {
                return false;
            }
        }

        bool ICollection.IsSynchronized
        {
            get
            {
                return false;
            }
        }

        object ICollection.SyncRoot
        {
            get
            {
                throw new NotSupportedException(Environment2.GetResourceString("ConcurrentCollection_SyncRoot_NotSupported"));
            }
        }

        bool IDictionary.IsFixedSize
        {
            get
            {
                return false;
            }
        }

        bool IDictionary.IsReadOnly
        {
            get
            {
                return false;
            }
        }

        object IDictionary.this[object key]
        {
            get
            {
                if (key == null)
                {
                    throw new ArgumentNullException("key");
                }
                TValue tValue;
                if (key is TKey && TryGetValue((TKey)((object)key), out tValue))
                {
                    return tValue;
                }
                return null;
            }
            set
            {
                if (key == null)
                {
                    throw new ArgumentNullException("key");
                }
                if (!(key is TKey))
                {
                    throw new ArgumentException(GetResource("ConcurrentDictionary_TypeOfKeyIncorrect"));
                }
                if (!(value is TValue))
                {
                    throw new ArgumentException(GetResource("ConcurrentDictionary_TypeOfValueIncorrect"));
                }
                this[(TKey)((object)key)] = (TValue)((object)value);
            }
        }

        ICollection IDictionary.Keys
        {
            get
            {
                return GetKeys();
            }
        }

        ICollection IDictionary.Values
        {
            get
            {
                return GetValues();
            }
        }

        public ICollection<TValue> Values
        {
            get
            {
                return GetValues();
            }
        }

        private const int DEFAULT_CAPACITY = 31;

        private const int DEFAULT_CONCURRENCY_MULTIPLIER = 4;

        [NonSerialized]
        private volatile ConcurrentDictionary<TKey, TValue>.Node[] m_buckets;

        private IEqualityComparer<TKey> m_comparer;

        [NonSerialized]
        private volatile int[] m_countPerLock;

        [NonSerialized]
        private object[] m_locks;

        private KeyValuePair<TKey, TValue>[] m_serializationArray;

        private int m_serializationCapacity;

        private int m_serializationConcurrencyLevel;

        private class DictionaryEnumerator : IDictionaryEnumerator, IEnumerator
        {
            internal DictionaryEnumerator(ConcurrentDictionary<TKey, TValue> dictionary)
            {
                m_enumerator = dictionary.GetEnumerator();
            }

            public bool MoveNext()
            {
                return m_enumerator.MoveNext();
            }

            public void Reset()
            {
                m_enumerator.Reset();
            }

            public object Current
            {
                get
                {
                    return Entry;
                }
            }

            public DictionaryEntry Entry
            {
                get
                {
                    KeyValuePair<TKey, TValue> current = m_enumerator.Current;
                    object m_entry = current.Key;
                    KeyValuePair<TKey, TValue> current2 = m_enumerator.Current;
                    return new DictionaryEntry(m_entry, current2.Value);
                }
            }

            public object Key
            {
                get
                {
                    KeyValuePair<TKey, TValue> current = m_enumerator.Current;
                    return current.Key;
                }
            }

            public object Value
            {
                get
                {
                    KeyValuePair<TKey, TValue> current = m_enumerator.Current;
                    return current.Value;
                }
            }

            private IEnumerator<KeyValuePair<TKey, TValue>> m_enumerator;
        }

        private class Node
        {
            internal Node(TKey key, TValue value, int hashcode) : this(key, value, hashcode, null)
            {
            }

            internal Node(TKey key, TValue value, int hashcode, ConcurrentDictionary<TKey, TValue>.Node next)
            {
                m_key = key;
                m_value = value;
                m_next = next;
                m_hashcode = hashcode;
            }

            internal int m_hashcode;

            internal TKey m_key;

            internal volatile ConcurrentDictionary<TKey, TValue>.Node m_next;

            internal TValue m_value;
        }
    }
}
