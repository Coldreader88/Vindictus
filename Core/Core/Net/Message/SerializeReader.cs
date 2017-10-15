using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;

namespace Devcat.Core.Net.Message
{
	public struct SerializeReader
	{
		private SerializeReader(Packet packet)
		{
			this.data = packet;
			this.position = packet.Offset + packet.BodyOffset;
		}

		public static void FromBinary<T>(Packet packet, out T value)
		{
			int categoryId = ClassInfo<T>.CategoryId;
			SerializeReader serializeReader = new SerializeReader(packet);
			if (packet.CategoryId == 0)
			{
				object obj;
				SerializeReaderHelper<object, SerializeReader>.Deserialize(ref serializeReader, out obj);
				value = (T)((object)obj);
				return;
			}
			if (packet.CategoryId != categoryId && typeof(T).IsSealed)
			{
				throw new SerializationException(string.Format("Unexpected category {0:X8} for type {1}", packet.CategoryId, typeof(T).AssemblyQualifiedName));
			}
			SerializeReaderHelper<T, SerializeReader>.Deserialize(ref serializeReader, out value);
		}

		private void Read(out sbyte value)
		{
			value = (sbyte)this.data.Array[this.position++];
		}

		private void Read(out byte value)
		{
			value = this.data.Array[this.position++];
		}

		private void Read(out short value)
		{
			value = IPAddress.NetworkToHostOrder(BitConverter.ToInt16(this.data.Array, this.position));
			this.position += 2;
		}

		private void Read(out ushort value)
		{
			value = (ushort)IPAddress.NetworkToHostOrder(BitConverter.ToInt16(this.data.Array, this.position));
			this.position += 2;
		}

		private void Read(out int value)
		{
			value = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(this.data.Array, this.position));
			this.position += 4;
		}

		private void Read(out uint value)
		{
			value = (uint)IPAddress.NetworkToHostOrder(BitConverter.ToInt32(this.data.Array, this.position));
			this.position += 4;
		}

		private void Read(out long value)
		{
			value = IPAddress.NetworkToHostOrder(BitConverter.ToInt64(this.data.Array, this.position));
			this.position += 8;
		}

		private void Read(out ulong value)
		{
			value = (ulong)IPAddress.NetworkToHostOrder(BitConverter.ToInt64(this.data.Array, this.position));
			this.position += 8;
		}

		private void Read(out char value)
		{
			value = (char)IPAddress.NetworkToHostOrder(BitConverter.ToInt16(this.data.Array, this.position));
			this.position += 2;
		}

