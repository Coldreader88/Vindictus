using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Threading;
using Devcat.Core.Threading;
using Microsoft.Win32;
using RemoteControlSystem.ServerMessage;

namespace RemoteControlSystem.Client
{
	[ComVisible(true)]
	[ComSourceInterfaces(typeof(IRCClient))]
	[Guid("3B01CD29-5B73-46F3-ABF6-FEF66BC47F94")]
	[ClassInterface(ClassInterfaceType.None)]
	public class ComObject : IRCClient
	{
		public void WaitForProcessStart(string processName)
		{
			this.WaitForProcessStart(processName, -1);
		}

		public bool WaitForProcessStart(string processName, int millisecond)
		{
			return this.WaitForProcessState(processName, millisecond, RCProcess.ProcessState.On);
		}

		public void WaitForProcessStop(string processName)
		{
			this.WaitForProcessStop(processName, -1);
		}

		public bool WaitForProcessStop(string processName, int millisecond)
		{
			return this.WaitForProcessState(processName, millisecond, RCProcess.ProcessState.Off);
		}

		private bool WaitForProcessState(string processName, int millisecond, RCProcess.ProcessState state)
		{
			long ticks = DateTime.Now.Ticks;
			for (;;)
			{
				RCProcess rcprocess = Base.Client[processName];
				if (rcprocess == null || rcprocess.State == RCProcess.ProcessState.Crash)
				{
					break;
				}
				if (rcprocess.State == state)
				{
					return true;
				}
				if (millisecond != -1 && DateTime.Now.Ticks - ticks > (long)(millisecond * 10000))
				{
					return false;
				}
				Thread.Sleep(1000);
			}
			return false;
		}

		public bool StartProcess(string processName)
		{
			RCProcess process = Base.Client[processName];
			if (process != null)
			{
				this.MainThread(delegate
				{
					Base.Client.ProcessMessage(new StartProcessMessage(process.Name));
				});
				return true;
			}
			return false;
		}

		public bool StopProcess(string processName)
		{
			RCProcess process = Base.Client[processName];
			if (process != null)
			{
				this.MainThread(delegate
				{
					Base.Client.ProcessMessage(new StopProcessMessage(process.Name));
				});
				return true;
			}
			return false;
		}

		public void StartAllProcess()
		{
			this.MainThread(delegate
			{
				foreach (RCProcess rcprocess in Base.Client.ProcessList)
				{
					Base.Client.ProcessMessage(new StartProcessMessage(rcprocess.Name));
				}
			});
		}

		public void StopAllProcess()
		{
			this.MainThread(delegate
			{
				foreach (RCProcess rcprocess in Base.Client.ProcessList)
				{
					Base.Client.ProcessMessage(new StopProcessMessage(rcprocess.Name));
				}
			});
		}

		public static void Regist()
		{
			RegistrationServices registrationServices = new RegistrationServices();
			ComObject.cookie = registrationServices.RegisterTypeForComClients(typeof(ComObject), RegistrationClassContext.LocalServer | RegistrationClassContext.RemoteServer, RegistrationConnectionType.MultipleUse);
		}

		private void MainThread(ComObject.MethodInvoker code)
		{
			if (Base.ClientControlManager.Thread.IsInThread())
			{
				code();
				return;
			}
			Base.ClientControlManager.Thread.Enqueue(Job.Create(delegate
			{
				code();
			}));
		}

		public static void Unregist()
		{
			if (ComObject.cookie != -1)
			{
				RegistrationServices registrationServices = new RegistrationServices();
				registrationServices.UnregisterTypeForComClients(ComObject.cookie);
			}
		}

		[ComRegisterFunction]
		public static void RegisterFunction(Type t)
		{
			AttributeCollection attributes = TypeDescriptor.GetAttributes(t);
			ProgIdAttribute progIdAttribute = attributes[typeof(ProgIdAttribute)] as ProgIdAttribute;
			GuidAttribute guidAttribute = attributes[typeof(GuidAttribute)] as GuidAttribute;
			string value = (progIdAttribute != null) ? progIdAttribute.Value : t.FullName;
			string value2 = t.Module.FullyQualifiedName.Replace("\\", "/");
			string text = "{" + guidAttribute.Value + "}";
			string subkey = string.Format("CLSID\\{0}\\LocalServer32", text);
			string subkey2 = "AppID\\" + ComObject.AppIDGUID;
			RegistryKey registryKey = Registry.ClassesRoot.CreateSubKey(subkey2);
			registryKey.SetValue(null, t.Module.Name);
			registryKey.SetValue("LocalService", BaseConfiguration.ClientServiceName);
			registryKey.SetValue("AuthenticationLevel", 1, RegistryValueKind.DWord);
			string subkey3 = "AppID\\" + t.Module;
			RegistryKey registryKey2 = Registry.ClassesRoot.CreateSubKey(subkey3);
			registryKey2.SetValue("AppID", ComObject.AppIDGUID);
			string subkey4 = "CLSID\\" + text;
			RegistryKey registryKey3 = Registry.ClassesRoot.CreateSubKey(subkey4);
			registryKey3.SetValue("AppID", ComObject.AppIDGUID);
			RegistryKey registryKey4 = Registry.ClassesRoot.CreateSubKey(subkey);
			registryKey4.SetValue(null, value2);
			registryKey4.SetValue("Class", value);
			registryKey4.SetValue("ThreadingModel", "Both");
			Registry.ClassesRoot.DeleteSubKeyTree(string.Format("CLSID\\{0}\\InprocServer32", text));
		}

		[ComUnregisterFunction]
		public static void UnregisterFunction(Type t)
		{
			try
			{
				Registry.ClassesRoot.DeleteSubKeyTree("CLSID\\{" + t.GUID + "}\\LocalServer32");
				Registry.ClassesRoot.DeleteSubKeyTree("AppID\\" + ComObject.AppIDGUID);
				Registry.ClassesRoot.DeleteSubKeyTree("AppID\\" + t.Module);
			}
			catch (Exception)
			{
			}
		}

		private static readonly string AppIDGUID = "{A36F91A9-7344-402A-A53F-A7832633E86F}";

		private static int cookie = -1;

		private delegate void MethodInvoker();
	}
}
