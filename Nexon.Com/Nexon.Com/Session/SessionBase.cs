using System;
using System.Collections;
using System.Web;
using Nexon.Com.Encryption;

namespace Nexon.Com.Session
{
	public abstract class SessionBase : EncryptedParamContainer
	{
		public SessionBase()
		{
			if (HttpContext.Current != null)
			{
				if (!HttpContext.Current.Items.Contains("Framework::SessionCollection"))
				{
					HttpContext.Current.Items["Framework::SessionCollection"] = new Hashtable();
				}
				Hashtable hashtable = (Hashtable)HttpContext.Current.Items["Framework::SessionCollection"];
				if (!hashtable.Contains(this.UniqueInstanceNameInCurrentContext))
				{
					hashtable[this.UniqueInstanceNameInCurrentContext] = this;
				}
			}
		}

		public abstract string UniqueInstanceNameInCurrentContext { get; }

		protected abstract void ClearImpl();

		public virtual void Clear()
		{
			if (HttpContext.Current != null && HttpContext.Current.Items.Contains("Framework::SessionCollection"))
			{
				Hashtable hashtable = (Hashtable)HttpContext.Current.Items["Framework::SessionCollection"];
				if (hashtable.Contains(this.UniqueInstanceNameInCurrentContext))
				{
					hashtable.Remove(this.UniqueInstanceNameInCurrentContext);
				}
			}
			this.ClearImpl();
		}

		public static T GetSessionInCurrentContext<T>(string UniqueInstanceNameInCurrentContext) where T : SessionBase
		{
			if (HttpContext.Current == null)
			{
				return default(T);
			}
			if (HttpContext.Current.Items.Contains("Framework::SessionCollection"))
			{
				Hashtable hashtable = (Hashtable)HttpContext.Current.Items["Framework::SessionCollection"];
				return hashtable[UniqueInstanceNameInCurrentContext] as T;
			}
			return default(T);
		}

		private const string ContextName = "Framework::SessionCollection";
	}
}
