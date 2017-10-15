using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Nexon.Com.Web.UI.WebControls
{
	[DefaultProperty("DataSource")]
	[DefaultEvent("ItemCommand")]
	[PersistChildren(false)]
	[ParseChildren(true)]
	public class Repeater : System.Web.UI.WebControls.Repeater
    {
		public Repeater()
		{
			this.EnableViewState = true;
		}

		public int DataCount
		{
			get
			{
				if (this._DataCount == -1)
				{
					this._DataCount = 0;
					if (this.GetData() == null)
					{
						this._DataCount = (int)this.ViewState["_!ItemCount"];
					}
					else
					{
						foreach (object obj in this.GetData())
						{
							this._DataCount++;
						}
					}
				}
				return this._DataCount;
			}
		}

		public int ItemCount
		{
			get
			{
				return this._emptyItemCount + this.Items.Count;
			}
		}

		protected override void CreateControlHierarchy(bool useDataSource)
		{
			base.CreateControlHierarchy(useDataSource);
			if (!this._renderFooter && useDataSource && this.DataCount == 0 && this.Items.Count == 0)
			{
				this.AddAllEmptyItem();
			}
			if (!this._renderFooter && this.JumpSeparatorInterval > 0 && this._jumpSeparatorTemplate != null && this.Items.Count > 0)
			{
				this.AddEmptyItemAtTail();
			}
		}

		protected override void OnItemCreated(RepeaterItemEventArgs e)
		{
			if (this.EmptyItemCountBeforeItem > 0 && this.JumpSeparatorInterval > 0 && this.EmptyItemCountBeforeItem >= this.JumpSeparatorInterval)
			{
				throw new Exception("EmptyItemCountBeforeItem 는 JumpSeparatorInterval 보다 작아야 합니다. ");
			}
			base.OnItemCreated(e);
			if (e.Item.ItemType == ListItemType.Item && this.DataCount > 0 && this.EmptyItemCountBeforeItem > 0 && this._emptyItemTemplate != null && this.Items.Count == 0)
			{
				for (int i = 0; i < this.EmptyItemCountBeforeItem; i++)
				{
					EmptyItem emptyItem = new EmptyItem();
					this._emptyItemTemplate.InstantiateIn(emptyItem);
					this.Controls.Add(emptyItem);
					this._emptyItemCount++;
				}
			}
			if (e.Item.ItemType == ListItemType.Footer)
			{
				this._renderFooter = true;
				if (this.DataCount == 0)
				{
					this.AddAllEmptyItem();
				}
				if (this.JumpSeparatorInterval > 0 && this._jumpSeparatorTemplate != null && this.Items.Count > 0)
				{
					this.AddEmptyItemAtTail();
				}
			}
			if (this.JumpSeparatorInterval > 0 && this._jumpSeparatorTemplate != null && this.Items.Count > 0 && (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem) && this.ItemCount % this.JumpSeparatorInterval == 0)
			{
				if (this.SeparatorTemplate != null)
				{
					this.Controls.RemoveAt(this.Controls.Count - 1);
				}
				JumpSeparator jumpSeparator = new JumpSeparator();
				this._jumpSeparatorTemplate.InstantiateIn(jumpSeparator);
				this.Controls.Add(jumpSeparator);
			}
		}

		private void AddAllEmptyItem()
		{
			if (this._allEmptyItemTemplate != null)
			{
				AllEmptyItem allEmptyItem = new AllEmptyItem();
				this._allEmptyItemTemplate.InstantiateIn(allEmptyItem);
				this.Controls.Add(allEmptyItem);
			}
		}

		private void AddEmptyItemAtTail()
		{
			if (this._emptyItemTemplate != null)
			{
				int num = this.ItemCount % this.JumpSeparatorInterval;
				while (num != 0 && num < this.JumpSeparatorInterval)
				{
					EmptyItem emptyItem = new EmptyItem();
					this._emptyItemTemplate.InstantiateIn(emptyItem);
					this.Controls.Add(emptyItem);
					num++;
				}
			}
		}

		[TemplateContainer(typeof(RepeaterItem))]
		[Browsable(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue(null)]
		public virtual ITemplate EmptyItemTemplate
		{
			get
			{
				return this._emptyItemTemplate;
			}
			set
			{
				this._emptyItemTemplate = value;
			}
		}

		[DefaultValue(null)]
		[Browsable(false)]
		[TemplateContainer(typeof(RepeaterItem))]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public virtual ITemplate JumpSeparatorTemplate
		{
			get
			{
				return this._jumpSeparatorTemplate;
			}
			set
			{
				this._jumpSeparatorTemplate = value;
			}
		}

		[Browsable(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue(null)]
		[TemplateContainer(typeof(RepeaterItem))]
		public virtual ITemplate AllEmptyItemTemplate
		{
			get
			{
				return this._allEmptyItemTemplate;
			}
			set
			{
				this._allEmptyItemTemplate = value;
			}
		}

		private ITemplate _jumpSeparatorTemplate;

		private ITemplate _emptyItemTemplate;

		private ITemplate _allEmptyItemTemplate;

		private bool _renderFooter;

		public int EmptyItemCountBeforeItem;

		private int _DataCount = -1;

		private int _emptyItemCount;

		public int JumpSeparatorInterval;
	}
}
