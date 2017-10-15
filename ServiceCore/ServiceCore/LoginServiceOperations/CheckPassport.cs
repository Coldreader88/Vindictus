using System;
using System.Collections.Generic;
using System.Net;
using ServiceCore.EndPointNetwork;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.LoginServiceOperations
{
	[Serializable]
	public sealed class CheckPassport : Operation
	{
		public string Passport { get; set; }

		public IPAddress Address
		{
			get
			{
				return new IPAddress(this.address);
			}
			set
			{
				this.address = value.GetAddressBytes();
			}
		}

		public string HwID { get; set; }

		public string UpToDateInfo { get; set; }

		public long ChcekSum { get; set; }

		public int NexonSN
		{
			get
			{
				return this.nexonSN;
			}
		}

		public string NexonID
		{
			get
			{
				return this.nexonID;
			}
		}

		public IPAddress LoginAddr
		{
			get
			{
				return this.loginAddr;
			}
		}

		public Permissions Permissions
		{
			get
			{
				return this.permissions;
			}
		}

		public int Age
		{
			get
			{
				return this.age;
			}
		}

		public bool IsOnline
		{
			get
			{
				return this.isOnline;
			}
		}

		public byte SecureCode
		{
			get
			{
				return this.secureCode;
			}
		}

		public string WebShutDownMessage
		{
			get
			{
				return this.webShutDownMessage;
			}
		}

		public int RegionCode
		{
			get
			{
				return this.regionCode;
			}
		}

		public string ChannelUID
		{
			get
			{
				return this.channelUID;
			}
		}

		public byte ChannelCode
		{
			get
			{
				return this.channelCode;
			}
		}

		public LoginFailMessage.FailReason Reason
		{
			get
			{
				return this.reason;
			}
		}

		public bool IsGCA
		{
			get
			{
				return this.isGCA;
			}
		}

		public CheckPassport()
		{
		}

		public CheckPassport(LoginFailMessage.FailReason dummyReason)
		{
			this.reason = dummyReason;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new CheckPassport.Request(this);
		}

		private byte[] address;

		[NonSerialized]
		private int nexonSN;

		[NonSerialized]
		private string nexonID;

		[NonSerialized]
		private IPAddress loginAddr;

		[NonSerialized]
		private Permissions permissions;

		[NonSerialized]
		private int age;

		[NonSerialized]
		private bool isOnline;

		[NonSerialized]
		private byte secureCode;

		[NonSerialized]
		private string webShutDownMessage;

		[NonSerialized]
		private int regionCode;

		[NonSerialized]
		private string channelUID;

		[NonSerialized]
		private byte channelCode;

		[NonSerialized]
		private LoginFailMessage.FailReason reason;

		[NonSerialized]
		private bool isGCA;

		private class Request : OperationProcessor<CheckPassport>
		{
			public Request(CheckPassport op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				if (base.Feedback is int)
				{
					base.Operation.nexonSN = (int)base.Feedback;
					yield return null;
					base.Operation.nexonID = (base.Feedback as string);
					yield return null;
					base.Operation.loginAddr = (base.Feedback as IPAddress);
					yield return null;
					base.Operation.channelUID = (string)base.Feedback;
					yield return null;
					base.Operation.channelCode = (byte)base.Feedback;
					yield return null;
					base.Operation.permissions = (Permissions)base.Feedback;
					yield return null;
					base.Operation.age = (int)((ushort)base.Feedback);
					yield return null;
					base.Operation.isOnline = (bool)base.Feedback;
					yield return null;
					base.Operation.secureCode = (byte)base.Feedback;
					yield return null;
					base.Operation.webShutDownMessage = (base.Feedback as string);
					yield return null;
					base.Operation.regionCode = (int)base.Feedback;
					yield return null;
					base.Operation.isGCA = (bool)base.Feedback;
				}
				else if (base.Feedback is LoginFailMessage.FailReason)
				{
					base.Operation.reason = (LoginFailMessage.FailReason)base.Feedback;
				}
				yield break;
			}
		}
	}
}
