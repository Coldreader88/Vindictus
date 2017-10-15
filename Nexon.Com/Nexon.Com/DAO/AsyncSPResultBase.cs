using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Nexon.Com.DAO
{
	public abstract class AsyncSPResultBase
	{
		public AsyncSPResultBase()
		{
			this.SPFrameworkParameters = new List<SPFrameworkParameters>();
			this.ExceptionList = new List<Exception>();
		}

		public List<SPFrameworkParameters> SPFrameworkParameters { get; set; }

		public List<Exception> ExceptionList { get; set; }

		[MethodImpl(MethodImplOptions.Synchronized)]
		public void AddException(Exception ex)
		{
			this.ExceptionList.Add(ex);
		}

		[MethodImpl(MethodImplOptions.Synchronized)]
		public void AddSPFrameworkParameter(SPFrameworkParameters SPFrameworkParameter)
		{
			this.SPFrameworkParameters.Add(SPFrameworkParameter);
		}
	}
}
