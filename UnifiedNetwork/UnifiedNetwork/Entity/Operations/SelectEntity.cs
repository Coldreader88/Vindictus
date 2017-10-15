using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;
using Utility;

namespace UnifiedNetwork.Entity.Operations
{
	[Serializable]
	internal sealed class SelectEntity : Operation
	{
		public new SelectEntity.ResultCode Result
		{
			get
			{
				return this.result;
			}
		}

		public int RedirectServiceID
		{
			get
			{
				return this.redirectServiceID;
			}
		}

		public long ID { get; set; }

		public string Category { get; set; }

		public override OperationProcessor RequestProcessor()
		{
			return new SelectEntity.Request(this);
		}

		[NonSerialized]
		private SelectEntity.ResultCode result;

		[NonSerialized]
		private int redirectServiceID;

		public enum ResultCode : byte
		{
			Unknown,
			Ok,
			Redirect,
			NotExists,
			Error
		}

		private class Request : OperationProcessor<SelectEntity>
		{
			public Request(SelectEntity op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				if (!(base.Feedback is SelectEntity.ResultCode))
				{
					base.Result = false;
					base.Finished = true;
					Log<SelectEntity>.Logger.ErrorFormat("Invalid 1st Message in SelectEntity : [{0}]", base.Feedback);
				}
				else
				{
					base.Operation.result = (SelectEntity.ResultCode)base.Feedback;
					if (base.Operation.Result == SelectEntity.ResultCode.Redirect)
					{
						yield return null;
						if (!(base.Feedback is int))
						{
							base.Result = false;
							base.Finished = true;
							Log<SelectEntity>.Logger.ErrorFormat("Invalid 2nd Message in SelectEntity : [{0}]", base.Feedback);
						}
						else
						{
							base.Operation.redirectServiceID = (int)base.Feedback;
						}
					}
				}
				yield break;
			}
		}
	}
}
