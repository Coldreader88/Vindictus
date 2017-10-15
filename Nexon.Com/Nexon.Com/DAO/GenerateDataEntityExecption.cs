using System;

namespace Nexon.Com.DAO
{
	public class GenerateDataEntityExecption : Exception
	{
		public GenerateDataEntityExecption(string errorMessage, Exception ex) : base(errorMessage, ex)
		{
		}
	}
}
