using System;

namespace UnifiedNetwork.FunctionSelector
{
	public interface IFunctionSelector
	{
		bool RegisterFunction<FuncT>(FuncT function);

		FuncT GetFunction<FuncT>();
	}
}
