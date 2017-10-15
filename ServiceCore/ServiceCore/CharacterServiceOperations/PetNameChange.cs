using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.CharacterServiceOperations
{
	[Serializable]
	public sealed class PetNameChange : Operation
	{
		public long CID { get; set; }

		public long PetID { get; set; }

		public string PetName { get; set; }

		public long? NextPetID { get; set; }

		public string NextPetName { get; set; }

		public PetNameChange(long cid, long petid, string requestName)
		{
			this.CID = cid;
			this.PetID = petid;
			this.PetName = requestName;
			this.NextPetID = null;
			this.NextPetName = null;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new PetNameChange.Request(this);
		}

		public PetNameChange.ResultEnum ResultCode;

		public enum ResultEnum
		{
			Unknown,
			Success,
			Success_NextPet,
			Fail_ForbiddenName,
			Fail_AgainstNamingRule,
			Fail_PetNameCheckFailed,
			Fail_PetNameUpdateFailed
		}

		private class Request : OperationProcessor<PetNameChange>
		{
			public Request(PetNameChange op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				if (base.Feedback is PetNameChange.ResultEnum)
				{
					base.Operation.ResultCode = (PetNameChange.ResultEnum)base.Feedback;
					if (base.Operation.ResultCode == PetNameChange.ResultEnum.Success)
					{
						base.Result = true;
					}
					else if (base.Operation.ResultCode == PetNameChange.ResultEnum.Success_NextPet)
					{
						base.Result = true;
						yield return null;
						base.Operation.NextPetID = new long?((long)base.Feedback);
						yield return null;
						base.Operation.NextPetName = (string)base.Feedback;
					}
					else
					{
						base.Result = false;
					}
				}
				else
				{
					base.Result = false;
					base.Operation.ResultCode = PetNameChange.ResultEnum.Unknown;
				}
				yield break;
			}
		}
	}
}
