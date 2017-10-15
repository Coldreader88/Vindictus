using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;

namespace ServiceCore
{
	public class KeyValues
	{
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		public string Value
		{
			get
			{
				return this.value;
			}
			set
			{
				this.value = value;
			}
		}

		public KeyValues FindKey(string key)
		{
			KeyValues result;
			this.subkeys.TryGetValue(key, out result);
			return result;
		}

		public KeyValues FindKeyIgnoreCase(string key)
		{
			foreach (KeyValuePair<string, KeyValues> keyValuePair in this.subkeys)
			{
				if (keyValuePair.Key.ToLower() == key.ToLower())
				{
					return keyValuePair.Value;
				}
			}
			return null;
		}

		public KeyValues CreateKey(string key)
		{
			KeyValues result = new KeyValues(key);
			this.subkeys[key] = result;
			return result;
		}

		public KeyValues SetValue(string key, string value)
		{
			KeyValues keyValues;
			if (!this.subkeys.TryGetValue(key, out keyValues))
			{
				keyValues = new KeyValues(key, value);
				this.subkeys[key] = keyValues;
				return keyValues;
			}
			keyValues.value = value;
			return keyValues;
		}

		public IEnumerable<KeyValues> SubKeys
		{
			get
			{
				return this.subkeys.Values;
			}
		}

		public IEnumerable<string> SubValues
		{
			get
			{
				foreach (KeyValues subkey in this.SubKeys)
				{
					if (subkey.Value != null)
					{
						yield return subkey.Value;
					}
				}
				yield break;
			}
		}

		public KeyValues(string key)
		{
			this.name = key;
			this.subkeys = new Dictionary<string, KeyValues>();
		}

		public KeyValues(string key, string value) : this(key)
		{
			this.value = value;
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("\"").Append(this.name).Append("\"").Append(" ");
			if (this.value != null)
			{
				stringBuilder.Append("\"").Append(this.value).Append("\"");
			}
			else if (this.subkeys != null)
			{
				stringBuilder.Append("{").AppendLine();
				foreach (KeyValues keyValues in this.subkeys.Values)
				{
					stringBuilder.Append(keyValues.ToString()).AppendLine();
				}
				stringBuilder.Append("}");
			}
			else
			{
				stringBuilder.Append("*** invalid keyvalue ***");
			}
			return stringBuilder.ToString();
		}

		public static void CheckScheme(DataTable table)
		{
			int num = 0;
			foreach (object obj in table.Columns)
			{
				DataColumn dataColumn = (DataColumn)obj;
				if (dataColumn.ColumnName == "Key")
				{
					if (dataColumn.DataType != typeof(int) || dataColumn.AllowDBNull || !dataColumn.AutoIncrement)
					{
						throw new Exception("Key 타입이 int(not null auto increment)가 아닙니다.");
					}
					num++;
				}
				else if (dataColumn.ColumnName == "Name")
				{
					if (dataColumn.DataType != typeof(string) || dataColumn.AllowDBNull)
					{
						throw new Exception("Name 타입이 string(not null)이 아닙니다.");
					}
					num++;
				}
				else if (dataColumn.ColumnName == "Value")
				{
					if (dataColumn.DataType != typeof(string) || !dataColumn.AllowDBNull)
					{
						throw new Exception("Value 타입이 nullable string이 아닙니다.");
					}
					num++;
				}
				else if (dataColumn.ColumnName == "SubKey" || !dataColumn.AllowDBNull)
				{
					if (dataColumn.DataType != typeof(int))
					{
						throw new Exception("SubKey 타입이 int가 아닙니다.");
					}
					num++;
				}
				else if (dataColumn.ColumnName == "NextKey" || !dataColumn.AllowDBNull)
				{
					if (dataColumn.DataType != typeof(int))
					{
						throw new Exception("SubKey 타입이 int가 아닙니다.");
					}
					num++;
				}
			}
			if (num != 5)
			{
				throw new Exception("테이블 스키마가 맞지 않습니다.");
			}
		}

