using System;
using System.Collections.Generic;
using Nexon.Com.DAO;
using Nexon.Com.Group.Game.Wrapper.GroupBase.DataSource;
using Nexon.Com.Log;

namespace Nexon.Com.Group.Game.Wrapper.GroupBase.DAO
{
	internal abstract class GroupSPWrapperBase<TResult> : SPWrapperBase<TResult> where TResult : SPResultBase, new()
	{
		internal GroupSPWrapperBase()
		{
			this.CommandTimeout = GroupPlatform.ConnectionTimeout;
			this.SQLConnectionStringProvider = GroupConnectionStringProvider.GetInstalce;
		}

		protected override bool HandleDBError(Exception e)
		{
			List<string> arrLogElements = new List<string>
			{
				e.Message,
				e.StackTrace,
				base.Dump()
			};
			FileLog.CreateLog("", string.Format("GroupLog_{0}.log", DateTime.Today.ToString("yyyyMMddHHMM")), arrLogElements);
			return false;
		}

		protected override void HandleSPExecuteError()
		{
			List<string> arrLogElements = new List<string>
			{
				base.Dump()
			};
			FileLog.CreateLog("", string.Format("GroupLog_{0}.log", DateTime.Today.ToString("yyyyMMddHHMM")), arrLogElements);
			TResult result = base.Result;
			if (result.SPErrorCode < 0)
			{
				result = base.Result;
				int sperrorCode = result.SPErrorCode;
				result = base.Result;
				throw new SPFatalException(sperrorCode, result.SPErrorMessage, base.Dump());
			}
			result = base.Result;
			if (result.SPErrorCode > 0)
			{
				result = base.Result;
				int sperrorCode2 = result.SPErrorCode;
				result = base.Result;
				throw new SPLogicalException(sperrorCode2, result.SPErrorMessage, base.Dump());
			}
		}

		protected override ServiceCode serviceCode
		{
			get
			{
				return ServiceCode.guild;
			}
		}
	}
}
