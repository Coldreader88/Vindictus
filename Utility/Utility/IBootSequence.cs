using System;

namespace Utility
{
	public interface IBootSequence
	{
		bool BootResult { get; }

		bool IsBooting { get; }

		void OnBootSuccess(Action action);

		void OnBootFail(Action action);

		void BootFail();

		void AddBootStep();

		void BootStep();
	}
}
