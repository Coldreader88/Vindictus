using System;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace Nexon.Enterprise.ServiceFacade
{
	[DataContract]
	public class GenericContext<T>
	{
		private static bool IsDataContract(Type type)
		{
			object[] customAttributes = type.GetCustomAttributes(typeof(DataContractAttribute), false);
			return customAttributes.Length == 1;
		}

		public GenericContext(T value)
		{
			this.Value = value;
		}

		public GenericContext() : this(default(T))
		{
		}

		public static GenericContext<T> Current
		{
			get
			{
				OperationContext operationContext = OperationContext.Current;
				if (operationContext == null)
				{
					return null;
				}
				GenericContext<T> result;
				try
				{
					string text = typeof(T).ToString().Replace("`", "");
					text = text.Replace("[", "");
					text = text.Replace("]", "");
					string ns = typeof(T).Namespace ?? "";
					result = operationContext.IncomingMessageHeaders.GetHeader<GenericContext<T>>(text, ns);
				}
				catch (Exception)
				{
					result = null;
				}
				return result;
			}
			set
			{
				OperationContext operationContext = OperationContext.Current;
				string text = typeof(T).ToString().Replace("`", "");
				text = text.Replace("[", "");
				text = text.Replace("]", "");
				string text2 = typeof(T).Namespace ?? "";
				bool flag = false;
				try
				{
					operationContext.OutgoingMessageHeaders.GetHeader<GenericContext<T>>(text, text2);
					flag = true;
				}
				catch (MessageHeaderException)
				{
				}
				if (flag)
				{
					throw new InvalidOperationException(string.Concat(new string[]
					{
						"A header with name ",
						text,
						" and namespace ",
						text2,
						" already exists in the message."
					}));
				}
				MessageHeader<GenericContext<T>> messageHeader = new MessageHeader<GenericContext<T>>(value);
				operationContext.OutgoingMessageHeaders.Add(messageHeader.GetUntypedHeader(text, text2));
			}
		}

		[DataMember]
		public readonly T Value;
	}
}
