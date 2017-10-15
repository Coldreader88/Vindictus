using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;
using Devcat.Core.Math.GeometricAlgebra;

namespace Devcat.Core.Design
{
	public class Vector3Converter : MathTypeConverter
	{
		public Vector3Converter()
		{
			Type typeFromHandle = typeof(Vector3);
			this.propertyDescriptions = new PropertyDescriptorCollection(new PropertyDescriptor[]
			{
				new FieldPropertyDescriptor(typeFromHandle.GetField("X")),
				new FieldPropertyDescriptor(typeFromHandle.GetField("Y")),
				new FieldPropertyDescriptor(typeFromHandle.GetField("Z"))
			}).Sort(new string[]
			{
				"X",
				"Y",
				"Z"
			});
		}

		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			float[] array = MathTypeConverter.ConvertToValues<float>(context, culture, value, 3, new string[]
			{
				"X",
				"Y",
				"Z"
			});
			if (array != null)
			{
				return new Vector3(array[0], array[1], array[2]);
			}
			return base.ConvertFrom(context, culture, value);
		}

		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == typeof(string) && value is Vector3)
			{
				Vector3 vector = (Vector3)value;
				return MathTypeConverter.ConvertFromValues<float>(context, culture, new float[]
				{
					vector.X,
					vector.Y,
					vector.Z
				});
			}
			if (destinationType == typeof(InstanceDescriptor) && value is Vector3)
			{
				Vector3 vector2 = (Vector3)value;
				ConstructorInfo constructor = typeof(Vector3).GetConstructor(new Type[]
				{
					typeof(float),
					typeof(float),
					typeof(float)
				});
				if (constructor != null)
				{
					return new InstanceDescriptor(constructor, new object[]
					{
						vector2.X,
						vector2.Y,
						vector2.Z
					});
				}
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
		{
			if (propertyValues == null)
			{
				throw new ArgumentNullException("propertyValues", "Null are not allowed");
			}
			return new Vector3((float)propertyValues["X"], (float)propertyValues["Y"], (float)propertyValues["Z"]);
		}
	}
}
