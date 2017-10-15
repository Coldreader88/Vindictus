using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;

namespace Devcat.Core.Net.Message
{
	public class MessageHandlerFactory
	{
		public event EventHandler<EventArgs<object>> Deserialized;

		private static bool Validate(Type type)
		{
			if (type == null)
			{
				return false;
			}
			if (type.IsArray)
			{
				MessageHandlerFactory.Validate(type.GetElementType());
			}
			else if (type.IsGenericType)
			{
				Type[] genericArguments = type.GetGenericArguments();
				int i = 0;
				int num = genericArguments.Length;
				while (i < num)
				{
					MessageHandlerFactory.Validate(genericArguments[i]);
					i++;
				}
			}
			else if (type.GetType() == typeof(SerializeReader.UnknownType))
			{
				return false;
			}
			return true;
		}

		private void ProcessTypeConverter(object message, object tag)
		{
			MessageHandlerFactory.TypeConverter typeConverter = (MessageHandlerFactory.TypeConverter)message;
			if (typeConverter.Dictionary == null)
			{
				throw new ArgumentException("Dictionary is null.", "converter");
			}
			foreach (KeyValuePair<int, Type> keyValuePair in typeConverter.Dictionary)
			{
				int key = keyValuePair.Key;
				Type value = keyValuePair.Value;
				if (MessageHandlerFactory.Validate(value))
				{
					typeof(ClassInfo<>).MakeGenericType(new Type[]
					{
						value
					}).GetProperty("CategoryId").SetValue(null, key, null);
					MessageHandlerFactory.Category category;
					MessageHandlerFactory.Handler handler;
					if (this.categoryDict.TryGetValue(key, out category))
					{
						if (!(category.type == value))
						{
							throw new InvalidOperationException(string.Format("CategoryId '{0}' is already used.", key));
						}
					}
					else if (this.handlerDict.TryGetValue(value, out handler))
					{
						if (!value.IsSealed || handler.packetHandler == null)
						{
							throw new InvalidOperationException(string.Format("'{0}' is not sealed.", value.AssemblyQualifiedName));
						}
						this.categoryDict.Add(key, new MessageHandlerFactory.Category
						{
							type = value,
							packetHandler = handler.packetHandler
						});
					}
				}
			}
		}

		public MessageHandlerFactory() : this(false)
		{
		}

		public MessageHandlerFactory(bool external)
		{
			MessageHandlerFactory.Category value = default(MessageHandlerFactory.Category);
			value.type = typeof(object);
			value.packetHandler = delegate(Packet packet, object tag)
			{
				object obj;
				SerializeReader.FromBinary<object>(packet, out obj);
				if (this.Deserialized != null)
				{
					this.Deserialized(this, new EventArgs<object>(obj));
				}
				Type type = obj.GetType();
				while (type != null)
				{
					MessageHandlerFactory.Handler handler;
					if (this.handlerDict.TryGetValue(type, out handler))
					{
						handler.objectHandler(obj, tag);
						return;
					}
					Type[] interfaces = type.GetInterfaces();
					for (int i = 0; i < interfaces.Length; i++)
					{
						if (this.handlerDict.TryGetValue(interfaces[i], out handler))
						{
							handler.objectHandler(obj, tag);
							return;
						}
					}
					type = type.BaseType;
				}
				throw new InvalidOperationException(string.Format("'{0}' does not have registered handler.", obj.GetType().AssemblyQualifiedName));
			};
			this.categoryDict.Add(0, value);
			if (!external)
			{
				this.handlerDict.Add(typeof(MessageHandlerFactory.TypeConverter), new MessageHandlerFactory.Handler
				{
					objectHandler = new Action<object, object>(this.ProcessTypeConverter)
				});
			}
		}

		public void Register<T>(Action<T, object> handler)
		{
			this.Register<T>(handler, 0);
		}

