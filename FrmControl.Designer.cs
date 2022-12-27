namespace POREG
{
    partial class FrmControl
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.DataView = new System.Windows.Forms.DataGridView();
            this.SettingsGroup = new System.Windows.Forms.GroupBox();
            this.ShowProxiesButton = new System.Windows.Forms.Button();
            this.LoadProxyButton = new System.Windows.Forms.Button();
            this.ProxiesInput = new System.Windows.Forms.TextBox();
            this.ProxyLabel = new System.Windows.Forms.Label();
            this.StopButton = new System.Windows.Forms.Button();
            this.StartButton = new System.Windows.Forms.Button();
            this.ThreadsNumber = new System.Windows.Forms.NumericUpDown();
            this.ThreadLabel = new System.Windows.Forms.Label();
            this.LoadDataButton = new System.Windows.Forms.Button();
            this.AdvancedSettingsGroup = new System.Windows.Forms.GroupBox();
            this.API2CaptchaInput = new System.Windows.Forms.TextBox();
            this.APISimCodeInput = new System.Windows.Forms.TextBox();
            this.API2Captcha = new System.Windows.Forms.Label();
            this.APISimCode = new System.Windows.Forms.Label();
            this.RegInfoGroup = new System.Windows.Forms.GroupBox();
            this.FailedAccountsInput = new System.Windows.Forms.TextBox();
            this.SuccessAccountsInput = new System.Windows.Forms.TextBox();
            this.ReadyAccountsInput = new System.Windows.Forms.TextBox();
            this.SuccessAccountsLable = new System.Windows.Forms.Label();
            this.FailedAccountsLable = new System.Windows.Forms.Label();
            this.ReadyAccountsLable = new System.Windows.Forms.Label();
            this.Index = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Email = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Password = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AccountName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BirthDay = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IDCard = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Phone = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Bank = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.DataView)).BeginInit();
            this.SettingsGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ThreadsNumber)).BeginInit();
            this.AdvancedSettingsGroup.SuspendLayout();
            this.RegInfoGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // DataView
            // 
            this.DataView.AllowUserToAddRows = false;
            this.DataView.AllowUserToDeleteRows = false;
            this.DataView.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.DataView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.DataView.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.DataView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Index,
            this.Email,
            this.Password,
            this.AccountName,
            this.BirthDay,
            this.IDCard,
            this.Phone,
            this.Bank,
            this.Status});
            this.DataView.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DataView.GridColor = System.Drawing.SystemColors.ActiveBorder;
            this.DataView.Location = new System.Drawing.Point(62, 16);
            this.DataView.MultiSelect = false;
            this.DataView.Name = "DataView";
            this.DataView.ReadOnly = true;
            this.DataView.RowHeadersVisible = false;
            this.DataView.RowTemplate.Height = 25;
            this.DataView.Size = new System.Drawing.Size(932, 495);
            this.DataView.TabIndex = 0;
            // 
            // SettingsGroup
            // 
            this.SettingsGroup.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.SettingsGroup.Controls.Add(this.ShowProxiesButton);
            this.SettingsGroup.Controls.Add(this.LoadProxyButton);
            this.SettingsGroup.Controls.Add(this.ProxiesInput);
            this.SettingsGroup.Controls.Add(this.ProxyLabel);
            this.SettingsGroup.Controls.Add(this.StopButton);
            this.SettingsGroup.Controls.Add(this.StartButton);
            this.SettingsGroup.Controls.Add(this.ThreadsNumber);
            this.SettingsGroup.Controls.Add(this.ThreadLabel);
            this.SettingsGroup.Controls.Add(this.LoadDataButton);
            this.SettingsGroup.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.SettingsGroup.Location = new System.Drawing.Point(62, 524);
            this.SettingsGroup.Name = "SettingsGroup";
            this.SettingsGroup.Size = new System.Drawing.Size(287, 123);
            this.SettingsGroup.TabIndex = 1;
            this.SettingsGroup.TabStop = false;
            this.SettingsGroup.Text = "Setting";
            // 
            // ShowProxiesButton
            // 
            this.ShowProxiesButton.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.ShowProxiesButton.Location = new System.Drawing.Point(180, 88);
            this.ShowProxiesButton.Name = "ShowProxiesButton";
            this.ShowProxiesButton.Size = new System.Drawing.Size(90, 29);
            this.ShowProxiesButton.TabIndex = 12;
            this.ShowProxiesButton.Text = "View Proxy";
            this.ShowProxiesButton.UseVisualStyleBackColor = false;
            this.ShowProxiesButton.Click += new System.EventHandler(this.ShowProxiesButton_Click);
            // 
            // LoadProxyButton
            // 
            this.LoadProxyButton.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.LoadProxyButton.Location = new System.Drawing.Point(180, 56);
            this.LoadProxyButton.Name = "LoadProxyButton";
            this.LoadProxyButton.Size = new System.Drawing.Size(90, 29);
            this.LoadProxyButton.TabIndex = 11;
            this.LoadProxyButton.Text = "Load Proxy";
            this.LoadProxyButton.UseVisualStyleBackColor = false;
            this.LoadProxyButton.Click += new System.EventHandler(this.LoadProxyButton_Click);
            // 
            // ProxiesInput
            // 
            this.ProxiesInput.BackColor = System.Drawing.Color.White;
            this.ProxiesInput.Enabled = false;
            this.ProxiesInput.Location = new System.Drawing.Point(71, 58);
            this.ProxiesInput.Name = "ProxiesInput";
            this.ProxiesInput.Size = new System.Drawing.Size(88, 25);
            this.ProxiesInput.TabIndex = 10;
            this.ProxiesInput.Text = "0";
            this.ProxiesInput.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ProxyLabel
            // 
            this.ProxyLabel.AutoSize = true;
            this.ProxyLabel.Location = new System.Drawing.Point(6, 62);
            this.ProxyLabel.Name = "ProxyLabel";
            this.ProxyLabel.Size = new System.Drawing.Size(55, 17);
            this.ProxyLabel.TabIndex = 5;
            this.ProxyLabel.Text = "Proxies:";
            // 
            // StopButton
            // 
            this.StopButton.BackColor = System.Drawing.Color.LightSalmon;
            this.StopButton.Location = new System.Drawing.Point(90, 88);
            this.StopButton.Name = "StopButton";
            this.StopButton.Size = new System.Drawing.Size(69, 29);
            this.StopButton.TabIndex = 4;
            this.StopButton.Text = "Stop";
            this.StopButton.UseVisualStyleBackColor = false;
            this.StopButton.Click += new System.EventHandler(this.StopButton_Click);
            // 
            // StartButton
            // 
            this.StartButton.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.StartButton.Location = new System.Drawing.Point(7, 88);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(69, 29);
            this.StartButton.TabIndex = 3;
            this.StartButton.Text = "Start";
            this.StartButton.UseVisualStyleBackColor = false;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // ThreadsNumber
            // 
            this.ThreadsNumber.BackColor = System.Drawing.Color.White;
            this.ThreadsNumber.Location = new System.Drawing.Point(71, 25);
            this.ThreadsNumber.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.ThreadsNumber.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.ThreadsNumber.Name = "ThreadsNumber";
            this.ThreadsNumber.Size = new System.Drawing.Size(88, 25);
            this.ThreadsNumber.TabIndex = 2;
            this.ThreadsNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ThreadsNumber.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // ThreadLabel
            // 
            this.ThreadLabel.AutoSize = true;
            this.ThreadLabel.Location = new System.Drawing.Point(6, 30);
            this.ThreadLabel.Name = "ThreadLabel";
            this.ThreadLabel.Size = new System.Drawing.Size(59, 17);
            this.ThreadLabel.TabIndex = 1;
            this.ThreadLabel.Text = "Threads:";
            // 
            // LoadDataButton
            // 
            this.LoadDataButton.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.LoadDataButton.Location = new System.Drawing.Point(180, 23);
            this.LoadDataButton.Name = "LoadDataButton";
            this.LoadDataButton.Size = new System.Drawing.Size(90, 29);
            this.LoadDataButton.TabIndex = 0;
            this.LoadDataButton.Text = "Load Data";
            this.LoadDataButton.UseVisualStyleBackColor = false;
            this.LoadDataButton.Click += new System.EventHandler(this.LoadDataButton_Click);
            // 
            // AdvancedSettingsGroup
            // 
            this.AdvancedSettingsGroup.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.AdvancedSettingsGroup.Controls.Add(this.API2CaptchaInput);
            this.AdvancedSettingsGroup.Controls.Add(this.APISimCodeInput);
            this.AdvancedSettingsGroup.Controls.Add(this.API2Captcha);
            this.AdvancedSettingsGroup.Controls.Add(this.APISimCode);
            this.AdvancedSettingsGroup.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.AdvancedSettingsGroup.Location = new System.Drawing.Point(368, 524);
            this.AdvancedSettingsGroup.Name = "AdvancedSettingsGroup";
            this.AdvancedSettingsGroup.Size = new System.Drawing.Size(380, 123);
            this.AdvancedSettingsGroup.TabIndex = 2;
            this.AdvancedSettingsGroup.TabStop = false;
            this.AdvancedSettingsGroup.Text = "Advanced Settings";
            // 
            // API2CaptchaInput
            // 
            this.API2CaptchaInput.BackColor = System.Drawing.Color.White;
            this.API2CaptchaInput.Location = new System.Drawing.Point(118, 81);
            this.API2CaptchaInput.Name = "API2CaptchaInput";
            this.API2CaptchaInput.Size = new System.Drawing.Size(256, 25);
            this.API2CaptchaInput.TabIndex = 3;
            // 
            // APISimCodeInput
            // 
            this.APISimCodeInput.BackColor = System.Drawing.Color.White;
            this.APISimCodeInput.Location = new System.Drawing.Point(118, 36);
            this.APISimCodeInput.Name = "APISimCodeInput";
            this.APISimCodeInput.Size = new System.Drawing.Size(256, 25);
            this.APISimCodeInput.TabIndex = 2;
            // 
            // API2Captcha
            // 
            this.API2Captcha.AutoSize = true;
            this.API2Captcha.Location = new System.Drawing.Point(6, 84);
            this.API2Captcha.Name = "API2Captcha";
            this.API2Captcha.Size = new System.Drawing.Size(92, 17);
            this.API2Captcha.TabIndex = 1;
            this.API2Captcha.Text = "API 2Captcha:";
            // 
            // APISimCode
            // 
            this.APISimCode.AutoSize = true;
            this.APISimCode.Location = new System.Drawing.Point(6, 41);
            this.APISimCode.Name = "APISimCode";
            this.APISimCode.Size = new System.Drawing.Size(93, 17);
            this.APISimCode.TabIndex = 0;
            this.APISimCode.Text = "API Sim Code:";
            // 
            // RegInfoGroup
            // 
            this.RegInfoGroup.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.RegInfoGroup.Controls.Add(this.FailedAccountsInput);
            this.RegInfoGroup.Controls.Add(this.SuccessAccountsInput);
            this.RegInfoGroup.Controls.Add(this.ReadyAccountsInput);
            this.RegInfoGroup.Controls.Add(this.SuccessAccountsLable);
            this.RegInfoGroup.Controls.Add(this.FailedAccountsLable);
            this.RegInfoGroup.Controls.Add(this.ReadyAccountsLable);
            this.RegInfoGroup.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.RegInfoGroup.Location = new System.Drawing.Point(768, 524);
            this.RegInfoGroup.Name = "RegInfoGroup";
            this.RegInfoGroup.Size = new System.Drawing.Size(226, 123);
            this.RegInfoGroup.TabIndex = 5;
            this.RegInfoGroup.TabStop = false;
            this.RegInfoGroup.Text = "Reg Info";
            // 
            // FailedAccountsInput
            // 
            this.FailedAccountsInput.BackColor = System.Drawing.Color.IndianRed;
            this.FailedAccountsInput.Enabled = false;
            this.FailedAccountsInput.Location = new System.Drawing.Point(75, 86);
            this.FailedAccountsInput.Name = "FailedAccountsInput";
            this.FailedAccountsInput.Size = new System.Drawing.Size(145, 25);
            this.FailedAccountsInput.TabIndex = 9;
            this.FailedAccountsInput.Text = "0";
            this.FailedAccountsInput.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // SuccessAccountsInput
            // 
            this.SuccessAccountsInput.BackColor = System.Drawing.Color.YellowGreen;
            this.SuccessAccountsInput.Enabled = false;
            this.SuccessAccountsInput.Location = new System.Drawing.Point(75, 54);
            this.SuccessAccountsInput.Name = "SuccessAccountsInput";
            this.SuccessAccountsInput.Size = new System.Drawing.Size(145, 25);
            this.SuccessAccountsInput.TabIndex = 8;
            this.SuccessAccountsInput.Text = "0";
            this.SuccessAccountsInput.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ReadyAccountsInput
            // 
            this.ReadyAccountsInput.BackColor = System.Drawing.Color.White;
            this.ReadyAccountsInput.Enabled = false;
            this.ReadyAccountsInput.Location = new System.Drawing.Point(75, 22);
            this.ReadyAccountsInput.Name = "ReadyAccountsInput";
            this.ReadyAccountsInput.Size = new System.Drawing.Size(145, 25);
            this.ReadyAccountsInput.TabIndex = 7;
            this.ReadyAccountsInput.Text = "0";
            this.ReadyAccountsInput.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // SuccessAccountsLable
            // 
            this.SuccessAccountsLable.AutoSize = true;
            this.SuccessAccountsLable.Location = new System.Drawing.Point(6, 90);
            this.SuccessAccountsLable.Name = "SuccessAccountsLable";
            this.SuccessAccountsLable.Size = new System.Drawing.Size(46, 17);
            this.SuccessAccountsLable.TabIndex = 6;
            this.SuccessAccountsLable.Text = "Failed:";
            // 
            // FailedAccountsLable
            // 
            this.FailedAccountsLable.AutoSize = true;
            this.FailedAccountsLable.Location = new System.Drawing.Point(6, 58);
            this.FailedAccountsLable.Name = "FailedAccountsLable";
            this.FailedAccountsLable.Size = new System.Drawing.Size(57, 17);
            this.FailedAccountsLable.TabIndex = 5;
            this.FailedAccountsLable.Text = "Success:";
            // 
            // ReadyAccountsLable
            // 
            this.ReadyAccountsLable.AutoSize = true;
            this.ReadyAccountsLable.Location = new System.Drawing.Point(6, 27);
            this.ReadyAccountsLable.Name = "ReadyAccountsLable";
            this.ReadyAccountsLable.Size = new System.Drawing.Size(48, 17);
            this.ReadyAccountsLable.TabIndex = 4;
            this.ReadyAccountsLable.Text = "Ready:";
            // 
            // Index
            // 
            this.Index.DataPropertyName = "ID";
            this.Index.FillWeight = 50F;
            this.Index.HeaderText = "#";
            this.Index.Name = "Index";
            this.Index.ReadOnly = true;
            this.Index.Width = 50;
            // 
            // Email
            // 
            this.Email.DataPropertyName = "Email";
            this.Email.FillWeight = 180F;
            this.Email.HeaderText = "Email";
            this.Email.Name = "Email";
            this.Email.ReadOnly = true;
            this.Email.Width = 180;
            // 
            // Password
            // 
            this.Password.DataPropertyName = "Password";
            this.Password.HeaderText = "Password";
            this.Password.Name = "Password";
            this.Password.ReadOnly = true;
            // 
            // AccountName
            // 
            this.AccountName.DataPropertyName = "Name";
            this.AccountName.HeaderText = "Name";
            this.AccountName.Name = "AccountName";
            this.AccountName.ReadOnly = true;
            // 
            // BirthDay
            // 
            this.BirthDay.DataPropertyName = "BirthDay";
            this.BirthDay.FillWeight = 98F;
            this.BirthDay.HeaderText = "BirthDay";
            this.BirthDay.Name = "BirthDay";
            this.BirthDay.ReadOnly = true;
            this.BirthDay.Width = 98;
            // 
            // IDCard
            // 
            this.IDCard.DataPropertyName = "IDCard";
            this.IDCard.HeaderText = "IDCard";
            this.IDCard.Name = "IDCard";
            this.IDCard.ReadOnly = true;
            // 
            // Phone
            // 
            this.Phone.DataPropertyName = "Phone";
            this.Phone.HeaderText = "Phone";
            this.Phone.Name = "Phone";
            this.Phone.ReadOnly = true;
            // 
            // Bank
            // 
            this.Bank.DataPropertyName = "BankNumber";
            this.Bank.HeaderText = "Bank";
            this.Bank.Name = "Bank";
            this.Bank.ReadOnly = true;
            // 
            // Status
            // 
            this.Status.DataPropertyName = "Status";
            this.Status.HeaderText = "Status";
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            // 
            // FrmControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(1052, 665);
            this.Controls.Add(this.RegInfoGroup);
            this.Controls.Add(this.AdvancedSettingsGroup);
            this.Controls.Add(this.SettingsGroup);
            this.Controls.Add(this.DataView);
            this.MaximumSize = new System.Drawing.Size(1068, 704);
            this.MinimumSize = new System.Drawing.Size(1068, 704);
            this.Name = "FrmControl";
            this.Text = "REG Control";
            ((System.ComponentModel.ISupportInitialize)(this.DataView)).EndInit();
            this.SettingsGroup.ResumeLayout(false);
            this.SettingsGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ThreadsNumber)).EndInit();
            this.AdvancedSettingsGroup.ResumeLayout(false);
            this.AdvancedSettingsGroup.PerformLayout();
            this.RegInfoGroup.ResumeLayout(false);
            this.RegInfoGroup.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DataGridView DataView;
        private GroupBox SettingsGroup;
        private Button StopButton;
        private Button StartButton;
        private NumericUpDown ThreadsNumber;
        private Label ThreadLabel;
        private Button LoadDataButton;
        private GroupBox AdvancedSettingsGroup;
        private Label API2Captcha;
        private Label APISimCode;
        private TextBox API2CaptchaInput;
        private TextBox APISimCodeInput;
        private GroupBox RegInfoGroup;
        private TextBox FailedAccountsInput;
        private TextBox SuccessAccountsInput;
        private TextBox ReadyAccountsInput;
        private Label SuccessAccountsLable;
        private Label FailedAccountsLable;
        private Label ReadyAccountsLable;
        private Button LoadProxyButton;
        private TextBox ProxiesInput;
        private Label ProxyLabel;
        private Button ShowProxiesButton;
        private DataGridViewTextBoxColumn Index;
        private DataGridViewTextBoxColumn Email;
        private DataGridViewTextBoxColumn Password;
        private DataGridViewTextBoxColumn AccountName;
        private DataGridViewTextBoxColumn BirthDay;
        private DataGridViewTextBoxColumn IDCard;
        private DataGridViewTextBoxColumn Phone;
        private DataGridViewTextBoxColumn Bank;
        private DataGridViewTextBoxColumn Status;
    }
}