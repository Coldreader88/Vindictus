using System;
using UnifiedNetwork.Cooperation;
using Utility;

namespace UnifiedNetwork.Entity.Test
{
	internal class TestOpProcessor : ReturnOneProcessor<TestOp, Box<long>>, IEntityProcessor
	{
		public IEntity Callee { get; set; }

		public TestOpProcessor(Service service, TestOp op) : base(op)
		{
			this.service = service;
		}

		public override Box<long> Process()
		{
			Log<TestOpProcessor>.Logger.DebugFormat("{0} + {1} + {2}", this.Callee.ID, this.service.ID, base.Operation.Param);
			return new Box<long>
			{
				Value = this.Callee.ID + (long)this.service.ID + (long)base.Operation.Param
			};
		}

		private Service service;
	}
}
