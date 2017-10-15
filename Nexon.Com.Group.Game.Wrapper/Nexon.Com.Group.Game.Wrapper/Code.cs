using System;

namespace Nexon.Com.Group.Game.Wrapper
{
	public class Code
	{
		public int Code_Group(int n4GameCode, int n4ServerCode)
		{
			int result;
			if (GroupPlatform.isOverSea)
			{
				result = n4GameCode;
			}
			else
			{
				if (!Enum.IsDefined(typeof(GameCode), n4GameCode))
				{
					throw new ArgumentException("Unknown GameCode");
				}
				if (n4ServerCode == 1)
				{
					result = 268435471;
				}
				else
				{
					if (n4ServerCode != 11)
					{
						throw new ArgumentException("Unknown Heroes ServerCode");
					}
					result = 268435472;
				}
			}
			return result;
		}
	}
}
