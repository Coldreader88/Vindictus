using System;
using System.Collections.Generic;
using System.Text;
using ServiceCore.CommonOperations;
using ServiceCore.HeroesContents;
using ServiceCore.Properties;
using UnifiedNetwork.OperationService;
using UnifiedNetwork.Properties;
using Utility;

namespace ServiceCore
{
	public class FeatureMatrix
	{
		public static event Action<Service, UpdateFeatureMatrix> onUpdated;

		public static void OnUpdated(Service service, UpdateFeatureMatrix op)
		{
			if (FeatureMatrix.onUpdated != null)
			{
				FeatureMatrix.onUpdated(service, op);
			}
		}

		public static string LangTag
		{
			get
			{
				lock (FeatureMatrix.featureLock)
				{
					string result;
					if (FeatureMatrix.gameCodeMap.TryGetValue(FeatureMatrix.GameCode, out result))
					{
						return result;
					}
				}
				return "ko-KR";
			}
		}

		private static bool LoadFeatureMatrix()
		{
			bool result;
			lock (FeatureMatrix.featureLock)
			{
				if (FeatureMatrix.featureMatrixLoaded)
				{
					result = false;
				}
				else
				{
					FeatureMatrix.gameCodeMap = HeroesContentsLoader.GetGameCode();
					FeatureMatrix.featureMap = HeroesContentsLoader.GetFeatureMatrix(FeatureMatrix.LangTag);
					FeatureMatrix.featureMatrixLoaded = true;
					if (FeatureMatrix.GetString("OverrideSetting").Length > 0)
					{
						Dictionary<string, string> featureMatrix = HeroesContentsLoader.GetFeatureMatrix(FeatureMatrix.GetString("OverrideSetting"));
						foreach (KeyValuePair<string, string> keyValuePair in featureMatrix)
						{
							string key = keyValuePair.Key;
							if (key != "GameCode" && key != "CurrentVer" && key != "OverrideSetting")
							{
								FeatureMatrix.featureMap[key] = keyValuePair.Value;
							}
						}
					}
					if (ServiceCoreSettings.Default.NGM_Disable != "-1")
					{
						FeatureMatrix.overrideMap["NGM_disable"] = ServiceCoreSettings.Default.NGM_Disable;
					}
					result = true;
				}
			}
			return result;
		}

		public static bool IsMatchServerCode(string stringCode)
		{
			if (stringCode == null || stringCode.Length == 0)
			{
				return true;
			}
			if (stringCode.IndexOf("||") >= 0)
			{
				string[] separator = new string[]
				{
					"||"
				};
				string[] array = stringCode.Split(separator, StringSplitOptions.RemoveEmptyEntries);
				if (array.Length >= 2)
				{
					for (int i = 0; i < array.Length; i++)
					{
						if (FeatureMatrix.IsMatchServerCode(array[i]))
						{
							return true;
						}
					}
					return false;
				}
			}
			stringCode = stringCode.Trim();
			if (stringCode[0] == '!')
			{
				return !FeatureMatrix.IsMatchServerCode(stringCode.Substring(1));
			}
			int num = 0;
			try
			{
				num = int.Parse(stringCode);
			}
			catch
			{
				Log<FeatureMatrix>.Logger.ErrorFormat("Not an Integer value ServerCode - {0}", stringCode);
				return false;
			}
			return num > 0 && FeatureMatrix.ServerCode == num;
		}

		public static bool IsEnable(string featureName)
		{
			if (featureName == null || featureName.Length == 0)
			{
				return true;
			}
			if (featureName.IndexOf("||") >= 0)
			{
				string[] separator = new string[]
				{
					"||"
				};
				string[] array = featureName.Split(separator, StringSplitOptions.RemoveEmptyEntries);
				if (array.Length >= 2)
				{
					for (int i = 0; i < array.Length; i++)
					{
						if (FeatureMatrix.IsEnable(array[i]))
						{
							return true;
						}
					}
					return false;
				}
			}
			if (featureName.IndexOf("&&") >= 0)
			{
				string[] separator2 = new string[]
				{
					"&&"
				};
				string[] array2 = featureName.Split(separator2, StringSplitOptions.RemoveEmptyEntries);
				if (array2.Length >= 2)
				{
					for (int j = 0; j < array2.Length; j++)
					{
						if (!FeatureMatrix.IsEnable(array2[j]))
						{
							return false;
						}
					}
					return true;
				}
			}
			featureName = featureName.Trim();
			if (featureName[0] == '!')
			{
				return !FeatureMatrix.IsEnable(featureName.Substring(1));
			}
			string @string = FeatureMatrix.GetString(featureName);
			return @string != null && @string.Length > 0 && FeatureMatrix.CurrentVer.CompareTo(@string) >= 0;
		}

