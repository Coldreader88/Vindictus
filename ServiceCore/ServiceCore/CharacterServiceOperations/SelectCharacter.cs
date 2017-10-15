using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;
using Utility;

namespace ServiceCore.CharacterServiceOperations
{
	[Serializable]
	public sealed class SelectCharacter : Operation
	{
		public string NexonID { get; set; }

		public int UserID { get; set; }

		public int CafeType { get; set; }

		public int Limited { get; set; }

		public bool IsReturn { get; set; }

		public int UserCareType { get; set; }

		public int UserCareNextState { get; set; }

		public bool IsNeedUserCareMeetingState { get; set; }

		public int LoginIP { get; set; }

		public byte[] MachineID { get; set; }

		public byte Character
		{
			get
			{
				return this.character;
			}
		}

		public string CharacterName
		{
			get
			{
				return this.name;
			}
		}

		public int CharacterLevel
		{
			get
			{
				return this.level;
			}
		}

		public int RegionCode
		{
			get
			{
				return this.regionCode;
			}
		}

		public byte MapStateInfo
		{
			get
			{
				return this.mapStateInfo;
			}
		}

		public SelectCharacter(string nexonID, int userID, int cafeType, int limited, bool isReturn, int userCareType, int userCareNextState, bool isNeedUserCareMeetingState, int loginIP, byte[] machineID)
		{
			this.NexonID = nexonID;
			this.UserID = userID;
			this.CafeType = cafeType;
			this.Limited = limited;
			this.IsReturn = isReturn;
			this.UserCareType = userCareType;
			this.UserCareNextState = userCareNextState;
			this.IsNeedUserCareMeetingState = isNeedUserCareMeetingState;
			this.LoginIP = loginIP;
			this.MachineID = machineID;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new SelectCharacter.Request(this);
		}

		[NonSerialized]
		private byte character;

		[NonSerialized]
		private string name;

		[NonSerialized]
		private int level;

		[NonSerialized]
		private int regionCode;

		[NonSerialized]
		private byte mapStateInfo;

		private class Request : OperationProcessor<SelectCharacter>
		{
			public Request(SelectCharacter op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				if (base.Feedback is byte)
				{
					base.Operation.character = (byte)base.Feedback;
					yield return null;
					base.Operation.name = (base.Feedback as string);
					yield return null;
					base.Operation.level = (int)base.Feedback;
					yield return null;
					base.Operation.regionCode = (int)base.Feedback;
					yield return null;
					base.Operation.mapStateInfo = (byte)base.Feedback;
				}
				else
				{
					Log<SelectCharacter>.Logger.ErrorFormat("캐릭터 선택 실패 : [{0}]", base.Feedback);
					base.Result = false;
				}
				yield break;
			}
		}
	}
}