		public void Register<T>(Action<T, object> handler, int categoryId)
		{
			if (!typeof(T).IsSerializable)
			{
				throw new InvalidOperationException(string.Format("'{0}' is not serializable.", typeof(T).AssemblyQualifiedName));
			}
			if (handler == null)
			{
				throw new ArgumentNullException("handler");
			}
			MethodInfo method = handler.Method;
			if (!method.IsStatic)
			{
				throw new ArgumentException("Delegate has instance method.", "handler");
			}
			DynamicMethod dynamicMethod = new DynamicMethod(string.Format("{0}.{1}.ProcessMessage[{2}.{3}]", new object[]
			{
				typeof(MessageHandlerFactory).Namespace,
				typeof(MessageHandlerFactory).Name,
				typeof(T).Namespace,
				typeof(T).Name
			}), null, new Type[]
			{
				typeof(object),
				typeof(object)
			}, typeof(MessageHandlerFactory), true);
			dynamicMethod.DefineParameter(1, ParameterAttributes.In, "message");
			dynamicMethod.DefineParameter(2, ParameterAttributes.In, "tag");
			ILGenerator ilgenerator = dynamicMethod.GetILGenerator();
			ilgenerator.Emit(OpCodes.Ldarg_0);
			ilgenerator.Emit(OpCodes.Unbox_Any, method.GetParameters()[0].ParameterType);
			ilgenerator.Emit(OpCodes.Ldarg_1);
			ilgenerator.EmitCall(OpCodes.Call, method, null);
			ilgenerator.Emit(OpCodes.Ret);
			MessageHandlerFactory.Handler value = new MessageHandlerFactory.Handler
			{
				objectHandler = (dynamicMethod.CreateDelegate(typeof(Action<object, object>)) as Action<object, object>)
			};
			if (typeof(T).IsSealed)
			{
				value.packetHandler = delegate(Packet packet, object tag)
				{
					T t;
					SerializeReader.FromBinary<T>(packet, out t);
					if (this.Deserialized != null)
					{
						this.Deserialized(this, new EventArgs<object>(t));
					}
					handler(t, tag);
				};
			}
			this.handlerDict.Add(typeof(T), value);
			if (categoryId != 0)
			{
				if (!typeof(T).IsSealed || value.packetHandler == null)
				{
					throw new InvalidOperationException(string.Format("'{0}' is not sealed.", typeof(T).AssemblyQualifiedName));
				}
				this.categoryDict.Add(categoryId, new MessageHandlerFactory.Category
				{
					type = typeof(T),
					packetHandler = value.packetHandler
				});
				ClassInfo<T>.CategoryId = categoryId;
			}
		}

		private void Register<H, T>(MethodInfo methodInfo, int categoryId)
		{
			if (!typeof(T).IsSerializable)
			{
				throw new InvalidOperationException(string.Format("'{0}' is not serializable.", typeof(T).AssemblyQualifiedName));
			}
			if (methodInfo == null)
			{
				throw new ArgumentNullException("methodInfo");
			}
			DynamicMethod dynamicMethod = new DynamicMethod(string.Format("{0}.{1}.ProcessMessage[{2}.{3}]", new object[]
			{
				typeof(MessageHandlerFactory).Namespace,
				typeof(MessageHandlerFactory).Name,
				typeof(T).Namespace,
				typeof(T).Name
			}), null, new Type[]
			{
				typeof(object),
				typeof(object)
			}, typeof(MessageHandlerFactory), true);
			dynamicMethod.DefineParameter(1, ParameterAttributes.In, "message");
			dynamicMethod.DefineParameter(2, ParameterAttributes.In, "tag");
			ILGenerator ilgenerator = dynamicMethod.GetILGenerator();
			if (methodInfo.IsStatic)
			{
				ilgenerator.Emit(OpCodes.Ldarg_0);
				ilgenerator.Emit(OpCodes.Ldarg_1);
			}
			else
			{
				ilgenerator.Emit(OpCodes.Ldarg_1);
				ilgenerator.Emit(OpCodes.Unbox_Any, typeof(H));
				ilgenerator.Emit(OpCodes.Ldarg_0);
			}
			ilgenerator.EmitCall(OpCodes.Call, methodInfo, null);
			ilgenerator.Emit(OpCodes.Ret);
			MessageHandlerFactory.Handler handlers = new MessageHandlerFactory.Handler
			{
				objectHandler = (dynamicMethod.CreateDelegate(typeof(Action<object, object>)) as Action<object, object>)
			};
			if (typeof(T).IsSealed)
			{
				handlers.packetHandler = delegate(Packet packet, object tag)
				{
					T t;
					SerializeReader.FromBinary<T>(packet, out t);
					if (this.Deserialized != null)
					{
						this.Deserialized(this, new EventArgs<object>(t));
					}
					handlers.objectHandler(t, tag);
				};
			}
			this.handlerDict.Add(typeof(T), handlers);
			if (categoryId != 0)
			{
				if (!typeof(T).IsSealed || handlers.packetHandler == null)
				{
					throw new InvalidOperationException(string.Format("'{0}' is not sealed.", typeof(T).AssemblyQualifiedName));
				}
				this.categoryDict.Add(categoryId, new MessageHandlerFactory.Category
				{
					type = typeof(T),
					packetHandler = handlers.packetHandler
				});
				ClassInfo<T>.CategoryId = categoryId;
			}
		}

