using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;

namespace Devcat.Core.Design
{
	public class MathTypeConverter : ExpandableObjectConverter
	{
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return (this.supportStringConvert && sourceType == typeof(string)) || base.CanConvertFrom(context, sourceType);
		}

		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(InstanceDescriptor) || base.CanConvertTo(context, destinationType);
		}

		internal static string ConvertFromValues<T>(ITypeDescriptorContext context, CultureInfo culture, T[] values)
		{
			if (culture == null)
			{
				culture = CultureInfo.CurrentCulture;
			}
			string separator = culture.TextInfo.ListSeparator + " ";
			TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));
			string[] array = new string[values.Length];
			for (int i = 0; i < values.Length; i++)
			{
				array[i] = converter.ConvertToString(context, culture, values[i]);
			}
			return string.Join(separator, array);
		}

		internal static T[] ConvertToValues<T>(ITypeDescriptorContext context, CultureInfo culture, object value, int arrayCount, params string[] expectedParams)
		{
			string text = value as string;
			if (text == null)
			{
				return null;
			}
			text = text.Trim();
			if (culture == null)
			{
				culture = CultureInfo.CurrentCulture;
			}
			string[] array = text.Split(new string[]
			{
				culture.TextInfo.ListSeparator
			}, StringSplitOptions.None);
			T[] array2 = new T[array.Length];
			TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));
			for (int i = 0; i < array2.Length; i++)
			{
				try
				{
					array2[i] = (T)((object)converter.ConvertFromString(context, culture, array[i]));
				}
				catch (Exception innerException)
				{
					throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, "Invalid String Format", new object[]
					{
						string.Join(culture.TextInfo.ListSeparator, expectedParams)
					}), innerException);
				}
			}
			if (array2.Length != arrayCount)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, "Invalid String Format", new object[]
				{
					string.Join(culture.TextInfo.ListSeparator, expectedParams)
				}));
			}
			return array2;
		}

		public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
		{
			return this.propertyDescriptions;
		}

		public override bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		protected PropertyDescriptorCollection propertyDescriptions;

		protected bool supportStringConvert = true;
	}
}
