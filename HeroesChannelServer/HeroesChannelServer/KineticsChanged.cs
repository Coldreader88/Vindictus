using System;
using MMOServer;
using ServiceCore.EndPointNetwork;

namespace HeroesChannelServer
{
	public class KineticsChanged : IModifier
	{
		public long ID { get; private set; }

		public string Category
		{
			get
			{
				return "Move";
			}
		}

		public IComponentSpace Space { get; set; }

		public KineticsChanged(long id, ActionSync state)
		{
			this.ID = id;
			this.state = state;
		}

		public void Apply()
		{
			Kinetics kinetics = this.Space.Find(this.ID, "Kinetics") as Kinetics;
			if (kinetics == null)
			{
				return;
			}
			kinetics.State = this.state;
			this.Space.NotifyModified(kinetics);
		}

		private ActionSync state;
	}
}
