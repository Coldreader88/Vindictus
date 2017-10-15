using System;
using System.Reflection;
using System.Reflection.Emit;

namespace Devcat.Core.Memory
{
	public static class PureAllocator<T>
	{
		static PureAllocator()
		{
			PureAllocator<T>.GenerateAllocateMethod();
		}

		private static void GenerateAllocateMethod()
		{
			if (typeof(T).IsInterface || typeof(T).IsArray || typeof(T).IsAbstract || typeof(T) == typeof(string))
			{
				PureAllocator<T>.allocate = (() => default(T));
				return;
			}
			if (typeof(T).IsValueType)
			{
				PureAllocator<T>.allocate = (() => default(T));
			}
			else
			{
				DynamicMethod dynamicMethod = new DynamicMethod(string.Format("{0}.{1}.InternalAllocate", typeof(PureAllocator<T>).Namespace, typeof(PureAllocator<T>).Name), typeof(T), Type.EmptyTypes, typeof(PureAllocator<T>), true);
				ILGenerator ilgenerator = dynamicMethod.GetILGenerator();
				LocalBuilder local = ilgenerator.DeclareLocal(typeof(RuntimeTypeHandle));
				ilgenerator.Emit(OpCodes.Ldtoken, typeof(T));
				ilgenerator.Emit(OpCodes.Stloc, local);
				ilgenerator.Emit(OpCodes.Ldloca_S, local);
				ilgenerator.EmitCall(OpCodes.Call, typeof(RuntimeTypeHandle).GetMethod("Allocate", BindingFlags.Instance | BindingFlags.NonPublic), null);
				ilgenerator.Emit(OpCodes.Ret);
				PureAllocator<T>.allocate = (PureAllocator<T>.Allocator)dynamicMethod.CreateDelegate(typeof(PureAllocator<T>.Allocator));
			}
			ConstructorInfo constructor = typeof(T).GetConstructor(Type.EmptyTypes);
			if (constructor == null)
			{
				PureAllocator<T>.allocateDefault = PureAllocator<T>.allocate;
				return;
			}
			DynamicMethod dynamicMethod2 = new DynamicMethod(string.Format("{0}.{1}.InternalAllocateDefault", typeof(PureAllocator<T>).Namespace, typeof(PureAllocator<T>).Name), typeof(T), Type.EmptyTypes, typeof(PureAllocator<T>), true);
			ILGenerator ilgenerator2 = dynamicMethod2.GetILGenerator();
			ilgenerator2.Emit(OpCodes.Newobj, constructor);
			ilgenerator2.Emit(OpCodes.Ret);
			PureAllocator<T>.allocateDefault = (PureAllocator<T>.Allocator)dynamicMethod2.CreateDelegate(typeof(PureAllocator<T>.Allocator));
		}

		public static T Allocate()
		{
			return PureAllocator<T>.allocate();
		}

		public static T AllocateDefault()
		{
			return PureAllocator<T>.allocateDefault();
		}

		private static PureAllocator<T>.Allocator allocate;

		private static PureAllocator<T>.Allocator allocateDefault;

		private delegate T Allocator();
	}
}
