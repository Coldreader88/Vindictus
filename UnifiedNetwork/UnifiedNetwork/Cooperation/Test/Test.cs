using System;
using System.Collections.Generic;

namespace UnifiedNetwork.Cooperation.Test
{
	public class Test
	{
		public Test()
		{
			TestOp testOp = new TestOp
			{
				Value = 5
			};
			OperationProcessor operationProcessor = testOp.RequestProcessor();
			OperationProcessor operationProcessor2 = new TestOpProcessor(7, testOp);
			Queue<object> reqq = new Queue<object>();
			Queue<object> procq = new Queue<object>();
			operationProcessor.OnSend += delegate(object msg)
			{
				procq.Enqueue(msg);
			};
			operationProcessor2.OnSend += delegate(object msg)
			{
				reqq.Enqueue(msg);
			};
			operationProcessor.Start();
			operationProcessor2.Start();
			do
			{
				if (reqq.Count > 0)
				{
					operationProcessor.ProcessFeedback(reqq.Dequeue());
				}
				if (procq.Count > 0)
				{
					operationProcessor2.ProcessFeedback(procq.Dequeue());
				}
			}
			while (!operationProcessor2.Finished || !operationProcessor.Finished);
		}
	}
}
