using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;
using Devcat.Core.Math.GeometricAlgebra;

namespace Devcat.Core.Design
{
	public class Vector2Converter : MathTypeConverter
	{
		public Vector2Converter()
		{
			Type typeFromHandle = typeof(Vector2);
			this.propertyDescriptions = new PropertyDescriptorCollection(new PropertyDescriptor[]
			{
				new FieldPropertyDescriptor(typeFromHandle.GetField("X")),
				new FieldPropertyDescriptor(typeFromHandle.GetField("Y"))
			}).Sort(new string[]
			{
				"X",
				"Y"
			});
		}

		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			float[] array = MathTypeConverter.ConvertToValues<float>(context, culture, value, 2, new string[]
			{
				"X",
				"Y"
			});
			if (array != null)
			{
				return new Vector2(array[0], array[1]);
			}
			return base.ConvertFrom(context, culture, value);
		}

		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == typeof(string) && value is Vector2)
			{
				Vector2 vector = (Vector2)value;
				return MathTypeConverter.ConvertFromValues<float>(context, culture, new float[]
				{
					vector.X,
					vector.Y
				});
			}
			if (destinationType == typeof(InstanceDescriptor) && value is Vector2)
			{
				Vector2 vector2 = (Vector2)value;
				ConstructorInfo constructor = typeof(Vector2).GetConstructor(new Type[]
				{
					typeof(float),
					typeof(float)
				});
				if (constructor != null)
				{
					return new InstanceDescriptor(constructor, new object[]
					{
						vector2.X,
						vector2.Y
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
			return new Vector2((float)propertyValues["X"], (float)propertyValues["Y"]);
		}
	}
}
