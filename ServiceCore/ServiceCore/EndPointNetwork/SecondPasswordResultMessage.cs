using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class SecondPasswordResultMessage : IMessage
	{
		public SecondPasswordResultMessage.ProcessType OperationType { get; set; }

		public bool Passed { get; set; }

		public int FailCount { get; set; }

		public int RetryLockedSec { get; set; }

		public SecondPasswordResultMessage(SecondPasswordResultMessage.ProcessType operationType, bool passed, int failCount, DateTime? retryRestrictedUntil)
		{
			this.OperationType = operationType;
			this.Passed = passed;
			this.FailCount = failCount;
			if (retryRestrictedUntil == null || retryRestrictedUntil <= DateTime.UtcNow)
			{
				this.RetryLockedSec = -1;
				return;
			}
			this.RetryLockedSec = (int)(retryRestrictedUntil.Value - DateTime.UtcNow).TotalSeconds;
		}

		public SecondPasswordResultMessage(SecondPasswordResultMessage.ProcessType operationType, bool passed, int failCount, int retryLockedSec)
		{
			this.OperationType = operationType;
			this.Passed = passed;
			this.FailCount = failCount;
			this.RetryLockedSec = retryLockedSec;
		}

		public override string ToString()
		{
			return string.Format("SecondPasswordResultMessage [ {0} Passed = {1} Fail = {2} {3}]", new object[]
			{
				this.OperationType.ToString(),
				this.Passed,
				this.FailCount,
				this.RetryLockedSec
			});
		}

		public enum ProcessType
		{
			Check,
			Register,
			Clear,
			RegisterLimit
		}
	}
}
