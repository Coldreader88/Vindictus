using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace Devcat.Core.Net.Message
{
	internal class SerializedSizeCalculatorHelper<T, C>
	{
		static SerializedSizeCalculatorHelper()
		{
			SerializedSizeCalculatorHelper<T, C>.GenerateCalculator();
		}

		private static int CalculateSerializedSizeCore(T value)
		{
			return SerializedSizeCalculatorHelper<T, C>.calculateSerializedSizeCore(value);
		}

		private static int CalculateSerializedSizeVirtual(T value)
		{
			Type type = (value == null) ? typeof(T) : value.GetType();
			int num = SerializedSizeCalculatorHelper<Type, C>.CalculateSerializedSize(type);
			if (SerializedSizeCalculatorHelper<T, C>.calculators == null)
			{
				SerializedSizeCalculatorHelper<T, C>.calculators = new Dictionary<Type, SerializedSizeCalculatorHelper<T, C>.CalculateSerializedSizeDelegate>();
			}
			SerializedSizeCalculatorHelper<T, C>.CalculateSerializedSizeDelegate calculateSerializedSizeDelegate;
			if (!SerializedSizeCalculatorHelper<T, C>.calculators.TryGetValue(type, out calculateSerializedSizeDelegate))
			{
				MethodInfo method = typeof(SerializedSizeCalculatorHelper<, >).MakeGenericType(new Type[]
				{
					type,
					typeof(C)
				}).GetMethod("CalculateSerializedSizeCore", BindingFlags.Static | BindingFlags.NonPublic);
				DynamicMethod dynamicMethod = new DynamicMethod(string.Format("{0}.{1}.CalculateSerializedSizeAs[{2}.{3}]", new object[]
				{
					typeof(SerializedSizeCalculatorHelper<T, C>).Namespace,
					typeof(SerializedSizeCalculatorHelper<T, C>).Name,
					typeof(T).Namespace,
					typeof(T).Name
				}), typeof(int), new Type[]
				{
					typeof(T)
				}, typeof(SerializedSizeCalculatorHelper<T, C>), true);
				dynamicMethod.DefineParameter(1, ParameterAttributes.In, "value");
				ILGenerator ilgenerator = dynamicMethod.GetILGenerator();
				ilgenerator.Emit(OpCodes.Ldarg_0);
				ilgenerator.Emit(OpCodes.Unbox_Any, method.GetParameters()[0].ParameterType);
				ilgenerator.EmitCall(OpCodes.Call, method, null);
				ilgenerator.Emit(OpCodes.Ret);
				calculateSerializedSizeDelegate = (dynamicMethod.CreateDelegate(typeof(SerializedSizeCalculatorHelper<T, C>.CalculateSerializedSizeDelegate)) as SerializedSizeCalculatorHelper<T, C>.CalculateSerializedSizeDelegate);
				SerializedSizeCalculatorHelper<T, C>.calculators.Add(type, calculateSerializedSizeDelegate);
			}
			return num + calculateSerializedSizeDelegate(value);
		}

		public static int CalculateSerializedSize(T value)
		{
			return SerializedSizeCalculatorHelper<T, C>.calculateSerializedSize(value);
		}

		internal static int GetStaticSize(Type type)
		{
			if (type == typeof(bool) || type == typeof(sbyte) || type == typeof(byte))
			{
				return 1;
			}
			if (type == typeof(short) || type == typeof(ushort) || type == typeof(char))
			{
				return 2;
			}
			if (type == typeof(int) || type == typeof(uint) || type == typeof(float))
			{
				return 4;
			}
			if (type == typeof(long) || type == typeof(ulong) || type == typeof(double))
			{
				return 8;
			}
			return 0;
		}

		private static MethodInfo FindDefinedCalculateMethod(Type type)
		{
			if (type.IsEnum)
			{
				type = Enum.GetUnderlyingType(type);
			}
			MethodInfo methodInfo = typeof(C).GetMethod("CalculateSize", BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.ExactBinding, null, new Type[]
			{
				type
			}, null);
			if (methodInfo != null)
			{
				return methodInfo;
			}
			if (type.IsArray)
			{
				methodInfo = (from info in typeof(C).GetMethods(BindingFlags.Static | BindingFlags.NonPublic)
				where info.Name == "CalculateSize" && info.IsGenericMethodDefinition && info.GetParameters().Length == 1 && info.GetParameters()[0].ParameterType.IsArray && info.GetParameters()[0].ParameterType.GetElementType() == info.GetGenericArguments()[0] && info.GetParameters()[0].ParameterType.GetArrayRank() == type.GetArrayRank()
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
			IEnumerable<MethodInfo> source = from info in typeof(C).GetMethods(BindingFlags.Static | BindingFlags.NonPublic)
			where info.Name == "CalculateSize" && info.IsGenericMethodDefinition && info.GetParameters().Length == 1 && info.GetParameters()[0].ParameterType.IsGenericType
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
			methodInfo = (from info in typeof(C).GetMethods(BindingFlags.Static | BindingFlags.NonPublic)
			where info.Name == "CalculateSize" && info.IsGenericMethodDefinition && info.GetParameters().Length == 1 && info.GetParameters()[0].ParameterType == info.GetGenericArguments()[0]
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
			foreach (MethodInfo methodInfo2 in from info in typeof(C).GetMethods(BindingFlags.Static | BindingFlags.NonPublic)
			where info.Name == "CalculateSize" && info.GetParameters().Length == 1 && info.GetParameters()[0].ParameterType.IsAssignableFrom(type)
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

		private static void EmitCalculateFields(ILGenerator il, Type type)
		{
			int num = 0;
			foreach (FieldInfo fieldInfo in type.GetFields(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
			{
				if (!fieldInfo.IsInitOnly && !fieldInfo.IsLiteral && !fieldInfo.IsNotSerialized)
				{
					Type fieldType = fieldInfo.FieldType;
					int staticSize = SerializedSizeCalculatorHelper<T, C>.GetStaticSize(fieldType);
					if (0 < staticSize)
					{
						num += staticSize;
					}
					else
					{
						MethodInfo methodInfo = SerializedSizeCalculatorHelper<T, C>.FindDefinedCalculateMethod(fieldType);
						if (methodInfo == null)
						{
							methodInfo = typeof(SerializedSizeCalculatorHelper<, >).MakeGenericType(new Type[]
							{
								fieldType,
								typeof(C)
							}).GetMethod("CalculateSerializedSize");
						}
						il.Emit(OpCodes.Ldarg_0);
						il.Emit(OpCodes.Ldfld, fieldInfo);
						il.EmitCall(OpCodes.Call, methodInfo, null);
						il.Emit(OpCodes.Add);
					}
				}
			}
			if (0 < num)
			{
				il.Emit(OpCodes.Ldc_I4, num);
				il.Emit(OpCodes.Add);
			}
		}

		private static void GenerateCalculator()
		{
			DynamicMethod dynamicMethod = new DynamicMethod(string.Format("{0}.{1}.CalculateSerializedSizeCore[{2}.{3}]", new object[]
			{
				typeof(SerializedSizeCalculatorHelper<T, C>).Namespace,
				typeof(SerializedSizeCalculatorHelper<T, C>).Name,
				typeof(T).Namespace,
				typeof(T).Name
			}), typeof(int), new Type[]
			{
				typeof(T)
			}, typeof(SerializedSizeCalculatorHelper<T, C>), true);
			dynamicMethod.DefineParameter(1, ParameterAttributes.In, "value");
			ILGenerator ilgenerator = dynamicMethod.GetILGenerator();
			Type type = typeof(T);
			Label? label = null;
			if (!type.IsValueType && SerializedSizeCalculatorHelper<T, C>.FindDefinedCalculateMethod(type) == null)
			{
				ilgenerator.Emit(OpCodes.Ldc_I4_1);
				ilgenerator.Emit(OpCodes.Ldarg_0);
				label = new Label?(ilgenerator.DefineLabel());
				ilgenerator.Emit(OpCodes.Brfalse, label.Value);
			}
			while (type != null)
			{
				MethodInfo methodInfo = SerializedSizeCalculatorHelper<T, C>.FindDefinedCalculateMethod(type);
				if (methodInfo != null)
				{
					ilgenerator.Emit(OpCodes.Ldarg_0);
					ilgenerator.EmitCall(OpCodes.Call, methodInfo, null);
					if (type != typeof(T))
					{
						ilgenerator.Emit(OpCodes.Add);
						break;
					}
					break;
				}
				else
				{
					if (type == typeof(T))
					{
						int staticSize = SerializedSizeCalculatorHelper<T, C>.GetStaticSize(type);
						if (staticSize >= 1)
						{
							ilgenerator.Emit(OpCodes.Ldc_I4, staticSize);
							break;
						}
						ilgenerator.Emit(OpCodes.Ldc_I4_0);
					}
					SerializedSizeCalculatorHelper<T, C>.EmitCalculateFields(ilgenerator, type);
					type = type.BaseType;
				}
			}
			if (label != null)
			{
				ilgenerator.Emit(OpCodes.Add);
				ilgenerator.MarkLabel(label.Value);
			}
			ilgenerator.Emit(OpCodes.Ret);
			SerializedSizeCalculatorHelper<T, C>.calculateSerializedSizeCore = (dynamicMethod.CreateDelegate(typeof(SerializedSizeCalculatorHelper<T, C>.CalculateSerializedSizeDelegate)) as SerializedSizeCalculatorHelper<T, C>.CalculateSerializedSizeDelegate);
			if (typeof(T).IsSealed || SerializedSizeCalculatorHelper<T, C>.FindDefinedCalculateMethod(typeof(T)) != null)
			{
				SerializedSizeCalculatorHelper<T, C>.calculateSerializedSize = SerializedSizeCalculatorHelper<T, C>.calculateSerializedSizeCore;
				return;
			}
			SerializedSizeCalculatorHelper<T, C>.calculateSerializedSize = new SerializedSizeCalculatorHelper<T, C>.CalculateSerializedSizeDelegate(SerializedSizeCalculatorHelper<T, C>.CalculateSerializedSizeVirtual);
		}

		private static SerializedSizeCalculatorHelper<T, C>.CalculateSerializedSizeDelegate calculateSerializedSizeCore;

		private static SerializedSizeCalculatorHelper<T, C>.CalculateSerializedSizeDelegate calculateSerializedSize;

		[ThreadStatic]
		private static IDictionary<Type, SerializedSizeCalculatorHelper<T, C>.CalculateSerializedSizeDelegate> calculators;

		private delegate int CalculateSerializedSizeDelegate(T value);
	}
}
