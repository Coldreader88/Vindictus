using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.Serialization;

namespace Devcat.Core.Net.Message
{
	internal static class SerializeReaderHelper<T, R> where R : struct
	{
		static SerializeReaderHelper()
		{
			SerializeReaderHelper<T, R>.GenerateDeserializeMethod();
		}

		private static void DeserializeCore(ref R reader, out T value)
		{
			SerializeReaderHelper<T, R>.deserializeCore(ref reader, out value);
		}

		private static void Validate(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (type.IsArray)
			{
				SerializeReaderHelper<T, R>.Validate(type.GetElementType());
				return;
			}
			if (type.IsGenericType)
			{
				Type[] genericArguments = type.GetGenericArguments();
				int i = 0;
				int num = genericArguments.Length;
				while (i < num)
				{
					SerializeReaderHelper<T, R>.Validate(genericArguments[i]);
					i++;
				}
				return;
			}
			if (type.GetType() == typeof(SerializeReader.UnknownType))
			{
				throw new InvalidOperationException(string.Format("UnknownType {{ GUID = {0} }}", type.GUID));
			}
		}

		private static void DeserializeVirtual(ref R reader, out T value)
		{
			Type type;
			SerializeReaderHelper<Type, R>.Deserialize(ref reader, out type);
			SerializeReaderHelper<T, R>.Validate(type);
			if (SerializeReaderHelper<T, R>.deserializers == null)
			{
				SerializeReaderHelper<T, R>.deserializers = new Dictionary<Type, SerializeReaderHelper<T, R>.DeserializeDelegate>();
			}
			SerializeReaderHelper<T, R>.DeserializeDelegate deserializeDelegate;
			if (!SerializeReaderHelper<T, R>.deserializers.TryGetValue(type, out deserializeDelegate))
			{
				MethodInfo method = typeof(SerializeReaderHelper<, >).MakeGenericType(new Type[]
				{
					type,
					typeof(R)
				}).GetMethod("DeserializeCore", BindingFlags.Static | BindingFlags.NonPublic);
				DynamicMethod dynamicMethod = new DynamicMethod(string.Format("{0}.{1}.DeserializeAs[{2}.{3}]", new object[]
				{
					typeof(SerializeReaderHelper<T, R>).Namespace,
					typeof(SerializeReaderHelper<T, R>).Name,
					typeof(T).Namespace,
					typeof(T).Name
				}), null, new Type[]
				{
					typeof(R).MakeByRefType(),
					typeof(T).MakeByRefType()
				}, typeof(SerializeReaderHelper<T, R>), true);
				dynamicMethod.DefineParameter(1, ParameterAttributes.In | ParameterAttributes.Out, "reader");
				dynamicMethod.DefineParameter(2, ParameterAttributes.Out, "value");
				ILGenerator ilgenerator = dynamicMethod.GetILGenerator();
				ilgenerator.Emit(OpCodes.Ldarg_0);
				LocalBuilder local = ilgenerator.DeclareLocal(type);
				ilgenerator.Emit(OpCodes.Ldloca_S, local);
				ilgenerator.EmitCall(OpCodes.Call, method, null);
				ilgenerator.Emit(OpCodes.Ldarg_1);
				ilgenerator.Emit(OpCodes.Ldloc, local);
				if (type.IsValueType)
				{
					ilgenerator.Emit(OpCodes.Box, type);
				}
				ilgenerator.Emit(OpCodes.Stind_Ref);
				ilgenerator.Emit(OpCodes.Ret);
				deserializeDelegate = (dynamicMethod.CreateDelegate(typeof(SerializeReaderHelper<T, R>.DeserializeDelegate)) as SerializeReaderHelper<T, R>.DeserializeDelegate);
				SerializeReaderHelper<T, R>.deserializers.Add(type, deserializeDelegate);
			}
			deserializeDelegate(ref reader, out value);
		}

		public static void Deserialize(ref R reader, out T value)
		{
			SerializeReaderHelper<T, R>.deserialize(ref reader, out value);
		}

