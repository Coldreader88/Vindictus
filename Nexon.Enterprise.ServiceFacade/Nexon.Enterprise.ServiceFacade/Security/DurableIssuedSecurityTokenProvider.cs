using System;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.ServiceModel;
using System.ServiceModel.Security.Tokens;
using System.Threading;

namespace Nexon.Enterprise.ServiceFacade.Security
{
	public class DurableIssuedSecurityTokenProvider : SecurityTokenProvider, ICommunicationObject
	{
		public DurableIssuedSecurityTokenProvider(IssuedSecurityTokenProvider innerTokenProvider, IssuedTokenCache cache)
		{
			if (cache == null)
			{
				throw new ArgumentNullException("cache");
			}
			if (innerTokenProvider == null)
			{
				throw new ArgumentNullException("innerTokenProvider");
			}
			this.innerTokenProvider = innerTokenProvider;
			this.cache = cache;
			this.target = innerTokenProvider.TargetAddress;
			this.issuer = innerTokenProvider.IssuerAddress;
		}

		protected override SecurityToken GetTokenCore(TimeSpan timeout)
		{
			GenericXmlSecurityToken genericXmlSecurityToken;
			if (!this.cache.TryGetToken(this.target, this.issuer, out genericXmlSecurityToken))
			{
				genericXmlSecurityToken = (GenericXmlSecurityToken)this.innerTokenProvider.GetToken(timeout);
				this.cache.AddToken(genericXmlSecurityToken, this.target, this.issuer);
			}
			return genericXmlSecurityToken;
		}

		protected override IAsyncResult BeginGetTokenCore(TimeSpan timeout, AsyncCallback callback, object state)
		{
			GenericXmlSecurityToken token;
			if (this.cache.TryGetToken(this.target, this.issuer, out token))
			{
				return new DurableIssuedSecurityTokenProvider.CompletedAsyncResult(token, callback, state);
			}
			return this.innerTokenProvider.BeginGetToken(timeout, callback, state);
		}

		protected override SecurityToken EndGetTokenCore(IAsyncResult result)
		{
			DurableIssuedSecurityTokenProvider.CompletedAsyncResult completedAsyncResult = result as DurableIssuedSecurityTokenProvider.CompletedAsyncResult;
			if (completedAsyncResult != null)
			{
				return completedAsyncResult.Token;
			}
			GenericXmlSecurityToken genericXmlSecurityToken = (GenericXmlSecurityToken)this.innerTokenProvider.EndGetToken(result);
			this.cache.AddToken(genericXmlSecurityToken, this.target, this.issuer);
			return genericXmlSecurityToken;
		}

		public void Abort()
		{
			this.innerTokenProvider.Abort();
		}

		public IAsyncResult BeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.innerTokenProvider.BeginClose(timeout, callback, state);
		}

		public IAsyncResult BeginClose(AsyncCallback callback, object state)
		{
			return this.innerTokenProvider.BeginClose(callback, state);
		}

		public IAsyncResult BeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.innerTokenProvider.BeginOpen(timeout, callback, state);
		}

		public IAsyncResult BeginOpen(AsyncCallback callback, object state)
		{
			return this.innerTokenProvider.BeginOpen(callback, state);
		}

		public void Close(TimeSpan timeout)
		{
			this.innerTokenProvider.Close(timeout);
		}

		public void Close()
		{
			this.innerTokenProvider.Close();
		}

		public event EventHandler Closed
		{
			add
			{
				this.innerTokenProvider.Closed += value;
			}
			remove
			{
				this.innerTokenProvider.Closed -= value;
			}
		}

		public event EventHandler Closing
		{
			add
			{
				this.innerTokenProvider.Closing += value;
			}
			remove
			{
				this.innerTokenProvider.Closing -= value;
			}
		}

		public void EndClose(IAsyncResult result)
		{
			this.innerTokenProvider.EndClose(result);
		}

		public void EndOpen(IAsyncResult result)
		{
			this.innerTokenProvider.EndOpen(result);
		}

		public event EventHandler Faulted
		{
			add
			{
				this.innerTokenProvider.Faulted += value;
			}
			remove
			{
				this.innerTokenProvider.Faulted -= value;
			}
		}

		public void Open(TimeSpan timeout)
		{
			this.innerTokenProvider.Open(timeout);
		}

		public void Open()
		{
			this.innerTokenProvider.Open();
		}

		public event EventHandler Opened
		{
			add
			{
				this.innerTokenProvider.Opened += value;
			}
			remove
			{
				this.innerTokenProvider.Opened -= value;
			}
		}

		public event EventHandler Opening
		{
			add
			{
				this.innerTokenProvider.Opening += value;
			}
			remove
			{
				this.innerTokenProvider.Opening -= value;
			}
		}

		public CommunicationState State
		{
			get
			{
				return this.innerTokenProvider.State;
			}
		}

		private IssuedSecurityTokenProvider innerTokenProvider;

		private IssuedTokenCache cache;

		private EndpointAddress target;

		private EndpointAddress issuer;

		private class CompletedAsyncResult : IAsyncResult
		{
			public CompletedAsyncResult(GenericXmlSecurityToken token, AsyncCallback callback, object state)
			{
				this.token = token;
				this.state = state;
				if (callback != null)
				{
					callback(this);
				}
			}

			public GenericXmlSecurityToken Token
			{
				get
				{
					return this.token;
				}
			}

			public object AsyncState
			{
				get
				{
					return this.state;
				}
			}

			public WaitHandle AsyncWaitHandle
			{
				get
				{
					lock (this.thisLock)
					{
						if (this.handle == null)
						{
							this.handle = new ManualResetEvent(true);
						}
					}
					return this.handle;
				}
			}

			public bool CompletedSynchronously
			{
				get
				{
					return true;
				}
			}

			public bool IsCompleted
			{
				get
				{
					return true;
				}
			}

			private GenericXmlSecurityToken token;

			private object state;

			private WaitHandle handle;

			private object thisLock = new object();
		}
	}
}
