using System;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace Nexon.Enterprise.ServiceFacade
{
	public abstract class RequestSecurityTokenBase : BodyWriter
	{
		protected RequestSecurityTokenBase() : this(string.Empty, string.Empty, 0, null)
		{
		}

		protected RequestSecurityTokenBase(string context, string tokenType, int keySize, EndpointAddress appliesTo) : base(true)
		{
			this.m_context = context;
			this.m_tokenType = tokenType;
			this.m_keySize = keySize;
			this.m_appliesTo = appliesTo;
		}

		public string Context
		{
			get
			{
				return this.m_context;
			}
			set
			{
				this.m_context = value;
			}
		}

		public string TokenType
		{
			get
			{
				return this.m_tokenType;
			}
			set
			{
				this.m_tokenType = value;
			}
		}

		public int KeySize
		{
			get
			{
				return this.m_keySize;
			}
			set
			{
				this.m_keySize = value;
			}
		}

		public EndpointAddress AppliesTo
		{
			get
			{
				return this.m_appliesTo;
			}
			set
			{
				this.m_appliesTo = value;
			}
		}

		private string m_context;

		private string m_tokenType;

		private int m_keySize;

		private EndpointAddress m_appliesTo;
	}
}
