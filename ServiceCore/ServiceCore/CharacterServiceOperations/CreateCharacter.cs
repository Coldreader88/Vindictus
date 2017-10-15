using System;
using System.Collections.Generic;
using ServiceCore.EndPointNetwork;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.CharacterServiceOperations
{
	[Serializable]
	public sealed class CreateCharacter : Operation
	{
		public string NexonID { get; set; }

		public int NexonSN { get; set; }

		public string Name { get; set; }

		public CharacterTemplate Template { get; set; }

		public bool IsCheat { get; set; }

		public bool UsePreset { get; set; }

		public bool IsPremium { get; set; }

		public CreateCharacterResult ErrorCode
		{
			get
			{
				return this.errorcode;
			}
			set
			{
				this.errorcode = value;
			}
		}

		public CreateCharacter(string nexonID, int nexonSN, string name, CharacterTemplate template, bool isCheat, bool usePreset, bool isPremium)
		{
			this.NexonID = nexonID;
			this.NexonSN = nexonSN;
			this.Name = name;
			this.Template = template;
			this.IsCheat = isCheat;
			this.UsePreset = usePreset;
			this.IsPremium = isPremium;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new CreateCharacter.Request(this);
		}

		[NonSerialized]
		private CreateCharacterResult errorcode;

		public long CreatedCID;

		private class Request : OperationProcessor<CreateCharacter>
		{
			public Request(CreateCharacter op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				base.Finished = true;
				base.Result = (base.Feedback is long);
				if (base.Result)
				{
					base.Operation.CreatedCID = (long)base.Feedback;
				}
				else
				{
					base.Operation.ErrorCode = (CreateCharacterResult)base.Feedback;
					base.Operation.CreatedCID = -1L;
				}
				yield break;
			}
		}
	}
}
