using System;
using System.Runtime.Serialization;

namespace Nexon.Enterprise.ServiceFacade
{
	[DataContract]
	public class PriorityContext
	{
		public static CallPriority Priority
		{
			get
			{
				return GenericContext<CallPriority>.Current.Value;
			}
			set
			{
				GenericContext<CallPriority>.Current = new GenericContext<CallPriority>(value);
			}
		}
	}
}
