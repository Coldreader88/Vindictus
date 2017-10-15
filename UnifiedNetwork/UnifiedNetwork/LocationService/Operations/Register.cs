using System;
using System.Collections.Generic;
using log4net;
using UnifiedNetwork.Cooperation;
using UnifiedNetwork.Properties;

namespace UnifiedNetwork.LocationService.Operations
{
	[Serializable]
	public sealed class Register : Operation
	{
		public string FullName { get; private set; }

		public Guid GUID { get; private set; }

		public Guid ModuleVersionId { get; private set; }

		public int GameCode { get; private set; }

		public int ServerCode { get; private set; }

		public string Category
		{
			get
			{
				return this.FullName;
			}
		}

		public int ID
		{
			get
			{
				return (int)this.id;
			}
		}

		public int Port
		{
			get
			{
				return (int)this.port;
			}
		}

		public byte Suffix
		{
			get
			{
				return this.suffix;
			}
		}

		public int ServiceOrder
		{
			get
			{
				return this.serviceOrder;
			}
		}

		public int LocalOrder
		{
			get
			{
				return this.localOrder;
			}
		}

		public Register()
		{
		}

		public Register(Type type)
		{
			this.FullName = type.FullName;
			this.GUID = type.GUID;
			this.ModuleVersionId = type.Module.ModuleVersionId;
			this.GameCode = Settings.Default.GameCode;
			this.ServerCode = Settings.Default.ServerCode;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new Register.Request(this);
		}

		[NonSerialized]
		private ushort id;

		[NonSerialized]
		private ushort port;

		[NonSerialized]
		private byte suffix;

		[NonSerialized]
		private int serviceOrder;

		[NonSerialized]
		private int localOrder;

		private class Request : OperationProcessor<Register>
		{
			public Request(Register op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				base.Operation.id = (ushort)base.Feedback;
				ThreadContext.Properties["ServiceID"] = base.Operation.id;
				yield return null;
				base.Operation.port = (ushort)base.Feedback;
				yield return null;
				base.Operation.suffix = (byte)base.Feedback;
				yield return null;
				base.Operation.serviceOrder = (int)base.Feedback;
				yield return null;
				base.Operation.localOrder = (int)base.Feedback;
				yield break;
			}
		}
	}
}
