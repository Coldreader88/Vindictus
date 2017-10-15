using System;

namespace Nexon.Com.Group.Game.Wrapper.GroupBase
{
	internal class GroupMenuDefaultMenu
	{
		public string GameNotice
		{
			get
			{
				return this._GameNotice;
			}
			set
			{
				this._GameNotice = value;
			}
		}

		public int GameNotice_BoardSN
		{
			get
			{
				return this._GameNotice_BoardSN;
			}
			set
			{
				this._GameNotice_BoardSN = value;
			}
		}

		public string GameUpdate
		{
			get
			{
				return this._GameUpdate;
			}
			set
			{
				this._GameUpdate = value;
			}
		}

		public int GameUpdate_BoardSN
		{
			get
			{
				return this._GameUpdate_BoardSN;
			}
			set
			{
				this._GameUpdate_BoardSN = value;
			}
		}

		private string _GameNotice = string.Empty;

		private int _GameNotice_BoardSN;

		private string _GameUpdate = string.Empty;

		private int _GameUpdate_BoardSN;
	}
}
