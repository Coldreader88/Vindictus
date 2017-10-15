using System;
using UnifiedNetwork.Cooperation;
using UnifiedNetwork.Entity.Operations;

namespace UnifiedNetwork.Entity
{
	public class GetEntitySync : ISynchronizable
	{
		public Service Service { get; set; }

		public long EntityID { get; set; }

		public string EntityCategory { get; set; }

		public IEntity Entity { get; private set; }

		public void OnSync()
		{
			this.Service.BeginGetEntity(this.EntityID, this.EntityCategory, delegate(IEntity entity, BindEntityResult result)
			{
				this.Entity = entity;
				this.OnFinished(this);
			}, true);
		}

		public event Action<ISynchronizable> OnFinished;

		public bool Result
		{
			get
			{
				return this.Entity != null;
			}
		}
	}
}
