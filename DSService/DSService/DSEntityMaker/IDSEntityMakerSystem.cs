using System;
using ServiceCore.DSServiceOperations;

namespace DSService.DSEntityMaker
{
	public interface IDSEntityMakerSystem
	{
		void AddServiceInfo(int serviceID, int coreCount);

		void Enqueue(long id, DSType dsType);

		void Enqueue(long id, DSType dsType, int pvpServiceID);

		void Dequeue(long id, DSType dsType);

		void Process();

		void UnuseEntity(int serviceID, int dsID);

		void CloseEntity(int serviceID, int dsID);
	}
}
