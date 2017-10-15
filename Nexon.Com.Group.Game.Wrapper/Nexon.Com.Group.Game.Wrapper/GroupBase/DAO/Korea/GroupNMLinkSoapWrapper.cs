using System;
using Nexon.Com.DAO;
using Nexon.Com.Group.Game.Wrapper.hereoes;

namespace Nexon.Com.Group.Game.Wrapper.GroupBase.DAO.Korea
{
	public class GroupNMLinkSoapWrapper : SoapWrapperBase<heroes, GroupNMLinkSoapResult>
	{
		public GroupNMLinkSoapWrapper(int NexonSN)
		{
			this._NexonSN = NexonSN;
		}

		protected override void ExecuteImpl(out int errorCode, out string errorMessage)
		{
			errorCode = base.Soap.RefreshNMLink(this._NexonSN);
			errorMessage = string.Empty;
		}

		protected override ServiceCode serviceCode
		{
			get
			{
				return ServiceCode.guildingame;
			}
		}

		private int _NexonSN;
	}
}
