using System;
using System.Collections;
using System.Reflection;
using System.Text;

namespace Nexon.Com.Xml
{
	public class JsonSerializer
	{
		public JsonSerializer(Type type)
		{
			this.childType = type;
			this.properties = this.childType.GetProperties();
		}

		public void Serialize(object o, StringBuilder sb)
		{
			int num = 0;
			if (sb == null)
			{
				throw new Exception("StringBuilder is null.");
			}
			sb.Append("{");
			IEnumerable enumerable = o as IEnumerable;
			if (enumerable != null)
			{
				sb.AppendFormat("\"items\":[", new object[0]);
				foreach (object obj in enumerable)
				{
					if (num != 0)
					{
						sb.Append(",");
					}
					JsonSerializer jsonSerializer = new JsonSerializer(obj.GetType());
					jsonSerializer.Serialize(obj, sb);
					num++;
				}
				sb.AppendFormat("]", new object[0]);
			}
			else
			{
				foreach (PropertyInfo propertyInfo in this.properties)
				{
					bool flag = false;
					bool flag2 = false;
					object[] customAttributes = propertyInfo.GetCustomAttributes(typeof(XmlSerializeAttribute), false);
					if (customAttributes.Length == 1)
					{
						XmlSerializeAttribute xmlSerializeAttribute = customAttributes[0] as XmlSerializeAttribute;
						flag = xmlSerializeAttribute.Ignore;
						bool cdata = xmlSerializeAttribute.CData;
						flag2 = xmlSerializeAttribute.Serializable;
					}
					if (num != 0)
					{
						sb.Append(",");
					}
					if (flag2)
					{
						int num2 = 0;
						object value = propertyInfo.GetValue(o, null);
						IEnumerable enumerable2 = value as IEnumerable;
						if (enumerable2 != null)
						{
							sb.AppendFormat("[", new object[0]);
							foreach (object obj2 in enumerable2)
							{
								if (num2 != 0)
								{
									sb.Append(",");
								}
								JsonSerializer jsonSerializer2 = new JsonSerializer(obj2.GetType());
								jsonSerializer2.Serialize(obj2, sb);
								num2++;
							}
							sb.AppendFormat("]", new object[0]);
						}
					}
					else if (!flag)
					{
						sb.AppendFormat("\"{0}\":\"{1}\"", propertyInfo.Name, propertyInfo.GetValue(o, null).Parse(string.Empty).Replace("\\", "\\\\"));
					}
					num++;
				}
			}
			sb.Append("}");
		}

		private Type childType;

		private PropertyInfo[] properties;
	}
}
