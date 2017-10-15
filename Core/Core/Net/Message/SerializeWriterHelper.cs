using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.Serialization;

namespace Devcat.Core.Net.Message
{
	internal static class SerializeWriterHelper<T, W> where W : struct
	{
		static SerializeWriterHelper()
		{
			SerializeWriterHelper<T, W>.GenerateSerializeMethod();
		}

		private static void SerializeCore(ref W writer, T value)
		{
			SerializeWriterHelper<T, W>.serializeCore(ref writer, value);
		}

		private static void SerializeCoreVirtual(ref W writer, T value)
		{
			Type type = (value == null) ? typeof(T) : value.GetType();
			SerializeWriterHelper<Type, W>.Serialize(ref writer, type);
			if (SerializeWriterHelper<T, W>.serializers == null)
			{
				SerializeWriterHelper<T, W>.serializers = new Dictionary<Type, SerializeWriterHelper<T, W>.SerializeDelegate>();
			}
			SerializeWriterHelper<T, W>.SerializeDelegate serializeDelegate;
			if (!SerializeWriterHelper<T, W>.serializers.TryGetValue(type, out serializeDelegate))
			{
				MethodInfo method = typeof(SerializeWriterHelper<, >).MakeGenericType(new Type[]
				{
					type,
					typeof(W)
				}).GetMethod("SerializeCore", BindingFlags.Static | BindingFlags.NonPublic);
				DynamicMethod dynamicMethod = new DynamicMethod(string.Format("{0}.{1}.SerializeAs[{2}.{3}]", new object[]
				{
					typeof(SerializeWriterHelper<Type, W>).Namespace,
					typeof(SerializeWriterHelper<Type, W>).Name,
					typeof(T).Namespace,
					typeof(T)
				}), null, new Type[]
				{
					typeof(W).MakeByRefType(),
					typeof(T)
				}, typeof(SerializeWriterHelper<Type, W>), true);
				dynamicMethod.DefineParameter(1, ParameterAttributes.In | ParameterAttributes.Out, "writer");
				dynamicMethod.DefineParameter(2, ParameterAttributes.In, "value");
				ILGenerator ilgenerator = dynamicMethod.GetILGenerator();
				ilgenerator.Emit(OpCodes.Ldarg_0);
				ilgenerator.Emit(OpCodes.Ldarg_1);
				ilgenerator.Emit(OpCodes.Unbox_Any, method.GetParameters()[1].ParameterType);
				ilgenerator.EmitCall(OpCodes.Call, method, null);
				ilgenerator.Emit(OpCodes.Ret);
				serializeDelegate = (dynamicMethod.CreateDelegate(typeof(SerializeWriterHelper<T, W>.SerializeDelegate)) as SerializeWriterHelper<T, W>.SerializeDelegate);
				SerializeWriterHelper<T, W>.serializers.Add(type, serializeDelegate);
			}
			serializeDelegate(ref writer, value);
		}

		public static void Serialize(ref W writer, T value)
		{
			SerializeWriterHelper<T, W>.serialize(ref writer, value);
		}

