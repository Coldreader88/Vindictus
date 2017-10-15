using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq.Mapping;
using System.IO;
using System.Linq;
using System.Reflection;
using Devcat.Core.Threading;
using Utility;

namespace ServiceCore.HeroesContents
{
	public class HeroesContentsLoader
	{
		private static Assembly Assembly_Sqlite { get; set; }

		private static Type Type_SQLiteConnection { get; set; }

		private static Type Type_SQLiteCommand { get; set; }

		private static Type Type_SQLiteDataReader { get; set; }

		private static MethodInfo Method_Conn_Open { get; set; }

		private static MethodInfo Method_Conn_Close { get; set; }

		private static MethodInfo Method_Conn_CreateCommand { get; set; }

		private static MethodInfo Method_Cmd_ExecuteReader { get; set; }

		private static MethodInfo Method_Reader_Read { get; set; }

		private static MethodInfo Method_Reader_GetValue { get; set; }

		private static MethodInfo Method_Reader_GetOrdinal { get; set; }

		private static MethodInfo Method_Reader_GetString { get; set; }

		private static MethodInfo Method_Reader_GetName { get; set; }

		private static MethodInfo Method_Reader_Close { get; set; }

		private static PropertyInfo Property_Cmd_CommandText { get; set; }

		private static PropertyInfo Property_Cmd_CommandType { get; set; }

		private static PropertyInfo Property_Reader_Item { get; set; }

		private static PropertyInfo Property_Reader_FieldCount { get; set; }

		private static object SqliteConnection { get; set; }

		private static FileSystemWatcher FileSystemWatcher { get; set; }

		public static event Action DB3Changed;

		static HeroesContentsLoader()
		{
			string path = Path.Combine(Environment.CurrentDirectory, (IntPtr.Size == 4) ? "x86\\System.Data.SQLite.DLL" : "x64\\System.Data.SQLite.DLL");
			HeroesContentsLoader.DataFileName = ServiceCoreSettings.Default.heroesContentsDB3;
			HeroesContentsLoader.LocalizedTextFileName = ServiceCoreSettings.Default.localizedTextDB3;
			Log<HeroesContentsLoader>.Logger.InfoFormat("HeroesContentsLoader in {0}bit", (IntPtr.Size == 4) ? 32 : 64);
			HeroesContentsLoader.Assembly_Sqlite = Assembly.LoadFile(path);
			HeroesContentsLoader.Type_SQLiteConnection = HeroesContentsLoader.Assembly_Sqlite.GetType("System.Data.SQLite.SQLiteConnection");
			HeroesContentsLoader.Type_SQLiteCommand = HeroesContentsLoader.Assembly_Sqlite.GetType("System.Data.SQLite.SQLiteCommand");
			HeroesContentsLoader.Type_SQLiteDataReader = HeroesContentsLoader.Assembly_Sqlite.GetType("System.Data.SQLite.SQLiteDataReader");
			HeroesContentsLoader.Method_Conn_Open = HeroesContentsLoader.Type_SQLiteConnection.GetMethod("Open");
			HeroesContentsLoader.Method_Conn_Close = HeroesContentsLoader.Type_SQLiteConnection.GetMethod("Close");
			HeroesContentsLoader.Method_Conn_CreateCommand = HeroesContentsLoader.Type_SQLiteConnection.GetMethod("CreateCommand");
			HeroesContentsLoader.Method_Cmd_ExecuteReader = HeroesContentsLoader.Type_SQLiteCommand.GetMethod("ExecuteReader", new Type[0]);
			HeroesContentsLoader.Method_Reader_Read = HeroesContentsLoader.Type_SQLiteDataReader.GetMethod("Read");
			HeroesContentsLoader.Method_Reader_GetValue = HeroesContentsLoader.Type_SQLiteDataReader.GetMethod("GetValue", new Type[]
			{
				typeof(int)
			});
			HeroesContentsLoader.Method_Reader_GetOrdinal = HeroesContentsLoader.Type_SQLiteDataReader.GetMethod("GetOrdinal", new Type[]
			{
				typeof(string)
			});
			HeroesContentsLoader.Method_Reader_GetString = HeroesContentsLoader.Type_SQLiteDataReader.GetMethod("GetString", new Type[]
			{
				typeof(int)
			});
			HeroesContentsLoader.Method_Reader_GetName = HeroesContentsLoader.Type_SQLiteDataReader.GetMethod("GetName", new Type[]
			{
				typeof(int)
			});
			HeroesContentsLoader.Method_Reader_Close = HeroesContentsLoader.Type_SQLiteDataReader.GetMethod("Close");
			HeroesContentsLoader.Property_Cmd_CommandText = HeroesContentsLoader.Type_SQLiteCommand.GetProperty("CommandText");
			HeroesContentsLoader.Property_Cmd_CommandType = HeroesContentsLoader.Type_SQLiteCommand.GetProperty("CommandType");
			HeroesContentsLoader.Property_Reader_Item = HeroesContentsLoader.Type_SQLiteDataReader.GetProperty("Item", new Type[]
			{
				typeof(int)
			});
			HeroesContentsLoader.Property_Reader_FieldCount = HeroesContentsLoader.Type_SQLiteDataReader.GetProperty("FieldCount");
			string fullPath = Path.GetFullPath(ServiceCoreSettings.Default.heroesContentsDB3);
			string directoryName = Path.GetDirectoryName(fullPath);
			HeroesContentsLoader.FileSystemWatcher = new FileSystemWatcher
			{
				Path = directoryName,
				NotifyFilter = (NotifyFilters.Size | NotifyFilters.LastWrite),
				Filter = "*.db3",
				IncludeSubdirectories = false,
				EnableRaisingEvents = true
			};
			HeroesContentsLoader.FileSystemWatcher.Changed += delegate(object _, FileSystemEventArgs __)
			{
				Scheduler.Schedule(JobProcessor.Current, Job.Create(delegate
				{
					if (HeroesContentsLoader.DB3Changed != null)
					{
						HeroesContentsLoader.DB3Changed();
					}
				}), 3000);
			};
		}

