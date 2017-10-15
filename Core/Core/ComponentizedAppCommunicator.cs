using System;
using System.Runtime.Remoting.Lifetime;

namespace Devcat.Core
{
	public sealed class ComponentizedAppCommunicator : MarshalByRefObject
	{
		public object Parameter
		{
			get
			{
				return this.parameter;
			}
			set
			{
				this.parameter = value;
			}
		}

		internal ComponentizedAppCommunicator(ComponentizedApplication parent, object parameter)
		{
			this.parent = parent;
			this.parameter = parameter;
		}

		public override object InitializeLifetimeService()
		{
			ILease lease = (ILease)base.InitializeLifetimeService();
			if (lease.CurrentState == LeaseState.Initial)
			{
				lease.InitialLeaseTime = TimeSpan.Zero;
			}
			return lease;
		}

		public void ReportMessage(object message)
		{
			this.parent.ReportMessage(message);
		}

		private ComponentizedApplication parent;

		private object parameter;
	}
}