		private unsafe void Read(out float value)
		{
			int num = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(this.data.Array, this.position));
			value = *(float*)(&num);
			this.position += 4;
		}

		private unsafe void Read(out double value)
		{
			long num = IPAddress.NetworkToHostOrder(BitConverter.ToInt64(this.data.Array, this.position));
			value = *(double*)(&num);
			this.position += 8;
		}

		private void Read(out bool value)
		{
			value = (this.data.Array[this.position] != 0);
			this.position++;
		}

		private void ReadCount(out int value)
		{
			byte b;
			this.Read(out b);
			if ((b & 128) == 0)
			{
				value = (int)b;
				return;
			}
			if ((b & 224) == 192)
			{
				value = ((int)b << 6 & 1984);
			}
			else
			{
				if ((b & 240) == 224)
				{
					value = ((int)b << 12 & 61440);
				}
				else
				{
					if ((b & 248) == 240)
					{
						value = ((int)b << 18 & 1835008);
					}
					else
					{
						if ((b & 252) == 248)
						{
							value = ((int)b << 24 & 50331648);
						}
						else
						{
							if ((b & 252) != 252)
							{
								throw new SerializationException(string.Format("Invalid leading 6-bits '{0:X2}'", (int)(b & 252)));
							}
							value = ((int)b << 30 & -1073741824);
							this.Read(out b);
							if ((b & 192) != 128)
							{
								throw new SerializationException("0x10wwwwww");
							}
							value |= ((int)b << 24 & 1056964608);
							if (value <= 67108863)
							{
								throw new SerializationException("0x111111ww");
							}
						}
						this.Read(out b);
						if ((b & 192) != 128)
						{
							throw new SerializationException("0x10zzzzzz");
						}
						value |= ((int)b << 18 & 16515072);
						if (value <= 2097151)
						{
							throw new SerializationException("0x111110ww");
						}
					}
					this.Read(out b);
					if ((b & 192) != 128)
					{
						throw new SerializationException("0x10zzyyyy");
					}
					value |= ((int)b << 12 & 258048);
					if (value <= 65535)
					{
						throw new SerializationException("0x11110zzz");
					}
				}
				this.Read(out b);
				if ((b & 192) != 128)
				{
					throw new SerializationException("0x10yyyyxx");
				}
				value |= ((int)b << 6 & 4032);
				if (value <= 2047)
				{
					throw new SerializationException("0x1110yyyy");
				}
			}
			this.Read(out b);
			if ((b & 192) != 128)
			{
				throw new SerializationException("0x10xxxxxx");
			}
			value |= (int)(b & 63);
			if (value <= 127)
			{
				throw new SerializationException("0x110yyyxx");
			}
		}

		private void Read(out IntPtr value)
		{
			long num;
			this.Read(out num);
			value = new IntPtr(num);
		}

		private void Read(out string value)
		{
			int num;
			this.ReadCount(out num);
			if (num == 0)
			{
				value = null;
				return;
			}
			value = Encoding.UTF8.GetString(this.data.Array, this.position, --num);
			this.position += num;
		}

		private void Read(out Type value)
		{
			Guid guid;
			SerializeReaderHelper<Guid, SerializeReader>.Deserialize(ref this, out guid);
			if (guid == Guid.Empty)
			{
				value = null;
				return;
			}
			if (guid == SerializeReader.arrayGuid)
			{
				Type type;
				this.Read(out type);
				int num;
				this.ReadCount(out num);
				value = ((num == 0) ? type.MakeArrayType() : type.MakeArrayType(num));
				return;
			}
			if (SerializeReader.dictionary == null)
			{
				SerializeReader.dictionary = new Dictionary<Guid, Type>();
				foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
				{
					try
					{
						foreach (Type type2 in assembly.GetTypes())
						{
							if ((!(type2.Namespace != "System") || !(type2.Namespace != "System.Collections.Generic")) && type2.IsSerializable && !type2.IsDefined(typeof(ObsoleteAttribute), false) && !type2.IsInterface && !type2.IsAbstract && !type2.IsArray && type2.IsVisible)
							{
								SerializeReader.dictionary.Add(type2.GUID, type2);
							}
						}
					}
					catch (ReflectionTypeLoadException)
					{
					}
				}
			}
			if (!SerializeReader.dictionary.TryGetValue(guid, out value))
			{
				foreach (Assembly assembly2 in AppDomain.CurrentDomain.GetAssemblies())
				{
					try
					{
						foreach (Type type3 in assembly2.GetTypes())
						{
							if (!(type3.GUID != guid) && type3.IsSerializable && !type3.IsDefined(typeof(ObsoleteAttribute), false) && (type3.IsVisible || !type3.FullName.StartsWith("System.")))
							{
								value = type3;
								break;
							}
						}
						if (!(value == null))
						{
							break;
						}
					}
					catch (ReflectionTypeLoadException)
					{
					}
				}
				if (value == null)
				{
					value = new SerializeReader.UnknownType(guid);
					return;
				}
				foreach (Assembly assembly3 in AppDomain.CurrentDomain.GetAssemblies())
				{
					try
					{
						foreach (Type type4 in assembly3.GetTypes())
						{
							if (!(type4.Namespace != value.Namespace) && type4.IsSerializable && !type4.IsDefined(typeof(ObsoleteAttribute), false) && !type4.IsInterface && !type4.IsAbstract && !type4.IsArray && type4.IsVisible)
							{
								if (SerializeReader.dictionary.ContainsKey(type4.GUID))
								{
									SerializeReader.dictionary[type4.GUID] = null;
								}
								else
								{
									SerializeReader.dictionary.Add(type4.GUID, type4);
								}
							}
						}
					}
					catch (ReflectionTypeLoadException)
					{
					}
				}
			}
			if (value == null)
			{
				value = new SerializeReader.UnknownType(guid);
				return;
			}
			if (value.IsGenericTypeDefinition)
			{
				Type[] array = new Type[value.GetGenericArguments().Length];
				for (int num2 = 0; num2 < array.Length; num2++)
				{
					this.Read(out array[num2]);
				}
				value = value.MakeGenericType(array);
			}
		}

		private void Read(out sbyte[] value)
		{
			int num;
			this.ReadCount(out num);
			if (num == 0)
			{
				value = null;
				return;
			}
			value = new sbyte[--num];
			Buffer.BlockCopy(this.data.Array, this.position, value, 0, num);
			this.position += num;
		}

		private void Read(out byte[] value)
		{
			int num;
			this.ReadCount(out num);
			if (num == 0)
			{
				value = null;
				return;
			}
			value = new byte[--num];
			Buffer.BlockCopy(this.data.Array, this.position, value, 0, num);
			this.position += num;
		}

		private void Read(out short[] value)
		{
			int num;
			this.ReadCount(out num);
			if (num == 0)
			{
				value = null;
				return;
			}
			value = new short[--num];
			for (int i = 0; i < num; i++)
			{
				this.Read(out value[i]);
			}
		}

		private void Read(out ushort[] value)
		{
			int num;
			this.ReadCount(out num);
			if (num == 0)
			{
				value = null;
				return;
			}
			value = new ushort[--num];
			for (int i = 0; i < num; i++)
			{
				this.Read(out value[i]);
			}
		}

		private void Read(out int[] value)
		{
			int num;
			this.ReadCount(out num);
			if (num == 0)
			{
				value = null;
				return;
			}
			value = new int[--num];
			for (int i = 0; i < num; i++)
			{
				this.Read(out value[i]);
			}
		}

		private void Read(out uint[] value)
		{
			int num;
			this.ReadCount(out num);
			if (num == 0)
			{
				value = null;
				return;
			}
			value = new uint[--num];
			for (int i = 0; i < num; i++)
			{
				this.Read(out value[i]);
			}
		}

		private void Read(out long[] value)
		{
			int num;
			this.ReadCount(out num);
			if (num == 0)
			{
				value = null;
				return;
			}
			value = new long[--num];
			for (int i = 0; i < num; i++)
			{
				this.Read(out value[i]);
			}
		}

		private void Read(out ulong[] value)
		{
			int num;
			this.ReadCount(out num);
			if (num == 0)
			{
				value = null;
				return;
			}
			value = new ulong[--num];
			for (int i = 0; i < num; i++)
			{
				this.Read(out value[i]);
			}
		}

		private void Read(out char[] value)
		{
			int num;
			this.ReadCount(out num);
			if (num == 0)
			{
				value = null;
				return;
			}
			value = new char[--num];
			for (int i = 0; i < num; i++)
			{
				this.Read(out value[i]);
			}
		}

		private void Read(out float[] value)
		{
			int num;
			this.ReadCount(out num);
			if (num == 0)
			{
				value = null;
				return;
			}
			value = new float[--num];
			for (int i = 0; i < num; i++)
			{
				this.Read(out value[i]);
			}
		}

		private void Read(out double[] value)
		{
			int num;
			this.ReadCount(out num);
			if (num == 0)
			{
				value = null;
				return;
			}
			value = new double[--num];
			for (int i = 0; i < num; i++)
			{
				this.Read(out value[i]);
			}
		}

		private void Read(out bool[] value)
		{
			int num;
			this.ReadCount(out num);
			if (num == 0)
			{
				value = null;
				return;
			}
			value = new bool[--num];
			for (int i = 0; i < num; i++)
			{
				this.Read(out value[i]);
			}
		}

		private void Read(out string[] value)
		{
			int num;
			this.ReadCount(out num);
			if (num == 0)
			{
				value = null;
				return;
			}
			value = new string[--num];
			for (int i = 0; i < num; i++)
			{
				this.Read(out value[i]);
			}
		}

		private void Read(out Type[] value)
		{
			int num;
			this.ReadCount(out num);
			if (num == 0)
			{
				value = null;
				return;
			}
			value = new Type[--num];
			for (int i = 0; i < num; i++)
			{
				this.Read(out value[i]);
			}
		}

		private void Read<T>(out T[] value)
		{
			int num;
			this.ReadCount(out num);
			if (num == 0)
			{
				value = null;
				return;
			}
			value = new T[--num];
			for (int i = 0; i < num; i++)
			{
				SerializeReaderHelper<T, SerializeReader>.Deserialize(ref this, out value[i]);
			}
		}

		private void Read(out sbyte? value)
		{
			bool flag;
			this.Read(out flag);
			if (!flag)
			{
				value = null;
				return;
			}
			sbyte value2;
			this.Read(out value2);
			value = new sbyte?(value2);
		}

		private void Read(out byte? value)
		{
			bool flag;
			this.Read(out flag);
			if (!flag)
			{
				value = null;
				return;
			}
			byte value2;
			this.Read(out value2);
			value = new byte?(value2);
		}

		private void Read(out short? value)
		{
			bool flag;
			this.Read(out flag);
			if (!flag)
			{
				value = null;
				return;
			}
			short value2;
			this.Read(out value2);
			value = new short?(value2);
		}

		private void Read(out ushort? value)
		{
			bool flag;
			this.Read(out flag);
			if (!flag)
			{
				value = null;
				return;
			}
			ushort value2;
			this.Read(out value2);
			value = new ushort?(value2);
		}

		private void Read(out int? value)
		{
			bool flag;
			this.Read(out flag);
			if (!flag)
			{
				value = null;
				return;
			}
			int value2;
			this.Read(out value2);
			value = new int?(value2);
		}

		private void Read(out uint? value)
		{
			bool flag;
			this.Read(out flag);
			if (!flag)
			{
				value = null;
				return;
			}
			uint value2;
			this.Read(out value2);
			value = new uint?(value2);
		}

		private void Read(out long? value)
		{
			bool flag;
			this.Read(out flag);
			if (!flag)
			{
				value = null;
				return;
			}
			long value2;
			this.Read(out value2);
			value = new long?(value2);
		}

		private void Read(out ulong? value)
		{
			bool flag;
			this.Read(out flag);
			if (!flag)
			{
				value = null;
				return;
			}
			ulong value2;
			this.Read(out value2);
			value = new ulong?(value2);
		}

		private void Read(out char? value)
		{
			bool flag;
			this.Read(out flag);
			if (!flag)
			{
				value = null;
				return;
			}
			char value2;
			this.Read(out value2);
			value = new char?(value2);
		}

		private void Read(out float? value)
		{
			bool flag;
			this.Read(out flag);
			if (!flag)
			{
				value = null;
				return;
			}
			float value2;
			this.Read(out value2);
			value = new float?(value2);
		}

		private void Read(out double? value)
		{
			bool flag;
			this.Read(out flag);
			if (!flag)
			{
				value = null;
				return;
			}
			double value2;
			this.Read(out value2);
			value = new double?(value2);
		}

		private void Read(out bool? value)
		{
			bool flag;
			this.Read(out flag);
			if (!flag)
			{
				value = null;
				return;
			}
			bool value2;
			this.Read(out value2);
			value = new bool?(value2);
		}

		private void Read<T>(out T? value) where T : struct
		{
			bool flag;
			this.Read(out flag);
			if (!flag)
			{
				value = null;
				return;
			}
			T value2;
			SerializeReaderHelper<T, SerializeReader>.Deserialize(ref this, out value2);
			value = new T?(value2);
		}

		private void Read(out ArraySegment<byte> value)
		{
			int num;
			this.ReadCount(out num);
			if (num == 0)
			{
				value = default(ArraySegment<byte>);
				return;
			}
			value = new ArraySegment<byte>(this.data.Array, this.position, --num);
			this.position += num;
		}

		private void Read<T>(out ArraySegment<T> value)
		{
			T[] array;
			this.Read<T>(out array);
			if (array == null)
			{
				value = default(ArraySegment<T>);
				return;
			}
			value = new ArraySegment<T>(array);
		}

		private void Read(out ICollection values)
		{
			object[] array;
			this.Read<object>(out array);
			values = array;
		}

		private void Read(out IDictionary values)
		{
			int num;
			this.ReadCount(out num);
			if (num == 0)
			{
				values = null;
				return;
			}
			values = new Hashtable(--num);
			for (int i = 0; i < num; i++)
			{
				DictionaryEntry dictionaryEntry;
				SerializeReaderHelper<DictionaryEntry, SerializeReader>.Deserialize(ref this, out dictionaryEntry);
				values.Add(dictionaryEntry.Key, dictionaryEntry.Value);
			}
		}

		private void Read<T>(out ICollection<T> values)
		{
			T[] array;
			this.Read<T>(out array);
			values = array;
		}

		private void Read<T>(out IList<T> values)
		{
			T[] array;
			this.Read<T>(out array);
			values = array;
		}

		private void Read<TKey, TValue>(out IDictionary<TKey, TValue> values)
		{
			int num;
			this.ReadCount(out num);
			if (num == 0)
			{
				values = null;
				return;
			}
			num--;
			values = new Dictionary<TKey, TValue>(num);
			for (int i = 0; i < num; i++)
			{
				KeyValuePair<TKey, TValue> item;
				SerializeReaderHelper<KeyValuePair<TKey, TValue>, SerializeReader>.Deserialize(ref this, out item);
				values.Add(item);
			}
		}

		private void Read<T, TCollection>(out TCollection values) where TCollection : ICollection<T>, new()
		{
			int num;
			this.ReadCount(out num);
			if (num == 0)
			{
				values = default(TCollection);
				return;
			}
			num--;
			values = ((default(TCollection) == null) ? Activator.CreateInstance<TCollection>() : default(TCollection));
			for (int i = 0; i < num; i++)
			{
				T item;
				SerializeReaderHelper<T, SerializeReader>.Deserialize(ref this, out item);
				values.Add(item);
			}
		}

		private Packet data;

		private int position;

		private static readonly Guid arrayGuid = typeof(Array).GUID;

		[ThreadStatic]
		private static IDictionary<Guid, Type> dictionary;

		internal sealed class UnknownType : Type
		{
			public UnknownType(Guid guid)
			{
				this.guid = guid;
			}

			public override Assembly Assembly
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			public override string AssemblyQualifiedName
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			public override Type BaseType
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			public override string FullName
			{
				get
				{
					return string.Format("{{{0}}}", this.guid.ToString());
				}
			}

			public override Guid GUID
			{
				get
				{
					return this.guid;
				}
			}

			protected override TypeAttributes GetAttributeFlagsImpl()
			{
				throw new NotImplementedException();
			}

			protected override ConstructorInfo GetConstructorImpl(BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
			{
				throw new NotImplementedException();
			}

			public override ConstructorInfo[] GetConstructors(BindingFlags bindingAttr)
			{
				throw new NotImplementedException();
			}

			public override Type GetElementType()
			{
				throw new NotImplementedException();
			}

			public override EventInfo GetEvent(string name, BindingFlags bindingAttr)
			{
				throw new NotImplementedException();
			}

			public override EventInfo[] GetEvents(BindingFlags bindingAttr)
			{
				throw new NotImplementedException();
			}

			public override FieldInfo GetField(string name, BindingFlags bindingAttr)
			{
				throw new NotImplementedException();
			}

			public override FieldInfo[] GetFields(BindingFlags bindingAttr)
			{
				throw new NotImplementedException();
			}

			public override Type GetInterface(string name, bool ignoreCase)
			{
				throw new NotImplementedException();
			}

			public override Type[] GetInterfaces()
			{
				throw new NotImplementedException();
			}

			public override MemberInfo[] GetMembers(BindingFlags bindingAttr)
			{
				throw new NotImplementedException();
			}

			protected override MethodInfo GetMethodImpl(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
			{
				throw new NotImplementedException();
			}

			public override MethodInfo[] GetMethods(BindingFlags bindingAttr)
			{
				throw new NotImplementedException();
			}

			public override Type GetNestedType(string name, BindingFlags bindingAttr)
			{
				throw new NotImplementedException();
			}

			public override Type[] GetNestedTypes(BindingFlags bindingAttr)
			{
				throw new NotImplementedException();
			}

			public override PropertyInfo[] GetProperties(BindingFlags bindingAttr)
			{
				throw new NotImplementedException();
			}

			protected override PropertyInfo GetPropertyImpl(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers)
			{
				throw new NotImplementedException();
			}

			protected override bool HasElementTypeImpl()
			{
				return false;
			}

			public override object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters)
			{
				throw new NotImplementedException();
			}

			protected override bool IsArrayImpl()
			{
				return false;
			}

			protected override bool IsByRefImpl()
			{
				return false;
			}

			protected override bool IsCOMObjectImpl()
			{
				return false;
			}

			protected override bool IsPointerImpl()
			{
				return false;
			}

			protected override bool IsPrimitiveImpl()
			{
				return false;
			}

			public override Module Module
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			public override string Namespace
			{
				get
				{
					return "{UnknownNamespace}";
				}
			}

			public override Type UnderlyingSystemType
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			public override object[] GetCustomAttributes(Type attributeType, bool inherit)
			{
				throw new NotImplementedException();
			}

			public override object[] GetCustomAttributes(bool inherit)
			{
				throw new NotImplementedException();
			}

			public override bool IsDefined(Type attributeType, bool inherit)
			{
				return false;
			}

			public override string Name
			{
				get
				{
					return "{UnknownType}";
				}
			}

			private Guid guid;
		}
	}
}
