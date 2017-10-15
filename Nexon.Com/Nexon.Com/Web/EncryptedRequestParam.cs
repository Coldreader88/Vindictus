using System;
using System.Reflection;
using Nexon.Com.Encryption;

namespace Nexon.Com.Web
{
	public abstract class EncryptedRequestParam : Url
	{
		protected virtual string RequestParamName
		{
			get
			{
				return "ERP";
			}
		}

		protected abstract ushort PrivateKey { get; }

		public EncryptedRequestParam()
		{
			this.AddKeys();
		}

		public EncryptedRequestParam(string url) : base(url)
		{
			this.AddKeys();
		}

		private void AddKeys()
		{
			string text = base[this.RequestParamName];
			if (text != null && text != string.Empty)
			{
				ParamEncoder paramEncoder = new ParamEncoder(this.PrivateKey);
				try
				{
					if (!paramEncoder.SetToken(text))
					{
						throw new EncryptedParamException("InvalidToken", null);
					}
				}
				catch (Exception innerException)
				{
					throw new EncryptedParamException("DecryptingTokenFailed", innerException);
				}
				this._type = base.GetType();
				this._fields = this._type.GetFields();
				foreach (FieldInfo fieldInfo in this._fields)
				{
					object[] customAttributes = fieldInfo.GetCustomAttributes(typeof(RequestParamAttribute), false);
					if (customAttributes.Length == 1)
					{
						RequestParamAttribute requestParamAttribute = customAttributes[0] as RequestParamAttribute;
						if (requestParamAttribute.Key != null && requestParamAttribute.Key != string.Empty)
						{
							object paramValue = paramEncoder.GetParamValue(requestParamAttribute.Key);
							if (paramValue != null)
							{
								try
								{
									fieldInfo.SetValue(this, paramValue);
								}
								catch (MethodAccessException)
								{
									throw new EncryptedParamException("FieldAccessDenied : " + fieldInfo.Name);
								}
								catch (ArgumentException)
								{
									throw new EncryptedParamException("ConvertingFailed : " + fieldInfo.Name);
								}
								catch (Exception innerException2)
								{
									throw new EncryptedParamException("DecodingFieldsFailed : " + fieldInfo.Name, innerException2);
								}
							}
						}
					}
				}
			}
		}

		public override string URL
		{
			get
			{
				return string.Concat(new string[]
				{
					base.BaseFolder,
					base.PageName,
					"?",
					this.RequestParamName,
					"=",
					this.GetToken()
				});
			}
		}

		private string GetToken()
		{
			ParamEncoder paramEncoder = new ParamEncoder(this.PrivateKey);
			this._type = base.GetType();
			this._fields = this._type.GetFields();
			foreach (FieldInfo fieldInfo in this._fields)
			{
				object[] customAttributes = fieldInfo.GetCustomAttributes(typeof(RequestParamAttribute), false);
				if (customAttributes.Length == 1)
				{
					RequestParamAttribute requestParamAttribute = customAttributes[0] as RequestParamAttribute;
					EncryptedRequestParam.AddParam(paramEncoder, requestParamAttribute.Key, fieldInfo.GetValue(this));
				}
			}
			return paramEncoder.GetToken();
		}

		private static void AddParam(ParamEncoder encoder, string name, object value)
		{
			if (value == null)
			{
				return;
			}
			switch (Type.GetTypeCode(value.GetType()))
			{
			case TypeCode.SByte:
				encoder.AddParam(name, (sbyte)value);
				return;
			case TypeCode.Byte:
				encoder.AddParam(name, (byte)value);
				return;
			case TypeCode.Int16:
				encoder.AddParam(name, (short)value);
				return;
			case TypeCode.UInt16:
				encoder.AddParam(name, (ushort)value);
				return;
			case TypeCode.Int32:
				encoder.AddParam(name, (int)value);
				return;
			case TypeCode.UInt32:
				encoder.AddParam(name, (uint)value);
				return;
			case TypeCode.Int64:
				encoder.AddParam(name, (long)value);
				return;
			case TypeCode.UInt64:
				encoder.AddParam(name, (ulong)value);
				return;
			case TypeCode.String:
				encoder.AddParam(name, (string)value, true);
				return;
			}
			throw new EncryptedParamException("Unsupported type : " + value.GetType().ToString(), null);
		}

		private Type _type;

		private FieldInfo[] _fields;
	}
}
