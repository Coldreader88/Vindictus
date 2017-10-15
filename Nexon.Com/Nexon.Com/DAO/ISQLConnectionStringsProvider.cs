using System;

namespace Nexon.Com.DAO
{
	public interface ISQLConnectionStringsProvider
	{
		string[] GetConnectionStrings();
	}
}