		public static string GetString(string featureName)
		{
			if (!FeatureMatrix.featureMatrixLoaded)
			{
				FeatureMatrix.LoadFeatureMatrix();
			}
			lock (FeatureMatrix.featureLock)
			{
				string result;
				if (FeatureMatrix.overrideMap.TryGetValue(featureName, out result))
				{
					return result;
				}
				if (FeatureMatrix.featureMap.TryGetValue(featureName, out result))
				{
					return result;
				}
			}
			return "";
		}

		public static int GetInteger(string featureName)
		{
			string @string = FeatureMatrix.GetString(featureName);
			int result;
			try
			{
				result = int.Parse(@string);
			}
			catch (FormatException)
			{
				Log<FeatureMatrix>.Logger.ErrorFormat("Not an Integer value - Feature {0}='{1}'", featureName, @string);
				result = 0;
			}
			return result;
		}

		public static int? GetNullableInt(string featureName)
		{
			string @string = FeatureMatrix.GetString(featureName);
			if (@string == null || @string == "")
			{
				return null;
			}
			int? result;
			try
			{
				result = new int?(int.Parse(@string));
			}
			catch (FormatException)
			{
				Log<FeatureMatrix>.Logger.ErrorFormat("Not an Integer value - Feature {0}='{1}'", featureName, @string);
				result = null;
			}
			return result;
		}

		public static float GetFloat(string featureName)
		{
			string @string = FeatureMatrix.GetString(featureName);
			float result;
			try
			{
				result = float.Parse(@string);
			}
			catch (FormatException)
			{
				Log<FeatureMatrix>.Logger.ErrorFormat("Not a Float value - Feature {0}='{1}'", featureName, @string);
				result = 0f;
			}
			return result;
		}

		public static void OverrideFeature(Dictionary<string, string> features)
		{
			lock (FeatureMatrix.featureLock)
			{
				foreach (KeyValuePair<string, string> keyValuePair in features)
				{
					if (keyValuePair.Value != null)
					{
						FeatureMatrix.overrideMap[keyValuePair.Key] = keyValuePair.Value;
					}
					else
					{
						FeatureMatrix.overrideMap.Remove(keyValuePair.Key);
					}
				}
			}
		}

		public static string MakeDigest()
		{
			StringBuilder stringBuilder = new StringBuilder();
			lock (FeatureMatrix.featureLock)
			{
				foreach (KeyValuePair<string, string> keyValuePair in FeatureMatrix.overrideMap)
				{
					stringBuilder.AppendFormat("{0} \"{1}\" ", keyValuePair.Key, keyValuePair.Value);
				}
			}
			return stringBuilder.ToString();
		}

		public static Dictionary<string, string> GetFeatureMatrixDic()
		{
			Dictionary<string, string> result;
			lock (FeatureMatrix.featureLock)
			{
				result = new Dictionary<string, string>(FeatureMatrix.overrideMap);
			}
			return result;
		}

		public static int GameCode
		{
			get
			{
				return UnifiedNetwork.Properties.Settings.Default.GameCode;
			}
		}

		public static int ServerCode
		{
			get
			{
				return UnifiedNetwork.Properties.Settings.Default.ServerCode;
			}
		}

		public static string CurrentVer
		{
			get
			{
				if (FeatureMatrix.currentVer == null)
				{
					FeatureMatrix.currentVer = FeatureMatrix.GetString("CurrentVer");
				}
				return FeatureMatrix.currentVer;
			}
		}

		public static string BCPPathString
		{
			get
			{
				return ServiceCore.Properties.Settings.Default.heroesBcpPath;
			}
		}

		public static string XMLPathString
		{
			get
			{
				return ServiceCore.Properties.Settings.Default.heroesXMLPath;
			}
		}

		public static string MaterialPathString
		{
			get
			{
				return ServiceCore.Properties.Settings.Default.heroesMaterialPath;
			}
		}

		private static object featureLock = new object();

		private static Dictionary<int, string> gameCodeMap = new Dictionary<int, string>();

		private static Dictionary<string, string> featureMap = new Dictionary<string, string>();

		private static Dictionary<string, string> overrideMap = new Dictionary<string, string>();

		private static bool featureMatrixLoaded = false;

		private static string currentVer = null;
	}
}
