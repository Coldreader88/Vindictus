using System;

namespace DSService.WaitingQueue
{
	public enum DSShipState
	{
		Initial,
		Launching,
		Launched,
		LaunchFail,
		Finished
	}
}
