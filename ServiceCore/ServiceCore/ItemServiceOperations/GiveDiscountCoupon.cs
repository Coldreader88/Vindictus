using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.ItemServiceOperations
{
	[Serializable]
	public sealed class GiveDiscountCoupon : Operation
	{
		public string CouponCode { get; set; }

		public GiveDiscountCoupon.ResultEnum ErrorCode { get; set; }

		public GiveDiscountCoupon(string couponCode)
		{
			this.CouponCode = couponCode;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new GiveDiscountCoupon.Request(this);
		}

		public enum ResultEnum
		{
			Success,
			Mailed_UniqueViolation,
			Mailed_NoEmptySlot,
			Mailed_Unknown,
			Failed
		}

		private class Request : OperationProcessor<GiveDiscountCoupon>
		{
			public Request(GiveDiscountCoupon op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				if (base.Feedback is GiveDiscountCoupon.ResultEnum)
				{
					base.Operation.ErrorCode = (GiveDiscountCoupon.ResultEnum)base.Feedback;
					base.Result = true;
				}
				else
				{
					base.Operation.ErrorCode = GiveDiscountCoupon.ResultEnum.Failed;
					base.Result = false;
				}
				yield break;
			}
		}
	}
}