		private static MethodInfo FindDefinedSerializeMethod(Type type)
		{
			if (type.IsEnum)
			{
				type = Enum.GetUnderlyingType(type);
			}
			MethodInfo methodInfo = typeof(W).GetMethod("Write", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.ExactBinding, null, new Type[]
			{
				type
			}, null);
			if (methodInfo != null)
			{
				return methodInfo;
			}
			if (type.IsArray)
			{
				methodInfo = (from info in typeof(W).GetMethods(BindingFlags.Instance | BindingFlags.NonPublic)
				where info.Name == "Write" && info.IsGenericMethodDefinition && info.GetParameters().Length == 1 && info.GetParameters()[0].ParameterType.IsArray && info.GetParameters()[0].ParameterType.GetElementType() == info.GetGenericArguments()[0] && info.GetParameters()[0].ParameterType.GetArrayRank() == type.GetArrayRank()
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
			IEnumerable<MethodInfo> source = from info in typeof(W).GetMethods(BindingFlags.Instance | BindingFlags.NonPublic)
			where info.Name == "Write" && info.IsGenericMethodDefinition && info.GetParameters().Length == 1 && info.GetParameters()[0].ParameterType.IsGenericType
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
			methodInfo = (from info in typeof(W).GetMethods(BindingFlags.Instance | BindingFlags.NonPublic)
			where info.Name == "Write" && info.IsGenericMethodDefinition && info.GetParameters().Length == 1 && info.GetParameters()[0].ParameterType == info.GetGenericArguments()[0]
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
			foreach (MethodInfo methodInfo2 in from info in typeof(W).GetMethods(BindingFlags.Instance | BindingFlags.NonPublic)
			where info.Name == "Write" && info.GetParameters().Length == 1 && info.GetParameters()[0].ParameterType.IsAssignableFrom(type)
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
			return null;
		}

		private static void EmitWritePredefinedType(ILGenerator il, MethodInfo methodInfo)
		{
			il.Emit(OpCodes.Ldarg_0);
			il.Emit(OpCodes.Ldarg_1);
			il.EmitCall(OpCodes.Call, methodInfo, null);
		}

		private static void EmitWriteFields(ILGenerator il, Type type)
		{
			foreach (FieldInfo fieldInfo in type.GetFields(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
			{
				if (!fieldInfo.IsInitOnly && !fieldInfo.IsLiteral && !fieldInfo.IsNotSerialized)
				{
					Type fieldType = fieldInfo.FieldType;
					MethodInfo methodInfo = SerializeWriterHelper<T, W>.FindDefinedSerializeMethod(fieldType);
					if (methodInfo == null)
					{
						methodInfo = typeof(SerializeWriterHelper<, >).MakeGenericType(new Type[]
						{
							fieldType,
							typeof(W)
						}).GetMethod("Serialize");
					}
					il.Emit(OpCodes.Ldarg_0);
					il.Emit(OpCodes.Ldarg_1);
					il.Emit(OpCodes.Ldfld, fieldInfo);
					il.EmitCall(OpCodes.Call, methodInfo, null);
				}
			}
		}

        private static void GenerateSerializeMethod()
        {
            object[] @namespace = new object[] { typeof(SerializeWriterHelper<Type, W>).Namespace, typeof(SerializeWriterHelper<Type, W>).Name, typeof(T).Namespace, typeof(T).Name };
            string str = string.Format("{0}.{1}.SerializeCore[{2}.{3}]", @namespace);
            Type[] typeArray = new Type[] { typeof(W).MakeByRefType(), typeof(T) };
            DynamicMethod dynamicMethod = new DynamicMethod(str, null, typeArray, typeof(SerializeWriterHelper<T, W>), true);
            dynamicMethod.DefineParameter(1, ParameterAttributes.In | ParameterAttributes.Out, "writer");
            dynamicMethod.DefineParameter(2, ParameterAttributes.In, "value");
            ILGenerator lGenerator = dynamicMethod.GetILGenerator();
            Type baseType = typeof(T);
            if (!baseType.IsSerializable && SerializeWriterHelper<T, W>.FindDefinedSerializeMethod(baseType) == null)
            {
                throw new SerializationException(string.Format("Type is not serializable: {0}", baseType.AssemblyQualifiedName));
            }
            Label? nullable = null;
            if (!baseType.IsValueType && SerializeWriterHelper<T, W>.FindDefinedSerializeMethod(baseType) == null)
            {
                MethodInfo method = SerializeWriterHelper<T, W>.FindDefinedSerializeMethod(typeof(bool));
                if (method == null)
                {
                    Type type = typeof(SerializeWriterHelper<,>);
                    Type[] typeArray1 = new Type[] { typeof(bool), typeof(W) };
                    method = type.MakeGenericType(typeArray1).GetMethod("Serialize");
                }
                lGenerator.Emit(OpCodes.Ldarg_0);
                lGenerator.Emit(OpCodes.Ldarg_1);
                lGenerator.Emit(OpCodes.Ldnull);
                lGenerator.Emit(OpCodes.Ceq);
                lGenerator.Emit(OpCodes.Ldc_I4_0);
                lGenerator.Emit(OpCodes.Ceq);
                lGenerator.EmitCall(OpCodes.Call, method, null);
                lGenerator.Emit(OpCodes.Ldarg_1);
                nullable = new Label?(lGenerator.DefineLabel());
                lGenerator.Emit(OpCodes.Brfalse, nullable.Value);
            }
            Stack<Type> types = new Stack<Type>();
            while (true)
            {
                if (baseType != null)
                {
                    MethodInfo methodInfo = SerializeWriterHelper<T, W>.FindDefinedSerializeMethod(baseType);
                    if (methodInfo == null)
                    {
                        if (baseType.IsSerializable)
                        {
                            types.Push(baseType);
                        }
                        baseType = baseType.BaseType;
                    }
                    else
                    {
                        SerializeWriterHelper<T, W>.EmitWritePredefinedType(lGenerator, methodInfo);
                        break;
                    }
                }
                else
                {
                    break;
                }
            }
            while (0 < types.Count)
            {
                SerializeWriterHelper<T, W>.EmitWriteFields(lGenerator, types.Pop());
            }
            if (nullable.HasValue)
            {
                lGenerator.MarkLabel(nullable.Value);
            }
            lGenerator.Emit(OpCodes.Ret);
            SerializeWriterHelper<T, W>.serializeCore = dynamicMethod.CreateDelegate(typeof(SerializeWriterHelper<T, W>.SerializeDelegate)) as SerializeWriterHelper<T, W>.SerializeDelegate;
            if (typeof(T).IsSealed || SerializeWriterHelper<T, W>.FindDefinedSerializeMethod(typeof(T)) != null)
            {
                SerializeWriterHelper<T, W>.serialize = SerializeWriterHelper<T, W>.serializeCore;
                return;
            }
            SerializeWriterHelper<T, W>.serialize = new SerializeWriterHelper<T, W>.SerializeDelegate(SerializeWriterHelper<T, W>.SerializeCoreVirtual);
        }

        private static SerializeWriterHelper<T, W>.SerializeDelegate serializeCore;

		private static SerializeWriterHelper<T, W>.SerializeDelegate serialize;

		[ThreadStatic]
		private static IDictionary<Type, SerializeWriterHelper<T, W>.SerializeDelegate> serializers;

		private delegate void SerializeDelegate(ref W writer, T value);
	}
}
