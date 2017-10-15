using System;

namespace Nexon.Com
{
	public class InvalidParameterException : Exception
	{
		public InvalidParameterException(string parameterName, bool value) : this(parameterName, value.ToString())
		{
		}

		public InvalidParameterException(string parameterName, bool? value) : this(parameterName, value.ToString())
		{
		}

		public InvalidParameterException(string parameterName, byte value) : this(parameterName, value.ToString())
		{
		}

		public InvalidParameterException(string parameterName, byte? value) : this(parameterName, value.ToString())
		{
		}

		public InvalidParameterException(string parameterName, sbyte value) : this(parameterName, value.ToString())
		{
		}

		public InvalidParameterException(string parameterName, sbyte? value) : this(parameterName, value.ToString())
		{
		}

		public InvalidParameterException(string parameterName, short value) : this(parameterName, value.ToString())
		{
		}

		public InvalidParameterException(string parameterName, short? value) : this(parameterName, value.ToString())
		{
		}

		public InvalidParameterException(string parameterName, ushort value) : this(parameterName, value.ToString())
		{
		}

		public InvalidParameterException(string parameterName, ushort? value) : this(parameterName, value.ToString())
		{
		}

		public InvalidParameterException(string parameterName, int value) : this(parameterName, value.ToString())
		{
		}

		public InvalidParameterException(string parameterName, int? value) : this(parameterName, value.ToString())
		{
		}

		public InvalidParameterException(string parameterName, uint value) : this(parameterName, value.ToString())
		{
		}

		public InvalidParameterException(string parameterName, uint? value) : this(parameterName, value.ToString())
		{
		}

		public InvalidParameterException(string parameterName, long value) : this(parameterName, value.ToString())
		{
		}

		public InvalidParameterException(string parameterName, long? value) : this(parameterName, value.ToString())
		{
		}

		public InvalidParameterException(string parameterName, ulong value) : this(parameterName, value.ToString())
		{
		}

		public InvalidParameterException(string parameterName, ulong? value) : this(parameterName, value.ToString())
		{
		}

		public InvalidParameterException(string parameterName, DateTime value) : this(parameterName, value.ToLongDateString())
		{
		}

		public InvalidParameterException(string parameterName, DateTime? value) : this(parameterName, value.ToString())
		{
		}

		public InvalidParameterException(string parameterName, float value) : this(parameterName, value.ToString())
		{
		}

		public InvalidParameterException(string parameterName, float? value) : this(parameterName, value.ToString())
		{
		}

		public InvalidParameterException(string parameterName, double value) : this(parameterName, value.ToString())
		{
		}

		public InvalidParameterException(string parameterName, double? value) : this(parameterName, value.ToString())
		{
		}

		public InvalidParameterException(string parameterName, decimal value) : this(parameterName, value.ToString())
		{
		}

		public InvalidParameterException(string parameterName, decimal? value) : this(parameterName, value.ToString())
		{
		}

		public InvalidParameterException(string parameterName, char value) : this(parameterName, value.ToString())
		{
		}

		public InvalidParameterException(string parameterName, char? value) : this(parameterName, value.ToString())
		{
		}

		public InvalidParameterException(string parameterName, object value) : this(parameterName, value.ToString())
		{
		}

		public InvalidParameterException(string parameterName, string value) : base(string.Format("{0} is invalid. value ='{1}'", parameterName ?? "null", value ?? "null"))
		{
		}
	}
}
