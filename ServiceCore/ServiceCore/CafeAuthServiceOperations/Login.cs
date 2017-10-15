using System;
using System.Collections.Generic;
using System.Net;
using Nexon.CafeAuth;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.CafeAuthServiceOperations
{
	[Serializable]
	public sealed class Login : Operation
	{
		public int NexonSN { get; private set; }

		public string NexonID { get; private set; }

		public string CharacterID { get; private set; }

		public IPAddress LocalAddress { get; private set; }

		public IPAddress RemoteAddress { get; private set; }

		public bool CanTry { get; private set; }

		public bool IsTrial { get; private set; }

		public MachineID MachineID { get; private set; }

		public int GameRoomClient { get; private set; }

		public byte ChannelCode { get; private set; }

		public Login(int nexonSN, string nexonID, string characterID, IPAddress localAddress, IPAddress remoteAddress, bool canTry, bool isTrial, MachineID machineID, int gameRoomClient, byte channelCode)
		{
			this.NexonSN = nexonSN;
			this.NexonID = nexonID;
			this.CharacterID = characterID;
			this.LocalAddress = localAddress;
			this.RemoteAddress = remoteAddress;
			this.CanTry = canTry;
			this.IsTrial = this.IsTrial;
			this.MachineID = machineID;
			this.GameRoomClient = gameRoomClient;
			this.ChannelCode = channelCode;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new Login.Request(this);
		}

		[NonSerialized]
		public bool IsCafe;

		[NonSerialized]
		public bool IsKicked;

		[NonSerialized]
		public int Message;

		[NonSerialized]
		public bool IsShutdownEnabled;

		[NonSerialized]
		public int CafeLevel;

		private class Request : OperationProcessor<Login>
		{
			public Request(Login op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				if (base.Feedback is bool)
				{
					base.Result = true;
					base.Operation.IsCafe = (bool)base.Feedback;
					yield return null;
					base.Operation.IsKicked = (bool)base.Feedback;
					yield return null;
					base.Operation.Message = (int)base.Feedback;
					yield return null;
					base.Operation.IsShutdownEnabled = (bool)base.Feedback;
					yield return null;
					base.Operation.CafeLevel = (int)base.Feedback;
				}
				else
				{
					base.Result = false;
					base.Operation.IsCafe = false;
					base.Operation.IsKicked = true;
					base.Operation.IsShutdownEnabled = false;
				}
				yield break;
			}
		}
	}
}
