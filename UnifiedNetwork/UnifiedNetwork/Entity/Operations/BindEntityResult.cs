using System;

namespace UnifiedNetwork.Entity.Operations
{
	public enum BindEntityResult
	{
		Unknown,
		Success_NewEntity,
		Success_Existing,
		Fail_NoExistingEntity,
		Fail_InvalidServiceID,
		Fail_Exception
	}
}
