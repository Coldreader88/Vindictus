using System;
using ServiceCore.CharacterServiceOperations;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class VocationLevelUpMessage : IMessage
	{
		public VocationEnum VocationClass { get; set; }

		public int Level { get; set; }

		public VocationLevelUpMessage(VocationEnum vocationClass, int level)
		{
			this.VocationClass = vocationClass;
			this.Level = level;
		}

		public override string ToString()
		{
			return string.Format("VocationLevelUpMessage [ {0} ]", this.Level);
		}
	}
}
