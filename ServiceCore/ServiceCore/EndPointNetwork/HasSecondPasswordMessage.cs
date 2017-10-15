using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class HasSecondPasswordMessage : IMessage
	{
		public bool IsFirstQuery { get; set; }

		public bool HasSecondPassword { get; set; }

		public bool IsPassed { get; set; }

		public int FailCount { get; set; }

		public int RetryLockedSec { get; set; }

		public HasSecondPasswordMessage(bool isFirstQuery, bool hasSecondPassword, bool isPassed, int failCount, DateTime? retryRestrictedUntil)
		{
			this.IsFirstQuery = isFirstQuery;
			this.HasSecondPassword = hasSecondPassword;
			this.IsPassed = isPassed;
			this.FailCount = failCount;
			if (retryRestrictedUntil == null || retryRestrictedUntil <= DateTime.UtcNow)
			{
				this.RetryLockedSec = -1;
				return;
			}
			this.RetryLockedSec = (int)(retryRestrictedUntil.Value - DateTime.UtcNow).TotalSeconds;
		}

		public HasSecondPasswordMessage(bool isFirstQuery, bool hasSecondPassword, bool IsPassed, int failCount, int retryLockedSec)
		{
			this.IsFirstQuery = isFirstQuery;
			this.HasSecondPassword = hasSecondPassword;
			this.IsPassed = IsPassed;
			this.FailCount = failCount;
			this.RetryLockedSec = retryLockedSec;
		}

		public override string ToString()
		{
			return string.Format("HasSecondPasswordMessage [ isFirst = {0} Has = {1} Fail = {2} {3}]", new object[]
			{
				this.IsFirstQuery,
				this.HasSecondPassword,
				this.FailCount,
				this.RetryLockedSec
			});
		}
	}
}
