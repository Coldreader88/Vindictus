using System;
using UnifiedNetwork.Entity;

namespace TalkService
{
	internal class TalkClient
	{
		public TalkService Service { get; private set; }

		public IEntity Entity { get; private set; }

		public IEntityProxy FrontendConn { get; set; }

		public TalkClient(TalkService service, IEntity entity)
		{
			this.Service = service;
			this.Entity = entity;
		}
	}
}
