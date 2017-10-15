using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.ItemServiceOperations.PetOperations
{
	[Serializable]
	public sealed class PetNameCheck : Operation
	{
		public bool IsSuccess
		{
			get
			{
				return this.isSuccess;
			}
		}

		public string ResultType
		{
			get
			{
				return this.resultType;
			}
		}

		public string PetName { get; set; }

		public PetNameCheck(string petName)
		{
			this.PetName = petName;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new PetNameCheck.Request(this);
		}

		[NonSerialized]
		private bool isSuccess;

		[NonSerialized]
		private string resultType;

		private class Request : OperationProcessor<PetNameCheck>
		{
			public Request(PetNameCheck op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				base.Operation.isSuccess = false;
				base.Operation.resultType = "SystemError";
				yield return null;
				if (base.Feedback is bool)
				{
					base.Operation.isSuccess = (bool)base.Feedback;
					base.Result = base.Operation.isSuccess;
					yield return null;
					base.Operation.resultType = (string)base.Feedback;
				}
				else
				{
					base.Result = false;
				}
				yield break;
			}
		}
	}
}