		public static IEnumerable<KeyValues> LoadFromTable(DataTable table)
		{
			KeyValues.CheckScheme(table);
			Dictionary<int, KeyValues> keyvalues = new Dictionary<int, KeyValues>();
			foreach (object obj in table.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				if (dataRow.IsNull("Value"))
				{
					keyvalues[(int)dataRow["Key"]] = new KeyValues((string)dataRow["Name"]);
				}
				else
				{
					keyvalues[(int)dataRow["Key"]] = new KeyValues((string)dataRow["Name"], (string)dataRow["Value"]);
				}
			}
			foreach (object obj2 in table.Rows)
			{
				DataRow dataRow2 = (DataRow)obj2;
				KeyValues keyValues = keyvalues[(int)dataRow2["Key"]];
				if (!dataRow2.IsNull("SubKey"))
				{
					keyValues.firstSubKey = keyvalues[(int)dataRow2["SubKey"]];
				}
				if (!dataRow2.IsNull("NextKey"))
				{
					keyValues.nextKey = keyvalues[(int)dataRow2["NextKey"]];
				}
			}
			foreach (KeyValues keyValues2 in keyvalues.Values)
			{
				for (KeyValues keyValues3 = keyValues2.firstSubKey; keyValues3 != null; keyValues3 = keyValues3.nextKey)
				{
					keyValues2.subkeys[keyValues3.Name] = keyValues3;
					keyValues3.parent = keyValues2;
				}
			}
			foreach (KeyValues key in keyvalues.Values)
			{
				if (key.parent == null)
				{
					yield return key;
				}
			}
			yield break;
		}

		public static DataRow SaveToTable(DataTable table, KeyValues keyvalues)
		{
			KeyValues.CheckScheme(table);
			DataRow dataRow = table.NewRow();
			dataRow["Name"] = keyvalues.Name;
			if (keyvalues.value != null)
			{
				dataRow["Value"] = keyvalues.value;
			}
			else
			{
				DataRow dataRow2 = null;
				foreach (KeyValues keyvalues2 in keyvalues.subkeys.Values)
				{
					DataRow dataRow3 = KeyValues.SaveToTable(table, keyvalues2);
					if (dataRow2 == null)
					{
						dataRow["SubKey"] = dataRow3["Key"];
						dataRow2 = dataRow3;
					}
					else
					{
						dataRow2["NextKey"] = dataRow3["Key"];
						dataRow2 = dataRow3;
					}
				}
			}
			table.Rows.Add(dataRow);
			return dataRow;
		}

		public static IEnumerable<KeyValues> LoadFromReader(TextReader reader)
		{
			return KeyValues.LoadFromReader(reader, false);
		}

		private static IEnumerable<KeyValues> LoadFromReader(TextReader reader, bool sub)
		{
			KeyValues.ConsumeWhiteSpace(reader);
			while (reader.Peek() > 0)
			{
				bool quote;
				string token = KeyValues.ReadToken(reader, out quote);
				if (!quote && token == "{")
				{
					if (sub)
					{
						throw new Exception("Syntax error");
					}
					KeyValues result = new KeyValues("");
					foreach (KeyValues keyValues in KeyValues.LoadFromReader(reader, true))
					{
						result.subkeys[keyValues.name] = keyValues;
					}
					yield return result;
				}
				else if (!quote && token == "}")
				{
					if (!sub)
					{
						throw new Exception("Syntax error");
					}
					break;
				}
				else
				{
					if (reader.Peek() < 0)
					{
						throw new Exception("Syntax error");
					}
					string value = KeyValues.ReadToken(reader, out quote);
					if (!quote && value == "{")
					{
						KeyValues result2 = new KeyValues(token);
						foreach (KeyValues keyValues2 in KeyValues.LoadFromReader(reader, true))
						{
							result2.subkeys[keyValues2.name] = keyValues2;
						}
						yield return result2;
					}
					else
					{
						yield return new KeyValues(token, value);
					}
				}
			}
			yield break;
		}

		private static void ConsumeWhiteSpace(TextReader reader)
		{
			while (char.IsWhiteSpace((char)reader.Peek()))
			{
				reader.Read();
			}
		}

		private static string ReadToken(TextReader reader, out bool quote)
		{
			quote = false;
			while (reader.Peek() == 47)
			{
				reader.ReadLine();
				KeyValues.ConsumeWhiteSpace(reader);
			}
			int num = reader.Peek();
			if (num == 123)
			{
				reader.Read();
				KeyValues.ConsumeWhiteSpace(reader);
				return "{";
			}
			if (num == 125)
			{
				reader.Read();
				KeyValues.ConsumeWhiteSpace(reader);
				return "}";
			}
			if (num == 34)
			{
				quote = true;
				reader.Read();
			}
			List<char> list = new List<char>();
			while (reader.Peek() >= 0 && (!quote || reader.Peek() != 34) && (quote || !char.IsWhiteSpace((char)reader.Peek())) && (quote || (reader.Peek() != 123 && reader.Peek() != 125)))
			{
				list.Add((char)reader.Read());
			}
			if (quote)
			{
				if (reader.Peek() < 0)
				{
					throw new Exception(string.Format("따옴표를 닫으세요 : {0}", new string(list.ToArray())));
				}
				reader.Read();
			}
			KeyValues.ConsumeWhiteSpace(reader);
			return new string(list.ToArray());
		}

		private string name;

		private string value;

		private KeyValues parent;

		private KeyValues firstSubKey;

		private KeyValues nextKey;

		private Dictionary<string, KeyValues> subkeys;
	}
}
