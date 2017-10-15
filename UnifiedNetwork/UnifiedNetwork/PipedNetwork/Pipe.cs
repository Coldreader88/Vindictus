using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading;
using Devcat.Core.Net;
using Devcat.Core.Net.Message;
using Utility;

namespace UnifiedNetwork.PipedNetwork
{
	public class Pipe : ITransmitter<Packet>
	{
		public int ID { get; private set; }

		public object Tag { get; set; }

		public event Action<Packet> PacketSending;

		public event Action<Packet> PacketReceived;

		public event Action<Pipe> Closed;

		public event Action<object> MessageReceived;

		public bool IsClosed
		{
			get
			{
				return this.isclosed;
			}
		}

		public Pipe(int id)
		{
			this.ID = id;
			this.thread = Thread.CurrentThread.ManagedThreadId;
		}

		public void Send<T>(T msg)
		{
			if (this.thread != Thread.CurrentThread.ManagedThreadId)
			{
				Log<Pipe>.Logger.FatalFormat("Thread Unsafe code Send<T> : [{0} != {1} ({2})]", this.thread, Thread.CurrentThread.ManagedThreadId, typeof(T));
			}
			this.SendPacket(SerializeWriter.ToBinary<T>(msg));
		}

		public void SendObject(object msg)
		{
			if (this.thread != Thread.CurrentThread.ManagedThreadId)
			{
				Log<Pipe>.Logger.FatalFormat("Thread Unsafe code SendObject : [{0} != {1} ({2})]", this.thread, Thread.CurrentThread.ManagedThreadId, msg);
			}
			if (Pipe.serializers == null)
			{
				Pipe.serializers = new Dictionary<Type, Converter<object, Packet>>();
			}
			Converter<object, Packet> converter;
			if (!Pipe.serializers.TryGetValue(msg.GetType(), out converter))
			{
				MethodInfo methodInfo = (from info in typeof(SerializeWriter).GetMethods(BindingFlags.Static | BindingFlags.Public)
				where info.IsGenericMethod && info.Name == "ToBinary" && info.GetParameters().Length == 1 && info.GetParameters()[0].ParameterType == info.GetGenericArguments()[0]
				select info).SingleOrDefault<MethodInfo>();
				methodInfo = methodInfo.MakeGenericMethod(new Type[]
				{
					msg.GetType()
				});
				DynamicMethod dynamicMethod = new DynamicMethod(string.Format("{0}.{1}.Send[{2}.{3}]", new object[]
				{
					typeof(Pipe).Namespace,
					typeof(Pipe).Name,
					msg.GetType().Namespace,
					msg.GetType().Name
				}), typeof(Packet), new Type[]
				{
					typeof(object)
				}, typeof(Pipe), true);
				dynamicMethod.DefineParameter(1, ParameterAttributes.In, "message");
				ILGenerator ilgenerator = dynamicMethod.GetILGenerator();
				ilgenerator.Emit(OpCodes.Ldarg_0);
				ilgenerator.Emit(OpCodes.Unbox_Any, methodInfo.GetParameters()[0].ParameterType);
				ilgenerator.EmitCall(OpCodes.Call, methodInfo, null);
				ilgenerator.Emit(OpCodes.Ret);
				converter = (dynamicMethod.CreateDelegate(typeof(Converter<object, Packet>)) as Converter<object, Packet>);
				Pipe.serializers.Add(msg.GetType(), converter);
			}
			Packet packet = converter(msg);
			this.SendPacket(packet);
		}

		public void SendPacket(Packet packet)
		{
			if (this.thread != Thread.CurrentThread.ManagedThreadId)
			{
				Log<Pipe>.Logger.FatalFormat("Thread Unsafe code SendPacket : [{0} != {1} ({2})]", this.thread, Thread.CurrentThread.ManagedThreadId, packet);
			}
			packet.InstanceId = (long)this.ID;
			this.PacketSending(packet);
		}

		internal void ProcessMessage(object message)
		{
			if (this.thread != Thread.CurrentThread.ManagedThreadId)
			{
				Log<Pipe>.Logger.FatalFormat("Thread Unsafe code ProcessMessage : [{0} != {1} ({2})]", this.thread, Thread.CurrentThread.ManagedThreadId, message);
			}
			if (this.MessageReceived != null)
			{
				this.MessageReceived(message);
			}
		}

		internal void ProcessPacket(Packet packet)
		{
			if (this.thread != Thread.CurrentThread.ManagedThreadId)
			{
				Log<Pipe>.Logger.FatalFormat("Thread Unsafe code ProcessPacket : [{0} != {1} ({2})]", this.thread, Thread.CurrentThread.ManagedThreadId, packet);
			}
			if (this.isclosed)
			{
				Log<Pipe>.Logger.InfoFormat("recv from closed pipe {0} {1} {2}", this.ID, packet.CategoryId, packet.Length);
				return;
			}
			if (this.PacketReceived != null)
			{
				this.PacketReceived(packet);
			}
		}

		void ITransmitter<Packet>.Transmit(Packet data)
		{
			this.SendPacket(data);
		}

		public void SendClose()
		{
			if (this.thread != Thread.CurrentThread.ManagedThreadId)
			{
				Log<Pipe>.Logger.FatalFormat("Thread Unsafe code SendClose : [{0} != {1} ({2})]", this.thread, Thread.CurrentThread.ManagedThreadId, this.Tag);
			}
			ClosePipe value = new ClosePipe(this.ID);
			Packet obj = SerializeWriter.ToBinary<ClosePipe>(value);
			obj.InstanceId = 0L;
			this.PacketSending(obj);
		}

		public void Close()
		{
			if (this.thread != Thread.CurrentThread.ManagedThreadId)
			{
				Log<Pipe>.Logger.FatalFormat("Thread Unsafe code Close : [{0} != {1} ({2})]", this.thread, Thread.CurrentThread.ManagedThreadId, this.Tag);
			}
			if (this.isclosed)
			{
				Log<Pipe>.Logger.InfoFormat("try to close pipe already closed : {0}", this.ID);
				return;
			}
			this.isclosed = true;
			if (this.Closed != null)
			{
				this.Closed(this);
			}
			this.PacketSending = null;
			this.PacketSending += delegate(Packet x)
			{
				Log<Pipe>.Logger.WarnFormat("send to closed pipe {0} {1} {2}", this.ID, x.CategoryId, x.Length);
			};
		}

		private bool isclosed;

		private int thread;

		[ThreadStatic]
		private static IDictionary<Type, Converter<object, Packet>> serializers;
	}
}
