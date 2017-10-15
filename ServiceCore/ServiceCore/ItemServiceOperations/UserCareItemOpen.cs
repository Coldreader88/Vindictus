using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.ItemServiceOperations
{
	[Serializable]
	public sealed class UserCareItemOpen : Operation
	{
		public int UserCareType { get; private set; }

		public int UserCareNextState { get; private set; }

		public int ItemIndex { get; private set; }

		public int ReturnState
		{
			get
			{
				return this.returnState;
			}
		}

		public bool IsNeedUserCareMeetingState
		{
			get
			{
				return this.isNeedUserCareMeetingState;
			}
		}

		public UserCareItemOpen.FailReason Reason
		{
			get
			{
				return this.reason;
			}
		}

		public UserCareItemOpen(int userCareType, int userCareNextState, int itemIndex)
		{
			this.UserCareType = userCareType;
			this.UserCareNextState = userCareNextState;
			this.ItemIndex = itemIndex;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new UserCareItemOpen.Request(this);
		}

		[NonSerialized]
		private int returnState;

		[NonSerialized]
		private bool isNeedUserCareMeetingState;

		[NonSerialized]
		private UserCareItemOpen.FailReason reason;

		public enum FailReason
		{
			None,
			EntityFail,
			StateFail,
			ItemDataFail,
			ItemInfoFail,
			ItemOpenFail
		}

		private class Request : OperationProcessor<UserCareItemOpen>
		{
			public Request(UserCareItemOpen op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				if (base.Feedback is int)
				{
					base.Result = true;
					base.Operation.returnState = (int)base.Feedback;
					yield return null;
					base.Operation.isNeedUserCareMeetingState = (bool)base.Feedback;
				}
				else if (base.Feedback is UserCareItemOpen.FailReason)
				{
					base.Result = false;
					base.Operation.reason = (UserCareItemOpen.FailReason)base.Feedback;
				}
				yield break;
			}
		}
	}
}