		public static void TestConnection()
		{
			HeroesContentsLoader.GetGameCode();
		}

		public static IEnumerable<T> GetTable<T>() where T : class
		{
			Type type_Table = typeof(T);
			Log<HeroesContentsLoader>.Logger.InfoFormat("GetTable : {0}", type_Table.Name);
			HeroesContentsLoader.SqliteConnection = Activator.CreateInstance(HeroesContentsLoader.Type_SQLiteConnection, new object[]
			{
				"Data Source=" + ((type_Table.Name == "LocalizedText") ? HeroesContentsLoader.LocalizedTextFileName : HeroesContentsLoader.DataFileName)
			});
			HeroesContentsLoader.Method_Conn_Open.Invoke(HeroesContentsLoader.SqliteConnection, new object[0]);
			List<PropertyInfo> pi_Table = new List<PropertyInfo>();
			foreach (PropertyInfo propertyInfo in type_Table.GetProperties())
			{
				if (propertyInfo.GetCustomAttributes(typeof(ColumnAttribute), true).Count<object>() > 0)
				{
					pi_Table.Add(propertyInfo);
				}
			}
			object cmd = HeroesContentsLoader.Method_Conn_CreateCommand.Invoke(HeroesContentsLoader.SqliteConnection, new object[0]);
			HeroesContentsLoader.Property_Cmd_CommandText.SetValue(cmd, string.Format("Select * From [{0}]", type_Table.Name), null);
			HeroesContentsLoader.Property_Cmd_CommandType.SetValue(cmd, CommandType.Text, null);
			object reader = HeroesContentsLoader.Method_Cmd_ExecuteReader.Invoke(cmd, new object[0]);
			Dictionary<string, bool> isRealBoolType = new Dictionary<string, bool>();
			Dictionary<string, int> coloumnOrder = new Dictionary<string, int>();
			while ((bool)HeroesContentsLoader.Method_Reader_Read.Invoke(reader, new object[0]))
			{
				object row = null;
				try
				{
					row = Activator.CreateInstance(type_Table);
					foreach (PropertyInfo propertyInfo2 in pi_Table)
					{
						int num;
						if (coloumnOrder.ContainsKey(propertyInfo2.Name))
						{
							num = coloumnOrder[propertyInfo2.Name];
						}
						else
						{
							num = (int)HeroesContentsLoader.Method_Reader_GetOrdinal.Invoke(reader, new object[]
							{
								propertyInfo2.Name
							});
							coloumnOrder[propertyInfo2.Name] = num;
						}
						object value = HeroesContentsLoader.Property_Reader_Item.GetValue(reader, new object[]
						{
							num
						});
						if (value is DBNull)
						{
							propertyInfo2.SetValue(row, null, null);
						}
						else if (value is bool)
						{
							if (!isRealBoolType.ContainsKey(propertyInfo2.Name))
							{
								try
								{
									object obj = HeroesContentsLoader.Method_Reader_GetString.Invoke(reader, new object[]
									{
										num
									});
									propertyInfo2.SetValue(row, obj as string == "True", null);
									isRealBoolType[propertyInfo2.Name] = false;
									continue;
								}
								catch
								{
									propertyInfo2.SetValue(row, value, null);
									isRealBoolType[propertyInfo2.Name] = true;
									continue;
								}
							}
							if (isRealBoolType[propertyInfo2.Name])
							{
								propertyInfo2.SetValue(row, value, null);
							}
							else
							{
								object obj2 = HeroesContentsLoader.Method_Reader_GetString.Invoke(reader, new object[]
								{
									num
								});
								propertyInfo2.SetValue(row, obj2 as string == "True", null);
							}
						}
						else
						{
							propertyInfo2.SetValue(row, value, null);
						}
					}
				}
				catch (Exception ex)
				{
					Log<HeroesContentsLoader>.Logger.FatalFormat("Error while Loading {0}", type_Table.Name);
					Log<HeroesContentsLoader>.Logger.Fatal("Exception Occurred", ex);
				}
				yield return row as T;
			}
			HeroesContentsLoader.Method_Reader_Close.Invoke(reader, new object[0]);
			HeroesContentsLoader.Method_Conn_Close.Invoke(HeroesContentsLoader.SqliteConnection, new object[0]);
			Log<HeroesContentsLoader>.Logger.InfoFormat("GetTableEnd : {0}", type_Table.Name);
			yield break;
		}