		public void Register<H>(IDictionary<Type, int> typeConverters, string methodName)
		{
			if (typeConverters == null)
			{
				throw new ArgumentNullException("typeConverters");
			}
			if (methodName == null)
			{
				throw new ArgumentNullException("methodName");
			}
			foreach (KeyValuePair<Type, int> keyValuePair in typeConverters)
			{
				Type key = keyValuePair.Key;
				MethodInfo methodInfo = MessageHandlerFactory.FindDefinedMethod<H>(key, methodName);
				MethodInfo methodInfo2 = typeof(MessageHandlerFactory).GetMethod("Register", BindingFlags.Instance | BindingFlags.NonPublic, Type.DefaultBinder, new Type[]
				{
					typeof(MethodInfo),
					typeof(int)
				}, null);
				methodInfo2 = methodInfo2.MakeGenericMethod(new Type[]
				{
					typeof(H),
					key
				});
				int value = keyValuePair.Value;
				methodInfo2.Invoke(this, new object[]
				{
					methodInfo,
					value
				});
			}
		}

		private static MethodInfo FindDefinedMethod<H>(Type type, string methodName)
		{
			MethodInfo methodInfo = typeof(H).GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.ExactBinding, null, new Type[]
			{
				type
			}, null);
			if (methodInfo != null && !methodInfo.IsGenericMethod)
			{
				return methodInfo;
			}
			if (type.IsArray)
			{
				methodInfo = (from info in typeof(H).GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
				where info.Name == methodName && info.IsGenericMethodDefinition && info.GetParameters().Length == 1 && info.GetParameters()[0].ParameterType.IsArray && info.GetParameters()[0].ParameterType.GetElementType() == info.GetGenericArguments()[0] && info.GetParameters()[0].ParameterType.GetArrayRank() == type.GetArrayRank()
				select info).SingleOrDefault<MethodInfo>();
				if (methodInfo != null)
				{
					methodInfo = methodInfo.MakeGenericMethod(new Type[]
					{
						type.GetElementType()
					});
					if (methodInfo != null)
					{
						return methodInfo;
					}
				}
			}
			IEnumerable<MethodInfo> source = from info in typeof(H).GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
			where info.Name == methodName && info.IsGenericMethodDefinition && info.GetParameters().Length == 1 && info.GetParameters()[0].ParameterType.IsGenericType
			select info;
			if (type.IsGenericType)
			{
				methodInfo = (from info in source
				where type.GetGenericTypeDefinition() == info.GetParameters()[0].ParameterType.GetGenericTypeDefinition()
				select info).SingleOrDefault<MethodInfo>();
				if (methodInfo != null)
				{
					methodInfo = methodInfo.MakeGenericMethod(type.GetGenericArguments());
					if (methodInfo != null)
					{
						return methodInfo;
					}
				}
			}
			Type[] interfaces = type.GetInterfaces();
			for (int i = 0; i < interfaces.Length; i++)
			{
				Type interfaceType = interfaces[i];
				if (interfaceType.IsGenericType)
				{
					methodInfo = (from info in source
					where interfaceType.GetGenericTypeDefinition() == info.GetParameters()[0].ParameterType.GetGenericTypeDefinition()
					select info).SingleOrDefault<MethodInfo>();
					if (!(methodInfo == null))
					{
						methodInfo = methodInfo.MakeGenericMethod(interfaceType.GetGenericArguments());
						if (methodInfo != null)
						{
							return methodInfo;
						}
					}
				}
			}
			methodInfo = (from info in typeof(H).GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
			where info.Name == methodName && info.IsGenericMethodDefinition && info.GetParameters().Length == 1 && info.GetParameters()[0].ParameterType == info.GetGenericArguments()[0]
			select info).SingleOrDefault<MethodInfo>();
			if (methodInfo != null)
			{
				methodInfo = methodInfo.MakeGenericMethod(new Type[]
				{
					type
				});
				if (methodInfo != null)
				{
					return methodInfo;
				}
			}
			foreach (MethodInfo methodInfo2 in from info in typeof(H).GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
			where info.Name == methodName && info.GetParameters().Length == 1 && info.GetParameters()[0].ParameterType.IsAssignableFrom(type)
			select info)
			{
				if (methodInfo == null || (methodInfo.GetParameters()[0].ParameterType.IsInterface && !methodInfo2.GetParameters()[0].ParameterType.IsInterface) || methodInfo.GetParameters()[0].ParameterType.IsAssignableFrom(methodInfo2.GetParameters()[0].ParameterType))
				{
					methodInfo = methodInfo2;
				}
			}
			if (methodInfo != null)
			{
				return methodInfo;
			}
			methodInfo = typeof(H).GetMethod(methodName, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.ExactBinding, null, new Type[]
			{
				type,
				typeof(object)
			}, null);
			if (methodInfo != null && !methodInfo.IsGenericMethod)
			{
				return methodInfo;
			}
			if (type.IsArray)
			{
				methodInfo = (from info in typeof(H).GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
				where info.Name == methodName && info.IsGenericMethodDefinition && info.GetParameters().Length == 2 && info.GetParameters()[0].ParameterType.IsArray && info.GetParameters()[0].ParameterType.GetElementType() == info.GetGenericArguments()[0] && info.GetParameters()[0].ParameterType.GetArrayRank() == type.GetArrayRank() && info.GetParameters()[1].ParameterType == typeof(object)
				select info).SingleOrDefault<MethodInfo>();
				if (methodInfo != null)
				{
					methodInfo = methodInfo.MakeGenericMethod(new Type[]
					{
						type.GetElementType()
					});
					if (methodInfo != null)
					{
						return methodInfo;
					}
				}
			}
			source = from info in typeof(H).GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
			where info.Name == methodName && info.IsGenericMethodDefinition && info.GetParameters().Length == 2 && info.GetParameters()[0].ParameterType.IsGenericType && info.GetParameters()[1].ParameterType == typeof(object)
			select info;
			if (type.IsGenericType)
			{
				methodInfo = (from info in source
				where type.GetGenericTypeDefinition() == info.GetParameters()[0].ParameterType.GetGenericTypeDefinition()
				select info).SingleOrDefault<MethodInfo>();
				if (methodInfo != null)
				{
					methodInfo = methodInfo.MakeGenericMethod(type.GetGenericArguments());
					if (methodInfo != null)
					{
						return methodInfo;
					}
				}
			}
			Type[] interfaces2 = type.GetInterfaces();
			for (int j = 0; j < interfaces2.Length; j++)
			{
				Type interfaceType = interfaces2[j];
				if (interfaceType.IsGenericType)
				{
					methodInfo = (from info in source
					where interfaceType.GetGenericTypeDefinition() == info.GetParameters()[0].ParameterType.GetGenericTypeDefinition()
					select info).SingleOrDefault<MethodInfo>();
					if (!(methodInfo == null))
					{
						methodInfo = methodInfo.MakeGenericMethod(interfaceType.GetGenericArguments());
						if (methodInfo != null)
						{
							return methodInfo;
						}
					}
				}
			}
			methodInfo = (from info in typeof(H).GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
			where info.Name == methodName && info.IsGenericMethodDefinition && info.GetParameters().Length == 2 && info.GetParameters()[0].ParameterType == info.GetGenericArguments()[0] && info.GetParameters()[1].ParameterType == typeof(object)
			select info).SingleOrDefault<MethodInfo>();
			if (methodInfo != null)
			{
				methodInfo = methodInfo.MakeGenericMethod(new Type[]
				{
					type
				});
				if (methodInfo != null)
				{
					return methodInfo;
				}
			}
			foreach (MethodInfo methodInfo3 in from info in typeof(H).GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
			where info.Name == methodName && info.GetParameters().Length == 2 && info.GetParameters()[0].ParameterType.IsAssignableFrom(type) && info.GetParameters()[1].ParameterType == typeof(object)
			select info)
			{
				if (methodInfo == null || (methodInfo.GetParameters()[0].ParameterType.IsInterface && !methodInfo3.GetParameters()[0].ParameterType.IsInterface) || methodInfo.GetParameters()[0].ParameterType.IsAssignableFrom(methodInfo3.GetParameters()[0].ParameterType))
				{
					methodInfo = methodInfo3;
				}
			}
			if (methodInfo != null)
			{
				return methodInfo;
			}
			return null;
		}

