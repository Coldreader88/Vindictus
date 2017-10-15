using System;

namespace RemoteControlSystem
{
	public interface IWorkGroupStructureNode
	{
		string Name { get; }

		IWorkGroupStructureNode[] ChildNodes { get; }

		IWorkGroupCondition[] Childs { get; }

		Authority Authority { get; }

		bool IsLeafNode { get; }
	}
}
