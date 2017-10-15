using System;
using System.Data.Linq.Mapping;
using System.Xml.Linq;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.StoryNpcTalkInfo")]
	public class StoryNpcTalkInfo
	{
		[Column(Name = "[Key]", Storage = "_Key", DbType = "NVarChar(30) NOT NULL", CanBeNull = false)]
		public string Key
		{
			get
			{
				return this._Key;
			}
			set
			{
				if (this._Key != value)
				{
					this._Key = value;
				}
			}
		}

		[Column(Storage = "_Script", DbType = "Xml NOT NULL", CanBeNull = false, UpdateCheck = UpdateCheck.Never)]
		public XElement Script
		{
			get
			{
				return this._Script;
			}
			set
			{
				if (this._Script != value)
				{
					this._Script = value;
				}
			}
		}

		private string _Key;

		private XElement _Script;
	}
}
