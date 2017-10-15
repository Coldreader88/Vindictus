using System;
using System.Collections.Generic;
using HeroesChannelServer;
using ServiceCore.CharacterServiceOperations;
using ServiceCore.EndPointNetwork;
using ServiceCore.FrontendServiceOperations;
using ServiceCore.ItemServiceOperations;
using ServiceCore.MicroPlayServiceOperations;
using UnifiedNetwork.CacheSync;
using UnifiedNetwork.Cooperation;
using UnifiedNetwork.Entity;

namespace MMOChannelService
{
	internal class ChannelMember
	{
		public long CID { get; private set; }

		public long FID
		{
			get
			{
				return this.FrontendAdapter.RemoteID;
			}
		}

		public Client Client { get; set; }

		public IEntityAdapter FrontendAdapter
		{
			get
			{
				return this.fAdapter;
			}
			set
			{
				this.fAdapter = value;
				if (this.fAdapter != null)
				{
					this.fAdapter.Tag = this;
				}
			}
		}

		public IEntityProxy FrontendConn
		{
			get
			{
				return this.fConn;
			}
			set
			{
				this.fConn = value;
				if (this.fConn != null)
				{
					this.fConn.Tag = this;
				}
			}
		}

		public IEntityProxy CharacterConn { get; set; }

		public ChannelEntity ChannelJoined { get; set; }

		public long InitialPartitionID { get; private set; }

		public ActionSync InitialAction { get; private set; }

		public IObserver LookObserver { get; private set; }

		public IObserver EquipmentObserver { get; private set; }

		public CharacterSummary Look { get; private set; }

		public SharingInfo SharingInfo { get; set; }

		public bool IsInChannel { get; set; }

		public event EventHandler Closed;

		public ChannelMember(long cid, IEntityAdapter fa, long ipid, ActionSync iaction)
		{
			this.CID = cid;
			this.FrontendAdapter = fa;
			this.InitialPartitionID = ipid;
			this.InitialAction = iaction;
			this.IsInChannel = false;
		}

		public void Close()
		{
			if (this.ChannelJoined != null && this.IsInChannel)
			{
				this.ChannelJoined.Leave(this);
				this.IsInChannel = false;
			}
			if (this.FrontendAdapter != null && !this.FrontendAdapter.IsClosed)
			{
				this.FrontendAdapter.Close();
			}
			if (this.FrontendConn != null && !this.FrontendConn.IsClosed)
			{
				this.FrontendConn.Close();
			}
			if (this.CharacterConn != null && !this.CharacterConn.IsClosed)
			{
				this.CharacterConn.Close();
			}
			if (this.Client != null)
			{
				this.Client.Close();
				this.Client = null;
			}
			if (this.Closed != null)
			{
				this.Closed(this, null);
			}
		}

		public void BindCharacterConn(IEntityProxy conn)
		{
			this.CharacterConn = conn;
			this.LookObserver = ObserverFactory.MakeObserver(conn, this.CID, "Stat");
			this.LookObserver.SyncBegun += delegate(IObserver observer, Action<IObserver> callback)
			{
				observer.EndSync(true);
				callback(observer);
			};
			this.LookObserver.ForceSync(delegate(IObserver _)
			{
			});
			this.LookObserver.SetDirty += this.LookObserver_SetDirty;
			this.LookObserver_SetDirty(this.LookObserver, delegate(IObserver _)
			{
			});
			this.EquipmentObserver = ObserverFactory.MakeObserver(conn, this.CID, "Equipment");
			this.EquipmentObserver.SyncBegun += delegate(IObserver observer, Action<IObserver> callback)
			{
				observer.EndSync(true);
				callback(observer);
			};
			this.EquipmentObserver.ForceSync(delegate(IObserver _)
			{
			});
			this.EquipmentObserver.SetDirty += this.EquipmentObserver_SetDirty;
			this.EquipmentObserver_SetDirty(this.EquipmentObserver, delegate(IObserver _)
			{
			});
		}

		private void LookObserver_SetDirty(IObserver observer, Action<IObserver> callback)
		{
			WhoIs whois = new WhoIs(WhoIsOption.Stat | WhoIsOption.Costume);
			whois.OnComplete += delegate(Operation op)
			{
				observer.Cache = whois;
				this.Look = whois.Summary;
				if (this.Client != null)
				{
					if (!this.lookUpdated && this.ChannelJoined != null)
					{
						this.Client.Enter(this.ChannelJoined.Channel, this.InitialPartitionID, this.InitialAction, this.Look);
						this.lookUpdated = true;
					}
					else if (this.Client.Player != null)
					{
						this.Client.Player.UpdateLook(this.Look);
					}
				}
				observer.EndSync(true);
				callback(observer);
			};
			whois.OnFail += delegate(Operation op)
			{
				observer.EndSync(false);
				callback(observer);
			};
			observer.Connection.RequestOperation(whois);
		}

		private void EquipmentObserver_SetDirty(IObserver observer, Action<IObserver> callback)
		{
			QueryConsumablesInTown consumableQuery = new QueryConsumablesInTown
			{
				CID = this.CID
			};
			consumableQuery.OnComplete += delegate(Operation op)
			{
				observer.Cache = consumableQuery;
				if (consumableQuery.Consumables != null)
				{
					BattleInventory battleInventory = new BattleInventory();
					foreach (KeyValuePair<int, ConsumablesInfo> keyValuePair in consumableQuery.Consumables)
					{
						battleInventory.SetConsumable(keyValuePair.Key, keyValuePair.Value);
					}
					this.FrontendConn.RequestOperation(SendPacket.Create<UpdateBattleInventoryInTownMessage>(new UpdateBattleInventoryInTownMessage
					{
						BattleInventory = battleInventory
					}));
				}
				observer.EndSync(true);
				callback(observer);
			};
			consumableQuery.OnFail += delegate(Operation op)
			{
				observer.EndSync(false);
				callback(observer);
			};
			observer.Connection.RequestOperation(consumableQuery);
		}

		private IEntityAdapter fAdapter;

		private IEntityProxy fConn;

		private bool lookUpdated;
	}
}
