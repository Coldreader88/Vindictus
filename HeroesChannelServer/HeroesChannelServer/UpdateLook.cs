using System;
using MMOServer;
using ServiceCore.CharacterServiceOperations;

namespace HeroesChannelServer
{
	public class UpdateLook : IModifier
	{
		public long ID { get; private set; }

		public string Category
		{
			get
			{
				return "UpdateLook";
			}
		}

		public IComponentSpace Space { get; set; }

		public UpdateLook(long id, CharacterSummary data)
		{
			this.ID = id;
			this.data = data;
		}

		public void Apply()
		{
			Look look = this.Space.Find(this.ID, "Look") as Look;
			if (look == null)
			{
				return;
			}
			look.Data = this.data;
			this.Space.NotifyModified(look);
		}

		private CharacterSummary data;
	}
}
