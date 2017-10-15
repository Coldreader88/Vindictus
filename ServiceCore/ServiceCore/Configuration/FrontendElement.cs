using System;
using System.Configuration;

namespace ServiceCore.Configuration
{
	public class FrontendElement : ConfigurationElement
	{
		[ConfigurationProperty("capacity", DefaultValue = 65536)]
		[IntegerValidator(MinValue = 0)]
		public int Capacity
		{
			get
			{
				return (int)base["capacity"];
			}
			set
			{
				base["capacity"] = value;
			}
		}

		[IntegerValidator(MinValue = 0)]
		[ConfigurationProperty("maximumtickets", DefaultValue = 512)]
		public int MaximumTickets
		{
			get
			{
				return (int)base["maximumtickets"];
			}
			set
			{
				base["maximumtickets"] = value;
			}
		}

		[IntegerValidator(MinValue = 0, MaxValue = 65535)]
		[ConfigurationProperty("tcpPort", DefaultValue = 27015)]
		public int TcpPort
		{
			get
			{
				return (int)base["tcpPort"];
			}
			set
			{
				base["tcpPort"] = value;
			}
		}

		[ConfigurationProperty("pingpong", DefaultValue = false)]
		public bool PingPong
		{
			get
			{
				return bool.Parse(base["pingpong"].ToString());
			}
			set
			{
				base["pingpong"] = value;
			}
		}

		[ConfigurationProperty("isCafeUserAvailable", DefaultValue = true)]
		public bool IsCafeUserAvailable
		{
			get
			{
				return bool.Parse(base["isCafeUserAvailable"].ToString());
			}
			set
			{
				base["isCafeUserAvailable"] = value;
			}
		}

		[ConfigurationProperty("isPackageUserAvailable", DefaultValue = true)]
		public bool IsPackageUserAvailable
		{
			get
			{
				return bool.Parse(base["isPackageUserAvailable"].ToString());
			}
			set
			{
				base["isPackageUserAvailable"] = value;
			}
		}

		[ConfigurationProperty("isPublicUserAvailable", DefaultValue = true)]
		public bool IsPublicUserAvailable
		{
			get
			{
				return bool.Parse(base["isPublicUserAvailable"].ToString());
			}
			set
			{
				base["isPublicUserAvailable"] = value;
			}
		}

		[ConfigurationProperty("KickUnregisteredCafe", DefaultValue = false)]
		public bool KickUnregisteredCafe
		{
			get
			{
				return bool.Parse(base["KickUnregisteredCafe"].ToString());
			}
			set
			{
				base["KickUnregisteredCafe"] = value;
			}
		}

		[ConfigurationProperty("cafeKinds", DefaultValue = CafeKinds.All)]
		public CafeKinds CafeKinds
		{
			get
			{
				CafeKinds result;
				try
				{
					result = (CafeKinds)Enum.Parse(typeof(CafeKinds), base["cafeKinds"].ToString());
				}
				catch (ArgumentException)
				{
					result = (CafeKinds)0;
				}
				return result;
			}
			set
			{
				base["cafeKinds"] = value;
			}
		}
	}
}
