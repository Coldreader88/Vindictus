using System;
using System.Reflection;

namespace Nexon.Com.Encryption
{
	public abstract class EncryptedParamContainer
	{
		public bool IsValid
		{
			get
			{
				return this._isValid;
			}
		}

		protected abstract ushort PrivateKey { get; }

		public EncryptedParamContainer()
		{
		}

		public virtual void Reset()
		{
			this._isValid = false;
		}

        public virtual void SetToken(string token)
        {
            this.Reset();
            if (token == null)
            {
                throw new EncryptedParamException("NullToken", null);
            }
            ParamEncoder paramEncoder = new ParamEncoder(this.PrivateKey);
            try
            {
                if (!paramEncoder.SetToken(token))
                {
                    throw new EncryptedParamException("InvalidToken", null);
                }
            }
            catch (Exception exception)
            {
                throw new EncryptedParamException("DecryptingTokenFailed", exception);
            }
            Type type = this.GetType();
            FieldInfo[] fields = type.GetFields();
            PropertyInfo[] properties = type.GetProperties();
            FieldInfo[] fieldInfoArray = fields;
            for (int i = 0; i < (int)fieldInfoArray.Length; i++)
            {
                FieldInfo fieldInfo = fieldInfoArray[i];
                object[] customAttributes = fieldInfo.GetCustomAttributes(typeof(EncryptedParamAttribute), false);
                if ((int)customAttributes.Length == 1)
                {
                    EncryptedParamAttribute encryptedParamAttribute = customAttributes[0] as EncryptedParamAttribute;
                    object paramValue = paramEncoder.GetParamValue(encryptedParamAttribute.Key);
                    if (paramValue != null)
                    {
                        try
                        {
                            fieldInfo.SetValue(this, paramValue);
                        }
                        catch (FieldAccessException)
                        {
                            throw new EncryptedParamException(string.Concat("FieldAccessDenied : ", fieldInfo.Name));
                        }
                        catch (ArgumentException)
                        {
                            throw new EncryptedParamException(string.Concat("ConvertingFailed : ", fieldInfo.Name));
                        }
                        catch (Exception exception2)
                        {
                            Exception exception1 = exception2;
                            throw new EncryptedParamException(string.Concat("DecodingFieldsFailed : ", fieldInfo.Name), exception1);
                        }
                    }
                    else if (!encryptedParamAttribute.Optional)
                    {
                        string[] name = new string[] { "ParamNotExists : ", fieldInfo.Name, "[", encryptedParamAttribute.Key, "]" };
                        throw new EncryptedParamException(string.Concat(name));
                    }
                }
            }
            PropertyInfo[] propertyInfoArray = properties;
            for (int j = 0; j < (int)propertyInfoArray.Length; j++)
            {
                PropertyInfo propertyInfo = propertyInfoArray[j];
                object[] objArray = propertyInfo.GetCustomAttributes(typeof(EncryptedParamAttribute), false);
                if ((int)objArray.Length == 1)
                {
                    EncryptedParamAttribute encryptedParamAttribute1 = objArray[0] as EncryptedParamAttribute;
                    object obj = paramEncoder.GetParamValue(encryptedParamAttribute1.Key);
                    if (obj != null)
                    {
                        try
                        {
                            propertyInfo.SetValue(this, obj, null);
                        }
                        catch (MethodAccessException)
                        {
                            throw new EncryptedParamException(string.Concat("FieldAccessDenied : ", propertyInfo.Name));
                        }
                        catch (ArgumentException)
                        {
                            throw new EncryptedParamException(string.Concat("ConvertingFailed : ", propertyInfo.Name));
                        }
                        catch (Exception exception4)
                        {
                            Exception exception3 = exception4;
                            throw new EncryptedParamException(string.Concat("DecodingFieldsFailed : ", propertyInfo.Name), exception3);
                        }
                    }
                    else if (!encryptedParamAttribute1.Optional)
                    {
                        string[] strArrays = new string[] { "ParamNotExists : ", propertyInfo.Name, "[", encryptedParamAttribute1.Key, "]" };
                        throw new EncryptedParamException(string.Concat(strArrays));
                    }
                }
            }
            this._isValid = true;
        }

    private static void AddParam(ParamEncoder encoder, string name, object value, bool unicode)
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
				encoder.AddParam(name, (string)value, unicode);
				return;
			}
			throw new EncryptedParamException("Unsupported type : " + value.GetType().ToString(), null);
		}

		public string GetToken()
		{
			ParamEncoder paramEncoder = new ParamEncoder(this.PrivateKey);
			Type type = base.GetType();
			FieldInfo[] fields = type.GetFields();
			foreach (FieldInfo fieldInfo in fields)
			{
				object[] customAttributes = fieldInfo.GetCustomAttributes(typeof(EncryptedParamAttribute), false);
				if (customAttributes.Length == 1)
				{
					EncryptedParamAttribute encryptedParamAttribute = customAttributes[0] as EncryptedParamAttribute;
					EncryptedParamContainer.AddParam(paramEncoder, encryptedParamAttribute.Key, fieldInfo.GetValue(this), encryptedParamAttribute.Unicode);
				}
			}
			PropertyInfo[] properties = type.GetProperties();
			foreach (PropertyInfo propertyInfo in properties)
			{
				object[] customAttributes2 = propertyInfo.GetCustomAttributes(typeof(EncryptedParamAttribute), false);
				if (customAttributes2.Length == 1)
				{
					EncryptedParamAttribute encryptedParamAttribute2 = customAttributes2[0] as EncryptedParamAttribute;
					EncryptedParamContainer.AddParam(paramEncoder, encryptedParamAttribute2.Key, propertyInfo.GetValue(this, null), encryptedParamAttribute2.Unicode);
				}
			}
			return paramEncoder.GetToken();
		}

		private bool _isValid;
	}
}
