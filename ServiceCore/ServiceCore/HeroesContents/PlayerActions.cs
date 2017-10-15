using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.PlayerActions")]
	public class PlayerActions
	{
		[Column(Storage = "_ActionName", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
		public string ActionName
		{
			get
			{
				return this._ActionName;
			}
			set
			{
				if (this._ActionName != value)
				{
					this._ActionName = value;
				}
			}
		}

		private string _ActionName;
	}
}
