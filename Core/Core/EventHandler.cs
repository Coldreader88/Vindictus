using System;

namespace Devcat.Core
{
	[Serializable]
	public delegate void EventHandler<TSender, TEventArgs>(TSender sender, TEventArgs e) where TEventArgs : EventArgs;
}
