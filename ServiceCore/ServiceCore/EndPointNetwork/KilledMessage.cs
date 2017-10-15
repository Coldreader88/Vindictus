using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class KilledMessage : IMessage
	{
		public int Tag
		{
			get
			{
				return this.uid;
			}
		}

		public int TargetKind
		{
			get
			{
				return this.targetKind;
			}
		}

		public string TargetName
		{
			get
			{
				return this.targetName;
			}
		}

		public KilledMessage(int uid, int targetKind, string targetName)
		{
			this.uid = uid;
			this.targetKind = targetKind;
			this.targetName = targetName;
		}

		public override string ToString()
		{
			return string.Format("KilledMessage[ uid = {0} kind = {1} name = {2} ]", this.uid, this.targetKind, this.targetName);
		}

		private int uid;

		private int targetKind;

		private string targetName;
	}
}
