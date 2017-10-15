using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class PropBrokenMessage : IMessage
	{
		public int BrokenProp
		{
			get
			{
				return this.prop;
			}
		}

		public string EntityName
		{
			get
			{
				return this.entityName;
			}
		}

		public int Attacker
		{
			get
			{
				return this.attacker;
			}
		}

		public PropBrokenMessage(int prop, string entityName, int attacker)
		{
			this.prop = prop;
			this.entityName = entityName;
			this.attacker = attacker;
		}

		public override string ToString()
		{
			return string.Format("PropBrokenMessage[ prop = {0} entityName = {1} attacker = {2} ]", this.prop, this.entityName, this.attacker);
		}

		private int prop;

		private string entityName;

		private int attacker;
	}
}
