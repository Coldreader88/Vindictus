using System;
using System.Collections.Generic;
using System.Net;
using ServiceCore.EndPointNetwork;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.LoginServiceOperations
{
	[Serializable]
	public sealed class UserLogin : Operation
	{
		public string ID { get; set; }

		public IPAddress LoginAddr { get; set; }

		public IPAddress RemoteAddr { get; set; }

		public bool IsSecuredLogin { get; set; }

		public byte ChannelCode { get; set; }

		public int PackageCount
		{
			get
			{
				return this.packagecount;
			}
			private set
			{
				this.packagecount = value;
			}
		}

		public LoginFailMessage.FailReason Reason
		{
			get
			{
				return this.reason;
			}
		}

		public string LastLoginIP
		{
			get
			{
				return this.lastLoginIP;
			}
		}

		public int LockLastSecs
		{
			get
			{
				return this.lockLastSecs;
			}
		}

		public int Limited
		{
			get
			{
				return this.limited;
			}
		}

		public string BanedReason
		{
			get
			{
				return this.bannedReason;
			}
		}

		public bool IsDormant
		{
			get
			{
				return this.isDormant;
			}
		}

		public string FacebookToken
		{
			get
			{
				return this.facebookToken;
			}
		}

		public bool IsReturn
		{
			get
			{
				return this.isReturn;
			}
		}

		public override OperationProcessor RequestProcessor()
		{
			return new UserLogin.Request(this);
		}

		[NonSerialized]
		private int packagecount;

		[NonSerialized]
		private LoginFailMessage.FailReason reason;

		[NonSerialized]
		private string lastLoginIP;

		[NonSerialized]
		private int lockLastSecs;

		[NonSerialized]
		private int limited;

		[NonSerialized]
		private string bannedReason = "";

		[NonSerialized]
		private bool isDormant;

		[NonSerialized]
		private string facebookToken;

		[NonSerialized]
		private bool isReturn;

		private class Request : OperationProcessor<UserLogin>
		{
			public Request(UserLogin op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				if (base.Feedback is int)
				{
					base.Result = true;
					base.Operation.PackageCount = (int)base.Feedback;
					yield return null;
					base.Operation.lastLoginIP = (string)base.Feedback;
					yield return null;
					base.Operation.lockLastSecs = (int)base.Feedback;
					yield return null;
					base.Operation.limited = (int)base.Feedback;
					yield return null;
					base.Operation.isDormant = (bool)base.Feedback;
					yield return null;
					base.Operation.facebookToken = (string)base.Feedback;
					yield return null;
					base.Operation.isReturn = (bool)base.Feedback;
				}
				else if (base.Feedback is LoginFailMessage.FailReason)
				{
					base.Result = false;
					base.Operation.reason = (LoginFailMessage.FailReason)base.Feedback;
					if (base.Operation.reason == LoginFailMessage.FailReason.Banned)
					{
						yield return null;
						base.Operation.bannedReason = (string)base.Feedback;
					}
				}
				yield break;
			}
		}
	}
}