		public static Dictionary<int, string> GetGameCode()
		{
			Dictionary<int, string> dictionary = new Dictionary<int, string>();
			Log<HeroesContentsLoader>.Logger.InfoFormat("GetGameCode : FeatureMatrix", new object[0]);
			HeroesContentsLoader.SqliteConnection = Activator.CreateInstance(HeroesContentsLoader.Type_SQLiteConnection, new object[]
			{
				"Data Source=" + HeroesContentsLoader.DataFileName
			});
			HeroesContentsLoader.Method_Conn_Open.Invoke(HeroesContentsLoader.SqliteConnection, new object[0]);
			object obj = HeroesContentsLoader.Method_Conn_CreateCommand.Invoke(HeroesContentsLoader.SqliteConnection, new object[0]);
			HeroesContentsLoader.Property_Cmd_CommandText.SetValue(obj, string.Format("SELECT * FROM FeatureMatrix WHERE Feature='GameCode';", new object[0]), null);
			HeroesContentsLoader.Property_Cmd_CommandType.SetValue(obj, CommandType.Text, null);
			object obj2 = HeroesContentsLoader.Method_Cmd_ExecuteReader.Invoke(obj, new object[0]);
			while ((bool)HeroesContentsLoader.Method_Reader_Read.Invoke(obj2, new object[0]))
			{
				try
				{
					foreach (int num in Enumerable.Range(0, (int)HeroesContentsLoader.Property_Reader_FieldCount.GetValue(obj2, null)))
					{
						string s = HeroesContentsLoader.Method_Reader_GetString.Invoke(obj2, new object[]
						{
							num
						}) as string;
						string value = HeroesContentsLoader.Method_Reader_GetName.Invoke(obj2, new object[]
						{
							num
						}) as string;
						int key;
						if (int.TryParse(s, out key))
						{
							dictionary[key] = value;
						}
					}
				}
				catch (Exception ex)
				{
					Log<HeroesContentsLoader>.Logger.FatalFormat("Error while Loading GameCode", new object[0]);
					Log<HeroesContentsLoader>.Logger.Fatal("Exception Occurred", ex);
				}
			}
			HeroesContentsLoader.Method_Reader_Close.Invoke(obj2, new object[0]);
			HeroesContentsLoader.Method_Conn_Close.Invoke(HeroesContentsLoader.SqliteConnection, new object[0]);
			Log<HeroesContentsLoader>.Logger.InfoFormat("GetGameCodeEnd : FeatureMatrix", new object[0]);
			return dictionary;
		}

