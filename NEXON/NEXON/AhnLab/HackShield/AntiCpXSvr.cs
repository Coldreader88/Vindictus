using System;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;

namespace AhnLab.HackShield
{
	public static class AntiCpXSvr
	{
		static AntiCpXSvr()
		{
			AssemblyName name = new AssemblyName(Guid.NewGuid().ToString());
			AssemblyBuilder assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(name, AssemblyBuilderAccess.Run);
			ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule("AhnLab.HackShield");
			TypeBuilder typeBuilder = moduleBuilder.DefineType(string.Format("{0}.AntiCpXSvr", moduleBuilder.ScopeName));
			string dllName = Path.Combine((IntPtr.Size == 4) ? "x86" : "x64", "AntiCpXSvr.dll");
			MethodAttributes attributes = MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.Static | MethodAttributes.PinvokeImpl;
			CallingConventions callingConvention = CallingConventions.Standard;
			CallingConvention nativeCallConv = CallingConvention.StdCall;
			CharSet nativeCharSet = CharSet.Ansi;
			MethodBuilder methodBuilder = typeBuilder.DefinePInvokeMethod("_AhnHS_CreateServerObject", dllName, attributes, callingConvention, typeof(IntPtr), new Type[]
			{
				typeof(string)
			}, nativeCallConv, nativeCharSet);
			methodBuilder.SetImplementationFlags(methodBuilder.GetMethodImplementationFlags() | MethodImplAttributes.PreserveSig);
			methodBuilder = typeBuilder.DefinePInvokeMethod("_AhnHS_CloseServerHandle", dllName, attributes, callingConvention, null, new Type[]
			{
				typeof(IntPtr)
			}, nativeCallConv, nativeCharSet);
			methodBuilder.SetImplementationFlags(methodBuilder.GetMethodImplementationFlags() | MethodImplAttributes.PreserveSig);
			methodBuilder = typeBuilder.DefinePInvokeMethod("_AhnHS_CreateClientObject", dllName, attributes, callingConvention, typeof(IntPtr), new Type[]
			{
				typeof(IntPtr)
			}, nativeCallConv, nativeCharSet);
			methodBuilder.SetImplementationFlags(methodBuilder.GetMethodImplementationFlags() | MethodImplAttributes.PreserveSig);
			methodBuilder = typeBuilder.DefinePInvokeMethod("_AhnHS_CloseClientHandle", dllName, attributes, callingConvention, null, new Type[]
			{
				typeof(IntPtr)
			}, nativeCallConv, nativeCharSet);
			methodBuilder.SetImplementationFlags(methodBuilder.GetMethodImplementationFlags() | MethodImplAttributes.PreserveSig);
			methodBuilder = typeBuilder.DefinePInvokeMethod("_AhnHS_MakeRequest", dllName, attributes, callingConvention, typeof(AntiCpXSvr.Error), new Type[]
			{
				typeof(IntPtr),
				typeof(AntiCpXSvr.TransBuffer).MakeByRefType()
			}, nativeCallConv, nativeCharSet);
			methodBuilder.DefineParameter(2, ParameterAttributes.Out, "requestBuffer");
			methodBuilder.SetImplementationFlags(methodBuilder.GetMethodImplementationFlags() | MethodImplAttributes.PreserveSig);
			methodBuilder = typeBuilder.DefinePInvokeMethod("_AhnHS_VerifyResponse", dllName, attributes, callingConvention, typeof(AntiCpXSvr.Error), new Type[]
			{
				typeof(IntPtr),
				typeof(IntPtr),
				typeof(int)
			}, nativeCallConv, nativeCharSet);
			methodBuilder.SetImplementationFlags(methodBuilder.GetMethodImplementationFlags() | MethodImplAttributes.PreserveSig);
			methodBuilder = typeBuilder.DefinePInvokeMethod("_AhnHS_VerifyResponseEx", dllName, attributes, callingConvention, typeof(AntiCpXSvr.Recommend), new Type[]
			{
				typeof(IntPtr),
				typeof(IntPtr),
				typeof(int),
				typeof(AntiCpXSvr.Error).MakeByRefType()
			}, nativeCallConv, nativeCharSet);
			methodBuilder.DefineParameter(4, ParameterAttributes.Out, "errorCode");
			methodBuilder.SetImplementationFlags(methodBuilder.GetMethodImplementationFlags() | MethodImplAttributes.PreserveSig);
			AntiCpXSvr.type = typeBuilder.CreateType();
		}

		public static AntiCpXSvr.Error MakeRequest(AntiCpXSvr.SafeClientHandle clientHandle, out ArraySegment<byte> requestBuffer)
		{
			object[] array = new object[]
			{
				clientHandle.DangerousGetHandle(),
				default(AntiCpXSvr.TransBuffer)
			};
			AntiCpXSvr.Error result = (AntiCpXSvr.Error)AntiCpXSvr.type.InvokeMember("_AhnHS_MakeRequest", BindingFlags.InvokeMethod, null, null, array);
			AntiCpXSvr.TransBuffer transBuffer = (AntiCpXSvr.TransBuffer)array[1];
			requestBuffer = new ArraySegment<byte>(transBuffer.buffer, 0, (int)transBuffer.length);
			return result;
		}

