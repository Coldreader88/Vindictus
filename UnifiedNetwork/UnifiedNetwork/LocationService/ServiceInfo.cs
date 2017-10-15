using System;
using System.Net;

namespace UnifiedNetwork.LocationService
{
	[Serializable]
	public sealed class ServiceInfo
	{
		public int ID { get; set; }

		public IPEndPoint EndPoint { get; set; }

		public byte Suffix { get; set; }

		public int GameCode { get; set; }

		public int ServerCode { get; set; }

		public string FullName { get; set; }

		public Guid GUID { get; set; }

		public Guid ModuleVersionId { get; set; }

		public int ServiceOrder { get; set; }

		public int LocalServiceOrder { get; set; }
	}
}
