using System;
using ServiceCore.Properties;

namespace ServiceCore
{
	public static class ServiceCoreSettings
	{
		public static Settings Default
		{
			get
			{
				return Settings.Default;
			}
		}

		public static int ServerCode
		{
			get
			{
				if (!FeatureMatrix.IsEnable("koKR"))
				{
					return FeatureMatrix.ServerCode;
				}
				if (FeatureMatrix.ServerCode != 11)
				{
					return 1;
				}
				return 11;
			}
		}
	}
}