		public unsafe static AntiCpXSvr.Error VerifyResponse(AntiCpXSvr.SafeClientHandle clientHandle, ArraySegment<byte> responseBuffer)
		{
			fixed (byte* array = responseBuffer.Array)
			{
				object[] args = new object[]
				{
					clientHandle.DangerousGetHandle(),
					new IntPtr((void*)((byte*)array + responseBuffer.Offset)),
					responseBuffer.Count
				};
				return (AntiCpXSvr.Error)AntiCpXSvr.type.InvokeMember("_AhnHS_VerifyResponse", BindingFlags.InvokeMethod, null, null, args);
			}
		}

		public unsafe static AntiCpXSvr.Error VerifyResponse(AntiCpXSvr.SafeClientHandle clientHandle, ArraySegment<byte> responseBuffer, out AntiCpXSvr.Recommend recommend)
		{
			fixed (byte* array = responseBuffer.Array)
			{
				object[] array2 = new object[]
				{
					clientHandle.DangerousGetHandle(),
					new IntPtr((void*)((byte*)array + responseBuffer.Offset)),
					responseBuffer.Count,
					AntiCpXSvr.Error.Success
				};
				recommend = (AntiCpXSvr.Recommend)AntiCpXSvr.type.InvokeMember("_AhnHS_VerifyResponseEx", BindingFlags.InvokeMethod, null, null, array2);
				return (AntiCpXSvr.Error)array2[3];
			}
		}

		private const int BaseCode = -385613824;

		private const int TRANS_BUFFER_MAX = 600;

		private static Type type;

		public enum Error
		{
			Success,
			FileAccessDenied = -385613823,
			FileNotFound,
			InvalidParameter,
			BadFormat,
			NotYetReceivedResponse,
			NoWaiting,
			NotEnoughMemory,
			BadMessage,
			RelayAttack,
			HShieldFileAttack,
			ClientFileAttack,
			MemoryAttack,
			OldVersionClientExpired,
			UnknownClient,
			V3SEngineFileAttack,
			NanoEngineFileAttack,
			InvalidHackShieldVersion,
			InvalidEngineVersion,
			CreateSvrObjException,
			MakeReqException,
			VerifyException,
			TraceException,
			CreateClientObjectException,
			AbnormalHackshieldStatus,
			DetectCallbackIsNotified,
			VerifyExException,
			AbnormalHackshieldXStatus,
			OldHackshieldVersion,
			OptionAttack,
			StatusXAbnormal,
			StatusXSuspicious,
			InvalidEnvFileVersion,
			HSEnvFileAttack,
			InvalidHEIVersion,
			HEIAttack,
			HSThreadIsNotRunning,
			Unknown = -385613569
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		private struct TransBuffer
		{
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 600)]
			public byte[] buffer;

			public ushort length;
		}

		public class SafeServerHandle : SafeHandle
		{
			public SafeServerHandle(string filePath) : base(IntPtr.Zero, true)
			{
				IntPtr handle = (IntPtr)AntiCpXSvr.type.InvokeMember("_AhnHS_CreateServerObject", BindingFlags.InvokeMethod, null, null, new object[]
				{
					filePath
				});
				base.SetHandle(handle);
			}

			public override bool IsInvalid
			{
				get
				{
					return this.handle == IntPtr.Zero;
				}
			}

			protected override bool ReleaseHandle()
			{
				if (this.IsInvalid)
				{
					return false;
				}
				AntiCpXSvr.type.InvokeMember("_AhnHS_CloseServerHandle", BindingFlags.InvokeMethod, null, null, new object[]
				{
					this.handle
				});
				return true;
			}
		}

		public class SafeClientHandle : SafeHandle
		{
			public SafeClientHandle(AntiCpXSvr.SafeServerHandle serverHandle) : base(IntPtr.Zero, true)
			{
				IntPtr handle = (IntPtr)AntiCpXSvr.type.InvokeMember("_AhnHS_CreateClientObject", BindingFlags.InvokeMethod, null, null, new object[]
				{
					serverHandle.DangerousGetHandle()
				});
				base.SetHandle(handle);
			}

			public override bool IsInvalid
			{
				get
				{
					return this.handle == IntPtr.Zero;
				}
			}

			protected override bool ReleaseHandle()
			{
				if (this.IsInvalid)
				{
					return false;
				}
				AntiCpXSvr.type.InvokeMember("_AhnHS_CloseClientHandle", BindingFlags.InvokeMethod, null, null, new object[]
				{
					this.handle
				});
				return true;
			}
		}

		public enum Recommend
		{
			CloseSession = 101,
			KeepSession
		}
	}
}
