using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace UnifiedNetwork.LocationService.Test
{
	[Serializable]
	internal sealed class Plus1 : Operation
	{
		public int Input
		{
			get
			{
				return this.input;
			}
			set
			{
				this.input = value;
			}
		}

		public int Output
		{
			get
			{
				return this.output;
			}
		}

		public override OperationProcessor RequestProcessor()
		{
			return new Plus1.Request(this);
		}

		private int input;

		[NonSerialized]
		private int output;

		private class Request : OperationProcessor<Plus1>
		{
			public Request(Plus1 op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				base.Operation.output = (int)base.Feedback;
				yield break;
			}
		}
	}
}
