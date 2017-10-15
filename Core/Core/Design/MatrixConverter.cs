using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;
using Devcat.Core.Math.GeometricAlgebra;

namespace Devcat.Core.Design
{
	public class MatrixConverter : MathTypeConverter
	{
		public MatrixConverter()
		{
			Type typeFromHandle = typeof(Matrix);
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeFromHandle);
			PropertyDescriptorCollection propertyDescriptions = new PropertyDescriptorCollection(new PropertyDescriptor[]
			{
				properties.Find("Translation", true),
				new FieldPropertyDescriptor(typeFromHandle.GetField("M11")),
				new FieldPropertyDescriptor(typeFromHandle.GetField("M12")),
				new FieldPropertyDescriptor(typeFromHandle.GetField("M13")),
				new FieldPropertyDescriptor(typeFromHandle.GetField("M14")),
				new FieldPropertyDescriptor(typeFromHandle.GetField("M21")),
				new FieldPropertyDescriptor(typeFromHandle.GetField("M22")),
				new FieldPropertyDescriptor(typeFromHandle.GetField("M23")),
				new FieldPropertyDescriptor(typeFromHandle.GetField("M24")),
				new FieldPropertyDescriptor(typeFromHandle.GetField("M31")),
				new FieldPropertyDescriptor(typeFromHandle.GetField("M32")),
				new FieldPropertyDescriptor(typeFromHandle.GetField("M33")),
				new FieldPropertyDescriptor(typeFromHandle.GetField("M34")),
				new FieldPropertyDescriptor(typeFromHandle.GetField("M41")),
				new FieldPropertyDescriptor(typeFromHandle.GetField("M42")),
				new FieldPropertyDescriptor(typeFromHandle.GetField("M43")),
				new FieldPropertyDescriptor(typeFromHandle.GetField("M44"))
			});
			this.propertyDescriptions = propertyDescriptions;
			this.supportStringConvert = false;
		}

		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == typeof(InstanceDescriptor) && value is Matrix)
			{
				Matrix matrix = (Matrix)value;
				ConstructorInfo constructor = typeof(Matrix).GetConstructor(new Type[]
				{
					typeof(float),
					typeof(float),
					typeof(float),
					typeof(float),
					typeof(float),
					typeof(float),
					typeof(float),
					typeof(float),
					typeof(float),
					typeof(float),
					typeof(float),
					typeof(float),
					typeof(float),
					typeof(float),
					typeof(float),
					typeof(float)
				});
				if (constructor != null)
				{
					return new InstanceDescriptor(constructor, new object[]
					{
						matrix.M11,
						matrix.M12,
						matrix.M13,
						matrix.M14,
						matrix.M21,
						matrix.M22,
						matrix.M23,
						matrix.M24,
						matrix.M31,
						matrix.M32,
						matrix.M33,
						matrix.M34,
						matrix.M41,
						matrix.M42,
						matrix.M43,
						matrix.M44
					});
				}
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
		{
			if (propertyValues == null)
			{
				throw new ArgumentNullException("propertyValues", "Null not allowed");
			}
			return new Matrix((float)propertyValues["M11"], (float)propertyValues["M12"], (float)propertyValues["M13"], (float)propertyValues["M14"], (float)propertyValues["M21"], (float)propertyValues["M22"], (float)propertyValues["M23"], (float)propertyValues["M24"], (float)propertyValues["M31"], (float)propertyValues["M32"], (float)propertyValues["M33"], (float)propertyValues["M34"], (float)propertyValues["M41"], (float)propertyValues["M42"], (float)propertyValues["M43"], (float)propertyValues["M44"]);
		}
	}
}
