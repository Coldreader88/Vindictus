using System;
using AhnLab.HackShield;
using Devcat.Core.Threading;
using ServiceCore;
using ServiceCore.EndPointNetwork;
using ServiceCore.FrontendServiceOperations;
using UnifiedNetwork.Entity;
using Utility;

namespace HackShieldServiceCore
{
	internal class HackShieldClient
	{
		public HackShieldService Service { get; private set; }

		public IEntity Entity { get; private set; }

		public DateTime ID
		{
			get
			{
				return new DateTime(this.Entity.ID);
			}
		}

		public AntiCpXSvr.SafeClientHandle Handle { get; private set; }

		public IEntityProxy FrontendConn { get; set; }

		public HackShieldClient(HackShieldService service, IEntity entity, AntiCpXSvr.SafeClientHandle clientHandle)
		{
			this.Service = service;
			this.Entity = entity;
			this.Entity.Closed += delegate(object sender, EventArgs e)
			{
				if (!this.Handle.IsClosed)
				{
					this.Handle.Dispose();
				}
			};
			this.Handle = clientHandle;
		}

		internal void MakeRequest()
		{
			if (this.Handle.IsClosed)
			{
				this.Entity.Close();
				return;
			}
			if (this.FrontendConn == null)
			{
				this.Entity.Close();
				return;
			}
			ArraySegment<byte> request;
			AntiCpXSvr.Error error = AntiCpXSvr.MakeRequest(this.Handle, out request);
			AntiCpXSvr.Error error2 = error;
			if (error2 == AntiCpXSvr.Error.Success)
			{
				HackShieldRequestMessage serializeObject = new HackShieldRequestMessage(request);
				this.FrontendConn.RequestOperation(SendPacket.Create<HackShieldRequestMessage>(serializeObject));
				Scheduler.Schedule(this.Service.Thread, Job.Create(new Action(this.MakeRequest)), this.Service.NextMilliseconds());
				return;
			}
			this.Entity.Close();
		}

		internal void MakeTcProtectRequest()
		{
			if (this.Handle.IsClosed)
			{
				this.Entity.Close();
				return;
			}
			if (this.FrontendConn == null)
			{
				this.Entity.Close();
				return;
			}
			if (FeatureMatrix.IsEnable("TcProtect"))
			{
				TcProtectRequestMessage serializeObject = new TcProtectRequestMessage(this.Service.TcProtectMd5, this.Service.TcProtectEncoded);
				this.FrontendConn.RequestOperation(SendPacket.Create<TcProtectRequestMessage>(serializeObject));
				if (!FeatureMatrix.IsEnable("RepeatTcProtectCheckOff"))
				{
					Log<HackShieldService>.Logger.Warn("Current FeatureMatrix Info [RepeatTcprotectCheckOff Off]");
					Scheduler.Schedule(this.Service.Thread, Job.Create(new Action(this.MakeTcProtectRequest)), this.Service.NextMilliseconds());
					return;
				}
				Log<HackShieldService>.Logger.Warn("Current FeatureMatrix Info [RepeatTcprotectCheckOff On]");
			}
		}
	}
}
