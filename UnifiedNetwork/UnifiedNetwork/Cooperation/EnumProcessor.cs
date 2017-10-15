using System;
using System.Collections.Generic;
using Utility;

namespace UnifiedNetwork.Cooperation
{
	public class EnumProcessor<TOperation, TErrorCode> : OperationProcessor<TOperation> where TOperation : Operation, IErrorCode<TErrorCode> where TErrorCode : struct, IComparable, IFormattable, IConvertible
	{
		public EnumProcessor(TOperation op, TErrorCode DefaultValue, HashSet<TErrorCode> SuccessValues) : base(op)
		{
			this.DefaultValue = DefaultValue;
			this.SuccessValues = SuccessValues;
		}

		public override IEnumerable<object> Run()
		{
			yield return null;
			IErrorCode<TErrorCode> op = base.Operation;
			if (op == null)
			{
				Log<EnumProcessor<TOperation, TErrorCode>>.Logger.Error("EnumProcessor : 오퍼레이션이 IErrorCode가 아닙니다.");
				base.Result = false;
			}
			else if (!(base.Feedback is int))
			{
				op.ErrorCode = this.DefaultValue;
				if (base.Feedback is OkMessage)
				{
					base.Result = true;
				}
				else if (base.Feedback is FailMessage)
				{
					base.Result = false;
				}
				else
				{
					base.Result = false;
					Log<EnumProcessor<TOperation, TErrorCode>>.Logger.Error("EnumProcessor : int 혹은 OkMessage, FailMessage만 받을 수 있습니다.");
				}
			}
			else
			{
				op.ErrorCode = (TErrorCode)((object)Enum.ToObject(typeof(TErrorCode), (int)base.Feedback));
				base.Result = false;
				yield return null;
				base.Result = (base.Feedback is OkMessage);
			}
			yield break;
		}

		private TErrorCode DefaultValue;

		private HashSet<TErrorCode> SuccessValues;
	}
}