		private static MethodInfo FindDefinedDeserializeMethod(Type type)
		{
			if (type.IsEnum)
			{
				type = Enum.GetUnderlyingType(type);
			}
			MethodInfo methodInfo = typeof(R).GetMethod("Read", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.ExactBinding, null, new Type[]
			{
				type.MakeByRefType()
			}, null);
			if (methodInfo != null)
			{
				return methodInfo;
			}
			if (type.IsArray)
			{
				methodInfo = (from info in typeof(R).GetMethods(BindingFlags.Instance | BindingFlags.NonPublic)
				where info.Name == "Read" && info.IsGenericMethodDefinition && info.GetParameters().Length == 1 && info.GetParameters()[0].IsOut && info.GetParameters()[0].ParameterType.GetElementType().IsArray
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
			IEnumerable<MethodInfo> enumerable = from info in typeof(R).GetMethods(BindingFlags.Instance | BindingFlags.NonPublic)
			where info.Name == "Read" && info.IsGenericMethodDefinition && info.GetParameters().Length == 1 && info.GetParameters()[0].IsOut && info.GetParameters()[0].ParameterType.GetElementType().IsGenericType
			select info;
			if (type.IsGenericType)
			{
				methodInfo = (from info in enumerable
				where type.GetGenericTypeDefinition() == info.GetParameters()[0].ParameterType.GetElementType().GetGenericTypeDefinition() && info.GetGenericArguments().Length == type.GetGenericArguments().Length
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
			enumerable = from info in typeof(R).GetMethods(BindingFlags.Instance | BindingFlags.NonPublic)
			where info.Name == "Read" && info.IsGenericMethodDefinition && info.GetParameters().Length == 1 && info.GetParameters()[0].IsOut && info.GetParameters()[0].ParameterType == info.GetGenericArguments().Last<Type>().MakeByRefType()
			select info;
			Type type2 = type;
			while (type2 != null)
			{
				foreach (MethodInfo methodInfo2 in enumerable)
				{
					Type[] genericArguments = type2.GetGenericArguments();
					Type[] array = new Type[genericArguments.Length + 1];
					Array.Copy(genericArguments, array, genericArguments.Length);
					array[array.Length - 1] = type;
					if (array.Length == methodInfo2.GetGenericArguments().Length)
					{
						try
						{
							methodInfo = methodInfo2.MakeGenericMethod(array);
							if (methodInfo != null)
							{
								return methodInfo;
							}
						}
						catch (ArgumentException)
						{
						}
					}
				}
				type2 = type2.BaseType;
			}
			foreach (Type type3 in type.GetInterfaces())
			{
				foreach (MethodInfo methodInfo3 in enumerable)
				{
					Type[] genericArguments2 = type3.GetGenericArguments();
					Type[] array2 = new Type[genericArguments2.Length + 1];
					Array.Copy(genericArguments2, array2, genericArguments2.Length);
					array2[array2.Length - 1] = type;
					if (array2.Length == methodInfo3.GetGenericArguments().Length)
					{
						try
						{
							methodInfo = methodInfo3.MakeGenericMethod(array2);
							if (methodInfo != null)
							{
								return methodInfo;
							}
						}
						catch (ArgumentException)
						{
						}
					}
				}
			}
			return null;
		}

		private static void EmitReadPredefinedType(ILGenerator il, MethodInfo methodInfo)
		{
			il.Emit(OpCodes.Ldarg_0);
			il.Emit(OpCodes.Ldarg_1);
			il.EmitCall(OpCodes.Call, methodInfo, null);
		}

		private static void EmitReadFields(ILGenerator il, Type type)
		{
			foreach (FieldInfo fieldInfo in type.GetFields(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
			{
				if (!fieldInfo.IsInitOnly && !fieldInfo.IsLiteral && !fieldInfo.IsNotSerialized)
				{
					Type fieldType = fieldInfo.FieldType;
					MethodInfo methodInfo = SerializeReaderHelper<T, R>.FindDefinedDeserializeMethod(fieldType);
					if (methodInfo == null)
					{
						methodInfo = typeof(SerializeReaderHelper<, >).MakeGenericType(new Type[]
						{
							fieldType,
							typeof(R)
						}).GetMethod("Deserialize");
					}
					il.Emit(OpCodes.Ldarg_0);
					il.Emit(OpCodes.Ldarg_1);
					if (!type.IsValueType)
					{
						il.Emit(OpCodes.Ldind_Ref);
					}
					il.Emit(OpCodes.Ldflda, fieldInfo);
					il.EmitCall(OpCodes.Call, methodInfo, null);
				}
			}
		}

        private static void GenerateDeserializeMethod()
        {
            object[] @namespace = new object[] { typeof(SerializeReaderHelper<T, R>).Namespace, typeof(SerializeReaderHelper<T, R>).Name, typeof(T).Namespace, typeof(T).Name };
            string str = string.Format("{0}.{1}.DeserializeCore[{2}.{3}]", @namespace);
            Type[] typeArray = new Type[] { typeof(R).MakeByRefType(), typeof(T).MakeByRefType() };
            DynamicMethod dynamicMethod = new DynamicMethod(str, null, typeArray, typeof(SerializeReaderHelper<T, R>), true);
            dynamicMethod.DefineParameter(1, ParameterAttributes.In | ParameterAttributes.Out, "reader");
            dynamicMethod.DefineParameter(2, ParameterAttributes.Out, "value");
            ILGenerator lGenerator = dynamicMethod.GetILGenerator();
            Type baseType = typeof(T);
            if (!baseType.IsSerializable && SerializeReaderHelper<T, R>.FindDefinedDeserializeMethod(baseType) == null)
            {
                throw new SerializationException(string.Format("Type is not serializable: {0}", baseType.AssemblyQualifiedName));
            }
            Label? nullable = null;
            if (baseType.IsValueType)
            {
                lGenerator.Emit(OpCodes.Ldarg_1);
                lGenerator.Emit(OpCodes.Initobj, baseType);
            }
            else if (SerializeReaderHelper<T, R>.FindDefinedDeserializeMethod(baseType) == null)
            {
                MethodInfo method = SerializeReaderHelper<T, R>.FindDefinedDeserializeMethod(typeof(bool));
                if (method == null)
                {
                    Type type = typeof(SerializeReaderHelper<,>);
                    Type[] typeArray1 = new Type[] { typeof(bool), typeof(R) };
                    method = type.MakeGenericType(typeArray1).GetMethod("Deserialize");
                }
                lGenerator.Emit(OpCodes.Ldarg_0);
                LocalBuilder localBuilder = lGenerator.DeclareLocal(typeof(bool));
                lGenerator.Emit(OpCodes.Ldloca_S, localBuilder);
                lGenerator.EmitCall(OpCodes.Call, method, null);
                lGenerator.Emit(OpCodes.Ldloc, localBuilder);
                nullable = new Label?(lGenerator.DefineLabel());
                lGenerator.Emit(OpCodes.Brfalse, nullable.Value);
                if (baseType.IsInterface || baseType.IsAbstract)
                {
                    lGenerator.Emit(OpCodes.Ldstr, string.Format("Type cannot be properly initialized: {0}", baseType.AssemblyQualifiedName));
                    Type type1 = typeof(SerializationException);
                    Type[] typeArray2 = new Type[] { typeof(string) };
                    ConstructorInfo constructor = type1.GetConstructor(BindingFlags.Instance | BindingFlags.Public, null, typeArray2, null);
                    lGenerator.Emit(OpCodes.Newobj, constructor);
                    lGenerator.Emit(OpCodes.Throw);
                }
                else
                {
                    lGenerator.Emit(OpCodes.Ldarg_1);
                    ConstructorInfo constructorInfo = baseType.GetConstructor(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, Type.EmptyTypes, null);
                    if (constructorInfo == null)
                    {
                        MethodInfo methodInfo = typeof(RuntimeTypeHandle).GetMethod("Allocate", BindingFlags.Static | BindingFlags.NonPublic);
                        MethodInfo method1 = typeof(Type).GetMethod("GetTypeFromHandle", BindingFlags.Static | BindingFlags.Public);
                        lGenerator.Emit(OpCodes.Ldtoken, baseType);
                        lGenerator.Emit(OpCodes.Call, method1);
                        lGenerator.EmitCall(OpCodes.Call, methodInfo, null);
                    }
                    else
                    {
                        lGenerator.Emit(OpCodes.Newobj, constructorInfo);
                    }
                    lGenerator.Emit(OpCodes.Stind_Ref);
                }
            }
            Stack<Type> types = new Stack<Type>();
            while (true)
            {
                if (baseType != null)
                {
                    MethodInfo methodInfo1 = SerializeReaderHelper<T, R>.FindDefinedDeserializeMethod(baseType);
                    if (methodInfo1 == null)
                    {
                        if (baseType.IsSerializable)
                        {
                            types.Push(baseType);
                        }
                        baseType = baseType.BaseType;
                    }
                    else
                    {
                        SerializeReaderHelper<T, R>.EmitReadPredefinedType(lGenerator, methodInfo1);
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
                SerializeReaderHelper<T, R>.EmitReadFields(lGenerator, types.Pop());
            }
            if (nullable.HasValue)
            {
                lGenerator.MarkLabel(nullable.Value);
            }
            lGenerator.Emit(OpCodes.Ret);
            SerializeReaderHelper<T, R>.deserializeCore = dynamicMethod.CreateDelegate(typeof(SerializeReaderHelper<T, R>.DeserializeDelegate)) as SerializeReaderHelper<T, R>.DeserializeDelegate;
            if (typeof(T).IsSealed || SerializeReaderHelper<T, R>.FindDefinedDeserializeMethod(typeof(T)) != null)
            {
                SerializeReaderHelper<T, R>.deserialize = SerializeReaderHelper<T, R>.deserializeCore;
                return;
            }
            SerializeReaderHelper<T, R>.deserialize = new SerializeReaderHelper<T, R>.DeserializeDelegate(SerializeReaderHelper<T, R>.DeserializeVirtual);
        }

        private static SerializeReaderHelper<T, R>.DeserializeDelegate deserializeCore;

		private static SerializeReaderHelper<T, R>.DeserializeDelegate deserialize;

		[ThreadStatic]
		private static IDictionary<Type, SerializeReaderHelper<T, R>.DeserializeDelegate> deserializers;

		private delegate void DeserializeDelegate(ref R reader, out T value);
	}
}