		public void Handle(Packet packet, object tag)
		{
			MessageHandlerFactory.Category category;
			if (!this.categoryDict.TryGetValue(packet.CategoryId, out category))
			{
				throw new InvalidOperationException(string.Format("CategoryId '{0}' does not have registered handler.", packet.CategoryId));
			}
			category.packetHandler(packet, tag);
		}

		public static void Register(IDictionary<Type, int> typeConverters)
		{
			if (typeConverters == null)
			{
				throw new ArgumentNullException("typeConverters");
			}
			foreach (KeyValuePair<Type, int> keyValuePair in typeConverters)
			{
				Type key = keyValuePair.Key;
				int value = keyValuePair.Value;
				typeof(ClassInfo<>).MakeGenericType(new Type[]
				{
					key
				}).GetProperty("CategoryId").SetValue(null, value, null);
			}
		}

		public Type GetType(Packet packet)
		{
			if (packet.CategoryId == 0)
			{
				Type result;
				SerializeReader.FromBinary<Type>(packet, out result);
				return result;
			}
			MessageHandlerFactory.Category category;
			if (!this.categoryDict.TryGetValue(packet.CategoryId, out category))
			{
				throw new InvalidOperationException(string.Format("CategoryId '{0}' does not have registered handler.", packet.CategoryId));
			}
			return category.type;
		}

