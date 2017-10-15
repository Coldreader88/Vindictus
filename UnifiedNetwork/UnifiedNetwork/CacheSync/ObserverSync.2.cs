using System;
using UnifiedNetwork.Cooperation;

namespace UnifiedNetwork.CacheSync
{
	public class ObserverSync<ResultT> : ISynchronizable<ResultT>, ISynchronizable
	{
		public IObserver Observer { get; private set; }

		public ObserverSync(IObserver observer)
		{
			this.Observer = observer;
		}

		public ObserverSync(IObserver observer, Func<object, ResultT> converter) : this(observer)
		{
			this.converter = converter;
		}

		public void OnSync()
		{
			this.Observer.ForceSync(delegate(IObserver _)
			{
				this.Result = !this.Observer.IsDirty;
				if (this.Result)
				{
					if (this.converter == null)
					{
						if (this.Observer.Cache != null && typeof(ResultT).IsAssignableFrom(this.Observer.Cache.GetType()))
						{
							this.ReturnValue = (ResultT)((object)this.Observer.Cache);
						}
					}
					else
					{
						this.ReturnValue = this.converter(this.Observer.Cache);
					}
				}
				this.OnFinished(this);
			});
		}

		public event Action<ISynchronizable> OnFinished;

		public bool Result { get; set; }

		public ResultT ReturnValue { get; private set; }

		private Func<object, ResultT> converter;
	}
}
