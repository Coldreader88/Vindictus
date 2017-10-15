using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Threading;

namespace Devcat.Core
{
	[ComVisible(true)]
	[Serializable]
	internal class OperationCanceledException2 : OperationCanceledException
	{
		public OperationCanceledException2() : base(Environment2.GetResourceString("OperationCanceled"))
		{
		}

		public OperationCanceledException2(string message) : base(message)
		{
		}

		public OperationCanceledException2(CancellationToken token) : this()
		{
			this.CancellationToken = token;
		}

		protected OperationCanceledException2(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		public OperationCanceledException2(string message, Exception innerException) : base(message, innerException)
		{
		}

		public OperationCanceledException2(string message, CancellationToken token) : this(message)
		{
			this.CancellationToken = token;
		}

		public OperationCanceledException2(string message, Exception innerException, CancellationToken token) : this(message, innerException)
		{
			this.CancellationToken = token;
		}

		public new CancellationToken CancellationToken
		{
			get
			{
				return this._cancellationToken;
			}
			private set
			{
				this._cancellationToken = value;
			}
		}

		[NonSerialized]
		private CancellationToken _cancellationToken;
	}
}
