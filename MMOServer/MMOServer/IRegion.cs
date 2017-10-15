using System;

namespace MMOServer
{
	internal interface IRegion
	{
		string BSP { get; }

		int MaxDegree { get; }
	}
}
