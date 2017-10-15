using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;
using Devcat.Core.Math.GeometricAlgebra;

namespace Devcat.Core.Design
{
	public class QuaternionConverter : MathTypeConverter
	{
		public QuaternionConverter()
		{
			Type typeFromHandle = typeof(Quaternion);
			this.propertyDescriptions = new PropertyDescriptorCollection(new PropertyDescriptor[]
			{
				new FieldPropertyDescriptor(typeFromHandle.GetField("X")),
				new FieldPropertyDescriptor(typeFromHandle.GetField("Y")),
				new FieldPropertyDescriptor(typeFromHandle.GetField("Z")),
				new FieldPropertyDescriptor(typeFromHandle.GetField("W"))
			}).Sort(new string[]
			{
				"X",
				"Y",
				"Z",
				"W"
			});
		}

		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			float[] array = MathTypeConverter.ConvertToValues<float>(context, culture, value, 4, new string[]
			{
				"X",
				"Y",
				"Z",
				"W"
			});
			if (array != null)
			{
				return new Quaternion(array[0], array[1], array[2], array[3]);
			}
			return base.ConvertFrom(context, culture, value);
		}

		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == typeof(string) && value is Quaternion)
			{
				Quaternion quaternion = (Quaternion)value;
				return MathTypeConverter.ConvertFromValues<float>(context, culture, new float[]
				{
					quaternion.X,
					quaternion.Y,
					quaternion.Z,
					quaternion.W
				});
			}
			if (destinationType == typeof(InstanceDescriptor) && value is Quaternion)
			{
				Quaternion quaternion2 = (Quaternion)value;
				ConstructorInfo constructor = typeof(Quaternion).GetConstructor(new Type[]
				{
					typeof(float),
					typeof(float),
					typeof(float),
					typeof(float)
				});
				if (constructor != null)
				{
					return new InstanceDescriptor(constructor, new object[]
					{
						quaternion2.X,
						quaternion2.Y,
						quaternion2.Z,
						quaternion2.W
					});
				}
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
		{
			if (propertyValues == null)
			{
				throw new ArgumentNullException("propertyValues", "Null Not Allowed");
			}
			return new Quaternion((float)propertyValues["X"], (float)propertyValues["Y"], (float)propertyValues["Z"], (float)propertyValues["W"]);
		}
	}
}
