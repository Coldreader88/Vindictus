namespace AdminClient
{
	public partial class AdminClientForm : global::System.Windows.Forms.Form
	{

		private void InitializeComponent()
		{
            this.ShutDownBottuon = new System.Windows.Forms.Button();
            this.KickCharacterIDTextBox = new System.Windows.Forms.TextBox();
            this.ShutDownTimeListBox = new System.Windows.Forms.ListBox();
            this.RefreshUserCountButton = new System.Windows.Forms.Button();
            this.NotifyButton = new System.Windows.Forms.Button();
            this.KickUserButton = new System.Windows.Forms.Button();
            this.NotifyTextBox = new System.Windows.Forms.TextBox();
            this.UserCountTextBox = new System.Windows.Forms.TextBox();
            this.UserIDLabel = new System.Windows.Forms.Label();
            this.CharacterIDLabel = new System.Windows.Forms.Label();
            this.StopServiceButton = new System.Windows.Forms.Button();
            this.ResumeServiceButton = new System.Windows.Forms.Button();
            this.StopServiceSelectComboBox = new System.Windows.Forms.ComboBox();
            this.ResumeServiceSelectComboBox = new System.Windows.Forms.ComboBox();
            this.KickUserIDTextBox = new System.Windows.Forms.TextBox();
            this.ConsoleCommandTextBox = new System.Windows.Forms.TextBox();
            this.HostCommandButton = new System.Windows.Forms.Button();
            this.ClientCommandButton = new System.Windows.Forms.Button();
            this.ShurinkButton_c = new System.Windows.Forms.Button();
            this.ExpandButton_a = new System.Windows.Forms.Button();
            this.CopyToClipBoardButton = new System.Windows.Forms.Button();
            this.ExpandButton_b = new System.Windows.Forms.Button();
            this.ShurinkButton_b = new System.Windows.Forms.Button();
            this.ExpandButton_c = new System.Windows.Forms.Button();
            this.ShurinkButton_a = new System.Windows.Forms.Button();
            this.ItemFestivalButton = new System.Windows.Forms.Button();
            this.ItemFestivalClassNameTextBox = new System.Windows.Forms.TextBox();
            this.ItemFestivalAmountUpDown = new System.Windows.Forms.NumericUpDown();
            this.ItemFestivalMessageTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.CafeCheckBox = new System.Windows.Forms.CheckBox();
            this.ExtendExpireTimeButton = new System.Windows.Forms.Button();
            this.CashExtendMinutesUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.GameServerCommandButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ItemFestivalAmountUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CashExtendMinutesUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // ShutDownBottuon
            // 
            this.ShutDownBottuon.Location = new System.Drawing.Point(10, 203);
            this.ShutDownBottuon.Name = "ShutDownBottuon";
            this.ShutDownBottuon.Size = new System.Drawing.Size(100, 56);
            this.ShutDownBottuon.TabIndex = 2;
            this.ShutDownBottuon.Text = "ShutDown";
            this.ShutDownBottuon.UseVisualStyleBackColor = true;
            this.ShutDownBottuon.Click += new System.EventHandler(this.ShutDownBottuon_Click);
            // 
            // KickCharacterIDTextBox
            // 
            this.KickCharacterIDTextBox.Location = new System.Drawing.Point(186, 295);
            this.KickCharacterIDTextBox.Name = "KickCharacterIDTextBox";
            this.KickCharacterIDTextBox.Size = new System.Drawing.Size(200, 20);
            this.KickCharacterIDTextBox.TabIndex = 5;
            this.KickCharacterIDTextBox.TextChanged += new System.EventHandler(this.KickCharacterIDTextBox_TextChanged);
            this.KickCharacterIDTextBox.Enter += new System.EventHandler(this.KickCharacterIDTextBox_Enter);
            // 
            // ShutDownTimeListBox
            // 
            this.ShutDownTimeListBox.FormattingEnabled = true;
            this.ShutDownTimeListBox.Items.AddRange(new object[] {
            "Exit after 3600 seconds (1 hour)",
            "End in 1800 seconds (30 minutes)",
            "End in 600 seconds (10 minutes)",
            "End after 300 seconds (5 minutes)",
            "Exit after 60 seconds",
            "Exit after 15 seconds",
            "Exit after 10 seconds",
            "Exit after 5 seconds"});
            this.ShutDownTimeListBox.Location = new System.Drawing.Point(116, 203);
            this.ShutDownTimeListBox.Name = "ShutDownTimeListBox";
            this.ShutDownTimeListBox.Size = new System.Drawing.Size(271, 56);
            this.ShutDownTimeListBox.TabIndex = 7;
            // 
            // RefreshUserCountButton
            // 
            this.RefreshUserCountButton.Location = new System.Drawing.Point(10, 13);
            this.RefreshUserCountButton.Name = "RefreshUserCountButton";
            this.RefreshUserCountButton.Size = new System.Drawing.Size(100, 26);
            this.RefreshUserCountButton.TabIndex = 8;
            this.RefreshUserCountButton.Text = "RefreshUser";
            this.RefreshUserCountButton.UseVisualStyleBackColor = true;
            this.RefreshUserCountButton.Click += new System.EventHandler(this.RefreshUserCountButton_Click);
            // 
            // NotifyButton
            // 
            this.NotifyButton.Location = new System.Drawing.Point(10, 140);
            this.NotifyButton.Name = "NotifyButton";
            this.NotifyButton.Size = new System.Drawing.Size(100, 56);
            this.NotifyButton.TabIndex = 9;
            this.NotifyButton.Text = "Notify";
            this.NotifyButton.UseVisualStyleBackColor = true;
            this.NotifyButton.Click += new System.EventHandler(this.NotifyButton_Click);
            // 
            // KickUserButton
            // 
            this.KickUserButton.Location = new System.Drawing.Point(10, 265);
            this.KickUserButton.Name = "KickUserButton";
            this.KickUserButton.Size = new System.Drawing.Size(100, 56);
            this.KickUserButton.TabIndex = 10;
            this.KickUserButton.Text = "Kick User";
            this.KickUserButton.UseVisualStyleBackColor = true;
            this.KickUserButton.Click += new System.EventHandler(this.KickUserButton_Click);
            // 
            // NotifyTextBox
            // 
            this.NotifyTextBox.Location = new System.Drawing.Point(116, 140);
            this.NotifyTextBox.Multiline = true;
            this.NotifyTextBox.Name = "NotifyTextBox";
            this.NotifyTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.NotifyTextBox.Size = new System.Drawing.Size(271, 56);
            this.NotifyTextBox.TabIndex = 11;
            this.NotifyTextBox.Enter += new System.EventHandler(this.NotifyTextBox_Enter);
            // 
            // UserCountTextBox
            // 
            this.UserCountTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.UserCountTextBox.Location = new System.Drawing.Point(117, 13);
            this.UserCountTextBox.Multiline = true;
            this.UserCountTextBox.Name = "UserCountTextBox";
            this.UserCountTextBox.ReadOnly = true;
            this.UserCountTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.UserCountTextBox.Size = new System.Drawing.Size(270, 119);
            this.UserCountTextBox.TabIndex = 12;
            this.UserCountTextBox.WordWrap = false;
            // 
            // UserIDLabel
            // 
            this.UserIDLabel.AutoSize = true;
            this.UserIDLabel.Location = new System.Drawing.Point(113, 268);
            this.UserIDLabel.Name = "UserIDLabel";
            this.UserIDLabel.Size = new System.Drawing.Size(43, 13);
            this.UserIDLabel.TabIndex = 13;
            this.UserIDLabel.Text = "User ID";
            // 
            // CharacterIDLabel
            // 
            this.CharacterIDLabel.AutoSize = true;
            this.CharacterIDLabel.Location = new System.Drawing.Point(113, 302);
            this.CharacterIDLabel.Name = "CharacterIDLabel";
            this.CharacterIDLabel.Size = new System.Drawing.Size(67, 13);
            this.CharacterIDLabel.TabIndex = 14;
            this.CharacterIDLabel.Text = "Character ID";
            // 
            // StopServiceButton
            // 
            this.StopServiceButton.Location = new System.Drawing.Point(10, 447);
            this.StopServiceButton.Name = "StopServiceButton";
            this.StopServiceButton.Size = new System.Drawing.Size(100, 24);
            this.StopServiceButton.TabIndex = 15;
            this.StopServiceButton.Text = "Stop Service";
            this.StopServiceButton.UseVisualStyleBackColor = true;
            this.StopServiceButton.Click += new System.EventHandler(this.StopServiceButton_Click);
            // 
            // ResumeServiceButton
            // 
            this.ResumeServiceButton.Location = new System.Drawing.Point(10, 478);
            this.ResumeServiceButton.Name = "ResumeServiceButton";
            this.ResumeServiceButton.Size = new System.Drawing.Size(100, 24);
            this.ResumeServiceButton.TabIndex = 18;
            this.ResumeServiceButton.Text = "Resume Service";
            this.ResumeServiceButton.UseVisualStyleBackColor = true;
            this.ResumeServiceButton.Click += new System.EventHandler(this.ResumeServiceButton_Click);
            // 
            // StopServiceSelectComboBox
            // 
            this.StopServiceSelectComboBox.FormattingEnabled = true;
            this.StopServiceSelectComboBox.Items.AddRange(new object[] {
            "CashShop",
            "CraftItem",
            "CreateCharacter",
            "Mailing",
            "Shipping",
            "StoryTelling"});
            this.StopServiceSelectComboBox.Location = new System.Drawing.Point(117, 450);
            this.StopServiceSelectComboBox.Name = "StopServiceSelectComboBox";
            this.StopServiceSelectComboBox.Size = new System.Drawing.Size(269, 21);
            this.StopServiceSelectComboBox.Sorted = true;
            this.StopServiceSelectComboBox.TabIndex = 19;
            // 
            // ResumeServiceSelectComboBox
            // 
            this.ResumeServiceSelectComboBox.FormattingEnabled = true;
            this.ResumeServiceSelectComboBox.Location = new System.Drawing.Point(117, 478);
            this.ResumeServiceSelectComboBox.Name = "ResumeServiceSelectComboBox";
            this.ResumeServiceSelectComboBox.Size = new System.Drawing.Size(269, 21);
            this.ResumeServiceSelectComboBox.Sorted = true;
            this.ResumeServiceSelectComboBox.TabIndex = 20;
            // 
            // KickUserIDTextBox
            // 
            this.KickUserIDTextBox.Location = new System.Drawing.Point(186, 265);
            this.KickUserIDTextBox.Name = "KickUserIDTextBox";
            this.KickUserIDTextBox.Size = new System.Drawing.Size(200, 20);
            this.KickUserIDTextBox.TabIndex = 4;
            this.KickUserIDTextBox.TextChanged += new System.EventHandler(this.KickUserIDTextBox_TextChanged);
            this.KickUserIDTextBox.Enter += new System.EventHandler(this.KickUserIDTextBox_Enter);
            // 
            // ConsoleCommandTextBox
            // 
            this.ConsoleCommandTextBox.Location = new System.Drawing.Point(116, 361);
            this.ConsoleCommandTextBox.Multiline = true;
            this.ConsoleCommandTextBox.Name = "ConsoleCommandTextBox";
            this.ConsoleCommandTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.ConsoleCommandTextBox.Size = new System.Drawing.Size(271, 78);
            this.ConsoleCommandTextBox.TabIndex = 22;
            // 
            // HostCommandButton
            // 
            this.HostCommandButton.Location = new System.Drawing.Point(10, 385);
            this.HostCommandButton.Name = "HostCommandButton";
            this.HostCommandButton.Size = new System.Drawing.Size(100, 24);
            this.HostCommandButton.TabIndex = 23;
            this.HostCommandButton.Text = "Host Command";
            this.HostCommandButton.UseVisualStyleBackColor = true;
            this.HostCommandButton.Click += new System.EventHandler(this.HostCommandButton_Click);
            // 
            // ClientCommandButton
            // 
            this.ClientCommandButton.Location = new System.Drawing.Point(10, 361);
            this.ClientCommandButton.Name = "ClientCommandButton";
            this.ClientCommandButton.Size = new System.Drawing.Size(100, 24);
            this.ClientCommandButton.TabIndex = 24;
            this.ClientCommandButton.Text = "Client Command";
            this.ClientCommandButton.UseVisualStyleBackColor = true;
            this.ClientCommandButton.Click += new System.EventHandler(this.ClientCommandButton_Click);
            // 
            // ShurinkButton_c
            // 
            this.ShurinkButton_c.Location = new System.Drawing.Point(180, 597);
            this.ShurinkButton_c.Name = "ShurinkButton_c";
            this.ShurinkButton_c.Size = new System.Drawing.Size(100, 24);
            this.ShurinkButton_c.TabIndex = 25;
            this.ShurinkButton_c.Text = "Fold";
            this.ShurinkButton_c.UseVisualStyleBackColor = true;
            this.ShurinkButton_c.Click += new System.EventHandler(this.ShurinkButton_c_Click);
            // 
            // ExpandButton_a
            // 
            this.ExpandButton_a.Location = new System.Drawing.Point(285, 140);
            this.ExpandButton_a.Name = "ExpandButton_a";
            this.ExpandButton_a.Size = new System.Drawing.Size(100, 24);
            this.ExpandButton_a.TabIndex = 26;
            this.ExpandButton_a.Text = "Expand";
            this.ExpandButton_a.UseVisualStyleBackColor = true;
            this.ExpandButton_a.Click += new System.EventHandler(this.ExpandButton_a_Click);
            // 
            // CopyToClipBoardButton
            // 
            this.CopyToClipBoardButton.Location = new System.Drawing.Point(10, 46);
            this.CopyToClipBoardButton.Name = "CopyToClipBoardButton";
            this.CopyToClipBoardButton.Size = new System.Drawing.Size(100, 24);
            this.CopyToClipBoardButton.TabIndex = 27;
            this.CopyToClipBoardButton.Text = "CopyToClipBoard";
            this.CopyToClipBoardButton.UseVisualStyleBackColor = true;
            this.CopyToClipBoardButton.Click += new System.EventHandler(this.CopyToClipBoardButton_Click);
            // 
            // ExpandButton_b
            // 
            this.ExpandButton_b.Location = new System.Drawing.Point(285, 330);
            this.ExpandButton_b.Name = "ExpandButton_b";
            this.ExpandButton_b.Size = new System.Drawing.Size(100, 24);
            this.ExpandButton_b.TabIndex = 28;
            this.ExpandButton_b.Text = "Expand";
            this.ExpandButton_b.UseVisualStyleBackColor = true;
            this.ExpandButton_b.Click += new System.EventHandler(this.ExpandButton_b_Click);
            // 
            // ShurinkButton_b
            // 
            this.ShurinkButton_b.Location = new System.Drawing.Point(180, 330);
            this.ShurinkButton_b.Name = "ShurinkButton_b";
            this.ShurinkButton_b.Size = new System.Drawing.Size(100, 24);
            this.ShurinkButton_b.TabIndex = 29;
            this.ShurinkButton_b.Text = "Fold";
            this.ShurinkButton_b.UseVisualStyleBackColor = true;
            this.ShurinkButton_b.Click += new System.EventHandler(this.ShurinkButton_b_Click);
            // 
            // ExpandButton_c
            // 
            this.ExpandButton_c.Enabled = false;
            this.ExpandButton_c.Location = new System.Drawing.Point(285, 597);
            this.ExpandButton_c.Name = "ExpandButton_c";
            this.ExpandButton_c.Size = new System.Drawing.Size(100, 24);
            this.ExpandButton_c.TabIndex = 30;
            this.ExpandButton_c.Text = "Expand";
            this.ExpandButton_c.UseVisualStyleBackColor = true;
            // 
            // ShurinkButton_a
            // 
            this.ShurinkButton_a.Enabled = false;
            this.ShurinkButton_a.Location = new System.Drawing.Point(180, 140);
            this.ShurinkButton_a.Name = "ShurinkButton_a";
            this.ShurinkButton_a.Size = new System.Drawing.Size(100, 24);
            this.ShurinkButton_a.TabIndex = 31;
            this.ShurinkButton_a.Text = "Fold";
            this.ShurinkButton_a.UseVisualStyleBackColor = true;
            // 
            // ItemFestivalButton
            // 
            this.ItemFestivalButton.Location = new System.Drawing.Point(10, 508);
            this.ItemFestivalButton.Name = "ItemFestivalButton";
            this.ItemFestivalButton.Size = new System.Drawing.Size(100, 52);
            this.ItemFestivalButton.TabIndex = 32;
            this.ItemFestivalButton.Text = "Item Festival";
            this.ItemFestivalButton.UseVisualStyleBackColor = true;
            this.ItemFestivalButton.Click += new System.EventHandler(this.ItemFestivalButton_Click);
            // 
            // ItemFestivalClassNameTextBox
            // 
            this.ItemFestivalClassNameTextBox.Location = new System.Drawing.Point(172, 508);
            this.ItemFestivalClassNameTextBox.Name = "ItemFestivalClassNameTextBox";
            this.ItemFestivalClassNameTextBox.Size = new System.Drawing.Size(115, 20);
            this.ItemFestivalClassNameTextBox.TabIndex = 33;
            // 
            // ItemFestivalAmountUpDown
            // 
            this.ItemFestivalAmountUpDown.Location = new System.Drawing.Point(291, 508);
            this.ItemFestivalAmountUpDown.Maximum = new decimal(new int[] {
            1410065408,
            2,
            0,
            0});
            this.ItemFestivalAmountUpDown.Name = "ItemFestivalAmountUpDown";
            this.ItemFestivalAmountUpDown.Size = new System.Drawing.Size(46, 20);
            this.ItemFestivalAmountUpDown.TabIndex = 34;
            this.ItemFestivalAmountUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // ItemFestivalMessageTextBox
            // 
            this.ItemFestivalMessageTextBox.Location = new System.Drawing.Point(172, 537);
            this.ItemFestivalMessageTextBox.Name = "ItemFestivalMessageTextBox";
            this.ItemFestivalMessageTextBox.Size = new System.Drawing.Size(215, 20);
            this.ItemFestivalMessageTextBox.TabIndex = 35;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(116, 541);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 36;
            this.label1.Text = "Message";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(116, 511);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 13);
            this.label2.TabIndex = 37;
            this.label2.Text = "Item";
            // 
            // CafeCheckBox
            // 
            this.CafeCheckBox.AutoSize = true;
            this.CafeCheckBox.Location = new System.Drawing.Point(343, 511);
            this.CafeCheckBox.Name = "CafeCheckBox";
            this.CafeCheckBox.Size = new System.Drawing.Size(48, 17);
            this.CafeCheckBox.TabIndex = 38;
            this.CafeCheckBox.Text = "Cafe";
            this.CafeCheckBox.UseVisualStyleBackColor = true;
            // 
            // ExtendExpireTimeButton
            // 
            this.ExtendExpireTimeButton.Location = new System.Drawing.Point(10, 567);
            this.ExtendExpireTimeButton.Name = "ExtendExpireTimeButton";
            this.ExtendExpireTimeButton.Size = new System.Drawing.Size(100, 24);
            this.ExtendExpireTimeButton.TabIndex = 39;
            this.ExtendExpireTimeButton.Text = "ExtendExpireTime";
            this.ExtendExpireTimeButton.UseVisualStyleBackColor = true;
            this.ExtendExpireTimeButton.Click += new System.EventHandler(this.ExtendExpireTimeButton_Click);
            // 
            // CashExtendMinutesUpDown1
            // 
            this.CashExtendMinutesUpDown1.Location = new System.Drawing.Point(196, 568);
            this.CashExtendMinutesUpDown1.Name = "CashExtendMinutesUpDown1";
            this.CashExtendMinutesUpDown1.Size = new System.Drawing.Size(46, 20);
            this.CashExtendMinutesUpDown1.TabIndex = 40;
            this.CashExtendMinutesUpDown1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(117, 572);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 13);
            this.label3.TabIndex = 41;
            this.label3.Text = "Current Time";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(248, 572);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(87, 13);
            this.label4.TabIndex = 42;
            this.label4.Text = "Minute";
            // 
            // GameServerCommandButton
            // 
            this.GameServerCommandButton.Location = new System.Drawing.Point(10, 415);
            this.GameServerCommandButton.Name = "GameServerCommandButton";
            this.GameServerCommandButton.Size = new System.Drawing.Size(100, 24);
            this.GameServerCommandButton.TabIndex = 23;
            this.GameServerCommandButton.Text = "GameServerCommand";
            this.GameServerCommandButton.UseVisualStyleBackColor = true;
            this.GameServerCommandButton.Click += new System.EventHandler(this.GameServerCommandButton_Click);
            // 
            // AdminClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(396, 628);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.CashExtendMinutesUpDown1);
            this.Controls.Add(this.ExtendExpireTimeButton);
            this.Controls.Add(this.CafeCheckBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ItemFestivalMessageTextBox);
            this.Controls.Add(this.ItemFestivalAmountUpDown);
            this.Controls.Add(this.ItemFestivalClassNameTextBox);
            this.Controls.Add(this.ItemFestivalButton);
            this.Controls.Add(this.ShurinkButton_a);
            this.Controls.Add(this.ExpandButton_c);
            this.Controls.Add(this.ShurinkButton_b);
            this.Controls.Add(this.ExpandButton_b);
            this.Controls.Add(this.CopyToClipBoardButton);
            this.Controls.Add(this.ExpandButton_a);
            this.Controls.Add(this.ShurinkButton_c);
            this.Controls.Add(this.ClientCommandButton);
            this.Controls.Add(this.GameServerCommandButton);
            this.Controls.Add(this.HostCommandButton);
            this.Controls.Add(this.ConsoleCommandTextBox);
            this.Controls.Add(this.ResumeServiceSelectComboBox);
            this.Controls.Add(this.StopServiceSelectComboBox);
            this.Controls.Add(this.ResumeServiceButton);
            this.Controls.Add(this.StopServiceButton);
            this.Controls.Add(this.CharacterIDLabel);
            this.Controls.Add(this.UserIDLabel);
            this.Controls.Add(this.UserCountTextBox);
            this.Controls.Add(this.NotifyTextBox);
            this.Controls.Add(this.KickUserButton);
            this.Controls.Add(this.NotifyButton);
            this.Controls.Add(this.RefreshUserCountButton);
            this.Controls.Add(this.ShutDownTimeListBox);
            this.Controls.Add(this.KickCharacterIDTextBox);
            this.Controls.Add(this.KickUserIDTextBox);
            this.Controls.Add(this.ShutDownBottuon);
            this.MaximizeBox = false;
            this.Name = "AdminClientForm";
            this.Text = "Heroes Server Admin Client";
            this.Load += new System.EventHandler(this.AdminClientForm_Load);
            this.Resize += new System.EventHandler(this.AdminClientForm_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.ItemFestivalAmountUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CashExtendMinutesUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		private global::System.Windows.Forms.Button ShutDownBottuon;

		private global::System.Windows.Forms.TextBox KickCharacterIDTextBox;

		private global::System.Windows.Forms.ListBox ShutDownTimeListBox;

		private global::System.Windows.Forms.Button RefreshUserCountButton;

		private global::System.Windows.Forms.Button NotifyButton;

		private global::System.Windows.Forms.Button KickUserButton;

		private global::System.Windows.Forms.TextBox NotifyTextBox;

		private global::System.Windows.Forms.TextBox UserCountTextBox;

		private global::System.Windows.Forms.Label UserIDLabel;

		private global::System.Windows.Forms.Label CharacterIDLabel;

		private global::System.Windows.Forms.Button StopServiceButton;

		private global::System.Windows.Forms.Button ResumeServiceButton;

		private global::System.Windows.Forms.ComboBox StopServiceSelectComboBox;

		private global::System.Windows.Forms.ComboBox ResumeServiceSelectComboBox;

		private global::System.Windows.Forms.TextBox KickUserIDTextBox;

		private global::System.Windows.Forms.TextBox ConsoleCommandTextBox;

		private global::System.Windows.Forms.Button HostCommandButton;

		private global::System.Windows.Forms.Button ClientCommandButton;

		private global::System.Windows.Forms.Button ShurinkButton_c;

		private global::System.Windows.Forms.Button ExpandButton_a;

		private global::System.Windows.Forms.Button CopyToClipBoardButton;

		private global::System.Windows.Forms.Button ExpandButton_b;

		private global::System.Windows.Forms.Button ShurinkButton_b;

		private global::System.Windows.Forms.Button ExpandButton_c;

		private global::System.Windows.Forms.Button ShurinkButton_a;

		private global::System.Windows.Forms.Button ItemFestivalButton;

		private global::System.Windows.Forms.TextBox ItemFestivalClassNameTextBox;

		private global::System.Windows.Forms.NumericUpDown ItemFestivalAmountUpDown;

		private global::System.Windows.Forms.TextBox ItemFestivalMessageTextBox;

		private global::System.Windows.Forms.Label label1;

		private global::System.Windows.Forms.Label label2;

		private global::System.Windows.Forms.CheckBox CafeCheckBox;

		private global::System.Windows.Forms.Button ExtendExpireTimeButton;

		private global::System.Windows.Forms.NumericUpDown CashExtendMinutesUpDown1;

		private global::System.Windows.Forms.Label label3;

		private global::System.Windows.Forms.Label label4;

		private global::System.Windows.Forms.Button GameServerCommandButton;
	}
}