		public static Dictionary<string, string> GetFeatureMatrix(string langTag)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			Log<HeroesContentsLoader>.Logger.InfoFormat("GetFeatureMatrix : {0}", langTag);
			HeroesContentsLoader.SqliteConnection = Activator.CreateInstance(HeroesContentsLoader.Type_SQLiteConnection, new object[]
			{
				"Data Source=" + HeroesContentsLoader.DataFileName
			});
			HeroesContentsLoader.Method_Conn_Open.Invoke(HeroesContentsLoader.SqliteConnection, new object[0]);
			object obj = HeroesContentsLoader.Method_Conn_CreateCommand.Invoke(HeroesContentsLoader.SqliteConnection, new object[0]);
			HeroesContentsLoader.Property_Cmd_CommandText.SetValue(obj, string.Format("SELECT Feature, [{0}] FROM FeatureMatrix;", langTag.ToUpper()), null);
			HeroesContentsLoader.Property_Cmd_CommandType.SetValue(obj, CommandType.Text, null);
			object obj2 = HeroesContentsLoader.Method_Cmd_ExecuteReader.Invoke(obj, new object[0]);
			while ((bool)HeroesContentsLoader.Method_Reader_Read.Invoke(obj2, new object[0]))
			{
				try
				{
					string key = HeroesContentsLoader.Method_Reader_GetString.Invoke(obj2, new object[]
					{
						0
					}) as string;
					object obj3 = HeroesContentsLoader.Method_Reader_GetValue.Invoke(obj2, new object[]
					{
						1
					});
					dictionary[key] = ((obj3 is DBNull) ? "" : (obj3 as string));
				}
				catch (Exception ex)
				{
					Log<HeroesContentsLoader>.Logger.FatalFormat("Error while Loading FeatureMatrix", new object[0]);
					Log<HeroesContentsLoader>.Logger.Fatal("Exception Occurred", ex);
				}
			}
			HeroesContentsLoader.Method_Reader_Close.Invoke(obj2, new object[0]);
			HeroesContentsLoader.Method_Conn_Close.Invoke(HeroesContentsLoader.SqliteConnection, new object[0]);
			Log<HeroesContentsLoader>.Logger.InfoFormat("GetFeatureMatrixEnd : {0}", langTag);
			return dictionary;
		}

		public static void Test()
		{
			foreach (QuestInfo questInfo in HeroesContentsLoader.GetTable<QuestInfo>())
			{
				Log<HeroesContentsLoader>.Logger.InfoFormat("{0} {1}", questInfo.ID, questInfo.Level);
			}
		}

		public static List<string> GetTableNames()
		{
			Log<HeroesContentsLoader>.Logger.InfoFormat("GetTableList() begin", new object[0]);
			HeroesContentsLoader.SqliteConnection = Activator.CreateInstance(HeroesContentsLoader.Type_SQLiteConnection, new object[]
			{
				"Data Source=" + HeroesContentsLoader.DataFileName
			});
			HeroesContentsLoader.Method_Conn_Open.Invoke(HeroesContentsLoader.SqliteConnection, new object[0]);
			object obj = HeroesContentsLoader.Method_Conn_CreateCommand.Invoke(HeroesContentsLoader.SqliteConnection, new object[0]);
			HeroesContentsLoader.Property_Cmd_CommandText.SetValue(obj, "select name from sqlite_master where type IN('table', 'view') AND name not like 'sqlite_%' union all select name from sqlite_temp_master where type in('table','view') order by 1;", null);
			HeroesContentsLoader.Property_Cmd_CommandType.SetValue(obj, CommandType.Text, null);
			object obj2 = HeroesContentsLoader.Method_Cmd_ExecuteReader.Invoke(obj, new object[0]);
			List<string> list = new List<string>();
			try
			{
				while ((bool)HeroesContentsLoader.Method_Reader_Read.Invoke(obj2, new object[0]))
				{
					foreach (int num in Enumerable.Range(0, (int)HeroesContentsLoader.Property_Reader_FieldCount.GetValue(obj2, null)))
					{
						HeroesContentsLoader.Method_Reader_GetName.Invoke(obj2, new object[]
						{
							num
						});
						string item = HeroesContentsLoader.Method_Reader_GetString.Invoke(obj2, new object[]
						{
							num
						}) as string;
						list.Add(item);
					}
				}
			}
			catch (Exception ex)
			{
				Log<HeroesContentsLoader>.Logger.FatalFormat("Error while ExecuteCommand: {0}", "select name from sqlite_master where type IN('table', 'view') AND name not like 'sqlite_%' union all select name from sqlite_temp_master where type in('table','view') order by 1;");
				Log<HeroesContentsLoader>.Logger.Fatal("Exception Occurred", ex);
			}
			HeroesContentsLoader.Method_Reader_Close.Invoke(obj2, new object[0]);
			HeroesContentsLoader.Method_Conn_Close.Invoke(HeroesContentsLoader.SqliteConnection, new object[0]);
			Log<HeroesContentsLoader>.Logger.InfoFormat("GetTableList end", new object[0]);
			return list;
		}

		public static string DataFileName;

		public static string LocalizedTextFileName;
	}
}
