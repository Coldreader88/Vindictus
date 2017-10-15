using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.ServiceModel.Channels;

namespace Nexon.Enterprise.ServiceFacade.Channels.Text
{
    internal class MessageVersionConverter : TypeConverter
    {
        public MessageVersionConverter()
        {
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (typeof(string) == sourceType)
            {
                return true;
            }
            return base.CanConvertFrom(context, sourceType);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (typeof(InstanceDescriptor) == destinationType)
            {
                return true;
            }
            return base.CanConvertTo(context, destinationType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (!(value is string))
            {
                return base.ConvertFrom(context, culture, value);
            }
            string str = (string)value;
            MessageVersion soap11WSAddressing10 = null;
            string str1 = str;
            string str2 = str1;
            if (str1 != null)
            {
                switch (str2)
                {
                    case "Soap11WSAddressing10":
                        {
                            soap11WSAddressing10 = MessageVersion.Soap11WSAddressing10;
                            break;
                        }
                    case "Soap12WSAddressing10":
                        {
                            soap11WSAddressing10 = MessageVersion.Soap12WSAddressing10;
                            break;
                        }
                    case "Soap11WSAddressingAugust2004":
                        {
                            soap11WSAddressing10 = MessageVersion.Soap11WSAddressingAugust2004;
                            break;
                        }
                    case "Soap12WSAddressingAugust2004":
                        {
                            soap11WSAddressing10 = MessageVersion.Soap12WSAddressingAugust2004;
                            break;
                        }
                    case "Soap11":
                        {
                            soap11WSAddressing10 = MessageVersion.Soap11;
                            break;
                        }
                    case "Soap12":
                        {
                            soap11WSAddressing10 = MessageVersion.Soap12;
                            break;
                        }
                    case "None":
                        {
                            soap11WSAddressing10 = MessageVersion.None;
                            break;
                        }
                    case "Default":
                        {
                            soap11WSAddressing10 = MessageVersion.Default;
                            break;
                        }
                    default:
                        {
                            throw new ArgumentOutOfRangeException("messageVersion");
                        }
                }
                return soap11WSAddressing10;
            }
            throw new ArgumentOutOfRangeException("messageVersion");
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (typeof(string) != destinationType || !(value is MessageVersion))
            {
                return base.ConvertTo(context, culture, value, destinationType);
            }
            string str = null;
            MessageVersion messageVersion = (MessageVersion)value;
            if (messageVersion == MessageVersion.Default)
            {
                str = "Default";
            }
            else if (messageVersion == MessageVersion.Soap11WSAddressing10)
            {
                str = "Soap11WSAddressing10";
            }
            else if (messageVersion == MessageVersion.Soap12WSAddressing10)
            {
                str = "Soap12WSAddressing10";
            }
            else if (messageVersion == MessageVersion.Soap11WSAddressingAugust2004)
            {
                str = "Soap11WSAddressingAugust2004";
            }
            else if (messageVersion == MessageVersion.Soap12WSAddressingAugust2004)
            {
                str = "Soap12WSAddressingAugust2004";
            }
            else if (messageVersion == MessageVersion.Soap11)
            {
                str = "Soap11";
            }
            else if (messageVersion != MessageVersion.Soap12)
            {
                if (messageVersion != MessageVersion.None)
                {
                    throw new ArgumentOutOfRangeException("messageVersion");
                }
                str = "None";
            }
            else
            {
                str = "Soap12";
            }
            return str;
        }
    }
}