		public object GetTypeConverter()
		{
			return new MessageHandlerFactory.TypeConverter(this);
		}

		private IDictionary<Type, MessageHandlerFactory.Handler> handlerDict = new Dictionary<Type, MessageHandlerFactory.Handler>();

		private IDictionary<int, MessageHandlerFactory.Category> categoryDict = new Dictionary<int, MessageHandlerFactory.Category>();

		private struct Handler
		{
			public Action<object, object> objectHandler;

			public Action<Packet, object> packetHandler;
		}

		private struct Category
		{
			public Type type;

			public Action<Packet, object> packetHandler;
		}

		[Guid("FFFFFFFF-FFFF-FFFF-FFFF-FFFFFFFFFFFF")]
		[Serializable]
		private struct TypeConverter
		{
			public IDictionary<int, Type> Dictionary { get; private set; }

			public TypeConverter(MessageHandlerFactory mf)
			{
				this = default(MessageHandlerFactory.TypeConverter);
				this.Dictionary = new Dictionary<int, Type>(mf.categoryDict.Count);
				foreach (KeyValuePair<int, MessageHandlerFactory.Category> keyValuePair in mf.categoryDict)
				{
					if (keyValuePair.Key != 0)
					{
						this.Dictionary.Add(keyValuePair.Key, keyValuePair.Value.type);
					}
				}
			}

			public override string ToString()
			{
				if (this.Dictionary == null)
				{
					return "TypeConverter { Dictionary = (null) }";
				}
				StringBuilder stringBuilder = new StringBuilder("TypeConverter { Dictionary = { ");
				bool flag = false;
				foreach (KeyValuePair<int, Type> keyValuePair in this.Dictionary)
				{
					if (flag)
					{
						stringBuilder.Append(", ");
					}
					flag = true;
					stringBuilder.Append("{ ");
					stringBuilder.AppendFormat("0x{0:X}", keyValuePair.Key);
					stringBuilder.Append(", { FullName = ");
					stringBuilder.Append(keyValuePair.Value.FullName);
					stringBuilder.Append(", GUID = ");
					stringBuilder.Append(keyValuePair.Value.GUID);
					stringBuilder.Append(" } }");
				}
				stringBuilder.Append(" } }");
				return stringBuilder.ToString();
			}
		}
	}
}
