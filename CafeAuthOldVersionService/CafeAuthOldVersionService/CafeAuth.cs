using System;
using System.Net;
using Nexon.CafeAuthOld;
using ServiceCore;
using UnifiedNetwork.Cooperation;
using UnifiedNetwork.Entity;
using Utility;

namespace CafeAuthOldVersionServiceCore
{
	internal class CafeAuth
	{
		private CafeAuthOldVersionService Service { get; set; }

		private IEntity Entity { get; set; }

		public IEntityProxy FrontendConn { get; set; }

		public CafeAuth(CafeAuthOldVersionService service, IEntity entity)
		{
			this.Service = service;
			this.Entity = entity;
			this.Entity.Closed += this.Entity_Closed;
		}

        private void Entity_Closed(object sender, EventArgs e)
        {
            if (this.valid)
            {
                if (this.Service.NxIDToEntityDic.ContainsKey(this.NexonID))
                {
                    this.Service.NxIDToEntityDic.Remove(this.NexonID);
                }
                this.Service.Logout(this.NexonID, this.CharacterID, this.RemoteAddress, this.CanTry);
            }
            try
            {
                EntityDataContext entityDataContext = new EntityDataContext();
                entityDataContext.AcquireService(new long?((sender as IEntity).ID), this.Service.Category, new int?(-1), new int?(this.Service.ID));
            }
            catch (Exception ex)
            {
                Log<CafeAuth>.Logger.ErrorFormat("Entity_Closed [EntityID : {0}] [ServiceID : {1}] [Category : {2}] - {3} ", new object[]
                {
                    (sender as IEntity).ID,
                    this.Service.ID,
                    this.Service.Category,
                    ex
                });
            }
        }

        public void ForceClose()
		{
			this.Entity_Closed(this.Entity, new EventArgs());
			this.Entity.Close();
		}

		internal AsyncResultSync BeginLogin(string nexonID, string characterID, IPAddress localAddress, IPAddress remoteAddress, bool canTry, bool isTrial, MachineID machineID, int gameRoomClient, object state)
		{
			this.valid = true;
			this.NexonID = nexonID;
			this.CharacterID = characterID;
			this.RemoteAddress = remoteAddress;
			this.CanTry = canTry;
			CafeAuth cafeAuth;
			if (this.Service.NxIDToEntityDic.TryGetValue(this.NexonID, out cafeAuth))
			{
				cafeAuth.ForceClose();
			}
			this.Service.NxIDToEntityDic.Add(this.NexonID, this);
			return this.Service.BeginLogin(nexonID, characterID, localAddress, remoteAddress, canTry, isTrial, machineID, gameRoomClient, state);
		}

		private string NexonID { get; set; }

		private string CharacterID { get; set; }

		private IPAddress RemoteAddress { get; set; }

		private bool CanTry { get; set; }

		private bool valid;
	}
}