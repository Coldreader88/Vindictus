using System;

namespace UnifiedNetwork.Cooperation
{
	public interface IErrorCode<TErrorCode> where TErrorCode : struct, IComparable, IFormattable, IConvertible
	{
		TErrorCode ErrorCode { get; set; }
	}
}
